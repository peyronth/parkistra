using System.ComponentModel.DataAnnotations;

namespace ParkIstra.Models.Main;
public class Image
{
    public int Id { get; init; }
    [Required(ErrorMessage = "This field is required")]
    public string? Url { get; set; }
    [Required(ErrorMessage = "This field is required")]
    public int ProjectId { get; set; }
    
    public Project Project { get; set; }
}