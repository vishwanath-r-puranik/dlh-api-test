using AutoMapper;
using DLHApi.DAL.Services;
using DLHApi.DAL.RequestResponse;
using DLHApi.DTO.V1.DTO;
using System.Net;
using DLHApi.Common.Utils;
using DLHApi.Common.Constants;
using Microsoft.AspNetCore.Mvc;

//DTO->EIS
using DLHApi.EIS.Services.PDFMerge;

namespace DLHApi.DTO.V1.Mapper
{
    public class DlhistoryModelMapper
    {
        private readonly IDlhService _dlhService;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        //DTO->EIS
        private readonly IPdfMergeService _pdfMergeService;

        public DlhistoryModelMapper(IDlhService dlhService, IMapper mapper, IPdfMergeService pdfMergeService, IAuditService auditService)
        {
            this._dlhService = dlhService;
            this._mapper = mapper;
            _pdfMergeService = pdfMergeService;
            _auditService = auditService;
        }

        public async Task<IActionResult> DLHHistoryData(int mvid)
        {

            //just to show
            if (mvid == 432)
               throw new ApiException(string.Format(ErrorConstants.IncorrectMvID, mvid), (int)HttpStatusCode.BadRequest);

            DlhRequest req = new DlhRequest { Mvid = mvid };

            var res = await _dlhService.GelDlhByMvid(req);

            if (res == null)
                throw new ApiException(ErrorConstants.NoData, (int)HttpStatusCode.NotFound);

            var resDTO = new DlhApiSuccessResponse<DTO.DlhistoryModel>();
            if (res != null && res.Success==true && res.DlhistoryModel != null)
            {
                resDTO.Data = _mapper.Map<DTO.DlhistoryModel>(res.DlhistoryModel);

                //FileContentResult result = new FileContentResult(resDTO.Data.FileContent, resDTO.Data.ContentType) { 
                //    FileDownloadName = resDTO.Data.FileName
                //};

                //call Doc merge service here

                _auditService.AddRequestAudit(mvid.ToString());
                     
                return new OkObjectResult(resDTO.Data);
            }

            return new NotFoundResult();

        }

        public async Task<IActionResult> DLHDocumentMerge(int mvid)
        {
            DlhRequest req = new DlhRequest { Mvid = mvid };

            //DAL->EIS
            //var res = await _dlhService.GelDlhDocumentByMvid(req);

            //DTO->EIS
            var res = await _pdfMergeService.GelDlhDocumentByMvid(req.Mvid);

            if (res == null)
                throw new ApiException(ErrorConstants.NoData, (int)HttpStatusCode.NotFound);

            var resDTO = new DlhApiSuccessResponse<byte[]>();
            if (res != null)
            {
                resDTO.Data = _mapper.Map<byte[]>(res);

                FileContentResult result = new FileContentResult(resDTO.Data, "application/pdf")
                {
                    FileDownloadName = "DlhPdf"
                };

                return result;
            }

            return new NotFoundResult();

        }
    }
}
