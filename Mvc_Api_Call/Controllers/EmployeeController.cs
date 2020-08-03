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
            hc.BaseAddress = new Uri("http://localhost:56605/");
            var consumeapi = hc.GetAsync("ListEmployee");
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

        public ActionResult AddEdit(int? id)
        {
            EmpModelCont empModel = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:56605/");

            var consumeApi = hc.GetAsync("GetEmployee?id=" + id.ToString());
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<EmpModelCont>();
                displayData.Wait();
                empModel = displayData.Result;
            }
            return View(empModel);
        }
        [HttpPost]
        public ActionResult AddEdit(EmpModelCont empModel)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:56605/");

            var insertRecord = hc.PostAsJsonAsync<EmpModelCont>("AddEditEmp", empModel);
            insertRecord.Wait();

            var saveData = insertRecord.Result;
            if(saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:56605/");
                        
            var deleteRecord = hc.DeleteAsync("Delete/" + id.ToString());
            deleteRecord.Wait();

            var displayData = deleteRecord.Result;
            if (displayData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}