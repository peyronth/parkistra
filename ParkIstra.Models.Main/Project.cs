    using System.ComponentModel.DataAnnotations;

namespace ParkIstra.Models.Main;
public class Project
{
    public int Id { get; init; }
    [Required(ErrorMessage = "This field is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "This field is required")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "This field is required")]
    public List<string>? Images { get; set; }

}