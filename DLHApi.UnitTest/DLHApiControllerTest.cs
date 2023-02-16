using AutoMapper;
using DLHApi.DAL.Repo;
using DLHApi.DAL.Services;
using DLHApi.DTO.V1.Mapper;
using DLHApi.DTO.V1.Profiles;
using DLHApi.EIS.Services.PDFMerge;
using DLHApi.EIS.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Org.OpenAPITools.Controllers;
using DLHApi.Common.Logger.Contracts;

namespace DLHApi.UnitTest
{
    public class DLHApiControllerTest: IClassFixture<DlhDbFixture>
    {
        private readonly DLHApiController _dLHApiController;
        private readonly ILoggerManager _logger;
        private readonly DlhistoryModelMapper _dlhmapper;

        private DlhDbFixture Fixture { get; }
        private DlhRepo _dlhrepo { get; set; }
        private AuditRepo _auditRepo { get; set; }
        private DlhService _dlhservice { get; set; }
        private AuditService _auditservice { get; set; }
        private PdfMergeService _pdfservice { get; set; } 
        private IMapper _mapper { get; set; }
        private ITokenHandler _tokenHandler { get; set; }

        public DLHApiControllerTest(DlhDbFixture  fixture)
        {
            Fixture = fixture;
         

            _logger = Mock.Of<ILoggerManager>();

            var dlhcontext = Fixture.CreateDlhContext();
            _dlhrepo = new DlhRepo(dlhcontext, _logger);
            _dlhservice = new DlhService(_dlhrepo);

            _tokenHandler = new TokenHandler();
           
          

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<DlhResponseProfile>());
            _mapper= mapperConfig.CreateMapper();

            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient<PdfMergeService>();

            IHttpClientFactory factory = services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
            _pdfservice = new PdfMergeService(factory, _tokenHandler,_logger);

            services.AddHttpClient<AuditRepo>();
            factory = services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();

            _auditRepo = new AuditRepo(factory,_tokenHandler,_logger);
            _auditservice = new AuditService(_auditRepo);

           

            _dlhmapper = new DlhistoryModelMapper(_dlhservice, _mapper, _pdfservice,_auditservice,_logger);

            _dLHApiController = new DLHApiController(_dlhmapper, _logger);
        }

        [Fact]
        public async void Get_ReturnsNotFound()
        {
           
            var res = await _dLHApiController.DLHDocumentMvidGet(123456);

            Assert.IsType<NotFoundResult>(res);
        }

        [Fact]
        public async void Get_ReturnsFile()
        {

            var res = await _dLHApiController.DLHDocumentMergeMvidGet(123456789);

            Assert.IsType<FileContentResult>(res);

            var val = (FileContentResult)res;
            Assert.NotNull(val.FileContents);
            Assert.Equal("application/pdf", val.ContentType);
        }

        [Fact]
        public async void Get_ReturnsData()
        {

            var res = await _dLHApiController.DLHDocumentMvidGet(123456789);

            Assert.IsType<OkObjectResult>(res);

            var val = (OkObjectResult)res;
            Assert.NotNull(val.Value);
        }

        [Fact]
        public async void Get_ReturnsNot_Found()
        {

            var res = await _dLHApiController.DLHDocumentMvidGet(123456);

            Assert.IsType<NotFoundResult>(res);

        }
    }
}