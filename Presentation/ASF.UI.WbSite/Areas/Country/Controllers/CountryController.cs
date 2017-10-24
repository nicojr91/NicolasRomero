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
            return View();
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
        // GET: /Country/Country/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Country/Country/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
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
    }
}
