using Database.Models.Gestao;
using Domain.DTO.Gestao;
using Domain.Repositories.Gestao;
using System;

namespace Domain.Services.Gestao
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
    public class SituacaoRecomendacaoService : BaseService<SituacaoRecomendacao>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly SituacaoRecomendacaoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public SituacaoRecomendacaoService(SituacaoRecomendacaoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastrar uma nova Situação no banco de dados
        /// </summary>
        /// <param name="dto">Objeto com as informações da Situação para inserção/atualização</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado que está realizando a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto aplicacao cadastrado</returns>
        public SituacaoRecomendacaoDTO Gravar(SituacaoRecomendacaoDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.nome) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                SituacaoRecomendacao objSituacaoRecomendacao;
                if (string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.nome);
                    if (existente != null)
                    {
                        if (existente.ativo)
                        {
                            throw new Exception("Esta Situação da Recomendação " + dto.nome + " já está cadastrada!");
                        }
                        else
                        {
                            objSituacaoRecomendacao = existente;
                            objSituacaoRecomendacao.nome = dto.nome;
                            objSituacaoRecomendacao.descricao = dto.descricao;
                            objSituacaoRecomendacao.ativo = dto.ativo;
                            objSituacaoRecomendacao.data_hora_alteracao = DateTime.Now;
                            objSituacaoRecomendacao.cs_colaborador_alteracao = cs_colaborador_logado;
                        }
                    }
                    else
                    {
                        objSituacaoRecomendacao = new SituacaoRecomendacao()
                        {
                            descricao = dto.descricao,
                            nome = dto.nome,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };
                    }
                }
                else
                {
                    objSituacaoRecomendacao = _repository.FindByID(dto.id.ToString());

                    if (objSituacaoRecomendacao != null)
                    {
                        objSituacaoRecomendacao.nome = dto.nome;
                        objSituacaoRecomendacao.descricao = dto.descricao;
                        objSituacaoRecomendacao.ativo = dto.ativo;
                        objSituacaoRecomendacao.data_hora_alteracao = DateTime.Now;
                        objSituacaoRecomendacao.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Situação da Recomendação não encontrada para efetuar a atualização.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objSituacaoRecomendacao, logOperacaoId).id;
                else
                    _repository.Update(objSituacaoRecomendacao, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para Situação da Recomendação não foram preenchidos");
            }
        }
    }
}
