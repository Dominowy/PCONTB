using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Response;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.DbContext;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class UpdateProjectRequest : IRequest<UpdateResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }

    public class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, UpdateResult> 
    {
        private readonly IApplicationDbContext _context;

        public UpdateProjectHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateResult> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Project>().FindAsync(request.Id, cancellationToken);

            if (entity == null) 
            {
                return new UpdateResult(false, ResponseStatus.NotFound);
            }

            entity.UpdateProject(request.Name, request.UserId);

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateResult();
        }
    }

    public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
    {
        public UpdateProjectRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithErrorCode(ErrorCodes.Project.ProjectNameEmpty.Code);
        }
    }
}
