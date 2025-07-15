using MediatR;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetAllUsersRequest : IRequest<GetAllUsersResponse>
    {
    }

    public class GetAllUsersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllUsersRequest, GetAllUsersResponse>
    {
        public async Task<GetAllUsersResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.UserRepository.GetAll(cancellationToken);

            return new GetAllUsersResponse
            {
                Users = [.. entity.Select(UserDto.Map)]
            };
        }
    }

    public class GetAllUsersResponse
    {
        public List<UserDto> Users { get; set; }
    }
}
