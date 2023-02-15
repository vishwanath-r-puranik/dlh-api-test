using DLHApi.EIS.Authentication;

namespace DLHApi.DAL.EISHandler.Authentication
{
    public  class GenerateToken
    {
        private readonly ITokenHandler _tokenHandler;

        public GenerateToken(ITokenHandler tokenHandler) 
        {
            _tokenHandler = tokenHandler;
        }

        public async Task<string?> GetToken()
        {
            var accessToken =  await _tokenHandler.RetrieveToken();
            if (accessToken != null)
            {
                return accessToken;
            }

            return null;
        }
    }
}
