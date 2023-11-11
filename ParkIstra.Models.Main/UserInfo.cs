using System.ComponentModel.DataAnnotations.Schema;

namespace ParkIstra.Models.Main;
public class UserInfo
{
    public UserInfo()
    {
    }

    public int UserInfoId { get; set; }
    public int UserType { get; set; }
    public string IdentifikacioniBroj { get; set; }
    public string Naziv { get; set; }
    public string Adresa { get; set; }
    public string? OdgovornoLice { get; set; }
    public string KontaktPodaci { get; set; }
    public bool Checked { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
}