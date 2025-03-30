using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Response;
using PCONTB.Panel.Application.Contracts.DbContext;
using PCONTB.Panel.Application.Models.Dto.Account.Users;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetUserHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Set<User>()
                .Where(u => u.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return new GetUserResponse(false, ResponseStatus.NotFound);
            }

            return new GetUserResponse()
            {
                User = UserDto.Map(entity),
            };
        }
    }

    public class GetUserResponse : BaseResponse
    {
        public UserDto User { get; set; }

        public GetUserResponse(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {
            
        }

        public GetUserResponse() : base()
        {
            Success = true;
            StatusCode = ResponseStatus.OK;
        }
    }
}
