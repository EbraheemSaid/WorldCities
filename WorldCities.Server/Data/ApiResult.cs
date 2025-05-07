using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace WorldCities.Server.Data
{
    public class ApiResult<T>
    {
        private ApiResult(
            List<T> data,
            int count,
            int pageIndex,
            int pageSize,
            string? sortColumnName,
            string? sortOrder)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortOrder = sortOrder;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public static async Task<ApiResult<T>> CreateAsync(
            IQueryable<T> source,
            int pageIndex,
            int pageSize,
            string? sortColumnName = null,
            string? sortOrder = null)

        {
            var count = await source.CountAsync();
            if (!string.IsNullOrEmpty(sortColumnName) && isValidProperty(sortColumnName))
            {
                sortOrder = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC"
                    ? "ASC"
                    : "DESC";

                source = source.OrderBy(string.Format("{0} {1}", sortColumnName, sortOrder));
            }



            source = source.Skip(pageIndex * pageSize).Take(pageSize);

            var data = await source.ToListAsync();

            return new ApiResult<T>(data, count, pageIndex, pageSize, sortColumnName, sortOrder);

        }

        public static bool isValidProperty(string propertyName, bool throwExceptionIfNotFound = true)
        {
            var prop = typeof(T).GetProperty(
                propertyName,
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance);

            if (prop == null && throwExceptionIfNotFound)
            {
                throw new NotSupportedException(
                    $"ERROR: Property {propertyName} does not exist");
            }

            return prop != null;
        }

        public List<T> Data { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public string? SortOrder { get; set; }
        public string? sortColumnName { get; set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => (PageIndex + 1) < TotalPages;
    }
}
