using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DLHApi.EIS.Authentication
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        static readonly HttpClient client = new HttpClient();

        public async Task<string> RetrieveToken()
        {
            // :: keycloak
            var url = Environment.GetEnvironmentVariable("KeyCloakUri");
            var username = Environment.GetEnvironmentVariable("KeyCloakUsername");
            var password = Environment.GetEnvironmentVariable("KeyCloakPassword");
            var client_id = Environment.GetEnvironmentVariable("KeyCloakClient_id");
            var client_secret = Environment.GetEnvironmentVariable("KeyCloakClient_secret");
            var grant_type = Environment.GetEnvironmentVariable("KeyCloakGrant_type");

            var form = new Dictionary<string, string>
            {
                    {"username", username},
                    {"password", password},
                    {"client_id", client_id},
                    {"client_secret", client_secret},
                    {"grant_type", grant_type},
            };

            HttpResponseMessage tokenResponse = await client.PostAsync(url, new FormUrlEncodedContent(form));
            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            Token tok = JsonConvert.DeserializeObject<Token>(jsonContent);
            return tok.AccessToken;
        }

        public async Task<string> RetrieveAccessToken()
        {
            var url = Environment.GetEnvironmentVariable("DMS_AccesToken_Uri");
            var client_id = Environment.GetEnvironmentVariable("DMS_ClientID");
            var client_secret = Environment.GetEnvironmentVariable("DMS_ClientSecret");
            var grant_type = Environment.GetEnvironmentVariable("DMS_GrantType");

            var form = new Dictionary<string, string>
            {
                {"client_id", client_id},
                {"client_secret", client_secret},
                {"grant_type", grant_type},
            };

            HttpResponseMessage tokenResponse = await client.PostAsync(url, new FormUrlEncodedContent(form));
            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            Token tok = JsonConvert.DeserializeObject<Token>(jsonContent);
            return tok.AccessToken;
        }

        internal class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
        }
    }
}
