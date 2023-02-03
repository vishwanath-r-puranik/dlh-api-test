using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.Common.Models
{
    public class DlhErrorModel
    {
        public string? Message { get; set; }

        public int Status { get; set; }

        public IEnumerable<DlhValidationError>? ValidationErrors { get; set; }
    }
}
