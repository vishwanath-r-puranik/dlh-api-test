using DLHApi.DAL.RequestResponse;

namespace DLHApi.DAL.Repo
{
    public interface IDlhRepo
    {
        Task<DlhResponse> GelDlhByMvid(DlhRequest req);
    }
}