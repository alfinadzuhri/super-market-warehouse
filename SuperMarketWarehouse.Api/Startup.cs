using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SuperMarketWarehouse.Core.Interfaces;
using SuperMarketWarehouse.Infrastructure.Data;
using SuperMarketWarehouse.Infrastructure.Repositories;
using SuperMarketWarehouse.Services;

namespace SuperMarketWarehouse.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<SuperMarketWarehouseContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IGudangRepository, GudangRepository>();
            services.AddScoped<IBarangRepository, BarangRepository>();
            services.AddScoped<IGudangService, GudangService>();
            services.AddScoped<IBarangService, BarangService>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
