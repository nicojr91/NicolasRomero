using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Areas.Category.Controllers
{
    public class CategoryController : Controller
    {
        private static ASF.UI.Process.CategoryProcess categoryProcess = new Process.CategoryProcess();
        
        //
        // GET: /Category/Category/
        public ActionResult Index()
        {
            var resp = categoryProcess.SelectList();

            return View();
        }

        //
        // GET: /Category/Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Category/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Category/Create
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
        // GET: /Category/Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Category/Category/Edit/5
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
        // GET: /Category/Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Category/Category/Delete/5
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
