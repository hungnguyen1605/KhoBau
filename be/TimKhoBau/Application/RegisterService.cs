using TimKhoBau.Application.KhoBau;

namespace TimKhoBau.Application
{
    public static class RegisterService
    {
        public static void RegisterAppService(this IServiceCollection services)
        {
            services.AddScoped<ITimKhoBauSevice, TimKhoBauSevice>();
        }

    }
}
