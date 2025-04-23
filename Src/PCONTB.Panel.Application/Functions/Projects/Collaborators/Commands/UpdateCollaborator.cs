using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Functions.Projects.Collaborators.Commands
{
    public class UpdateCollaboratorRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public Guid ProjectId { get; set; }
        public bool ManageProjectPermission { get; set; } = false;
        public bool ManageCommunityPermission { get; set; } = false;
        public bool ManageFulfillmentPermission { get; set; } = false;
    }

    public class UpdateCollaboratorHandler : IRequestHandler<UpdateCollaboratorRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateCollaboratorHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(UpdateCollaboratorRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Collaborator>().FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Collaborator.NotFound.Message);

            entity.SetManageProjectPermission(request.ManageProjectPermission);
            entity.SetManageCommunityPermission(request.ManageCommunityPermission);
            entity.SetManageFulfillmentPermission(request.ManageFulfillmentPermission);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}
