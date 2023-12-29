using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace ParkIstra.Models.Main;
public class Project
{
    public Project()
    {
        Images = new HashSet<Image>();
    }
    public int Id { get; init; }
    [Required(ErrorMessage = "This field is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "This field is required")]
    public string? Description { get; set; }
    public bool Drafted { get; set; }
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public ICollection<Image> Images { get; set; }
}