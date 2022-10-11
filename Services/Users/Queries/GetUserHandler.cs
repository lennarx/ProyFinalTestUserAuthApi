using Domain.Models;
using Domain.Repository.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Users.Queries
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IUsersRepository _repository;
        public GetUserHandler(IUsersRepository repository) => _repository = repository;

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(request.Id, cancellationToken);
        }
    }
}
