namespace ParkIstra.Libraries.Blazor;
public record ODataQuery
{
    public string? Query { get; set; }
    public List<string>? FilterList { get; set; }
    public List<string>? OrderByList { get; set; }
    public List<string>? ExpandList { get; set; }

    public string? Select { get; set; }
    public int? Skip { get; set; }
    public int? Top { get; set; }


    public override string ToString()
    {
        return $"?{GetComposedQuery()}";
    }

    private string GetComposedQuery()
    {
        var queryParts = new List<string>();

        if (!string.IsNullOrEmpty(Query))
        {
            queryParts.Add(Query);
        }

        if (FilterList is not null)
        { queryParts.Add($"$filter={string.Join(" and ", FilterList)}"); }

        if (OrderByList is not null)
        { queryParts.Add($"$orderby={string.Join(",", OrderByList)}"); }

        if (ExpandList is not null)
        { queryParts.Add($"$expand={string.Join(",", ExpandList)}"); }

        if (!string.IsNullOrEmpty(Select))
        { queryParts.Add($"$select={Select}"); }

        if (Skip is not null && Skip != 0)
        { queryParts.Add($"$skip={Skip}"); }

        if (Top is not null && Top != 0)
        { queryParts.Add($"$top={Top}"); }


        var a = $"{(queryParts.Count > 0 ? string.Join("&", queryParts) : "")}";
        return $"{(queryParts.Count > 0 ? string.Join("&", queryParts) : "")}";
    }
}