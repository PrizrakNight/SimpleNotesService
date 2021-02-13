namespace SimpleNotes.Server.Application.Options
{
    public class TokenProviderOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string ApplicationSecret { get; set; }

        public long TokenExpiration { get; set; }
    }
}
