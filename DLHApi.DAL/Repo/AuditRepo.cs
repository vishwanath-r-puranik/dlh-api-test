using DLHApi.DAL.Data;
using DLHApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Success,
            Failed
        }

        public void AddRequestAudit( string Mvid)
        {
            var reqAudit = new DlhRequestAudit()
            {
                Mvid = Mvid,
                RequestDate = DateTime.Now,
                ReqStatus = ReqStatus.Success.ToString()

            };
            try
            {
                _dlhAuditdbContxt.DlhRequestAudits.Add(reqAudit);
                _dlhAuditdbContxt.SaveChanges();
            }
            catch (Exception ex) {
            }


        }
    }
}
