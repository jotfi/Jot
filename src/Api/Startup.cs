using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jotfi.Jot;
using jotfi.Jot.Database.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace jotfi.Jot.Api
{
    public class Startup
    {
        private readonly Core.Application Application;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Application = new Core.Application(new Core.Settings.AppSettings());
            if (!Application.ViewModels.System.CheckDatabase(out string error))
            {
                throw new ApplicationException(error);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Application.ViewModels.System);
            services.AddSingleton(Application.ViewModels.User);
            services.AddSingleton(Application.ViewModels.Login);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
