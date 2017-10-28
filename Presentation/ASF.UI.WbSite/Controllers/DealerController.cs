using ASF.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Areas.Dealer.Controllers
{
    public class DealerController : Controller
    {
        private static CategoryProcess categoryProcess = new CategoryProcess();
        private static CountryProcess countryProcess = new CountryProcess();
        private static DealerProcess dealerProcess = new DealerProcess();

        //
        // GET: /Dealer/Dealer/
        public ActionResult Index()
        {
            var resp = dealerProcess.SelectList();

            return View(resp);
        }

        //
        // GET: /Dealer/Dealer/Details/5
        public ActionResult Details(int id)
        {
            var resp = categoryProcess.Find(id);

            return View(resp);
        }

        //
        // GET: /Dealer/Dealer/Create
        public ActionResult Create()
        {
            var listCategories = categoryProcess.SelectList();
            var listCountry = countryProcess.SelectList();

            ViewBag.categories = listCategories;
            ViewBag.countries = listCountry;

            return View();
        }

        //
        // POST: /Dealer/Dealer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dealer/Dealer/Edit/5
        public ActionResult Edit(int id)
        {
            var category = categoryProcess.Find(id);

            return View();
        }

        //
        // POST: /Dealer/Dealer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var category = categoryProcess.Find(id);
                category.Name = collection["Name"];
                category.ChangedOn = DateTime.Now;

                categoryProcess.Edit(category);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //return View();
            }
        }

        //
        // GET: /Dealer/Dealer/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var category = new ASF.Entities.Category();
                category.Id = id;

                categoryProcess.Remove(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



            return RedirectToAction("Index");
        }

        //
        // POST: /Dealer/Dealer/Delete/5
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
            var resp = categoryProcess.SelectList();

            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}
