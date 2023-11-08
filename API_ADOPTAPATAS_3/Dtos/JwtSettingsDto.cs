namespace API_ADOPTAPATAS_3.Dtos
{
    public class JwtSettingsDto
    {
      
        public bool ValidKey { get; set; } = true;
        public string Key { get; set; } = string.Empty;
        public bool ValidateIssuer { get; set; } = true;
        public string Issuer { get; set; } = string.Empty;
        public bool ValidateAudience { get; set; } = true;
        public string Audience { get; set; } = string.Empty;
        public bool RequireExpirationTime { get; set; }
        public bool FlagExpirationTimeHours { get; set; }
        public int ExpirationTimeHours { get; set; }
        public bool FlagExpirationTimeMinutes { get; set; }
        public int ExpirationTimeMinutes { get; set; }
        public bool ValidateLifeTime { get; set; }
    }
}
