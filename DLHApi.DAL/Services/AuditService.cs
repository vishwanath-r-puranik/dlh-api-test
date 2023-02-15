using DLHApi.DAL.Repo;
using DLHApi.Shared;

namespace DLHApi.DAL.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepo _auditRepo;

        public AuditService(IAuditRepo auditRepo)
        {
            _auditRepo = auditRepo;
        }

        public Task<AuditResponse> AddRequestAudit(CreateAuditRequest audit)
        {
          return  _auditRepo.AddRequestAudit(audit);    
        }

        public Task<AuditResponse> UpdateRequestAudit(UpdateAuditRequest audit)
        {
          return   _auditRepo.UpdateRequestAudit(audit);
        }
    }
}
