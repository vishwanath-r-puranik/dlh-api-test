namespace DLHApi.Shared
{
    public interface IAuditService
    {
        Task<AuditResponse> AddRequestAudit(CreateAuditRequest audit);
        Task<AuditResponse> UpdateRequestAudit(UpdateAuditRequest audit);
    }
}