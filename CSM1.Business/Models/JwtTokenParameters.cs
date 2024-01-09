namespace CSM1.Business.Models;

public class JwtTokenParameters
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Salt { get; set; }
    public int ExpireMinutes { get; set; }
}
