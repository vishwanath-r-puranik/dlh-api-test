﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.Common.Models
{
    public class DlhApiFailureResponse : IDlhApiResponse
    {
        public DlhErrorModel Error { get; set; }

        public DlhApiFailureResponse()
        {
        }

        public DlhApiFailureResponse(DlhErrorModel error)
        {
            Error = error;
        }
    }
}
