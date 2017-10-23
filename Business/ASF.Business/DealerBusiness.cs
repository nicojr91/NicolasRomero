using ASF.Data;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.Business
{
    public class DealerBusiness
    {
        /// <summary>
        /// Add method. 
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        public Dealer Add(Dealer dealer)
        {
            var dealerDac = new DealerDAC();
            return dealerDac.Create(dealer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            var dealerDac = new DealerDAC();
            dealerDac.DeleteById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Dealer> All()
        {
            var dealerDac = new DealerDAC();
            var result = dealerDac.Select();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dealer Find(int id)
        {
            var dealerDac = new DealerDAC();
            var result = dealerDac.SelectById(id);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dealer"></param>
        public void Edit(Dealer dealer)
        {
            var dealerDac = new DealerDAC();
            dealerDac.UpdateById(dealer);
        }
    }
}
