using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Features.Authentication.Dto
{
    public class SignInResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string token { get; set; }
    }
}
