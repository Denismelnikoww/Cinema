namespace Cinema.Options
{
    public class JwtOptions
    {
        public string AcсessSecretKey { get; set; }
        public string RefreshSecretKey { get; set; }
        public int AccessExpiresHours { get; set; }
        public int RefreshExpiresDays { get; set; }
    }
}