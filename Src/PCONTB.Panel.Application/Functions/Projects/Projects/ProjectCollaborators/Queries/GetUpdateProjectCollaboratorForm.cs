using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Commands;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Queries
{
    public class GetUpdateProjectCollaboratorFormRequest : BaseQuery, IRequest<GetUpdateProjectCollaboratorFormResponse>
    {
        public Guid ProjectId { get; set; }
    }

    public class GetUpdateProjectCollaboratorFormHandler : IRequestHandler<GetUpdateProjectCollaboratorFormRequest, GetUpdateProjectCollaboratorFormResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUpdateProjectCollaboratorFormHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUpdateProjectCollaboratorFormResponse> Handle(GetUpdateProjectCollaboratorFormRequest request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetBy(m => m.Id == request.ProjectId, cancellationToken);

            var entity = project.Collaborators.FirstOrDefault(m => m.Id == request.Id);

            if (entity is null) throw new NotFoundException(ErrorCodes.Collaborator.NotFound.Message);

            return await Task.FromResult(new GetUpdateProjectCollaboratorFormResponse()
            {
                Form = new UpdateProjectCollaboratorRequest
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

    public class GetUpdateProjectCollaboratorFormResponse
    {
        public UpdateProjectCollaboratorRequest Form { get; set; }
    }
}
