using Database.Models.APP;
using Domain.DTO.APP;
using Domain.Repositories.APP;
using System;

namespace Domain.Services.APP
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Service do Middleware para operações entre Front e Backend
    /// </atividades>
    /// <summary>
    /// Implementação da Interface de Service para chamadas das operações de banco de dados
    /// </summary>
    public class PerfilService : BaseService<Perfil>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly PerfilRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public PerfilService(PerfilRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastrar um novo Menu ou nova opção de Menu no banco de dados
        /// </summary>
        /// <param name="perfilDTO">Objeto de Perfil para manipulação de registros de Perfil</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado no sistema</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto Perfil cadastrado</returns>
        public PerfilDTO Gravar(PerfilDTO perfilDTO, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (!string .IsNullOrEmpty(perfilDTO.nome) && !string .IsNullOrEmpty(cs_colaborador_logado) && string.IsNullOrEmpty(perfilDTO.id.ToString()))
            {
                Perfil objPerfil = new Perfil()
                {
                    ativo = perfilDTO.ativo,
                    data_hora_criacao = DateTime.Now,
                    descricao = perfilDTO.descricao,
                    nome = perfilDTO.nome,
                    cs_colaborador_criacao = cs_colaborador_logado
                };

                objPerfil = _repository.Add(objPerfil, logOperacaoId);
                perfilDTO.id = objPerfil.id;

                return perfilDTO;
            }
            else if (!string .IsNullOrEmpty(perfilDTO.nome) && !string .IsNullOrEmpty(cs_colaborador_logado) && !string.IsNullOrEmpty(perfilDTO.id.ToString()) && perfilDTO.id.HasValue)
            {

                var objPerfil = _repository.FindByID(perfilDTO.id.ToString());

                if (objPerfil != null)
                {
                    objPerfil.ativo = perfilDTO.ativo;
                    objPerfil.data_hora_alteracao = DateTime.Now;
                    objPerfil.descricao = perfilDTO.descricao;
                    objPerfil.nome = perfilDTO.nome;
                    objPerfil.cs_colaborador_alteracao = cs_colaborador_logado;

                    _repository.Update(objPerfil, logOperacaoId);

                    return perfilDTO;
                }
                else
                    throw new InvalidOperationException("Perfil não encontrado para efetuar a atualização.");
            }
            else
                return null;

        }
    }
}