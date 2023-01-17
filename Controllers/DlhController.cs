using DLHAPI.Data;
using DLHAPI.Models;
using DLHAPI.Utils;
using IronPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DLHAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DlhController : ControllerBase
    {
        private DLHDbContext _dlhDbContext;
        private DlhdevAuditContext _auditContext;
        private string DlhfileLoc = "/Users/navpreetkaur/Downloads/pdfs/dlhfilereport.pdf";

        public DlhController(DLHDbContext dlhDbContext, DlhdevAuditContext auditContext)
        {
            _dlhDbContext = dlhDbContext;
            _auditContext = auditContext;
        }


        [HttpGet("{mvid}", Name = nameof(GetDlhData))]
        public async Task<IActionResult> GetDlhData([FromRoute] string mvid)
        {
            var reqAudit = new DlhRequest
            {
                Mvid = mvid,
                RequestDate = DateTime.Now,
                ReqStatus = "Success"
            };
            _auditContext.DlhRequests.Add(reqAudit);
           await _auditContext.SaveChangesAsync();

            var dlhData = await  _dlhDbContext.DlhModel.FirstOrDefaultAsync(x => x.MVID == mvid);

            if (dlhData != null )
            {
                //  return File(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(dlhData)), "application/json");
                if (dlhData.PDF != null)
                { 
                    return File(dlhData.PDF, "application/pdf", "sampletest.pdf"); 
                }
                else return Ok(dlhData);

            }
            else return NotFound(); 

        }

        [HttpGet("file/{mvid}", Name = nameof(GetDlhFile))]
        public async Task<IActionResult> GetDlhFile([FromRoute] string mvid)
        {

            //var dlhData = await _dlhDbContext.DlhModel.ToArrayAsync();
            var dlhData = DLHDataStorage.GetAllDlhistory().FirstOrDefault(x=> x.MVID == mvid);

            //if (dlhData != null)
            //{

                var renderer = new HtmlToPdf();
                renderer.RenderHtmlAsPdf(TemplateGenerator.GetHTMLString(dlhData)).SaveAs(DlhfileLoc);

                return Ok("Successfully created PDF document.");

            //}
            //else return NotFound();

        }
    }
}
