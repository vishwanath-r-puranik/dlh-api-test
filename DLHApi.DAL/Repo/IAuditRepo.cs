using DLHApi.Shared;

namespace DLHApi.DAL.Repo
{
    public interface IAuditRepo
    {
        Task<AuditResponse> AddRequestAudit(CreateAuditRequest audit);
        Task<AuditResponse> UpdateRequestAudit(UpdateAuditRequest audit);
    }
}
