namespace Wallet.API.Tools
{
    public class JwtTokenDefaults
    {
        public const string Secret = "WalletAPPVerySecretKey.8081:Wow!";
        public const string ValidAudiance = "https://wallet.web:8080";
        public const string ValidIssuer = "https://wallet.api:8081";
    }
}
