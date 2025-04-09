using MediatR;
using PCONTB.Panel.Application.Common.Models.Response;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Functions.Projects.Projects.Commands;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetUpdateProjectFormRequest : IRequest<GetUpdateProjectFormResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetUpdateProjectFormHandler : IRequestHandler<GetUpdateProjectFormRequest, GetUpdateProjectFormResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetUpdateProjectFormHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetUpdateProjectFormResponse> Handle(GetUpdateProjectFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Project>().FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                return new GetUpdateProjectFormResponse(false, ResponseStatus.NotFound);
            }

            return new GetUpdateProjectFormResponse()
            {
                FormData = new UpdateProjectRequest
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    UserId = entity.UserId,
                }
            };
        }
    }

    public class GetUpdateProjectFormResponse : BaseResponse
    {
        public UpdateProjectRequest FormData { get; set; }

        public GetUpdateProjectFormResponse(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {

        }

        public GetUpdateProjectFormResponse() : base()
        {
            Success = true;
            StatusCode = ResponseStatus.OK;
        }
    }

}
