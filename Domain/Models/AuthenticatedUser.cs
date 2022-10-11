using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AuthenticatedUser
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
