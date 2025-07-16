using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUserRequest : BaseQuery, IRequest<GetUserResponse>
    {
    }

    public class GetUserHandler(IUnitOfWork unitOfWork) 
        : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await unitOfWork.UserRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Users.User.NotFound.Message);

            var result = UserDto.Map(entity);

            result.Projects = [.. entity.Projects.Select(m => UserProjectDto.Map(m))];

            var collaborators = entity.Collaborators
                .Select(m => UserProjectDto.Map(m.Project))
                .ToList();

            result.Projects.AddRange(collaborators);

            return new GetUserResponse()
            {
                User = result,
            };
        }
    }

    public class GetUserResponse
    {
        public UserDto User { get; set; }
    }
}