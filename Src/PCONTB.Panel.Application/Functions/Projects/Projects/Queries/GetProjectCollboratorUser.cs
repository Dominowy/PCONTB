using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Functions.Projects.Projects.Commands.Models;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetProjectCollaboratorUserRequest : BaseCommand, IRequest<GetProjectCollaboratorUserResponse>
    {
        public string Email { get; set; }
    }

    public class GetProjectCollaboratorUserHandler : IRequestHandler<GetProjectCollaboratorUserRequest, GetProjectCollaboratorUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectCollaboratorUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetProjectCollaboratorUserResponse> Handle(GetProjectCollaboratorUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetBy(m => m.Email == request.Email, cancellationToken);

            if (user is null) throw new NotFoundException(ErrorCodes.Collaborator.UserExist.Message);

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
