using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Projects.Collaborators;

namespace PCONTB.Panel.Application.Functions.Projects.Collaborators.Queries
{
    public class GetAllCollaboratorsRequest : BaseQuery, IRequest<GetAllCollaboratorsResponse>
    {
    }

    public class GetAllCollaboratorHandler : IRequestHandler<GetAllCollaboratorsRequest, GetAllCollaboratorsResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        public GetAllCollaboratorHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllCollaboratorsResponse> Handle(GetAllCollaboratorsRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<ProjectCollaborator>()
                .Where(m => m.ProjectId == request.Id)
                .Include(m => m.User)
                .ToListAsync(cancellationToken);

            return new GetAllCollaboratorsResponse
            {
                Collaborators = [.. entity.Select(m => CollaboratorDto.Map(m))]
            };
        }
    }

    public class GetAllCollaboratorsResponse
    {
        public List<CollaboratorDto> Collaborators { get; set; }
    }
}
