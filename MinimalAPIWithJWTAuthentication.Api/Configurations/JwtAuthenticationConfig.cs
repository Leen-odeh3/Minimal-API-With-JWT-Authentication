namespace MinimalAPIWithJWTAuthentication.Api.Configurations;
public class JwtAuthenticationConfig
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
