using ASF.Data;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.Business
{
    public class OrderBusiness
    {
        ProductDAC pdac = new ProductDAC();

        /// <summary>
        /// Add method. 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order Add(Order order)
        {
            double totalprice = 0;
            foreach(OrderDetail detail in order.Details)
            {
                Product p =  pdac.SelectById(detail.ProductId);
                if(p == null) 
                {
                    throw new Exception("Product not found");
                }
                detail.Price = p.Price;
                totalprice += detail.Quantity * detail.Price;
            }
            order.TotalPrice = totalprice;
            order.CreatedOn = DateTime.Now;
            order.ChangedOn = DateTime.Now;
            var orderDac = new OrderDac();
            var orderDetailsDac = new OrderDetailDac();
            order = orderDac.Create(order);

            foreach (OrderDetail detail in order.Details)
            {
                detail.OrderId = order.Id;
                detail.CreatedOn = DateTime.Now;
                detail.ChangedOn = DateTime.Now;
                orderDetailsDac.Create(detail);
            }


            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            var orderDac = new OrderDac();
            orderDac.DeleteById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Order> All()
        {
            var orderDac = new OrderDac();
            var result = orderDac.Select();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order Find(int id)
        {
            var orderDac = new OrderDac();
            var result = orderDac.SelectById(id);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void Edit(Order order)
        {
            var orderDac = new OrderDac();
            orderDac.UpdateById(order);
        }
    }
}
