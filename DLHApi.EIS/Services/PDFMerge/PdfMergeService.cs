
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using DLHApi.Common.Constants;
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

        #region Constructors

        public PdfMergeService(IHttpClientFactory httpClientFactory, ITokenHandler tokenHandler)
        {

            this.httpClientFactory = httpClientFactory ??
                throw new ApiException((string.Format(ErrorConstants.ObjNotSet, "httpClientFactory")), (int)HttpStatusCode.FailedDependency);

            this.tokenHandler = tokenHandler ??
                throw new ApiException((string.Format(ErrorConstants.ObjNotSet, "tokenHandler")), (int)HttpStatusCode.FailedDependency);
        }

        #endregion

        public async Task<byte[]?> GelDlhDocumentByMvid(DocMergeApiRequest apiRequest)
        {
            //get download url...
            var DocMergrUri = Environment.GetEnvironmentVariable("DMSUri");

            return await DownloadPdfDataAsync(DocMergrUri, apiRequest);
        }

        #region Private functions

        private async Task<byte[]?> DownloadPdfDataAsync(string endpointUrl, DocMergeApiRequest apiRequest)
        {
            var fileName = "";

            //select file
            if (apiRequest == null || apiRequest?.Id == null)
                fileName = "dlhwithouthistory.docx";
            else if (apiRequest.historyInfo == null || apiRequest.historyInfo.Count == 0)
                fileName = "Dlhwithouttable.docx";
            else
                fileName = "Dlhwithhistory.docx";

            var fileLoc = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileName);

            //Encode file tto base 64...
            string base64Encoded = Base64DocEncode(fileLoc);

            Models.DMSRequest dmsRequest = new DMSRequest();
            dmsRequest.Template = base64Encoded;

            DlhDocMergeDetails dlhDocMergeDetails = MapDlhDocMergeDetails(apiRequest);

            DlhDockMergeDataReq dlhDockMergeDataReq = new DlhDockMergeDataReq();
            dlhDockMergeDataReq.Dlh = MapDlhReq(dlhDocMergeDetails);

            if (apiRequest.historyInfo != null && apiRequest.historyInfo.Count > 0)
                dlhDockMergeDataReq.DlhInfo = MapDlhHistoryInfo(apiRequest?.historyInfo);

            dmsRequest.jsonDataSet = System.Text.Json.JsonSerializer.Serialize(dlhDockMergeDataReq);

            PdfMergeServiceResponse? encodedFile = await SendAsync<Models.DMSRequest>(endpointUrl, HttpMethod.Post, dmsRequest);

            //decode to file bytes...
            if (encodedFile?.MergedResult != null)
                return Base64DocDecode(encodedFile.MergedResult);

            return null;
        }

        private async Task<PdfMergeServiceResponse?> SendAsync<T>(string endpointUrl, HttpMethod httpMethod, T data = default)
        {
            StringContent? content = GetContent(data);

            PdfMergeServiceResponse pdfResponse = null;

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

        private StringContent? GetContent<T>(T data)
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

            //string? jsonString = await result.Content.ReadAsStringAsync();

            return result;

        }

        private string? Base64DocEncode(string fileLocation)
        {
            byte[] rawBytes = File.ReadAllBytes(fileLocation);

            string? content = Convert.ToBase64String(rawBytes);

            return content;
        }

        private byte[]? Base64DocDecode(string encodedString)
        {
            byte[] content = Convert.FromBase64String(encodedString);

            return content;
        }

        private DlhDocMergeDetails MapDlhDocMergeDetails(DocMergeApiRequest apiRequest)
        {
            DlhDocMergeDetails dlhDocMergeDetails = new DlhDocMergeDetails();
            dlhDocMergeDetails.ReportDate = apiRequest?.ReportDate?.ToString("yyyy/MM/dd");
            dlhDocMergeDetails.Mvid = apiRequest?.MVID;
            dlhDocMergeDetails.LicenseNumber = apiRequest?.LicenseNumber;
            dlhDocMergeDetails.LastName = apiRequest?.FullName;
            //dlhDocMergeDetails.LastName = apiRequest?.LastName;
            //dlhDocMergeDetails.FirstName = apiRequest?.FirstName;
            //dlhDocMergeDetails.MiddleName = apiRequest?.MiddleName;
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

        private IList<DlhDocMergeDetails?>? MapDlhReq(DlhDocMergeDetails dlhDocMerge)
        {
            IList<DlhDocMergeDetails> dlhDocMergeDetails = new List<DlhDocMergeDetails>();
            dlhDocMergeDetails.Add(dlhDocMerge);

            return dlhDocMergeDetails;

        }

        private IList<DlhDocMergeHistoryDetails>? MapDlhHistoryInfo(IList<DlhistoryDisplayInfo>? docMergeDataHistoryInfos)
        {
            IList<DlhDocMergeHistoryDetails> dlhDocMergeDetailsString = new List<DlhDocMergeHistoryDetails>();

            foreach (var item in docMergeDataHistoryInfos)
            {
                DlhDocMergeHistoryDetails dlhDocMergeHistoryDetails = MapDlhHistoryInfoItem(item);
                if (dlhDocMergeHistoryDetails != null)
                    dlhDocMergeDetailsString.Add(dlhDocMergeHistoryDetails);
            }
            return dlhDocMergeDetailsString;

        }

        private DlhDocMergeHistoryDetails? MapDlhHistoryInfoItem(DlhistoryDisplayInfo? docMergeDataHistoryInfos)
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

