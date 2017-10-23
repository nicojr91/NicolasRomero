using ASF.Entities;
using ASF.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.UI.Process
{
    public class DealerProcess : ProcessComponent
    {
        public Dealer Add(Dealer category)
        {
            var response = HttpPost<Dealer>("rest/Dealer/Add", category, MediaType.Json);

            return response;
        }

        public Dealer Remove(Dealer category)
        {
            var response = HttpGet<FindResponse<Dealer>>("rest/Dealer/Remove/" + category.Id, new Dictionary<string, object>(), MediaType.Json);

            return null;
        }

        public Dealer Edit(Dealer category)
        {
            var response = HttpPost<Dealer>("rest/Dealer/Edit", category, MediaType.Json);

            return response;
        }

        public Dealer Find(int id)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            var response = HttpGet<FindResponse<Dealer>>("rest/Dealer/Find", parameters, MediaType.Json);
            //return response.Result;
            return response.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Dealer> SelectList()
        {
            var response = HttpGet<AllResponse<Dealer>>("rest/Dealer/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }
    }
}
