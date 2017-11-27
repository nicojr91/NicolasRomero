using ASF.Entities;
using ASF.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.UI.Process
{
    public class OrderProcess : ProcessComponent
    {
        public Order Add(Order order)
        {
            var response = HttpPost<Order>("rest/Order/Add", order, MediaType.Json);

            return response;
        }

        public Order Remove(Order order)
        {
            var response = HttpGet<FindResponse<Order>>("rest/Order/Remove/" + order.Id, new Dictionary<string, object>(), MediaType.Json);

            return null;
        }

        public Order Edit(Order order)
        {
            var response = HttpPost<Order>("rest/Order/Edit", order, MediaType.Json);

            return response;
        }

        public Order Find(int id)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            var response = HttpGet<FindResponse<Order>>("rest/Order/Find", parameters, MediaType.Json);
            //return response.Result;
            return response.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Order> SelectList()
        {
            var response = HttpGet<AllResponse<Order>>("rest/Order/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }
    }
}
