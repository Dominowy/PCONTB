using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Functions.Projects.Projects.Commands;
using PCONTB.Panel.Application.Models.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetUpdateProjectFormRequest : BaseQuery, IRequest<GetUpdateProjectFormResponse>
    {

    }

    public class GetUpdateProjectFormHandler : IRequestHandler<GetUpdateProjectFormRequest, GetUpdateProjectFormResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISessionAccesor _sessionAccesor;


        public GetUpdateProjectFormHandler(IApplicationDbContext context, ISessionAccesor sessionAccesor)
        {
            _context = context;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<GetUpdateProjectFormResponse> Handle(GetUpdateProjectFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Project>()
                .Include(m => m.Collaborators).ThenInclude(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("Project not found");

            _sessionAccesor.Verify(entity.UserId);

            return new GetUpdateProjectFormResponse()
            {
                Form = new UpdateProjectRequest
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CategoryId = entity.CategoryId,
                    SubcategoryId = entity.SubcategoryId,
                    CountryId = entity.CountryId,
                    Collaborators = [.. entity.Collaborators.Select(CollaboratorDto.Map)],
                    ImageId = entity.ImageId,
                    VideoId = entity.VideoId
                }
            };
        }
    }

    public class GetUpdateProjectFormResponse
    {
        public UpdateProjectRequest Form{ get; set; }
    }

}
