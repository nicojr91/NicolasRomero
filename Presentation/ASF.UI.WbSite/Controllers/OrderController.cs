using ASF.Entities;
using ASF.UI.Process;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Controllers
{
    public class OrderController : Controller
    {
        OrderProcess op = new OrderProcess();
        ProductProcess pp = new ProductProcess();

        public enum State
        {
            Reviewed = 1,
            Suspended = 2,
            Closed = 3,
            Cancelled = 4,
            Approved = 5,
        }

        //
        // GET: /Order/
        public ActionResult Index()
        {
            var resp = op.SelectList();

            return View(resp);
        }

        //
        // GET: /Order/Details/5
        public ActionResult Details(int id)
        {
            var resp = op.Find(id);
            ViewBag.order = resp;
            var states = Enum.GetValues(typeof(State)).Cast<State>().ToList();
            Dictionary<int, string> mapIdNameStates = new Dictionary<int, string>();
            int i = 1;
            foreach (State state in states)
            {
                mapIdNameStates.Add(i, state.ToString());
                i++;
            }
            Dictionary<int, string> mapIdNameProducts = new Dictionary<int, string>();
            foreach (ASF.Entities.OrderDetail detail in resp.Details)
            {
                var product = pp.Find(detail.ProductId);
                mapIdNameProducts.Add(product.Id, product.Title);
            }
            ViewBag.states = mapIdNameStates;
            ViewBag.products = mapIdNameProducts;

            return View(resp);
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
        public JsonResult Create(FormCollection collection)
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
                order.State = (int)State.Reviewed;
                order = op.Add(order);

                return Json(new { status = "ok", url = "/order/thankyou" });
            }
            catch
            {
                return Json(new { status = "error" });
            }
        }

        //
        // GET: /Order/Edit/5
        public ActionResult Edit(int id)
        {
            var resp = op.Find(id);
            ViewBag.order = op.Find(id);
            ViewBag.state = Enum.GetValues(typeof(State)).Cast<State>().ToList();
            Dictionary<int, string> mapIdNameProducts = new Dictionary<int, string>();
            foreach(ASF.Entities.OrderDetail detail in resp.Details)
            {
                var product = pp.Find(detail.ProductId);
                mapIdNameProducts.Add(product.Id, product.Title);
            }

            ViewBag.products = mapIdNameProducts;

            return View(resp);
        }

        //
        // POST: /Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var state = collection["State"];
                var order = op.Find(id);
                order.State = int.Parse(state);
                order.ChangedOn = DateTime.Now;
                op.Edit(order);

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

        //
        // POST: /Order/Delete/5
        public ActionResult ThankYou()
        {
            try
            {
                return View("ThankYouPage");
            }
            catch
            {
                return View();
            }
        }

    }

}
