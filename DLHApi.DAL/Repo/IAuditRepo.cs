using DLHApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.DAL.Repo
{
    public interface IAuditRepo
    {
        void AddRequestAudit(string Mvid);
    }
}
