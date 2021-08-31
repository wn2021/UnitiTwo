using eynaOA.Helper;
using System.Collections.Generic;
using System.Web.Http;

namespace eynaOA.Controllers
{
    public class BaseController : ApiController
    {

        public IDictionary<string, string> GetParameters()
        {
            IDictionary<string, string> queryParameters = Request.GetAllQueryParameters();
            return queryParameters;
        }       
    }
}
