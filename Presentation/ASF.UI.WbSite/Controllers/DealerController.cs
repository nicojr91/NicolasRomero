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
            return View();
        }

        //
        // GET: /Dealer/Dealer/Create
        public ActionResult Create()
        {
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
            return View();
        }

        //
        // POST: /Dealer/Dealer/Edit/5
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
        // GET: /Dealer/Dealer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
    }
}
