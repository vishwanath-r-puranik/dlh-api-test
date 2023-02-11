
namespace DLHApi.EIS.Models
{
	public class PdfMergeServiceResponse
    {
        public string? MergedResult { get; set; }
        public string? ErrorMessage { get; set; }
        public int ElapsedMsec { get; set; }
        public IList<string?>? TimingsMsec { get; set; }
        public string? AuthUser { get; set; }

    }
}

