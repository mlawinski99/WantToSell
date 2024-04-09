using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using WantToSell.Application.Models.Paging;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
        Pager pager)
    {
        var totalRecords = await query.CountAsync();
        var items = await query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToListAsync();

        return new PagedList<T>(pager.PageIndex, pager.PageSize,
            pager.SortColumn, pager.Ascending, totalRecords) { Items = items };
    }

    public static IQueryable<T> Sort<T>(
        this IQueryable<T> query,
        Pager pager)
    {
        if (string.IsNullOrWhiteSpace(pager.SortColumn))
            return query;

        var ordering = $"{pager.SortColumn} {(pager.Ascending ? "asc" : "desc")}";

        return query.OrderBy(ordering);
    }
}