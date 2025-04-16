using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdateUserFormRequest : IRequest<GetUpdateUserFormResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetUpdateUserFormHandler : IRequestHandler<GetUpdateUserFormRequest, GetUpdateUserFormResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ISessionAccesor _sessionAccesor;

        public GetUpdateUserFormHandler(IApplicationDbContext dbContext, ISessionAccesor sessionAccesor)
        {
            _dbContext = dbContext;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<GetUpdateUserFormResponse> Handle(GetUpdateUserFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<User>().FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.User.NotFound.Message);

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
