using BookStore.Core.Http.Results;
using System.Web.Http;

namespace BookStore.Core.Http
{
    public class ExtendedApiController : ApiController
    {
        protected IHttpActionResult Accepted()
        {
            return new AcceptedResult(this);
        }
    }
}
