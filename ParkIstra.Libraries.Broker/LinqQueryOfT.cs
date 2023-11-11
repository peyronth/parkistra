namespace ParkIstra.Libraries.Blazor;
public record LinqQuery<T>
{
    public List<Expression<Func<T, bool>>>? FilterList { get; set; }
    public List<Expression<Func<T, object>>>? OrderByList { get; set; }
    public List<Expression<Func<T, object>>>? DescendingOrderByList { get; set; }
    public int? Top { get; set; }

    public IQueryable<T> GetComposedQuery(IQueryable<T> queryble)
    {
        foreach (var expression in FilterList ?? new())
        { queryble = queryble.Where(expression); }

        foreach (var expression in OrderByList ?? new())
        { queryble = queryble.OrderBy(expression); }

        foreach (var expression in DescendingOrderByList ?? new())
        { queryble = queryble.OrderByDescending(expression); }

        if (Top is int top)
        { queryble = queryble.Take(top); }

        return queryble;
    }
}