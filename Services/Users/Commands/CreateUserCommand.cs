using Domain.Forms;
using Domain.Models;
using MediatR;

namespace Services.Users.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        public CreateUserCommand(UserCreationForm user)
        {
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
        }
    }
}