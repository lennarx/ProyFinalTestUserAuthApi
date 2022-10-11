using Domain.Models;
using MediatR;

namespace Services.Users.Queries
{
    public class GetUserQuery : IRequest<User>
    {
        public ulong Id { get; }
        public GetUserQuery(ulong id) => Id = id;
    }
}
