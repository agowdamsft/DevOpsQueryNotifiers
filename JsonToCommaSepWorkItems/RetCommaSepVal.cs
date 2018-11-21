using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace HelperFunctions
{
    public static class RetCommaSepVal
    {
        [FunctionName("RetCommaSepVal")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            if (name == null)
            {
                // Get request body
                WiqlworkItems workitemidobj = new WiqlworkItems();
                workitemidobj = await req.Content.ReadAsAsync<WiqlworkItems>();
                if(workitemidobj.WorkItems.Count()>0)
                {
                  for(int i=0; i < workitemidobj.WorkItems.Count();i++)
                    {
                        if (workitemidobj.WorkItems.Count() == 1 || i == workitemidobj.WorkItems.Count()-1)
                            name += workitemidobj.WorkItems[i].Id.ToString();
                        else
                            name += workitemidobj.WorkItems[i].Id.ToString() + ",";
                    }

                }

                //name = data?.name;
            }

            return name == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, name);
        }
    }
}
