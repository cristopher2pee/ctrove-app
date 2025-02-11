namespace CTrove.Api.Settings
{
    public class AzureAdConfig
    {
        public static string Name { get; } = "AzureAd";
        public string Instance { get; set; }
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Scopes { get; set; }
    }


}
