using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <task_url>https://esfera.teamworkpm.net/#tasks/14035934</task_url>
/// <autor>Fabricio kikina</autor>
/// <date>09/11/2017</date>
/// <sumary>
/// Cadastro de email
/// </sumary>


namespace Framework.Domain.Repository.CBL
{
    public class EmailServiceOrderRepository : RepositoryBase<EmailServiceOrder>
    {
        public IEnumerable<EmailServiceOrder> GetByLocationOrderId(int locationOrderId)
        {
            return (from e in Db.EmailServiceOrder
                    where e.location_id == locationOrderId && e.active == true
                    select e).ToList();
        }
    }
}
