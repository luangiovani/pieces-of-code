using AutoMapper;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Services.CBL
{
    public class AgentContactsService : ServiceBase<AgentContacts, AgentContactViewModel>
    {
        private readonly AgentContactsRepository _repository;

        public AgentContactsService(AgentContactsRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public virtual AgentContactViewModel GetByEmailSenha(string email, string password)
        {
            var retorno = _repository.GetByEmailSenha(email, password);
            return Mapper.Map<AgentContactViewModel>(retorno);
        }

        public string GetHash(string email)
        {
            return _repository.GetHash(email);
        }

        public List<AgentContacts> GetByAgentId(int agentId)
        {
            return _repository.GetByAgentId(agentId).ToList();
        }
    }
}