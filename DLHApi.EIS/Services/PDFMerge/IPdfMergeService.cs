namespace DLHApi.EIS.Services.PDFMerge
{
    public interface IPdfMergeService
	{
        public Task<byte[]?> GelDlhDocumentByMvid(int mvid);
    }
}

