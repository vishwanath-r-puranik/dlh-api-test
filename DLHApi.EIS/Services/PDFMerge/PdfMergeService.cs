
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using DLHApi.Common.Constants;
using DLHApi.Common.Logger.Contracts;
using DLHApi.Common.Utils;
using DLHApi.EIS.Authentication;
using DLHApi.EIS.Models;
using Newtonsoft.Json;

namespace DLHApi.EIS.Services.PDFMerge
{
    public class PdfMergeService: IPdfMergeService
	{
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ITokenHandler tokenHandler;
        private readonly ILoggerManager _logger;

        #region Constructors

        public PdfMergeService(IHttpClientFactory httpClientFactory, ITokenHandler tokenHandler, ILoggerManager logger)
        {

            this.httpClientFactory = httpClientFactory ??
                throw new ApiException((string.Format(ErrorConstants.ObjNotSet, "httpClientFactory")), (int)HttpStatusCode.FailedDependency);

            this.tokenHandler = tokenHandler ??
                throw new ApiException((string.Format(ErrorConstants.ObjNotSet, "tokenHandler")), (int)HttpStatusCode.FailedDependency);

            this._logger = logger;
        }

        #endregion

        public async Task<byte[]?> GelDlhDocumentByMvid(DocMergeApiRequest apiRequest)
        {
            //get download url...
            var DocMergrUri = Environment.GetEnvironmentVariable("DMSUri");
            _logger.LogInfo($"{Project.DLHAPIEIS} - Request document merge from {DocMergrUri}");

            return await DownloadPdfDataAsync(DocMergrUri, apiRequest);
        }

        #region Private functions

        private async Task<byte[]?> DownloadPdfDataAsync(string? endpointUrl, DocMergeApiRequest apiRequest)
        {
            _logger.LogInfo($"{Project.DLHAPIEIS} - Calling DownloadPdfDataAsync...");

            string fileName = "";

            //select file
            if (apiRequest == null || apiRequest?.Id == null)
                fileName = "dlhwithouthistory.docx";
            else if (apiRequest.historyInfo == null || apiRequest.historyInfo.Count == 0)
                fileName = "Dlhwithouttable.docx";
            else
                fileName = "Dlhwithhistory.docx";

            string fileLoc = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileName);

            //Encode file tto base 64...
            string? base64Encoded = Base64DocEncode(fileLoc);

            Models.DmsRequest dmsRequest = new DmsRequest();
            dmsRequest.Template = base64Encoded;

            DlhDocMergeDetails dlhDocMergeDetails = MapDlhDocMergeDetails(apiRequest);

            DlhDockMergeDataReq dlhDockMergeDataReq = new DlhDockMergeDataReq();
            dlhDockMergeDataReq.Dlh = MapDlhReq(dlhDocMergeDetails);

            if (apiRequest?.historyInfo != null && apiRequest.historyInfo.Count > 0)
                dlhDockMergeDataReq.DlhInfo = MapDlhHistoryInfo(apiRequest?.historyInfo);

            dmsRequest.jsonDataSet = System.Text.Json.JsonSerializer.Serialize(dlhDockMergeDataReq);

            PdfMergeServiceResponse? encodedFile = await SendAsync<Models.DmsRequest>(endpointUrl, HttpMethod.Post, dmsRequest);

            //decode to file bytes...
            if (encodedFile?.MergedResult != null)
                return Base64DocDecode(encodedFile.MergedResult);

            return null;
        }

        private async Task<PdfMergeServiceResponse?> SendAsync<T>(string? endpointUrl, HttpMethod httpMethod, T? data = default)
        {
            StringContent? content = GetContent(data);

            PdfMergeServiceResponse? pdfResponse = null;

            if (string.IsNullOrEmpty(endpointUrl)) return null;

            var response = await GetResponse(endpointUrl, httpMethod, content);

            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonstring = await response.Content.ReadAsStringAsync();

                var apiResponse = (!string.IsNullOrEmpty(jsonstring))
                                    ? JsonConvert.DeserializeObject<PdfMergeServiceResponse>(jsonstring) : default;

                pdfResponse = (apiResponse != null) ? apiResponse : default;
            }

