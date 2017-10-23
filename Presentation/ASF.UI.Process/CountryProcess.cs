using ASF.Entities;
using ASF.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.UI.Process
{
    public class CountryProcess : ProcessComponent
    {
        public Country Add(Country category)
        {
            var response = HttpPost<Country>("rest/Country/Add", category, MediaType.Json);

            return response;
        }

        public Country Remove(Country category)
        {
            var response = HttpGet<FindResponse<Country>>("rest/Country/Remove/" + category.Id, new Dictionary<string, object>(), MediaType.Json);

            return null;
        }

        public Country Edit(Country category)
        {
            var response = HttpPost<Country>("rest/Country/Edit", category, MediaType.Json);

            return response;
        }

        public Country Find(int id)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            var response = HttpGet<FindResponse<Country>>("rest/Country/Find", parameters, MediaType.Json);

            return response.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> SelectList()
        {
            var response = HttpGet<AllResponse<Country>>("rest/Country/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }
    }
}
