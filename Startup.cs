using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LukaszKijak.Service;
using LukaszKijak.Service.SortList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LukaszKijak
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IShowFolder, ShowFolder>();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();
        }
    }
}
