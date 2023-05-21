
using api.DAL.Implementations;
using api.DAL.Interfaces;
using api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace api
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
            services.TryAddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, HttpContextAccessor>(); 

            services.AddScoped<IEmail, Mail>();
            services.AddScoped<ISMS, SMS>();

            services.Configure<SMSSettings>(Configuration.GetSection("SMSSettings"));
            services.Configure<ComSettings>(Configuration.GetSection("ComSettings"));
                  
          
            services.AddCors(options =>
                       {
                           options.AddPolicy("CorsPolicy",
                           builder =>
                           {
                               builder.WithOrigins("http://localhost:4200")
                                                   .AllowAnyHeader()
                                                   .AllowCredentials()
                                                   .AllowAnyMethod();
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

            app.UseCors("CorsPolicy");
            app.UseRouting();
        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               
            });
        }
    }
}
