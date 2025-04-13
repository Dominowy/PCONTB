using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class UpdateProjectRequest : IRequest<CommandResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }

    public class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, CommandResult> 
    {
        private readonly IApplicationDbContext _context;

        public UpdateProjectHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Project>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("Project not found");
      
            entity.UpdateProject(request.Name, request.UserId);

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
    {
        public UpdateProjectRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(ErrorCodes.Project.ProjectNameEmpty.Message);
        }
    }
}
