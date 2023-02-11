using DLHApi.DAL.Models;
using DLHApi.DAL.RequestResponse;

namespace DLHApi.DAL.Services
{
    public interface IDlhService
    {
         Task<DlhResponse> GelDlhByMvid(DlhRequest req);

    }
}
