using ASF.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Areas.Country.Controllers
{
    public class CountryController : Controller
    {
        private static CountryProcess countryProcess = new CountryProcess();

        //
        // GET: /Country/Country/
        public ActionResult Index()
        {
            var resp = countryProcess.SelectList();

            return View(resp);
        }

        //
        // GET: /Country/Country/Details/5
        public ActionResult Details(int id)
        {
            var resp = countryProcess.Find(id);

            return View(resp);
        }

        //
        // GET: /Country/Country/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Country/Country/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var name = collection["Name"];

            var newCountry = new ASF.Entities.Country();
            newCountry.Name = name;
            newCountry.ChangedOn = DateTime.Now;
            newCountry.CreatedOn = DateTime.Now;
            try
            {
                newCountry = countryProcess.Add(newCountry);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Country/Country/Edit/5
        public ActionResult Edit(int id)
        {
            var category = countryProcess.Find(id);

            return View(category);
        }

        //
        // POST: /Country/Country/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var category = countryProcess.Find(id);
                category.Name = collection["Name"];
                category.ChangedOn = DateTime.Now;

                countryProcess.Edit(category);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Country/Country/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var category = new ASF.Entities.Country();
                category.Id = id;

                countryProcess.Remove(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



            return RedirectToAction("Index");
        }

        //
        // POST: /Country/Country/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetCountries()
        {
            var resp = countryProcess.SelectList();

            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}
