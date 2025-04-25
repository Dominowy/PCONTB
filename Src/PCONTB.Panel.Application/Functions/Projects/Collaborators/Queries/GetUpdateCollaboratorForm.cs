using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Functions.Projects.Collaborators.Commands;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Functions.Projects.Collaborators.Queries
{
    public class GetUpdateCollaboratorFormRequest : BaseQuery, IRequest<GetUpdateCollaboratorFormResponse>
    {
    }

    public class GetUpdateCollaboratorFormHandler : IRequestHandler<GetUpdateCollaboratorFormRequest, GetUpdateCollaboratorFormResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUpdateCollaboratorFormHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetUpdateCollaboratorFormResponse> Handle(GetUpdateCollaboratorFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Collaborator>()
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Collaborator.NotFound.Message);

            return await Task.FromResult(new GetUpdateCollaboratorFormResponse()
            {
                Form = new UpdateCollaboratorRequest
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    Email = entity.User.Email,
                    ProjectId = entity.ProjectId,
                    ManageProjectPermission = entity.ManageProjectPermission,
                    ManageCommunityPermission = entity.ManageCommunityPermission,
                    ManageFulfillmentPermission = entity.ManageFulfillmentPermission
                }
            });
        }
    }

    public class GetUpdateCollaboratorFormResponse
    {
        public UpdateCollaboratorRequest Form { get; set; }
    }
}
