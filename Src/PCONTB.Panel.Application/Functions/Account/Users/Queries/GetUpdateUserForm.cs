using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdateUserFormRequest : BaseQuery, IRequest<GetUpdateUserFormResponse>
    {

    }

    public class GetUpdateUserFormHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor) : IRequestHandler<GetUpdateUserFormRequest, GetUpdateUserFormResponse>
    {
        public async Task<GetUpdateUserFormResponse> Handle(GetUpdateUserFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.UserRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Users.User.NotFound.Message);

            sessionAccesor.Verify(entity.Id);

            return new GetUpdateUserFormResponse
            {
                Form = new UpdateUserRequest
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    Email = entity.Email,
                }
            };
        }
    }

    public class GetUpdateUserFormResponse
    {
        public UpdateUserRequest Form { get; set; }
    }
}
