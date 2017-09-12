using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using ASF.Entities;
using ASF.Services.Contracts;
using ASF.UI.Process;

namespace ASF.UI.Process
{
    public class CategoryProcess : ProcessComponent
    {
        public Category Add(Category category)
        {
            var response = HttpPost<Category>("rest/Category/Add", category, MediaType.Json);
            
            return response;
        }

        public Category Remove(Category category)
        {
            var response = HttpGet<FindResponse>("rest/Category/Remove/"+category.Id, new Dictionary<string, object>(), MediaType.Json);
            
            return null;
        }

        public Category Edit(Category category)
        {
            var response = HttpPost<Category>("rest/Category/Edit", category, MediaType.Json);
            
            return response;
        }

        public Category Find(int id)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            var response = HttpGet<FindResponse>("rest/Category/Find", parameters, MediaType.Json);
            //return response.Result;
            return response.Result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Category> SelectList()
        {
            var response = HttpGet<AllResponse>("rest/Category/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        
    }
}