using ej2_schedule_odata_services.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

namespace ej2_schedule_odata_services
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
            services.AddOData();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.AddDbContext<ScheduleContext>(options => options.UseInMemoryDatabase("ScheduleData"));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders", builder =>
                {
                    builder.WithOrigins("*");
                    builder.WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept", "Prefer", "Authorization", "MaxDataServiceVersion", "DataServiceVersion");
                    builder.WithMethods("GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS");
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowAllHeaders");

            app.UseMvc(options =>
            {
                options.Select().Filter().OrderBy().Count();
                options.MapODataServiceRoute("odata", "odata", GetEdmModel());
                options.EnableDependencyInjection();
            });

            app.UseOData(GetEdmModel());

            app.UseODataBatching();
        }

        private IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder ODataBuilder = new ODataConventionModelBuilder();
            ODataBuilder.EntitySet<ScheduleData>("Schedule");
            return ODataBuilder.GetEdmModel();
        }
    }
}
