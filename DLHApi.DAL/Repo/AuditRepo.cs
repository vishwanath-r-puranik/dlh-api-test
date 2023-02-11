using DLHApi.Common.Constants;
using System.Net;
using DLHApi.Common.Utils;
using DLHApi.DAL.Data;
using DLHApi.DAL.Models;

namespace DLHApi.DAL.Repo
{
    public class AuditRepo: IAuditRepo
    {

        private readonly DlhdevAuditContext _dlhAuditdbContxt;

        public AuditRepo(DlhdevAuditContext dlhAuditdbContext)
        {
            _dlhAuditdbContxt = dlhAuditdbContext;
        }

        public enum ReqStatus
        {
            Requested,
            Paid
        }

        public void AddRequestAudit( string Mvid)
        {
            var reqAudit = new DlhRequestAudit()
            {
                Mvid = Mvid,
                RequestDateTimeStamp = DateTime.Now,
                RecordStatus = ReqStatus.Requested.ToString(),
                RequestId = Guid.NewGuid().ToString(),

            };
            try
            {
                _dlhAuditdbContxt.DlhRequestAudits.Add(reqAudit);
                _dlhAuditdbContxt.SaveChanges();
            }
            catch (Exception ex) {
                throw new ApiException(ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
