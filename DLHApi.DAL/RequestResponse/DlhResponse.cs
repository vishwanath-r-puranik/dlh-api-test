using DLHApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.DAL.RequestResponse
{
    public class DlhResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public DlhistoryModel? DlhistoryModel { get; set; }

       // public DlhFile? DlhFile { get; set; }
    }

    public class DlhFile
    {
        public byte[]? FileContent { get; set; }
        public string? ContentType { get; set; }
        public string? FileName { get; set; }
    }
}
