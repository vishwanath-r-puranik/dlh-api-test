using System.Net;
using DLHApi.Common.Utils;
using DLHApi.Common.Constants;
using DLHApi.DAL.RequestResponse;
using DLHApi.DAL.Repo;
//DAL->EIS
//using DLHApi.EIS.Services.PDFMerge;

namespace DLHApi.DAL.Services
{
    public class DlhService : IDlhService
    {
        private readonly IDlhRepo _dlhrepo;
        //DAL->EIS
        //private readonly IPdfMergeService _pdfMergeService;

        public DlhService(IDlhRepo dlhrepo)//, IPdfMergeService pdfMergeService)
        {
            _dlhrepo = dlhrepo;
            //DAL->EIS
            //_pdfMergeService = pdfMergeService;
        }

        public async Task<DlhResponse> GelDlhByMvid(DlhRequest req)
        {
            //juust for show...
            if (req.Mvid == 432)
                throw new ApiException(string.Format(ErrorConstants.IncorrectMvID, req.Mvid), (int)HttpStatusCode.Conflict);


            //if (req.Mvid == null)
            //    throw new ApiException(string.Format(ErrorConstants.IncorrectMvID, req.Mvid), (int)HttpStatusCode.BadRequest);

            var resp = await _dlhrepo.GelDlhByMvid(req);

            return resp;
        }

        //DAL->EIS
        ////I think this should not go to DAl layer. DTO layer should directly call EIS layer service
        //public async Task<byte[]?> GelDlhDocumentByMvid(DlhRequest req)
        //{

        //    var resp = await _pdfMergeService.GelDlhDocumentByMvid(req.Mvid);

        //    return resp;
        //}
    }

}
