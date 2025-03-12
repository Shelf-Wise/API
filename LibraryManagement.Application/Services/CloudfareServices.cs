using Amazon.Runtime;
using Amazon.S3;
using LibraryManagement.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Application.Services
{
    public class CloudfareServices : ICloudfareServices
    {
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;

        public CloudfareServices(IConfiguration configuration, IAmazonS3 amazonS3)
        {
            _configuration = configuration;
            this._s3Client = amazonS3;

            var config = new AmazonS3Config
            {
                ServiceURL = configuration["Cloudflare:ServiceUrl"],
                ForcePathStyle = true,
                SignatureVersion = "4",
                RequestChecksumCalculation = RequestChecksumCalculation.WHEN_REQUIRED,
                ResponseChecksumValidation = ResponseChecksumValidation.WHEN_REQUIRED
            };
            _s3Client = new AmazonS3Client(
                new BasicAWSCredentials(configuration["Cloudflare:AccessKeyId"], configuration["Cloudflare:SecretAccessKey"]),
                config
            );
        }
        public async Task<R2UploadResult> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            try
            {
                var putRequest = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = _configuration["Cloudflare:BucketName"],
                    Key = fileName,
                    InputStream = fileStream,
                    ContentType = contentType,
                    DisablePayloadSigning = true,
                    AutoCloseStream = false,
                };

                var response = await _s3Client.PutObjectAsync(putRequest);

                var url = $"{_configuration["Cloudflare:PublicUrl"]}{fileName}";

                return new R2UploadResult
                {
                    Success = true,
                    Key = fileName,
                    Url = url
                };
            }
            catch (AmazonS3Exception s3Ex)
            {
                return new R2UploadResult
                {
                    Success = false,
                    ErrorMessage = $"S3 Error: {s3Ex.Message}. Error Code: {s3Ex.ErrorCode}"
                };
            }
            catch (Exception ex)
            {
                return new R2UploadResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
    public class R2UploadResult
    {
        public bool Success { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public string ErrorMessage { get; set; }
    }
}
