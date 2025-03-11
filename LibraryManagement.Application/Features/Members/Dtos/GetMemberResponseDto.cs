
namespace LibraryManagement.Application.Features.Members.Dtos
{
    public class GetMemberResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Email { get; set; }
        public string nic { get; set; }
        public string Telephone { get; set; }
        public string dob { get; set; }
    }
}
