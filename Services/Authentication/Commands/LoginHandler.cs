using Domain.Models;
using Domain.Repository.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Authentication.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthenticatedUser>
    {
        private readonly IUsersRepository _repository;
        private readonly string _apiKey;
        public LoginHandler(IUsersRepository repository, IConfiguration configuration)
        { 
            _repository = repository;
            _apiKey = configuration.GetSection("Key").Value;
        }

        public async Task<AuthenticatedUser> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var cryptedKey = Encoding.ASCII.GetBytes(_apiKey);
            var cryptedPassword = Encoding.ASCII.GetBytes(request.Password);
            var user = await _repository.Authenticate(request.Email, cryptedPassword);

            if (user == null)
            {
                throw new NoUserFoundException();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.GivenName, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(cryptedKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var writtenToken = tokenHandler.WriteToken(token);

            return new AuthenticatedUser
            {
                UserId = user.Id.ToString(),
                UserName = user.Email,
                Token = writtenToken
            };
        }
    }
}
