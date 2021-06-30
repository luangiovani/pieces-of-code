using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Database.Entity.CBL;


namespace Framework.Domain.Repository.CBL
{
    public class LogSistemaRepository : RepositoryBase<LogSistema>
    {
        public IEnumerable<string> GetAllModels()
        {
            string sSql = "SELECT Description FROM LogSistema GROUP BY Description";
            return Db.Database.SqlQuery<string>(sSql).ToList();
        }
        public void gravalog(string descricao)
        {
           // string query = @" INSERT INTO LogSistema(description,date)VALUES('"+descricao+"',getdate())";

           // Db.Database.SqlQuery(null,query,null);

            try
            {
                DbSet.Add(new LogSistema() {
                    description = descricao,
                    date = DateTime.Now
                });
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw;
            }
            
        }
    }
    public class LogSistemaUtil
    {
        public string Id { get; set; }
        public string description { get; set; }
        public int date { get; set; }
    }
}
