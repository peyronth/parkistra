namespace ParkIstra.Libraries.OData;

public class OdataEnableQueryAttribute : EnableQueryAttribute
{
    public int MaxExpansionDepth => base.MaxExpansionDepth = 3;
    
    public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
    {
        try
        { return base.ApplyQuery(queryable, queryOptions); }
        catch (Exception exception)
        { throw new Exception("See INNER exception for details.", exception); }
    }

    public override void ValidateQuery(HttpRequest request, ODataQueryOptions queryOptions)
    {
        try
        {
            Validate(queryOptions);
            base.ValidateQuery(request, queryOptions);
        }
        catch (Exception exception)
        { throw new Exception("see inner exception for details.", exception); }
    }

    public static void Validate(ODataQueryOptions queryOptions)
    {
        try
        { queryOptions.Validate(new ODataValidationSettings() { }); }
        catch (Exception)
        { throw; }
    }
}
