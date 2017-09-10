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

            return View(resp);
        }

        //
        // GET: /Category/Category/Details/5
        public ActionResult Details(int id)
        {
            var resp = categoryProcess.Find(id);

            return View(resp);
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
            // TODO: Add insert logic here
            var name = collection["Name"];

            var newCateogry = new ASF.Entities.Category();
            newCateogry.Name = name;
            newCateogry.ChangedOn = DateTime.Now;
            newCateogry.CreatedOn = DateTime.Now;
            try
            {
                newCateogry = categoryProcess.Add(newCateogry);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
                //return View();
            }
        }

        //
        // GET: /Category/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var category = categoryProcess.Find(id);

            return View(category);
        }

        //
        // POST: /Category/Category/Edit/5
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
            catch(Exception e)
            {
                throw new Exception(e.Message);
                //return View();
            }
        }

        //
        // GET: /Category/Category/Delete/5
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
