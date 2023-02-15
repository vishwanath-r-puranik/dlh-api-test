using DLHApi.DAL.Models;

namespace DLHApi.DAL.RequestResponse
{
    public class DlhResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public DlhistoryModel? DlhistoryModel { get; set; }

    }

    public class DlhFile
    {
        public byte[]? FileContent { get; set; }
        public string? ContentType { get; set; }
        public string? FileName { get; set; }
    }
}
