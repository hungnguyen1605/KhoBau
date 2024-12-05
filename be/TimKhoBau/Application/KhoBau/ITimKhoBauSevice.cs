using ContractApi.HttpConfig;
using TimKhoBau.Application.KhoBau.Dtos;

namespace TimKhoBau.Application.KhoBau
{
    public interface ITimKhoBauSevice
    {
        Task<ServiceResponse> KhoBau(TimKhoBauRequest request);
        Task<ServiceResponse> GetAllData();
        Task<ServiceResponse> DeletedAsync(Guid id);
     }
}
