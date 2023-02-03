
using DLHApi.Common.Utils;
using DLHApi.Common.Constants;
using System.Net;

namespace DLHApi.EIS.Services.PDFMerge
{
	public class PdfMergeService: IPdfMergeService
	{
        private readonly IHttpClientFactory httpClientFactory;

        #region Constructors

        public PdfMergeService(IHttpClientFactory httpClientFactory)
        {

            this.httpClientFactory = httpClientFactory ??
                throw new ApiException((string.Format(ErrorConstants.ObjNotSet, "httpClientFactory")), (int)HttpStatusCode.FailedDependency);
        }

        #endregion

        public async Task<byte[]?> GelDlhDocumentByMvid(int mvid)
        {
            //get download url...
            var DocMergrUri = Environment.GetEnvironmentVariable("DocMergrUri");
            DocMergrUri = DocMergrUri + "/" + mvid.ToString();

            return await DownloadAsync(DocMergrUri);
        }

        #region Private functions

        private async Task<byte[]?> DownloadAsync(string downloadUrl)
        {
            var client = httpClientFactory.CreateClient();

            using (var s = await client.GetStreamAsync(downloadUrl))
            {
                using (MemoryStream ms = new())
                {
                    s.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        #endregion
    }
}

