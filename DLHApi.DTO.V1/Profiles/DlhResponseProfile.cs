using AutoMapper;

namespace DLHApi.DTO.V1.Profiles
{
    public class DlhResponseProfile : Profile
    {
        public DlhResponseProfile()
        {
            CreateMap<DLHApi.DAL.Models.DlhHistoryDisplay, DLHApi.DTO.V1.DTO.DlhistoryDisplayInfo>().ReverseMap();
            CreateMap<DLHApi.DAL.Models.DlhistoryModel, DLHApi.DTO.V1.DTO.DlhistoryModel>().ReverseMap();

        }
    }
}
