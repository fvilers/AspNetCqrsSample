using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BookStore.Core.Http.Results
{
    public class AcceptedResult : StatusCodeResult
    {
        public AcceptedResult(HttpRequestMessage request)
            : base(HttpStatusCode.Accepted, request)
        {
        }

        public AcceptedResult(ApiController controller)
            : base(HttpStatusCode.Accepted, controller)
        {
        }
    }
}
