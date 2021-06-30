using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
    public class ContactRepository : RepositoryBase<Contact>
    {
        public void DeleteteById(string query)
        {
            Db.Database.ExecuteSqlCommand(query);
        }

        public void AtualizaContato(int? city_id, int contact_id)
        {
            string sSQL = "UPDATE Contact SET city_id = " + (city_id.HasValue ? city_id.Value.ToString() : "NULL") + " WHERE contact_id = " + contact_id.ToString();
            Db.Database.ExecuteSqlCommand(sSQL);
        }
    }
}
