using DLHApi.Common.Logger.Contracts;
using DLHApi.DAL.Repo;
using DLHApi.DAL.RequestResponse;
using DLHApi.DAL.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.UnitTest
{
    public class DbServiceTest : IClassFixture<DlhDbFixture>
    {
        public DlhDbFixture Fixture { get; }

        public IDlhRepo _dlhrepo { get; set; }

        private ILoggerManager _logger { get; }

        public DbServiceTest(DlhDbFixture fixture, IDlhRepo dlhrepo)
        {
            Fixture = fixture;
            var dlhcontext = Fixture.CreateDlhContext();
            _logger = Mock.Of<ILoggerManager>();
            _dlhrepo = new DlhRepo(dlhcontext,_logger);

   
        }


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

      
    }
}
