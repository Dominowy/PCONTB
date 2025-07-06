using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Files;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Functions.Projects.Projects.Projects.Commands;
using PCONTB.Panel.Application.Models.Projects.Collaborators;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Projects.Queries
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
            var entity = await _unitOfWork.ProjectRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("Project not found");

            _sessionAccesor.Verify(entity.UserId);

            FormFile image = null;

            if (entity.Image != null)
            {
                image = new FormFile
                {
                    ContentType = entity.Image.ContentType,
                    FileName = entity.Image.FileName,
                };
            }

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
                    Image = image,
                    ImageData = entity.Image == null ? null : entity.Image.Data
                }
            };
        }
    }

    public class GetUpdateProjectFormResponse
    {
        public UpdateProjectRequest Form{ get; set; }
    }

}
