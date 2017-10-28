using ASF.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Controllers
{
    public class CatalogController : Controller
    {
        private ProductProcess productProcess = new ProductProcess();

        //
        // GET: /Catalog/
        public ActionResult Index()
        {
            var products = productProcess.SelectList();

            return View(products);
        }

        //
        // GET: /Catalog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Catalog/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Catalog/Create
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
        // GET: /Catalog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Catalog/Edit/5
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
        // GET: /Catalog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Catalog/Delete/5
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
