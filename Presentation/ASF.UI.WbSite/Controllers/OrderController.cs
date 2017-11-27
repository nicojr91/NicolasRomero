using ASF.Entities;
using ASF.UI.Process;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Controllers
{
    public class OrderController : Controller
    {
        OrderProcess op = new OrderProcess();

        //
        // GET: /Order/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Order/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                dynamic result2 = JsonConvert.DeserializeObject(HttpUtility.HtmlDecode(Request.Params[0]));
                var products = new Dictionary<int, string>();
                var listItems = new List<OrderDetail>();
                // TODO: Add insert logic here
                var order = new Order();
                order.ClientId = result2.clientId;
                order.OrderDate = DateTime.Now;
                foreach (var i in result2.lines)
                {
                    var item = new OrderDetail();

                    item.ProductId = (int)i.product;
                    item.Quantity = (int)i.quantity;
                    listItems.Add(item);
                }
                order.ItemCount = listItems.Count;
                order.Details = listItems;
                op.Add(order);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Order/Edit/5
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
        // GET: /Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Order/Delete/5
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

    public class PersonViewModel
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
    }
}
