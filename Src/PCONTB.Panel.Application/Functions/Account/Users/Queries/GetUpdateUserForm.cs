using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdateUserFormRequest : BaseQuery, IRequest<GetUpdateUserFormResponse>
    {

    }

    public class GetUpdateUserFormHandler : IRequestHandler<GetUpdateUserFormRequest, GetUpdateUserFormResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionAccesor _sessionAccesor;

        public GetUpdateUserFormHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor)
        {
            _unitOfWork = unitOfWork;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<GetUpdateUserFormResponse> Handle(GetUpdateUserFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.UserRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.Users.User.NotFound.Message);

            _sessionAccesor.Verify(entity.Id);

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
