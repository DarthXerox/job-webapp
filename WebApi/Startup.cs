using System.Text.Json;
using Autofac;
using Business;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace WebApi
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
            services.AddControllersWithViews();
            
            //services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            //services.AddMvcCore().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(/*c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            }*/);
            services.AddDbContext<JobDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("AzureDB")));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new AutofacBusinessLayerConfig());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

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
