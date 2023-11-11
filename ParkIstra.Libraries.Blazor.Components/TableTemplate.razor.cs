namespace ParkIstra.Libraries.Blazor.Components;

public partial class TableTemplate<TItem>
{
    [Parameter]
    public RenderFragment? TableHeader { get; set; }
    [Parameter, AllowNull]
    public RenderFragment<TItem> TableRow { get; set; }
    [Parameter, AllowNull]
    public IReadOnlyList<TItem> Items { get; set; }
    [Parameter]
    public string TableCssClass { get; set; } = "table mb-0";
    [Parameter]
    public string? NoItemsText { get; set; } = "Collection is empty.";

    public string? NoItems => !Items.Any() ? NoItemsText : null;
}
