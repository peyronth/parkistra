using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkIstra.Models.Main;

[NotMapped]
public class Register
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? password { get; set; }
    [Required]
    public int? UserType { get; set; }

}

[NotMapped]
public class Login
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? password { get; set; }
}

[NotMapped]
public class Response
{
    public bool? Status { get; set; }
    public string? Message { get; set; }
    public string? StatusCode { get; set; }
    public string? Data { get; set; }
    public string? token { get; set; }
    public string? id { get; set; }
    public string? FullName { get; set; }
}
