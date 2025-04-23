using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Collaborators.Commands
{
    public class AddCollaboratorRequest : IRequest<CommandResult>
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }

        public bool ManageProjectPermission { get; set; } = false;
        public bool ManageCommunityPermission { get; set; } = false;
        public bool ManageFulfillmentPermission { get; set; } = false;
    }

    public class AddCollaboratorHandler : IRequestHandler<AddCollaboratorRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddCollaboratorHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(AddCollaboratorRequest request, CancellationToken cancellationToken)
        {
            var entity = new Collaborator(request.UserId, request.ProjectId);

            entity.SetManageProjectPermission(request.ManageProjectPermission);
            entity.SetCommunityPermission(request.ManageCommunityPermission);
            entity.SetManageFulfillmentPermission(request.ManageFulfillmentPermission);

            await _dbContext.Set<Collaborator>().AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class AddCollaboratorRequestValidator : AbstractValidator<AddCollaboratorRequest>
    {
        private readonly IApplicationDbContext _context;
        public AddCollaboratorRequestValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage(ErrorCodes.Collaborator.UserEmpty.Message)
                .MustAsync(UserExist).WithMessage(ErrorCodes.Collaborator.UserExist.Message);

            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage(ErrorCodes.Collaborator.ProjectEmpty.Message)
                .MustAsync(ProjectExist).WithMessage(ErrorCodes.Collaborator.ProjectExist.Message);
        }

        private async Task<bool> UserExist(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<User>().AnyAsync(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> ProjectExist(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<Project>().AnyAsync(m => m.Id == id, cancellationToken);
        }
    }
}
