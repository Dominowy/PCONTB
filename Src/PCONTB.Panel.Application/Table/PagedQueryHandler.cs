using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PCONTB.Panel.Application.Table
{

    public class PagedQueryHandler<TEntity, TDto>
    where TEntity : class
    {
        private readonly DbContext _context;

        public PagedQueryHandler(DbContext context)
        {
            _context = context;
        }

        // Funkcja mapująca z encji na DTO - przekazujesz ją w konstruktorze albo nadpisujesz w dziedziczeniu
        protected virtual TDto MapEntityToDto(TEntity entity)
        {
            throw new NotImplementedException("Override this method to provide mapping logic.");
        }

        public async Task<PagedResultDto<TDto>> HandleAsync(PagedQueryRequest<TDto> request, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            // (Filtrowanie, sortowanie itp. - tak jak wcześniej)

            // Total count
            var totalCount = await query.CountAsync(cancellationToken);

            // Pagination
            query = QueryHelper.ApplyPagination(query, request.Page, request.PageSize);

            // Pobieramy dane z bazy jako lista encji
            var entities = await query.ToListAsync(cancellationToken);

            // Mapujemy ręcznie na DTO
            var items = entities.Select(e => MapEntityToDto(e)).ToList();

            return new PagedResultDto<TDto>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        protected virtual string[] GetGlobalSearchProperties() =>
        Array.Empty<string>();
    }

    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class PagedQueryRequest<TDto> : IRequest<PagedResultDto<TDto>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public Dictionary<string, string>? Filters { get; set; }
        public List<SortInfo> Sorts { get; set; } = new();
    }

    public class SortInfo
    {
        public string Field { get; set; } = "";
        public bool Descending { get; set; }
    }
}

