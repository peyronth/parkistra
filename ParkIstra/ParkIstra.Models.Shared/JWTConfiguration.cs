namespace ParkIstra.Models.Shared
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}