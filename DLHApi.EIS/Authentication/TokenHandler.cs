using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace DLHApi.EIS.Authentication
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        static readonly HttpClient client = new HttpClient();

        public async Task<string> RetrieveToken()
        {
            // :: keycloak
            string url = "https://keycloak-keycloak.apps.pesdev.hcscloud.net/realms/pesrealm/protocol/openid-connect/token";
            string username = "test";
            string password = "test123";
            string client_id = "pesclient";
            string client_secret = "EVWo7ElvQU97ANVBjL0oXR2CxbiFRUxy";
            string grant_type = "password";

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
