namespace API_ADOPTAPATAS_3.Dtos
{
    public class JwtSettingsDto
    {
        public string Key { get; set; } = string.Empty;
        public bool ValidKey { get; set; } = true;
        public string Issuer { get; set; } = string.Empty;
        public bool ValidIssuer { get; set; } = true;
        public string Audience { get; set; } = string.Empty;
        public bool ValidAudience { get; set; } = true;
        public bool RequireExpirationTime { get; set; }
        public bool FlagExpirationTimeHours { get; set; }
        public int ExpirationTimeHours { get; set; }
        public bool FlagExpirationTimeMinutes { get; set; }
        public int ExpirationTimeMinutes { get; set; }
        public bool ValidateLifeTime { get; set; }
    }
}
