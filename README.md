# Asp.NetCoreConfig
Configuration approaches for ASP.Net core

## Sample appsettings.json which represents settings

```
{
  "Status": "FromConfig" 
}
```
Settings stored in appsettings.json will automatically be binded to the Configuration object.

## POCO object which reflects the JSON config


 ```csharp
    public class MySettings
    {
        public string Status { get; set; }
    }
```

## Approach 1 - Inject IConfiguration
### Setting it up
 ```csharp
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Add Configuration to IOC container.
            services.AddSingleton<IConfiguration>(Configuration);
        }
```
### Consuming it

 ```csharp

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IConfiguration _config;

        public ValuesController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return _config.GetValue<string>("Status");
        }
    }

```

## Approach 2 - Inject IOptions
### Setting it up
 ```csharp
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<MySettings>(Configuration);
            services.AddMvc();
        }
```
### Consuming it

 ```csharp

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IOptions<MySettings> _options;

        public ValuesController(IOptions<MySettings> options)
        {
            _options = options;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return _options.Value.Status;
        }
    }

```

## Approach 3 - Inject POCO Object (My preference)
### Setting it up
 ```csharp
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var config = new MySettings();
            Configuration.Bind(config);
            services.AddSingleton<MySettings>(config);
        }
```
### Consuming it

 ```csharp

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly MySettings _settings;

        public ValuesController(MySettings settings)
        {
            _settings = settings;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return return _settings.Status;
        }
    }

```
