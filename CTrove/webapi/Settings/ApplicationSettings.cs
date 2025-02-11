using Microsoft.Graph.Drives.Item.Items.Item.GetActivitiesByIntervalWithStartDateTimeWithEndDateTimeWithInterval;

namespace CTrove.Api.Settings
{
    public class ApplicationSettings
    {
        public static UserClaims? UserClaims { get; set; } = new UserClaims();
        public static SecurityConfig SecurityConfig { get; set; } = new SecurityConfig();
        public static AzureAdConfig AzureAdConfig { get; set; } = new AzureAdConfig();
        public static HRContributorAPIConfig HRContributorAPIConfig { get; set; } = new HRContributorAPIConfig();

    }

    public class HRContributorAPIConfig
    {
        public string ApiUrl { get; set; } = string.Empty;
    }
}
