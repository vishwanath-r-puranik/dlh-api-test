using DLHApi.DAL.Models;
using DLHApi.DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.DAL.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepo _auditRepo;

        public AuditService(IAuditRepo auditRepo)
        {
            _auditRepo = auditRepo;
        }

        public void AddRequestAudit(string mvid)
        {
            _auditRepo.AddRequestAudit(mvid);    
        }
    }
}
