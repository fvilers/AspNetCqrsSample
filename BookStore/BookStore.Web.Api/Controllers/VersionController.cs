using System;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;

namespace BookStore.Web.Api.Controllers
{
    [RoutePrefix("version")]
    public class VersionController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var assembly = Assembly.GetExecutingAssembly();

            if (assembly.Location == null)
            {
                throw new NotSupportedException();
            }

            var info = FileVersionInfo.GetVersionInfo(assembly.Location);

            return Ok(info.FileVersion);
        }
    }
}
