using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
    public class SmsMenuRepository : RepositoryBase<SmsMenu>
    {

        public int buscaQuantidadeCaracterMenuSms(string id)
        {
            string sSql = "select sum(len(pergunta))+sum(len(ordem)) from SmsMenu where sms_id not in (" + id + ")";
            return Db.Database.SqlQuery<int>(sSql).FirstOrDefault();
        }
    }
}
