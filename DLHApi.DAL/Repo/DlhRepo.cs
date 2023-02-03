using DLHApi.DAL.Data;
using DLHApi.DAL.Models;
using DLHApi.DAL.RequestResponse;
using DLHApi.DAL.Utils;
using IronPdf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.DAL.Repo
{
    public class DlhRepo : IDlhRepo
    {
        private readonly DlhdevDbContext _dlhdevdbContxt;

        public DlhRepo(DlhdevDbContext dlhdevdbContext)
        {
            _dlhdevdbContxt = dlhdevdbContext;
        }

        public async Task<DlhResponse> GelDlhByMvid(DlhRequest req)
        {
            DlhResponse response = new DlhResponse
            {
                Success = false,
                DlhistoryModel = null,
                Message = ""
            };
            try
            {
                // first check if the mvid exist in Client
                var client = await _dlhdevdbContxt.Clients.FirstOrDefaultAsync(c=>c.Mvid== req.Mvid);
                if (client == null)
                {
                    response.Message = "Mvid not found!";
                    return response;
                }

                var clientLicenseQry = (from c in _dlhdevdbContxt.Clients
                                        join l in _dlhdevdbContxt.Licences on c.Mvid equals l.Mvid
                                        join ld in _dlhdevdbContxt.LicenceDetails on l.Mvid equals ld.Mvid
                                        join lc in _dlhdevdbContxt.LicenceClasses on ld.LicClassId equals lc.LicClassId
                                        join lcon in _dlhdevdbContxt.LicenceConditions on ld.LicCond equals lcon.LicCond into gj
                                        from item in gj.DefaultIfEmpty()
                                        join con in _dlhdevdbContxt.Conditions on item.CondId equals con.CondId into rg
                                        from res in rg.DefaultIfEmpty()
                                        where c.Mvid == req.Mvid
                                        select new
                                        {
                                            Mvid = c.Mvid,
                                            LicNumber = l.LicNumber,
                                            Name = string.Join(",", string.IsNullOrEmpty(c.MiddleName)? new string[] {c.LastName,c.FirstName }:new string[] { c.LastName, c.FirstName, c.MiddleName }),
                                            Dob = ((DateTime)c.Dob).ToString("yyyy/MM/dd"),
                                            IssueDate = ((DateTime)ld.IssueDate).ToString("yyyy/MM/dd"),
                                            ExpiryDate = ((DateTime)ld.ExpiryDate).ToString("yyyy/MM/dd"),
                                            ServiceType = ld.ServiceType,
                                            LicClass = $"{lc.LicClass} - {lc.Description}",
                                            GDL = ld.Gdlstatus == "Y" ? "Yes" : "No",
                                            GDLExitDate = ((DateTime)ld.GdlexitDate).ToString("yyyy/MM/dd"),
                                            Condition = string.IsNullOrEmpty(res.Condition1) ? string.Empty : $"{res.Condition1} - {res.Description}",
                                        }
                                     );

                var clientLicenseResult = (from r in clientLicenseQry.ToList()
                                           group r by new { r.Mvid, r.LicNumber, r.Name, r.Dob, r.IssueDate, r.ExpiryDate, r.ServiceType, r.LicClass, r.GDL, r.GDLExitDate } into clientLicenseCondition
                                           select new
                                           {
                                               Mvid = clientLicenseCondition.FirstOrDefault().Mvid,
                                               LicNumber = clientLicenseCondition.FirstOrDefault().LicNumber,
                                               Name = clientLicenseCondition.FirstOrDefault().Name,
                                               Dob = clientLicenseCondition.FirstOrDefault().Dob,
                                               IssueDate = clientLicenseCondition.FirstOrDefault().IssueDate,
                                               ExpiryDate = clientLicenseCondition.FirstOrDefault().ExpiryDate,
                                               ServiceType = clientLicenseCondition.FirstOrDefault().ServiceType,
                                               LicClass = clientLicenseCondition.FirstOrDefault().LicClass,
                                               GDL = clientLicenseCondition.FirstOrDefault().GDL,
                                               GDLExitDate = clientLicenseCondition.FirstOrDefault().GDLExitDate,
                                               Conditions = (clientLicenseCondition.Select(x => x.Condition).Aggregate((a, b) => (a + Environment.NewLine + b)))
                                           }).FirstOrDefault();
                if (clientLicenseResult != null)
                {

                    var dlhistoryQry = (from h in _dlhdevdbContxt.DlhistoryInfos
                                        join lc in _dlhdevdbContxt.LicenceClasses on h.LicClassId equals lc.LicClassId
                                        where h.Mvid == req.Mvid
                                        orderby h.IssueDate descending
                                        select new DlhHistoryDisplay
                                        {
                                            IssueDate = ((DateTime)h.IssueDate).ToString("yyyy/MM/dd"),
                                            ServiceType = h.ServiceType,
                                            LicClass = $"{lc.LicClass} - {lc.Description}",
                                        }
                                        );
                    var dlhistoryResult = dlhistoryQry.ToList();

                    // var dlhData = DLHDataStorage.GetAllDlhistory().FirstOrDefault(x => x.MVID == req.Mvid.ToString());

                    var dlhData = new DlhistoryModel
                    {
                        MVID = clientLicenseResult.Mvid.ToMVIDFormat(),
                        FullName = clientLicenseResult.Name,
                        LicenseNumber = clientLicenseResult.LicNumber,
                        Dob = clientLicenseResult.Dob,
                        DateOfIssue = clientLicenseResult.IssueDate,
                        DateOfExpire = clientLicenseResult.ExpiryDate,
                        ServiceType = clientLicenseResult.ServiceType,
                        LicenseClass = clientLicenseResult.LicClass,
                        GDl = clientLicenseResult.GDL,
                        GDlExitDate = clientLicenseResult.GDLExitDate,
                        Conditions = clientLicenseResult.Conditions,
                        historyInfo = dlhistoryResult,
                    };

                    if (dlhData != null)
                    {
                        //var renderer = new HtmlToPdf();
                        //var pdf = await renderer.RenderHtmlAsPdfAsync(TemplateGenerator.GetHTMLString(dlhData));

                        //response.DlhFile = new DlhFile
                        //{
                        //    FileContent = pdf.BinaryData,
                        //    ContentType = "application/pdf",
                        //    FileName = $"sampledlh{req.Mvid}.pdf"
                        //};

                        response.DlhistoryModel = dlhData;
                        response.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();

            }
            return response;
        }
    }
}