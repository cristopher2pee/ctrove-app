using Azure.Identity;
using CTrove.Api.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace CTrove.Api.Azure
{
    public class AzureAdServices
    {
        private readonly IOptions<AzureAdConfig> _azureAdConfig;
        private readonly IConfiguration _configuration;
        private GraphServiceClient? _graphServiceClient;
        public bool IsServiceRunning { get; private set; } = false;

        public AzureAdServices(IOptions<AzureAdConfig> azureAdConfig, IConfiguration configuration)
        {
            _azureAdConfig = azureAdConfig;
            _configuration = configuration;
        }

        public bool StartService()
        {
            try
            {
                string secretKey = _configuration["AZURE_CLIENT_SECRET"] ?? "";

                var clientSecretCredential = new ClientSecretCredential(_azureAdConfig?.Value.TenantId, _azureAdConfig?.Value.ClientId, secretKey);

                _graphServiceClient = new GraphServiceClient(clientSecretCredential);

                this.IsServiceRunning = true;
            }
            catch (Exception)
            {
                throw;
            }

            return this.IsServiceRunning;
        }

        public async Task<Invitation> InviteUser(string email)
        {
            if (this.IsServiceRunning)
            {
                try
                {
                    InvitedUserMessageInfo info = new InvitedUserMessageInfo();
                   // info.AdditionalData.Add("email", email);
                   // info.AdditionalData.Add("Thank you", "Thank you");

                    var invitation = new Invitation()
                    {
                        InvitedUserEmailAddress = email,
                        InviteRedirectUrl = "http://localhost:4200",
                        InvitedUserMessageInfo = info,
                        SendInvitationMessage = true
                    };

                    var post = await _graphServiceClient!.Invitations.PostAsync(invitation);

                    return invitation;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                throw new Exception("Graph Service Client service is not running");
            }

            return null;
        }
    }
}
