using DLHApi.DAL.Repo;

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
