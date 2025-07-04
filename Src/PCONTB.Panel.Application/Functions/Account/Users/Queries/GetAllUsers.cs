using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetAllUsersRequest : IRequest<GetAllUsersResponse>
    {
    }

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, GetAllUsersResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllUsersResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.UserRepository.GetAll(cancellationToken);

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
