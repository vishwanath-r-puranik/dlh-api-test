namespace DLHApi.Common.Models
{
    public class DlhApiFailureResponse : IDlhApiResponse
    {
        public DlhErrorModel? Error { get; set; }

        public DlhApiFailureResponse(DlhErrorModel? error)
        {
            Error = error;
        }
    }
}
