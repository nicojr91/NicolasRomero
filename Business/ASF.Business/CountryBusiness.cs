using ASF.Data;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.Business
{
    public class CountryBusiness
    {
        /// <summary>
        /// Add method. 
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public Country Add(Country country)
        {
            var categoryDac = new CountryDAC();
            return categoryDac.Create(country);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            var categoryDac = new CountryDAC();
            categoryDac.DeleteById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> All()
        {
            var categoryDac = new CountryDAC();
            var result = categoryDac.Select();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Country Find(int id)
        {
            var categoryDac = new CountryDAC();
            var result = categoryDac.SelectById(id);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        public void Edit(Country country)
        {
            var categoryDac = new CountryDAC();
            categoryDac.UpdateById(country);
        }
    }
}
