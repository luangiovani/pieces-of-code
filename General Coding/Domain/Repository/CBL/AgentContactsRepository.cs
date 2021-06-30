using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
    public class AgentContactsRepository : RepositoryBase<AgentContacts>
    {
        public AgentContacts GetByEmailSenha(string email, string password)
        {
            return Db.AgentContacts.Where(c => c.email == email && c.password == password).FirstOrDefault();
        }
        internal string GetHash(string email)
        {
            string final = email + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss");
            string sSql = "SELECT CONVERT(VARCHAR(255), HashBytes('SHA1', '" + final + "'),2)";
            return Db.Database.SqlQuery<string>(sSql).FirstOrDefault();
        }

        public List<AgentContacts> GetByAgentId(int agentId)
        {
            return Db.AgentContacts.Where(c => c.agent_id == agentId).ToList();
        }
    }
}
