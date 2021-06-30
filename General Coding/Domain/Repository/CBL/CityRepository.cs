using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
    public class CityRepository : RepositoryBase<City>
    {
        public void InsereCidade(string UF, string Name)
        {
            string sSql = "INSERT INTO City SELECT S.state_id,1,'" + Name.Replace("'", "''") + "',0,NULL FROM State S WHERE S.initials = '" + UF + "'";
            Db.Database.ExecuteSqlCommand(sSql);
        }

        public City GetByNameAndUF(string cityName, string uf)
        {
            return (from c in Db.City
                    join s in Db.State on c.state_id equals s.state_id
                    where c.name.ToUpper().Contains(cityName.ToUpper()) && s.initials == uf
                    select c).FirstOrDefault();
        }

        public City GetByName(string cityName)
        {
            return (from c in Db.City
                    join s in Db.State on c.state_id equals s.state_id
                    where c.name.ToUpper().Contains(cityName.ToUpper())
                    select c).FirstOrDefault();
        }
    }
}
