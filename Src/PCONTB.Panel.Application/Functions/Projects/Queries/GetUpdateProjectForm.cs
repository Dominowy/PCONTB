using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Functions.Projects.Commands;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
{
    public class GetUpdateProjectFormRequest : BaseQuery, IRequest<GetUpdateProjectFormResponse>
    {

    }

    public class GetUpdateProjectFormHandler : IRequestHandler<GetUpdateProjectFormRequest, GetUpdateProjectFormResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionAccesor _sessionAccesor;


        public GetUpdateProjectFormHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor)
        {
            _unitOfWork = unitOfWork;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<GetUpdateProjectFormResponse> Handle(GetUpdateProjectFormRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await _unitOfWork.ProjectRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (aggregate == null) throw new NotFoundException("Project not found");

            _sessionAccesor.Verify(aggregate.UserId);

            FormFile image = null;

            if (aggregate.Image != null)
            {
                image = new FormFile
                {
                    ContentType = aggregate.Image.ContentType,
                    FileName = aggregate.Image.FileName,
                };
            }

            return new GetUpdateProjectFormResponse()
            {
                Form = new UpdateProjectRequest
                {
                    Id = aggregate.Id,
                    Name = aggregate.Name,
                    CategoryId = aggregate.CategoryId,
                    CountryId = aggregate.CountryId,
                    Image = image,
                    ImageData = aggregate.Image == null ? null : aggregate.Image.Data,
                    Collaborators = [.. aggregate.Collaborators.Select(UpdateProjectCollaboratorDto.Map)]
                }
            };
        }
    }

    public class GetUpdateProjectFormResponse
    {
        public UpdateProjectRequest Form{ get; set; }
    }

}
