using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Authentication.Commands
{
    public class LoginCommand : IRequest<AuthenticatedUser>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
