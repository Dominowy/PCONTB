using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Application.Services.Auth;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
{
    public class GetProjectCollaboratorUserRequest : BaseCommand, IRequest<GetProjectCollaboratorUserResponse>
    {
        public string Email { get; set; }
        public Guid? ProjectId { get; set; }
    }

    public class GetProjectCollaboratorUserHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor) 
        : IRequestHandler<GetProjectCollaboratorUserRequest, GetProjectCollaboratorUserResponse>
    {
        public async Task<GetProjectCollaboratorUserResponse> Handle(GetProjectCollaboratorUserRequest request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.GetBy(m => m.Email == request.Email, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Projects.ProjectCollaborator.UserExistInProject.Message);

            if (request.ProjectId != null)
            {
                var project = await unitOfWork.ProjectRepository.GetBy(m => m.Id == request.ProjectId, cancellationToken);

                if (project.UserId == user.Id)
                    throw new BadRequestException(ErrorCodes.Projects.ProjectCollaborator.TryToAddCreator.Message);
            }
            else 
            {
                if (user.Id == sessionAccesor.Session.UserId)
                    throw new BadRequestException(ErrorCodes.Projects.ProjectCollaborator.TryToAddCreator.Message);
            }

            return new GetProjectCollaboratorUserResponse 
            { 
                User = ProjectCollaboratorUserDto.Map(user),
            };
        }
    }

    public class GetProjectCollaboratorUserResponse
    {
        public ProjectCollaboratorUserDto User { get; set; }
    }
}
