using ASF.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Areas.Product.Controllers
{
    public class ProductController : Controller
    {
        private static ProductProcess productProcess = new ProductProcess();
        private static DealerProcess dealerProcess = new DealerProcess();

        //
        // GET: /Product/Product/
        public ActionResult Index()
        {
            var resp = productProcess.SelectList();

            return View(resp);
        }

        //
        // GET: /Product/Product/Details/5
        public ActionResult Details(int id)
        {
            var resp = productProcess.Find(id);

            return View(resp);
        }

        //
        // GET: /Product/Product/Create
        public ActionResult Create()
        {
            @ViewBag.dealers = dealerProcess.SelectList();

            return View();
        }

        //
        // POST: /Product/Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var file = Request.Files[0];

                string imgName = System.IO.Path.GetFileName(file.FileName);
                string fisicalPath = Server.MapPath("~/images/" + imgName);
                file.SaveAs(fisicalPath);

                var product = new ASF.Entities.Product();
                product.Title = collection["title"];
                product.Description = collection["description"];
                product.DealerId = Int32.Parse(collection["dealerId"]);
                product.Price = double.Parse(collection["price"]);
                product.Image = imgName;
                product.CreatedOn = DateTime.Now;
                product.ChangedOn = DateTime.Now;

                productProcess.Add(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Product/Edit/5
        public ActionResult Edit(int id)
        {
            var resp = productProcess.Find(id);
            ViewBag.product = resp;
            ViewBag.dealer = dealerProcess.Find(resp.DealerId);
            ViewBag.dealers = dealerProcess.SelectList();

            return View(resp);
        }

        //
        // POST: /Product/Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var prod = productProcess.Find(id);
                var file = Request.Files[0];
                if (file.FileName != "")
                {
                    string imgName = System.IO.Path.GetFileName(file.FileName);
                    string fisicalPath = Server.MapPath("~/images/" + imgName);
                    file.SaveAs(fisicalPath);
                    prod.Image = imgName;
                }
                prod.Title = collection["Title"];
                prod.Description = collection["Description"];
                prod.DealerId = Int32.Parse(collection["dealerId"]);
                prod.Price = Double.Parse(collection["Price"]);
                prod.ChangedOn = DateTime.Now;

                productProcess.Edit(prod);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Product/Product/Delete/5
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
