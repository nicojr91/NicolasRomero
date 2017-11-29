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
    public class OrderDac : DataAccessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order Create(Order order)
        {
            const string sqlStatement = "INSERT INTO [Order] ([ClientId], [OrderDate], [TotalPrice], [State], [OrderNumber], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]) " +
                "VALUES(@ClientId, @OrderDate, @TotalPrice, @State, @OrderNumber, @ItemCount, @Rowid, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@ClientId", DbType.Int32, order.ClientId);
                db.AddInParameter(cmd, "@OrderDate", DbType.DateTime, order.OrderDate);
                db.AddInParameter(cmd, "@TotalPrice", DbType.Double, order.TotalPrice);
                db.AddInParameter(cmd, "@State", DbType.Int32, order.State);
                db.AddInParameter(cmd, "@OrderNumber", DbType.Int32, order.OrderNumber);
                db.AddInParameter(cmd, "@ItemCount", DbType.Int32, order.ItemCount);
                db.AddInParameter(cmd, "@Rowid", DbType.Guid, order.Rowid);

                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime2, order.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.Int32, order.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime2, order.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.Int32, order.ChangedBy);
                // Obtener el valor de la primary key.
                order.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void UpdateById(Order order)
        {
            const string sqlStatement = "UPDATE [Order] " +
                "SET [Name]=@Name, " +
                    "[ChangedOn]=@ChangedOn, " +
                    "[ChangedBy]=@ChangedBy " +
                "WHERE [Id]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime2, order.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.Int32, order.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime2, order.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.Int32, order.ChangedBy);
                db.AddInParameter(cmd, "@Id", DbType.Int32, order.Id);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            const string sqlStatement = "DELETE [Order] WHERE [Id]=@Id ";
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
        public Order SelectById(int id)
        {
            const string sqlStatement = "SELECT [Id], [ClientId], [OrderDate], [TotalPrice], [State], [OrderNumber], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] " +
                "FROM [Order] WHERE [Id]=@Id ";

            Order order = null;
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) order = LoadOrder(dr);
                }
            }

            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>		
        public List<Order> Select()
        {
            // WARNING! Performance
            const string sqlStatement = "SELECT [Id], [ClientId], [OrderDate], [TotalPrice], [State], [OrderNumber], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] FROM [Order]";

            var result = new List<Order>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var order = LoadOrder(dr); // Mapper
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
        private static Order LoadOrder(IDataReader dr)
        {
            var order = new Order
            {
                Id = GetDataValue<int>(dr, "Id"),
                ClientId = GetDataValue<int>(dr, "ClientId"),
                OrderDate = GetDataValue<DateTime>(dr, "OrderDate"),
                TotalPrice = GetDataValue<double>(dr, "TotalPrice"),
                State = GetDataValue<string>(dr, "State"),
                OrderNumber = GetDataValue<int>(dr, "OrderNumber"),
                ItemCount = GetDataValue<int>(dr, "ItemCount"),
                Rowid = GetDataValue<Guid>(dr, "Rowid"),
                CreatedOn = GetDataValue<DateTime>(dr, "CreatedOn"),
                CreatedBy = GetDataValue<int>(dr, "CreatedBy"),
                ChangedOn = GetDataValue<DateTime>(dr, "ChangedOn"),
                ChangedBy = GetDataValue<int>(dr, "ChangedBy")
            };
            return order;
        }
    }
}
