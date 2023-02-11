using System;
namespace DLHApi.EIS.Models
{
	public class DlhDockMergeDataReq
	{
        public IList<DlhDocMergeDetails?>? Dlh { get; set; }
        public IList<DlhDocMergeHistoryDetails>? DlhInfo { get; set; }
    }
}

