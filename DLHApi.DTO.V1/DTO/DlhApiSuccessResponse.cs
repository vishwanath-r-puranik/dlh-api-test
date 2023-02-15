using DLHApi.Common.Models;

namespace DLHApi.DTO.V1.DTO
{
    public class DlhApiSuccessResponse<T> : IDlhApiResponse
    {
        public T? Data { get; set; }

        public DlhApiSuccessResponse()
        {
        }

        public DlhApiSuccessResponse(T? data)
        {
            Data = data;
        }

    }
}
