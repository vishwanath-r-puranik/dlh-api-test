using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.Common.Models
{
    public class DlhValidationError
    {
        public string Name { get; }

        public string Reason { get; }

        public DlhValidationError(string name, string reason)
        {
            Name = name;
            Reason = reason;
        }
    }
}
