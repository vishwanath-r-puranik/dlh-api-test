using DLHApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.DAL.Services
{
    public interface IAuditService
    {
        void AddRequestAudit(string mvid);
    }
}
