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
    public class DealerDAC : DataAccessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        public Dealer Create(Dealer dealer)
        {
            const string sqlStatement = "INSERT INTO dbo.Dealer ([FirstName], [LastName], [CategoryId], [CountryId], [Description], [TotalProducts], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]) "
                + "VALUES(@FirstName, @LastName, @CategoryId, @CountryId, @Description, @TotalProducts, @Rowid, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@FirstName", DbType.String, dealer.FirstName);
                db.AddInParameter(cmd, "@LastName", DbType.String, dealer.LastName);
                db.AddInParameter(cmd, "@CategoryId", DbType.Int32, dealer.CategoryId);
                db.AddInParameter(cmd, "@CountryId", DbType.Int32, dealer.CountryId);
                db.AddInParameter(cmd, "@Description", DbType.String, dealer.Description);
                db.AddInParameter(cmd, "@TotalProducts", DbType.Int32, dealer.TotalProducts);
                db.AddInParameter(cmd, "@Rowid", DbType.Guid, dealer.Rowid);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime2, dealer.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.Int32, dealer.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime2, dealer.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.Int32, dealer.ChangedBy);
                // Obtener el valor de la primary key.
                dealer.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return dealer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dealer"></param>
        public void UpdateById(Dealer dealer)
        {
            const string sqlStatement = "UPDATE dbo.Dealer SET [FirstName]=@FirstName, [LastName]=@LastName, [CategoryId]=@CategoryId, [CountryId]=@CountryId, [Description]=@Description, [TotalProducts]=@TotalProducts, [Rowid]=@Rowid, [ChangedOn]=@ChangedOn, [ChangedBy]=@ChangedBy WHERE Id = @Id;";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@FirstName", DbType.String, dealer.FirstName);
                db.AddInParameter(cmd, "@LastName", DbType.String, dealer.LastName);
                db.AddInParameter(cmd, "@CategoryId", DbType.Int32, dealer.CategoryId);
                db.AddInParameter(cmd, "@CountryId", DbType.Int32, dealer.CountryId);
                db.AddInParameter(cmd, "@Description", DbType.String, dealer.Description);
                db.AddInParameter(cmd, "@TotalProducts", DbType.Int32, dealer.TotalProducts);
                db.AddInParameter(cmd, "@Rowid", DbType.Guid, dealer.Rowid);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime2, dealer.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.Int32, dealer.ChangedBy);

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
        public Dealer SelectById(int id)
        {
            const string sqlStatement = "SELECT [Id], [FirstName], [LastName], [CategoryId], [CountryId], [Description], [TotalProducts], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] " +
                "FROM dbo.Dealer WHERE [Id]=@Id ";

            Dealer dealer = null;
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
        public List<Dealer> Select()
        {
            // WARNING! Performance
            const string sqlStatement = "SELECT [Id], [FirstName], [LastName], [CategoryId], [CountryId], [Description], [TotalProducts], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] "
                + " FROM dbo.Dealer ";

            var result = new List<Dealer>();
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
        private static Dealer LoadCategory(IDataReader dr)
        {
            var dealer = new Dealer
            {
                Id = GetDataValue<int>(dr, "Id"),
                FirstName = GetDataValue<string>(dr, "FirstName"),
                LastName = GetDataValue<string>(dr, "LastName"),
                CategoryId = GetDataValue<int>(dr, "CategoryId"),
                CountryId = GetDataValue<int>(dr, "CountryId"),
                Description = GetDataValue<string>(dr, "Description"),
                TotalProducts = GetDataValue<int>(dr, "TotalProducts"),
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
