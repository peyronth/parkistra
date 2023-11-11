namespace ParkIstra.Libraries.Blazor.Components;

public partial class Stars
{
    [Parameter]
    public decimal? Rating { get; set; }
    private string WidthCssProperty
    {
        get
        {
            var rating = Rating ?? 0;
            var rating18 = rating * 18;
            var rating52 = rating >= 1 ? (Math.Floor(rating) - 1) * 5.15M : 0;
            return $"{(rating18 + rating52).ToString(CultureInfo.InvariantCulture)}px";
        }
    }
}
