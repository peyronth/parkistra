namespace ParkIstra.Libraries.ASP;

public class ASPApiBehaviorOptions : ApiBehaviorOptions
{
    public bool IsDetailed { get; set; } = true;
    public bool IsTraceIdIncluded { get; set; } = true;
}
