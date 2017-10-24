using ASF.Entities;
using ASF.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.UI.Process
{
    public class ProductProcess : ProcessComponent
    {
        public Product Add(Product product)
        {
            var response = HttpPost<Product>("rest/Product/Add", product, MediaType.Json);

            return response;
        }

        public Product Remove(Product product)
        {
            var response = HttpGet<FindResponse<Product>>("rest/Product/Remove/" + product.Id, new Dictionary<string, object>(), MediaType.Json);

            return null;
        }

        public Product Edit(Product product)
        {
            var response = HttpPost<Product>("rest/Product/Edit", product, MediaType.Json);

            return response;
        }

        public Product Find(int id)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            var response = HttpGet<FindResponse<Product>>("rest/Product/Find", parameters, MediaType.Json);
            //return response.Result;
            return response.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Product> SelectList()
        {
            var response = HttpGet<AllResponse<Product>>("rest/Product/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }
    }
}
