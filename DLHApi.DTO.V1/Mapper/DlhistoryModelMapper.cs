using System.Net;
using AutoMapper;
using DLHApi.Common.Constants;
using DLHApi.Common.Logger.Contracts;
using DLHApi.Common.Utils;
using DLHApi.DAL.RequestResponse;
using DLHApi.DAL.Services;
using DLHApi.DTO.V1.DTO;
using DLHApi.EIS.Models;
using DLHApi.EIS.Services.PDFMerge;
using DLHApi.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DLHApi.DTO.V1.Mapper
{
    public class DlhistoryModelMapper
    {
        private readonly IDlhService _dlhService;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly IPdfMergeService _pdfMergeService;
        private readonly ILoggerManager _logger;

        public DlhistoryModelMapper(IDlhService dlhService, IMapper mapper, IPdfMergeService pdfMergeService, IAuditService auditService, ILoggerManager logger)
        {
            this._dlhService = dlhService;
            this._mapper = mapper;
            _pdfMergeService = pdfMergeService;
            _auditService = auditService;
            this._logger = logger;
        }

        public async Task<IActionResult> DLHHistoryData(int mvid)
        {
            DlhRequest req = new DlhRequest { Mvid = mvid };

            var res = await _dlhService.GelDlhByMvid(req);

            if (res == null) 
            {
                _logger.LogError($"{Project.DLHAPIDTO} - {ErrorConstants.NoData} {(int)HttpStatusCode.NotFound}");
                throw new ApiException(ErrorConstants.NoData, (int)HttpStatusCode.NotFound);
            }
            var resDTO = new DlhApiSuccessResponse<DTO.DlhistoryModel>();
            if (res != null && res.Success==true && res.DlhistoryModel != null)
            {

                //override this mapper.....
                resDTO.Data = _mapper.Map<DTO.DlhistoryModel>(res.DlhistoryModel);

                _logger.LogInfo($"{Project.DLHAPIDTO} - Fetched data successfully and returning to OpenApi ");
                return new OkObjectResult(resDTO.Data);
            }

            return new NotFoundResult();

        }

        public async Task<IActionResult> DLHDocumentMerge(int mvid)
        {
            // Create audit record, unique request id should come from the queue, temporarily appending audit for now
            var requestId = "audit-" + mvid.ToString();
             AddRequestAudit(mvid.ToString(), requestId);

            DlhRequest req = new DlhRequest { Mvid = mvid };

            var res = await _dlhService.GelDlhByMvid(req);

            //update audit record status to DataRetrieved
            _logger.LogInfo($"{Project.DLHAPIDTO} - Update audit table");
            var audit = new UpdateAuditRequest()
            {
                RequestId = requestId,
                Mvid = mvid.ToString(),
                RecordStatus = ReqStatus.DlhDataRetrieved.ToString(),
                DataRetrievedDateTimeStamp = DateTime.Now,
            };
            UpdateRequestAudit(audit);


            var resDTO = _mapper.Map<DTO.DlhistoryModel>(res.DlhistoryModel);

            //convert dlhistory model to docmergeapi request..
            DocMergeApiRequest docMergeApiRequest = MapDLhistoryToDocMergeApiRequest(resDTO, mvid);

            //DTO->EIS
            var fileRes = await _pdfMergeService.GelDlhDocumentByMvid(docMergeApiRequest);


            if (fileRes == null)
            {
                _logger.LogError($"{Project.DLHAPIDTO} - DLH PDF Document not found for Mvid:{mvid}");
                throw new ApiException(ErrorConstants.NoData, (int)HttpStatusCode.NotFound);
            }

            var fileResDTO = new DlhApiSuccessResponse<byte[]>();
            if (fileRes != null)
            {
                fileResDTO.Data = _mapper.Map<byte[]>(fileRes);

                FileContentResult result = new FileContentResult(fileResDTO.Data, "application/pdf")
                {
                    FileDownloadName = "DlhPdf"
                };

                //update audit record status to ReportGenerated
               audit = new UpdateAuditRequest()
                {
                    RequestId = requestId,
                    Mvid = mvid.ToString(),
                    RecordStatus = ReqStatus.ReportGenerated.ToString(),
                    ReportGeneratedDateTimeStamp = DateTime.Now,
                };
                UpdateRequestAudit(audit);

                _logger.LogInfo($"{Project.DLHAPIDTO} - DLH PDF Document found and sending to OpenApi");
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

        private IList<EIS.Models.DlhistoryDisplayInfo?>? MapHistoryInfoList(IList<DTO.DlhistoryDisplayInfo?>? dlhistoryInfoListModel)
        {
            IList<EIS.Models.DlhistoryDisplayInfo?>? dlhistoryDisplayInfos = new List<EIS.Models.DlhistoryDisplayInfo?>();

            if (dlhistoryInfoListModel != null && dlhistoryInfoListModel.Count > 0)
            {
                EIS.Models.DlhistoryDisplayInfo? dlhistoryDisplayInfoItem;

                foreach (var item in dlhistoryInfoListModel)
                {
                    if (item != null)
                    {
                        dlhistoryDisplayInfoItem = MapHistoryInfo(item);
                        if (dlhistoryDisplayInfoItem != null)
                            dlhistoryDisplayInfos.Add(dlhistoryDisplayInfoItem);
                    }
                }
            }
            return dlhistoryDisplayInfos;

        }

        private EIS.Models.DlhistoryDisplayInfo? MapHistoryInfo(DTO.DlhistoryDisplayInfo dlhistoryInfoModel)
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


        private async void AddRequestAudit(string mvid, string requestid)
        {
            try
            {
                var audit = new CreateAuditRequest
                {
                    Mvid = mvid,
                    RequestId = requestid
                };
                var auditResp = await _auditService.AddRequestAudit(audit);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Project.DLHAPIDTO} - AddRequestAudit failed. {ex.Message}");
                throw new ApiException(ex);
            }
        }

        private async void UpdateRequestAudit(UpdateAuditRequest audit)
        {
            try
            {              
                var auditResp = await _auditService.UpdateRequestAudit(audit);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Project.DLHAPIDTO} - UpdateRequestAudit failed. {ex.Message}");
                throw new ApiException(ex);
            }
        }

        #endregion
    }
}