            return pdfResponse;
        }

        private static StringContent? GetContent<T>(T data)
        {
            StringContent? content = default;
            if (data != null)
            {
                string? json = System.Text.Json.JsonSerializer.Serialize(data);

                if (!string.IsNullOrEmpty(json))
                    return new StringContent(json, Encoding.Default, MediaTypeNames.Application.Json);
            }
            return content;
        }

        private async Task<HttpResponseMessage?> GetResponse(string endpointUrl, HttpMethod httpMethod, HttpContent? content = default)
        {
            var client = httpClientFactory.CreateClient();

            var accessToken = await tokenHandler.RetrieveAccessToken();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(endpointUrl, UriKind.Absolute),
                Method = httpMethod,
                Content = content
            };

            var result = await client.SendAsync(request);

            return result;

        }

        private static string? Base64DocEncode(string? fileLocation)
        {
            if (fileLocation == null) return null;

            byte[] rawBytes = File.ReadAllBytes(fileLocation);

            string? content = Convert.ToBase64String(rawBytes);

            return content;
        }

        private static byte[]? Base64DocDecode(string? encodedString)
        {
            if (encodedString == null) return Array.Empty<byte>() ;

            byte[] content = Convert.FromBase64String(encodedString);

            return content;
        }

        private static DlhDocMergeDetails MapDlhDocMergeDetails(DocMergeApiRequest? apiRequest)
        {
            DlhDocMergeDetails dlhDocMergeDetails = new DlhDocMergeDetails();
            dlhDocMergeDetails.ReportDate = FormatExtension.ToDateTime(apiRequest?.ReportDate);
            dlhDocMergeDetails.Mvid = apiRequest?.MVID;
            dlhDocMergeDetails.LicenseNumber = apiRequest?.LicenseNumber;
            dlhDocMergeDetails.LastName = apiRequest?.FullName;
            dlhDocMergeDetails.Dob = apiRequest?.Dob;
            dlhDocMergeDetails.IssueDate = apiRequest?.DateOfIssue;
            dlhDocMergeDetails.ExpiryDate = apiRequest?.DateOfExpire;
            dlhDocMergeDetails.ServiceType = apiRequest?.ServiceType;
            dlhDocMergeDetails.LicenseClass = apiRequest?.LicenseClass;
            dlhDocMergeDetails.Gdl = apiRequest?.GDl;
            dlhDocMergeDetails.GdlExitDate = apiRequest?.GDlExitDate;
            dlhDocMergeDetails.Conditions = apiRequest?.Conditions;

            return dlhDocMergeDetails;
        }

        private static IList<DlhDocMergeDetails?>? MapDlhReq(DlhDocMergeDetails dlhDocMerge)
        {
            IList<DlhDocMergeDetails?> dlhDocMergeDetails = new List<DlhDocMergeDetails?>();
            dlhDocMergeDetails.Add(dlhDocMerge);

            return dlhDocMergeDetails;

        }

        private static IList<DlhDocMergeHistoryDetails>? MapDlhHistoryInfo(IList<DlhistoryDisplayInfo?>? docMergeDataHistoryInfos)
        {
            IList<DlhDocMergeHistoryDetails> dlhDocMergeDetailsString = new List<DlhDocMergeHistoryDetails>();

            if (docMergeDataHistoryInfos == null || docMergeDataHistoryInfos.Count <= 0) return dlhDocMergeDetailsString;

            foreach (var item in docMergeDataHistoryInfos)
            {
                DlhDocMergeHistoryDetails? dlhDocMergeHistoryDetails = MapDlhHistoryInfoItem(item);
                if (dlhDocMergeHistoryDetails != null)
                    dlhDocMergeDetailsString.Add(dlhDocMergeHistoryDetails);
            }
            return dlhDocMergeDetailsString;

        }

        private static DlhDocMergeHistoryDetails? MapDlhHistoryInfoItem(DlhistoryDisplayInfo? docMergeDataHistoryInfos)
        {
            DlhDocMergeHistoryDetails dlhDocMergeHistoryDetails;
            if (docMergeDataHistoryInfos != null)
            {
                dlhDocMergeHistoryDetails = new DlhDocMergeHistoryDetails();
                dlhDocMergeHistoryDetails.DlhClass = docMergeDataHistoryInfos?.LicenseClass;
                dlhDocMergeHistoryDetails.DlhIssueDate = docMergeDataHistoryInfos?.IssueDate;
                dlhDocMergeHistoryDetails.DlhServiceType = docMergeDataHistoryInfos?.ServiceType;

                return dlhDocMergeHistoryDetails;
            }
            return null;

        }

        #endregion
    }
}

