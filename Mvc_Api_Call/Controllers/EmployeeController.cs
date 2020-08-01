using Mvc_Api_Call.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc_Api_Call.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<EmpModelCont> empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:50691/api/Employee/GetEmployee");
            var consumeapi = hc.GetAsync("GetEmployee");
            consumeapi.Wait();
            var readData = consumeapi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<EmpModelCont>>();
                displayData.Wait();
                empobj = displayData.Result;
            }
            return View(empobj);
        }
    }
}