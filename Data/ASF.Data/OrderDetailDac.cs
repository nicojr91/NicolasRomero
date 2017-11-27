using ASF.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.Data
{
    public class OrderDetailDac : DataAccessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public OrderDetail Create(OrderDetail detail)
        {
            const string sqlStatement = "INSERT INTO [OrderDetail] ([OrderId], [ProductId], [Price], [Quantity], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]) " +
                "VALUES(@OrderId, @ProductId, @Price, @Quantity, @OrderNumber, @ItemCount, @Name, @Rowid, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@OrderId", DbType.Int32, detail.OrderId);
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, detail.ProductId);
                db.AddInParameter(cmd, "@Price", DbType.Double, detail.Price);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, detail.Quantity);

                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime2, detail.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.Int32, detail.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime2, detail.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.Int32, detail.ChangedBy);
                // Obtener el valor de la primary key.
                detail.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return detail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public void UpdateById(OrderDetail detail)
        {
            const string sqlStatement = "UPDATE [OrderDetail] " +
                "SET [Name]=@Name, " +
                    "[ChangedOn]=@ChangedOn, " +
                    "[ChangedBy]=@ChangedBy " +
                "WHERE [Id]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime2, detail.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.Int32, detail.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime2, detail.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.Int32, detail.ChangedBy);
                db.AddInParameter(cmd, "@Id", DbType.Int32, detail.Id);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            const string sqlStatement = "DELETE [OrderDetail] WHERE [Id]=@Id ";
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderDetail SelectById(int id)
        {
            const string sqlStatement = "SELECT [Id], [OrderId], [ProductId], [Price], [Quantity], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] " +
                "FROM [OrderDetail] WHERE [Id]=@Id ";

            OrderDetail detail = null;
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) detail = LoadOrderDetail(dr);
                }
            }

            return detail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>		
        public List<OrderDetail> Select()
        {
            // WARNING! Performance
            const string sqlStatement = "SELECT [Id], [OrderId], [ProductId], [Price], [Quantity], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] FROM [OrderDetail]";

            var result = new List<OrderDetail>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var order = LoadOrderDetail(dr); // Mapper
                        result.Add(order);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Crea una nueva Categoría desde un Datareader.
        /// </summary>
        /// <param name="dr">Objeto DataReader.</param>
        /// <returns>Retorna un objeto Categoria.</returns>		
        private static OrderDetail LoadOrderDetail(IDataReader dr)
        {
            var detail = new OrderDetail
            {
                Id = GetDataValue<int>(dr, "Id"),
                OrderId = GetDataValue<int>(dr, "OrderId"),
                Price = GetDataValue<double>(dr, "Price"),
                ProductId = GetDataValue<int>(dr, "ProductId"),
                Quantity = GetDataValue<int>(dr, "Quantity"),
                CreatedOn = GetDataValue<DateTime>(dr, "CreatedOn"),
                CreatedBy = GetDataValue<int>(dr, "CreatedBy"),
                ChangedOn = GetDataValue<DateTime>(dr, "ChangedOn"),
                ChangedBy = GetDataValue<int>(dr, "ChangedBy")
            };
            return detail;
        }
    }
}
