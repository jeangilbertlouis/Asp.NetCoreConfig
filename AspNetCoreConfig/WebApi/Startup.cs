using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //This is only needed if we want to bind to another json file. Included to be explicity
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = configBuilder.Build();
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Use the IOptions framework. This will only be evaluated when constructor is called
            services.AddOptions();
            services.Configure<MySettings>(Configuration);

            //Bind our own poco object. COnstructor expects the POCO object
            var config = new MySettings();
            Configuration.Bind(config); //Can also bind to a key in the appsettings
            services.AddSingleton<MySettings>(config);

            services.AddMvc();

            //Add Configuration to IOC container. Only needed if IConfiguration is defined in the constructor
            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
