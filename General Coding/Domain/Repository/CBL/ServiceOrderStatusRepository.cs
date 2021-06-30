using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
    public class ServiceOrderStatusUtil
    {
        public string desc { get; set; }
        public int orders { get; set; }
    }

    public class ServiceOrderUsersUtil
    {
        public string Id { get; set; }
        public string nome { get; set; }
        public int orders { get; set; }
    }

    public class ServiceOrderStatusRepository : RepositoryBase<ServiceOrderStatus>
    {
        public List<ServiceOrderStatusUtil> GetOrdersByStatusAndCount()
        {
            string query = @"   SELECT SO.name as 'desc', COUNT(DISTINCT S.serviceOrder_id) orders
                                 FROM ServiceOrderStatus SO
                            LEFT JOIN ServiceOrder S ON S.status = SO.name
                            GROUP BY SO.name, SO.[order]
                            ORDER BY SO.[order]";

            //nova query separando alguns status pra trazer so do mes e os substatus fazendo join com o substatus
            query = @"SELECT SO.name AS 'desc', COUNT(DISTINCT S.serviceOrder_id) orders,SO.[order]
                        FROM ServiceOrderStatus SO
                        LEFT JOIN ServiceOrder S ON S.status = SO.name
                        where SO.name not in ('Closed','Declined','Recovered','Unrecovered','Unrecovered DOA')
                        and serviceOrderStatusParent_id is null AND SO.name <> 'Transfer'
                        GROUP BY SO.name,SO.[order]
                        union all
                        SELECT SO.name AS 'desc',COUNT(DISTINCT S.serviceOrder_id) orders,SO.[order]
                        FROM ServiceOrderStatus SO
                        LEFT JOIN ServiceOrder S ON S.status = SO.name and MONTH(s.statusDate) = MONTH(getdate()) and Year(s.statusDate) = Year(getdate()) 
                        where SO.name  in ('Closed','Declined','Recovered','Unrecovered','Unrecovered DOA')
                        and serviceOrderStatusParent_id is null
                        GROUP BY SO.name, SO.[order]
                        union all
                        SELECT SO.name AS 'desc', COUNT(DISTINCT S.serviceOrder_id) orders,SO.[order]
                        FROM ServiceOrderStatus SO
                        LEFT JOIN ServiceOrder S ON S.subStatus = SO.name 
                        where serviceOrderStatusParent_id is not null OR SO.name = 'Transfer'
                        GROUP BY SO.name, SO.[order]
                        ORDER BY SO.[order]";

            return Db.Database.SqlQuery<ServiceOrderStatusUtil>(query).ToList();
        }

        public List<ServiceOrderUsersUtil> GetOrdersByUsersAndCount()
        {
            string query = @"  SELECT U.id_usuario as 'Id', U.nome, COUNT(DISTINCT S.serviceOrder_id) orders
                                 FROM Usuario U
                            LEFT JOIN ServiceOrder S ON S.userAssigned_id = U.id_usuario and S.status in ('Go Ahead','Incoming','Pending','Quoted','Waiting Destination','Negotiation','Investiment','Waiting parts','Transfer')
                            GROUP BY U.id_usuario, U.nome
                            ORDER BY U.nome,U.id_usuario";

            return Db.Database.SqlQuery<ServiceOrderUsersUtil>(query).ToList();
        }

        public int CountByStatus(string status)
        {
            return Db.ServiceOrder.Where(s => s.status.ToUpper().Equals(status.ToUpper())).Count();
        }
    }
}
