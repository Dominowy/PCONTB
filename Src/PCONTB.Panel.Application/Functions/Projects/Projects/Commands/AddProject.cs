using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class AddProjectRequest : IRequest<CommandResult>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }

    public class AddProjectHandler : IRequestHandler<AddProjectRequest, CommandResult>
    {
        private readonly IApplicationDbContext _context;

        public AddProjectHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(AddProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = new Project(Guid.NewGuid(), request.Name, request.UserId);

            await _context.Set<Project>().AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class AddProjectRequestValidator : AbstractValidator<AddProjectRequest>
    {
        public AddProjectRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithErrorCode(ErrorCodes.Project.ProjectNameEmpty.Code);
        }
    }
}
