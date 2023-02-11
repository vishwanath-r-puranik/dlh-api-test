using DLHApi.DAL.Repo;
using DLHApi.DAL.RequestResponse;
using DLHApi.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.UnitTest
{
    public class DbServiceTest : IClassFixture<DlhDbFixture>
    {
        public DbServiceTest(DlhDbFixture fixture)
        {
            Fixture = fixture;
            var dlhcontext = Fixture.CreateDlhContext();
            _dlhrepo = new DlhRepo(dlhcontext);

            var dlhAuditContext = Fixture.CreateDlhAuditContext();
            _auditRepo = new AuditRepo(dlhAuditContext);


        }


        public DlhDbFixture Fixture { get; }

        public DlhRepo _dlhrepo { get;set; }
        public AuditRepo _auditRepo { get; set; }


        [Fact]
        public async void Fetch_NonExisting_MVID_return_NotFound()
        {
            var service = new DlhService(_dlhrepo);
            var resp = await service.GelDlhByMvid(new DlhRequest() { Mvid = 123456 });
            Assert.False(resp.Success);
            Assert.Equal("Mvid not found!", resp.Message);

        }

        [Fact]
        public async void Fetch_Existing_MVID_return_True()
        {
            var service = new DlhService(_dlhrepo);
            var resp = await service.GelDlhByMvid(new DlhRequest() { Mvid = 123456789 });
            Assert.True(resp.Success);
            Assert.True(string.IsNullOrEmpty(resp.Message));
            Assert.NotNull(resp.DlhistoryModel);
            Assert.Equal("1234-56789", resp.DlhistoryModel?.MVID);

        }

        [Fact]
        public  void Add_Audit_Return_True()
        {
            try
            {
                var service = new AuditService(_auditRepo);
                service.AddRequestAudit("123456789");
                return;
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
  
        }
    }
}
