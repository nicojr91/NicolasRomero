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
    public class ProductDAC : DataAccessComponent
    {
        public Product Create(Product product)
        {
            const string sqlStatement = "INSERT INTO dbo.Product ([Title], [Description], [DealerId], [Image], [Price], [QuantitySold], [AvgStars], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy])"
                + "VALUES(@Title, @Description, @DealerId, @Image, @Price, @QuantitySold, @AvgStars, @Rowid, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Title", DbType.String, product.Title);
                db.AddInParameter(cmd, "@Description", DbType.String, product.Description);
                db.AddInParameter(cmd, "@DealerId", DbType.Int32, product.DealerId);
                db.AddInParameter(cmd, "@Image", DbType.Int32, product.Image);
                db.AddInParameter(cmd, "@Price", DbType.String, product.Price);
                db.AddInParameter(cmd, "@QuantitySold", DbType.Int32, product.QuantitySold);
                db.AddInParameter(cmd, "@AvgStars", DbType.Int32, product.AvgStars);
                db.AddInParameter(cmd, "@Rowid", DbType.Guid, product.Rowid);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime2, product.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.Int32, product.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime2, product.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.Int32, product.ChangedBy);
                // Obtener el valor de la primary key.
                product.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void UpdateById(Product product)
        {
            const string sqlStatement = "UPDATE dbo.Product SET [Title]=@Title, [Description]=@Description, [DealerId]=@DealerId, [Image]=@Image, [Price]=@Price, [QuantitySold]=@QuantitySold, [AvgStars]=@AvgStars, [Rowid]=@Rowid, [ChangedOn]=@ChangedOn, [ChangedBy]=@ChangedBy WHERE Id = @Id;";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Title", DbType.String, product.Title);
                db.AddInParameter(cmd, "@Description", DbType.String, product.Description);
                db.AddInParameter(cmd, "@DealerId", DbType.Int32, product.DealerId);
                db.AddInParameter(cmd, "@Image", DbType.Int32, product.Image);
                db.AddInParameter(cmd, "@Price", DbType.String, product.Price);
                db.AddInParameter(cmd, "@QuantitySold", DbType.Int32, product.QuantitySold);
                db.AddInParameter(cmd, "@AvgStars", DbType.Int32, product.AvgStars);
                db.AddInParameter(cmd, "@Rowid", DbType.Guid, product.Rowid);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime2, product.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.Int32, product.ChangedBy);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            const string sqlStatement = "DELETE dbo.Dealer WHERE [Id]=@Id ";
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
        public Product SelectById(int id)
        {
            const string sqlStatement = "SELECT [Id], [FirstName], [LastName], [CategoryId], [CountryId], [Description], [TotalProducts], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] " +
                "FROM dbo.Dealer WHERE [Id]=@Id ";

            Product dealer = null;
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) dealer = LoadCategory(dr);
                }
            }

            return dealer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>		
        public List<Product> Select()
        {
            // WARNING! Performance
            const string sqlStatement = "SELECT [Id], [Name], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] FROM dbo.Category ";

            var result = new List<Product>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var dealer = LoadCategory(dr); // Mapper
                        result.Add(dealer);
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
        private static Product LoadCategory(IDataReader dr)
        {
            var dealer = new Product
            {
                Id = GetDataValue<int>(dr, "Id"),
                Title = GetDataValue<string>(dr, "Title"),
                Description = GetDataValue<string>(dr, "Description"),
                DealerId = GetDataValue<int>(dr, "DealerId"),
                Image = GetDataValue<string>(dr, "Image"),
                Price = GetDataValue<float>(dr, "Price"),
                QuantitySold = GetDataValue<int>(dr, "QuantitySold"),
                AvgStars = GetDataValue<float>(dr, "AvgStars"),
                Rowid = GetDataValue<Guid>(dr, "Rowid"),
                CreatedOn = GetDataValue<DateTime>(dr, "CreatedOn"),
                CreatedBy = GetDataValue<int>(dr, "CreatedBy"),
                ChangedOn = GetDataValue<DateTime>(dr, "ChangedOn"),
                ChangedBy = GetDataValue<int>(dr, "ChangedBy")
            };

            return dealer;
        }
    }
}
