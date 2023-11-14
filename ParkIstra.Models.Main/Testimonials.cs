using System.ComponentModel.DataAnnotations;

namespace ParkIstra.Models.Main;
public class Testimonials
{
    public int Id { get; init; }
    [Required(ErrorMessage = "This field is required")]
    public string? Content { get; set; }
    [Required(ErrorMessage = "This field is required")]
    public string? Author { get; set; }
    [Required(ErrorMessage = "This field is required")]
    public string? Country { get; set; }
}