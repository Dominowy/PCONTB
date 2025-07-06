using MediatR;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Models.Projects.Collaborators;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Queries
{
    public class GetAllProjectCollaboratorsRequest : BaseQuery, IRequest<GetAllProjectCollaboratorsResponse>
    {
        public Guid ProjectId { get; set; }
    }

    public class GetAllProjectCollaboratorHandler : IRequestHandler<GetAllProjectCollaboratorsRequest, GetAllProjectCollaboratorsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllProjectCollaboratorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllProjectCollaboratorsResponse> Handle(GetAllProjectCollaboratorsRequest request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            return new GetAllProjectCollaboratorsResponse
            {
                Collaborators = [.. project.Collaborators.Select(CollaboratorDto.Map)]
            };
        }
    }

    public class GetAllProjectCollaboratorsResponse
    {
        public List<CollaboratorDto> Collaborators { get; set; }
    }
}
