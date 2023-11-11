namespace ParkIstra.Models.Util;

public class SYS_Exception
{

    public SYS_Exception()
    {
        
    }
    public SYS_Exception(string tip, string opis)
    {
        this.Tip = tip;
        this.Opis = opis;
    }

    public int Id { get; set; }
    public string? Tip { get; set; }
    public string? Opis { get; set; }
    public int? UserId { get; set; }

   
}