using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Functions.Account.Users.Queries;

namespace PCONTB.Panel.Application.Table
{
    public class PagedQueryRequest<TDto> : IRequest<PagedResultDto<TDto>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public Dictionary<string, string>? Filters { get; set; }
        public List<SortInfo> Sorts { get; set; } = new();
    }

    public class PagedQueryHandler<TEntity, TDto> : IRequestHandler<PagedQueryRequest<TDto>, PagedResultDto<TDto>> where TEntity : class
    {
        private IQueryable<TEntity>? query;

        protected void SetQuery(IQueryable<TEntity> query)
        {
            this.query = query;
        }

        public virtual async Task<PagedResultDto<TDto>> Handle(PagedQueryRequest<TDto> request, CancellationToken cancellationToken)
        {
            if (query == null) query = GetQuery();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var globalSearchProps = GetGlobalSearchProperties();

                query = QueryHelper.ApplyGlobalSearch(query, request.Search, globalSearchProps);
            }

            if (request.Filters != null)
            {
                foreach (var filter in request.Filters)
                {
                    query = QueryHelper.ApplyFilter(query, filter.Key, filter.Value);
                }
            }

            IOrderedQueryable<TEntity>? orderedQuery = null;
            foreach (var sort in request.Sorts)
            {
                orderedQuery = QueryHelper.ApplyOrder(query, sort.Field, sort.Descending, thenBy: orderedQuery != null, orderedQuery);
                if (orderedQuery != null)
                    query = orderedQuery;
            }

            var totalCount = await query.CountAsync(cancellationToken);

            query = QueryHelper.ApplyPagination(query, request.Page, request.PageSize);

            var entities = await query.ToListAsync(cancellationToken);

            var items = entities.Select(MapEntityToDto).ToList();

            return new PagedResultDto<TDto>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        protected virtual TDto MapEntityToDto(TEntity entity)
        {
            throw new NotImplementedException("Override this method to provide mapping logic.");
        }

        protected virtual IQueryable<TEntity> GetQuery()
        {
            throw new NotImplementedException("Override this method to provide mapping logic.");
        }

        protected virtual string[] GetGlobalSearchProperties() =>
        Array.Empty<string>();
    }

    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class SortInfo
    {
        public string Field { get; set; } = "";
        public bool Descending { get; set; }
    }
}

