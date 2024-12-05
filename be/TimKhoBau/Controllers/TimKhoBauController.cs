using ContractApi.HttpConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimKhoBau.Application.KhoBau;
using TimKhoBau.Application.KhoBau.Dtos;

namespace TimKhoBau.Controllers
{
    [Route("kho-bau")]
    [ApiController]
    public class TimKhoBauController : ControllerBase
    {
        private readonly ITimKhoBauSevice _service;
        public TimKhoBauController(ITimKhoBauSevice service)
        {
            _service = service;
        }

        [HttpPost("tim-kho-bau")]
        public async Task<ServiceResponse>TimKhoBau([FromBody] TimKhoBauRequest request)
        {
            try
            {
                return await _service.KhoBau(request);

            }catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("get-data")]
        public async Task<ServiceResponse> GetData()
        {
            return await _service.GetAllData();
        }
        [HttpDelete("delete-data/{id}")]
        public async Task<ServiceResponse>DeletedAsync(Guid id)
        {
            return await _service.DeletedAsync(id);
        }
    }
}
