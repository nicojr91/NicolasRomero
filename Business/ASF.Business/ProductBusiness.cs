using ASF.Data;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.Business
{
    public class ProductBusiness
    {
        /// <summary>
        /// Add method. 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Product Add(Product product)
        {
            var productDac = new ProductDAC();
            return productDac.Create(product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            var productDac = new ProductDAC();
            productDac.DeleteById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Product> All()
        {
            var productDac = new ProductDAC();
            var result = productDac.Select();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product Find(int id)
        {
            var productDac = new ProductDAC();
            var result = productDac.SelectById(id);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void Edit(Product product)
        {
            var productDac = new ProductDAC();
            productDac.UpdateById(product);
        }
    }
}
