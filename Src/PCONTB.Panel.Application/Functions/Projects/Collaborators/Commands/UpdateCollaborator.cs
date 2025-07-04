using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Projects.Projects;
using PCONTB.Panel.Domain.Projects.Projects.Collaborators;

namespace PCONTB.Panel.Application.Functions.Projects.Collaborators.Commands
{
    public class UpdateCollaboratorRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public Guid ProjectId { get; set; }
        public bool ManageProjectPermission { get; set; } = false;
        public bool ManageCommunityPermission { get; set; } = false;
        public bool ManageFulfillmentPermission { get; set; } = false;
    }

    public class UpdateCollaboratorHandler : IRequestHandler<UpdateCollaboratorRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateCollaboratorHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(UpdateCollaboratorRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<ProjectCollaborator>().FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Collaborator.NotFound.Message);

            entity.SetManageProjectPermission(request.ManageProjectPermission);
            entity.SetManageCommunityPermission(request.ManageCommunityPermission);
            entity.SetManageFulfillmentPermission(request.ManageFulfillmentPermission);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateCollaboratorRequestValidator : AbstractValidator<UpdateCollaboratorRequest>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCollaboratorRequestValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(ErrorCodes.Collaborator.UserEmpty.Message)
                .MustAsync(UserExist).WithMessage(ErrorCodes.Collaborator.UserExist.Message)
                .MustAsync(async (s, u, ct) => await UserExistInProject(s.Id, s.ProjectId, u, ct))
                .WithMessage(ErrorCodes.Collaborator.UserExistInProject.Message);

            RuleFor(p => p.ProjectId)
                .NotEmpty().WithMessage(ErrorCodes.Collaborator.ProjectEmpty.Message)
                .MustAsync(ProjectExist).WithMessage(ErrorCodes.Collaborator.ProjectExist.Message);
        }

        private async Task<bool> UserExist(string email, CancellationToken cancellationToken)
        {
            return await _context.Set<User>().AnyAsync(m => m.Email == email, cancellationToken);
        }

        private async Task<bool> UserExistInProject(Guid id, Guid projectId, string email, CancellationToken cancellationToken)
        {
            return !await _context.Set<ProjectCollaborator>()
                .AnyAsync(m => m.User.Email == email && m.ProjectId == projectId && m.Id != id, cancellationToken);
        }

        private async Task<bool> ProjectExist(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<Project>().AnyAsync(m => m.Id == id, cancellationToken);
        }
    }
}
