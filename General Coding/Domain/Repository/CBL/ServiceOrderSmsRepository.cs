using Framework.Database.Entity.CBL;
using Framework.Domain.Model;
using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
    public class ServiceOrderSmsRepository : RepositoryBase<ServiceOrderSms>
    {


        public int numeroContadorSms(string serviceOrderId)
        {
            string sSql = "";

            sSql = @"select count(1)+1 from serviceordersms where serviceOrder_id = " + serviceOrderId + @"";


            return Db.Database.SqlQuery<int>(sSql).FirstOrDefault();
        }
    }
}
