using ASF.Entities;
using ASF.UI.Process;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Controllers
{
    public class CartController : Controller
    {
        private ProductProcess pprocess = new ProductProcess();

        //
        // GET: /Cart/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cart/Create
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
        // GET: /Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cart/Edit/5
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
        // GET: /Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Cart/Delete/5
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

        //[HttpPost]
        public PartialViewResult GetCart(string json)
        {
            dynamic result2 = JsonConvert.DeserializeObject(HttpUtility.HtmlDecode(Request.QueryString[0]));
            var products = new Dictionary<int, string>();
            var listItems = new List<CartItem>();

            foreach (var i in result2.items)
            {
                var item = new CartItem();
                var product = pprocess.Find((int)i.product);
                products.Add(product.Id, product.Title);

                item.Price = product.Price;
                item.ProductId = product.Id;
                item.Quantity = i.quantity;
                listItems.Add(item);
            }

            ViewBag.products = products;
            ViewBag.listItems = listItems;

            return PartialView("_rowTableCart");//Json( new { foo="bar", baz="Blech" }, JsonRequestBehavior.AllowGet)
        }

    }
}
