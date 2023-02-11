using DLHApi.DAL.Repo;
using DLHApi.DAL.RequestResponse;
using DLHApi.DAL.Data;
using DLHApi.DAL.Models;

namespace DLHApi.DAL.Services
{
    public class DlhService : IDlhService
    {
        private readonly IDlhRepo _dlhrepo;
        
        public DlhService(IDlhRepo dlhrepo)
        {
            _dlhrepo = dlhrepo;
        }

        public async Task<DlhResponse> GelDlhByMvid(DlhRequest req)
        {
           
            var resp = await _dlhrepo.GelDlhByMvid(req);

            return resp;
        }

    }

}
