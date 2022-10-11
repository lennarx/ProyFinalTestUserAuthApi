using Domain.Models;
using Domain.Repository.Users;
using MediatR;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUsersRepository _repository;
        public CreateUserHandler(IUsersRepository repository) => _repository = repository;
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                IsActive = true,
                Password = Encoding.ASCII.GetBytes(request.Password)
            };

            await _repository.AddAsync(user, cancellationToken);

            return user;
        }
    }
}
