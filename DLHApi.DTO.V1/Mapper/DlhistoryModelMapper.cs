using System.Net;
using AutoMapper;
using DLHApi.Common.Constants;
using DLHApi.Common.Utils;
using DLHApi.DAL.RequestResponse;
using DLHApi.DAL.Services;
using DLHApi.DTO.V1.DTO;
using DLHApi.EIS.Models;
using DLHApi.EIS.Services.PDFMerge;
using Microsoft.AspNetCore.Mvc;

namespace DLHApi.DTO.V1.Mapper
{
    public class DlhistoryModelMapper
    {
        private readonly IDlhService _dlhService;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
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
            {
                var validationError = new List<Common.Models.DlhValidationError>
                {
                    new Common.Models.DlhValidationError("Mvid", string.Format(ErrorConstants.IncorrectMvID, mvid))
                };

                throw new ApiException(validationError, (int)HttpStatusCode.Unauthorized);
            }

            DlhRequest req = new DlhRequest { Mvid = mvid };

            var res = await _dlhService.GelDlhByMvid(req);

            if (res == null)
                throw new ApiException(ErrorConstants.NoData, (int)HttpStatusCode.NotFound);

            var resDTO = new DlhApiSuccessResponse<DTO.DlhistoryModel>();
            if (res != null && res.Success==true && res.DlhistoryModel != null)
            {
                //override this mapper.....
                resDTO.Data = _mapper.Map<DTO.DlhistoryModel>(res.DlhistoryModel);

                return new OkObjectResult(resDTO.Data);
            }

            return new NotFoundResult();

        }

        public async Task<IActionResult> DLHDocumentMerge(int mvid)
        {

            DlhRequest req = new DlhRequest { Mvid = mvid };

            var res = await _dlhService.GelDlhByMvid(req);

            var resDTO = _mapper.Map<DTO.DlhistoryModel>(res.DlhistoryModel);

            //convert dlhistory model to docmergeapi request..
            DocMergeApiRequest docMergeApiRequest = MapDLhistoryToDocMergeApiRequest(resDTO, mvid);

            //DTO->EIS
            var fileRes = await _pdfMergeService.GelDlhDocumentByMvid(docMergeApiRequest);

            //_auditService.AddRequestAudit(mvid.ToString());

            if (fileRes == null)
                throw new ApiException(ErrorConstants.NoData, (int)HttpStatusCode.NotFound);

            var fileResDTO = new DlhApiSuccessResponse<byte[]>();
            if (fileRes != null)
            {
                fileResDTO.Data = _mapper.Map<byte[]>(fileRes);

                FileContentResult result = new FileContentResult(fileResDTO.Data, "application/pdf")
                {
                    FileDownloadName = "DlhPdf"
                };

                return result;
            }

            return new NotFoundResult();

        }


        #region Private Functions

        private DocMergeApiRequest MapDLhistoryToDocMergeApiRequest(DTO.DlhistoryModel? dlhistoryModel, int mvid)
        {
            if (dlhistoryModel == null)
                return new DocMergeApiRequest()
                {

                    MVID = mvid.ToString(),
                    ReportDate = DateTime.Now
                };

            return new DocMergeApiRequest()
            {
                Id = 001,
                MVID = (dlhistoryModel.MVID != null) ? dlhistoryModel.MVID : mvid.ToString(),
                FullName = dlhistoryModel.FullName,
                Dob = dlhistoryModel.Dob,
                Address = string.Empty, 
                LicenseClass = dlhistoryModel.LicenseClass,
                ServiceType = dlhistoryModel.ServiceType,
                DateOfIssue = dlhistoryModel.DateOfIssue,
                DateOfExpire = dlhistoryModel.DateOfExpire,
                LicenseNumber = dlhistoryModel.LicenseNumber,
                GDl = dlhistoryModel.GDl,
                GDlExitDate = dlhistoryModel.GDlExitDate,
                Conditions = dlhistoryModel.Conditions,
                historyInfo = MapHistoryInfoList(dlhistoryModel.historyInfo),
                ReportDate = DateTime.Now
            };

        }

        private IList<EIS.Models.DlhistoryDisplayInfo> MapHistoryInfoList(IList<DTO.DlhistoryDisplayInfo> dlhistoryInfoListModel)
        {
            IList<EIS.Models.DlhistoryDisplayInfo> dlhistoryDisplayInfos = new List<EIS.Models.DlhistoryDisplayInfo>();

            if (dlhistoryInfoListModel != null && dlhistoryInfoListModel.Count > 0)
            {
                EIS.Models.DlhistoryDisplayInfo dlhistoryDisplayInfoItem;

                foreach (var item in dlhistoryInfoListModel)
                {
                    dlhistoryDisplayInfoItem = MapHistoryInfo(item);
                    if (dlhistoryDisplayInfoItem != null)
                        dlhistoryDisplayInfos.Add(dlhistoryDisplayInfoItem);
                }
            }
            return dlhistoryDisplayInfos;

        }

        private EIS.Models.DlhistoryDisplayInfo MapHistoryInfo(DTO.DlhistoryDisplayInfo dlhistoryInfoModel)
        {
            if (dlhistoryInfoModel != null)
                return new EIS.Models.DlhistoryDisplayInfo()
                {
                    IssueDate = dlhistoryInfoModel.IssueDate,
                    ServiceType = dlhistoryInfoModel.ServiceType,
                    LicenseClass = dlhistoryInfoModel.LicClass,
                };

            return null;

        }
     
        #endregion
    }
}
