using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class UpdateProjectRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public Guid CountryId { get; set; }
        public Guid? ImageId { get; internal set; }
        public Guid? VideoId { get; internal set; }
    }

    public class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, CommandResult> 
    {
        private readonly IApplicationDbContext _context;
        private readonly ISessionAccesor _sessionAccesor;

        public UpdateProjectHandler(IApplicationDbContext context, ISessionAccesor sessionAccesor)
        {
            _context = context;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<CommandResult> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Project>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("Project not found");

            _sessionAccesor.Verify(entity.UserId);

            entity.SetName(request.Name);
            entity.SetCountry(request.CountryId);
            entity.SetCategory(request.CategoryId);
            entity.SetSubcategory(request.SubcategoryId);

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
    {
        private readonly IApplicationDbContext _context;
        public UpdateProjectRequestValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(ErrorCodes.Project.ProjectNameEmpty.Message);

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage(ErrorCodes.Project.CategoryEmpty.Message)
                .MustAsync(CategoryExist).WithMessage(ErrorCodes.Project.CategoryExist.Message);

            RuleFor(p => p.SubcategoryId)
                .MustAsync(SubcategoryExist).When(p => p.SubcategoryId.HasValue).WithMessage(ErrorCodes.Project.SubcategoryExist.Message);

            RuleFor(p => p.CountryId)
                .NotEmpty().WithMessage(ErrorCodes.Project.CountryEmpty.Message)
                .MustAsync(ConuntryExit).WithMessage(ErrorCodes.Project.CountryExist.Message);

        }

        private async Task<bool> CategoryExist(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<Category>().AnyAsync(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> SubcategoryExist(Guid? id, CancellationToken cancellationToken)
        {
            return await _context.Set<Subcategory>().AnyAsync(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> ConuntryExit(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<Country>().AnyAsync(m => m.Id == id, cancellationToken);
        }
    }
}
