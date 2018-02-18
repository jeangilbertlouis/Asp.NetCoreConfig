using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IOptions<MySettings> _options;
        private readonly MySettings _settings;

        public ValuesController(IConfiguration config, IOptions<MySettings> options, MySettings settings)
        {
            _config = config;
            _options = options;
            _settings = settings;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return _options.Value.Status;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            if (id == 1) return _settings.Status;

            return _config.GetValue<string>("Status");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
