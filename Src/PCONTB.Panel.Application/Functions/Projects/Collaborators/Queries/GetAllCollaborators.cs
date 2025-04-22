using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Projects.Collaborators;

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
            var entity = await _dbContext.Set<Collaborator>()
                .Where(m => m.ProjectId == request.Id)
                .ToListAsync(cancellationToken);

            return new GetAllCollaboratorsResponse
            {
                Users = [.. entity.Select(m => UserDto.Map(m.User))]
            };
        }
    }

    public class GetAllCollaboratorsResponse
    {
        public List<UserDto> Users { get; set; }
    }
}
