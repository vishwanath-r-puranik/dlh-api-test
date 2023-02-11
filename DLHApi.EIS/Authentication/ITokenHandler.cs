namespace DLHApi.EIS.Authentication
{
    public  interface ITokenHandler
    {
        Task<string> RetrieveToken();
        Task<string> RetrieveAccessToken();
    }
}
