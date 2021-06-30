using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Framework.Domain.Repository.CBL
{
    public class FollowUpMessagesToSendRepository : RepositoryBase<FollowUpMessagesToSend>
    {

        public IEnumerable<FollowUpMessagesToSendViewModel> getAllFromView()
        {
            string sSql = "";
            sSql += @"select f.*, so.status OS_Status, c.name Customer,
            CASE WHEN so.locationReceived_id IS NOT NULL THEN lrs.OS_Series ELSE ls.OS_Series END OS_Series
		   from FollowUpMessagesToSend f
		   inner join ServiceOrder so on f.serviceOrder_id = so.serviceOrder_id
            left join Locations ls on ls.location_id = so.location_id
			left join Locations lrs on lrs.location_id = so.locationReceived_id
		   inner join Customer c on c.customer_id = so.customer_id 
            WHERE CONVERT(DATE, dateToSend,103) <= CONVERT(DATE, GETDATE()+1,103)";
            return Db.Database.SqlQuery<FollowUpMessagesToSendViewModel>(sSql).ToList();
        }

        public ICollection<FollowUpMessagesToSend> getAllByServiceOrderId(decimal serviceOrderId)
        {
            return (from F in Db.FollowUpMessagesToSend
                          where F.serviceOrder_id == serviceOrderId
                           select F).ToList();
        }
    }
}
