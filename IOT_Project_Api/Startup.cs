using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Project_DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Dapper;
using IOT_Project_BLL.ShopCar;
using IOT_Project_Model;
using IOT_Project_BLL.Login;

namespace IOT_Project_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IDataAccess<Productinfo>, DataAccess<Productinfo>>();
            services.AddScoped<IGoodsList, GoodsList>();

            services.AddScoped<ILogin,LoginBll>();

            // 配置跨域处理，允许所有来源
            services.AddCors(options =>
            {
                // Policy 名稱 CorsPolicy 是自訂的，可以自己改
                options.AddPolicy("getd", policy =>
                {
                    // 設定允許跨域的來源，有多個的話可以用 `,` 隔開
                    policy.WithOrigins("http://localhost:50812", "http://localhost:50784")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("getd");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
