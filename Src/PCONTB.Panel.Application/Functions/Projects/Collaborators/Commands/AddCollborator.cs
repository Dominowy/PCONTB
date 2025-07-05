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
    //public class AddCollaboratorRequest : BaseCommand, IRequest<CommandResult>
    //{
    //    public string Email { get; set; }
    //    public Guid ProjectId { get; set; }

    //    public bool ManageProjectPermission { get; set; } = false;
    //    public bool ManageCommunityPermission { get; set; } = false;
    //    public bool ManageFulfillmentPermission { get; set; } = false;
    //}

    //public class AddCollaboratorHandler : IRequestHandler<AddCollaboratorRequest, CommandResult>
    //{
    //    private readonly IApplicationDbContext _dbContext;

    //    public AddCollaboratorHandler(IApplicationDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }

    //    public async Task<CommandResult> Handle(AddCollaboratorRequest request, CancellationToken cancellationToken)
    //    {
    //        var user = await _dbContext.Set<User>().FirstOrDefaultAsync(m => m.Email == request.Email, cancellationToken);

    //        if (user is null) throw new NotFoundException(ErrorCodes.Collaborator.UserExist.Message);

    //        var entity = new ProjectCollaborator(user.Id, request.ProjectId);

    //        entity.SetManageProjectPermission(request.ManageProjectPermission);
    //        entity.SetManageCommunityPermission(request.ManageCommunityPermission);
    //        entity.SetManageFulfillmentPermission(request.ManageFulfillmentPermission);

    //        await _dbContext.Set<ProjectCollaborator>().AddAsync(entity, cancellationToken);

    //        await _dbContext.SaveChangesAsync(cancellationToken);

    //        return new CommandResult(entity.Id);
    //    }
    //}

    //public class AddCollaboratorRequestValidator : AbstractValidator<AddCollaboratorRequest>
    //{
    //    private readonly IApplicationDbContext _context;
    //    public AddCollaboratorRequestValidator(IApplicationDbContext context)
    //    {
    //        _context = context;

    //        RuleFor(p => p.Email)
    //            .NotEmpty().WithMessage(ErrorCodes.Collaborator.UserEmpty.Message)
    //            .MustAsync(UserExist).WithMessage(ErrorCodes.Collaborator.UserExist.Message)
    //            .MustAsync(async (s, u, ct) => await UserExistInProject(s.ProjectId, u, ct))
    //            .WithMessage(ErrorCodes.Collaborator.UserExistInProject.Message);

    //        RuleFor(p => p.ProjectId)
    //            .NotEmpty().WithMessage(ErrorCodes.Collaborator.ProjectEmpty.Message)
    //            .MustAsync(ProjectExist).WithMessage(ErrorCodes.Collaborator.ProjectExist.Message);
    //    }

    //    private async Task<bool> UserExist(string email, CancellationToken cancellationToken)
    //    {
    //        return await _context.Set<User>().AnyAsync(m => m.Email == email, cancellationToken);
    //    }

    //    private async Task<bool> UserExistInProject(Guid projectId, string email, CancellationToken cancellationToken)
    //    {
    //        return !await _context.Set<ProjectCollaborator>()
    //            .AnyAsync(m => m.User.Email == email && m.ProjectId == projectId, cancellationToken);
    //    }

    //    private async Task<bool> ProjectExist(Guid id, CancellationToken cancellationToken)
    //    {
    //        return await _context.Set<Project>().AnyAsync(m => m.Id == id, cancellationToken);
    //    }
    //}
}
