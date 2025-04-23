using MediatR;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;

namespace PCONTB.Panel.Application.Functions.Projects.Images.Commands
{
    public class UpdateImageRequest : IRequest<CommandResult>
    {
    }

    public class UpdateImageHandler : IRequestHandler<UpdateImageRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateImageHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<CommandResult> Handle(UpdateImageRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
