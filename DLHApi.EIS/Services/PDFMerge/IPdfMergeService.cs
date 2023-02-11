using DLHApi.EIS.Models;

namespace DLHApi.EIS.Services.PDFMerge
{
    public interface IPdfMergeService
	{
        public Task<byte[]?> GelDlhDocumentByMvid(DocMergeApiRequest apiRequest);

    }
}

