using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Functions.Categories.Commands;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Queries
{
    public class GetUpdateCategoryFormRequest : BaseQuery, IRequest<GetUpdateCategoryFormResponse>
    {

    }

    public class GetUpdateCategoryFormHandler(IUnitOfWork unitOfWork) 
        : IRequestHandler<GetUpdateCategoryFormRequest, GetUpdateCategoryFormResponse>
    {
        public async Task<GetUpdateCategoryFormResponse> Handle(GetUpdateCategoryFormRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await unitOfWork.CategoryRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Categories.Category.NotFound.Message);

            return new GetUpdateCategoryFormResponse
            {
                Form = new UpdateCategoryRequest
                {
                    Id = aggregate.Id,
                    Name = aggregate.Name,
                    Enabled = aggregate.Enabled
                }
            };
        }
    }

    public class GetUpdateCategoryFormResponse
    {
        public UpdateCategoryRequest Form { get; set; }
    }
}
