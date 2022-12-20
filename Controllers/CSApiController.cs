using ConsolidationSystem.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsolidationSystem.Controllers
{
    public class CSApiController : Controller
    {
        // GET: CSApi
        //Hosted web API REST Service base url
        string Baseurl = "http://universities.hipolabs.com";

        public async Task<ActionResult> Index()
        {
            List<ProcessResult> EmpInfo = new List<ProcessResult>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("/search?country=United+States");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<ProcessResult>>(EmpResponse);
                }
                //returning the employee list to view
                return View(EmpInfo);
            }
        }
    }
}