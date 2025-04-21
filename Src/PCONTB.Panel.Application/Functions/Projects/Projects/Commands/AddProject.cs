using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class AddProjectRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid? CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public Guid? CountryId { get; set; }
    }

    public class AddProjectHandler : IRequestHandler<AddProjectRequest, CommandResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISessionAccesor _sessionAccesor;

        public AddProjectHandler(IApplicationDbContext context, ISessionAccesor sessionAccesor)
        {
            _context = context;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<CommandResult> Handle(AddProjectRequest request, CancellationToken cancellationToken)
        {
            var userId = _sessionAccesor.Session.UserId;

            var entity = new Project(Guid.NewGuid(), request.Name, userId, (Guid)request.CountryId, (Guid)request.CategoryId, request.SubcategoryId);

            await _context.Set<Project>().AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class AddProjectRequestValidator : AbstractValidator<AddProjectRequest>
    {
        private readonly IApplicationDbContext _context;
        public AddProjectRequestValidator(IApplicationDbContext context)
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

        private async Task<bool> CategoryExist(Guid? id, CancellationToken cancellationToken)
        {
            return await _context.Set<Category>().AnyAsync(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> SubcategoryExist(Guid? id, CancellationToken cancellationToken)
        {
            return await _context.Set<Subcategory>().AnyAsync(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> ConuntryExit(Guid? id, CancellationToken cancellationToken)
        {
            return await _context.Set<Country>().AnyAsync(m => m.Id == id, cancellationToken);
        }
    }
}