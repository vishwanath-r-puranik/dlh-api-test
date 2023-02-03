using DLHApi.DAL.Models;
using DLHApi.DAL.RequestResponse;

namespace DLHApi.DAL.Services
{
    public interface IDlhService
    {
         Task<DlhResponse> GelDlhByMvid(DlhRequest req);

        //DAL->EIS
        ////I think this should not go to DAl layer. DTO layer should directly call EIS layer service
        //Task<byte[]> GelDlhDocumentByMvid(DlhRequest req);
    }
}
