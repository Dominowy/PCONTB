using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Models.Projects.Collaborators;
using PCONTB.Panel.Application.Services.Auth;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
{
    public class GetProjectCollaboratorUserRequest : BaseCommand, IRequest<GetProjectCollaboratorUserResponse>
    {
        public string Email { get; set; }
    }

    public class GetProjectCollaboratorUserHandler : IRequestHandler<GetProjectCollaboratorUserRequest, GetProjectCollaboratorUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionAccesor _sessionAccesor;

        public GetProjectCollaboratorUserHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor)
        {
            _unitOfWork = unitOfWork;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<GetProjectCollaboratorUserResponse> Handle(GetProjectCollaboratorUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetBy(m => m.Email == request.Email, cancellationToken);

            if (user is null) throw new NotFoundException(ErrorCodes.Projects.ProjectCollaborator.UserExistInProject.Message);

            if (user.Id == _sessionAccesor.Session.UserId) throw new BadRequestException(ErrorCodes.Projects.ProjectCollaborator.TryToAddCreator.Message);

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
