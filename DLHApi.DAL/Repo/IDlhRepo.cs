using DLHApi.DAL.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.DAL.Repo
{
    public interface IDlhRepo
    {
        Task<DlhResponse> GelDlhByMvid(DlhRequest req);
    }
}