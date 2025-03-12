using LibraryManagement.Application.Services;

namespace LibraryManagement.Application.Abstractions.Services
{
    public interface ICloudfareServices
    {
        Task<R2UploadResult> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    }
}
