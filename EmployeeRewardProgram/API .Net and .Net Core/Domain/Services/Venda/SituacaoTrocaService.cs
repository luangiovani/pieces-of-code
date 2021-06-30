using Database.Models.Venda;
using Domain.DTO.Loja;
using Domain.Repositories.Venda;
using System;

namespace Domain.Services.Venda
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
    public class SituacaoTrocaService : BaseService<SituacaoTroca>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly SituacaoTrocaRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public SituacaoTrocaService(SituacaoTrocaRepository repository) : base(repository)
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
        public SituacaoTrocaDTO Gravar(SituacaoTrocaDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.descricao) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                SituacaoTroca objSituacao = null;
                if (string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.descricao);
                    if (existente != null)
                    {
                        if (existente.ativo)
                        {
                            throw new Exception("Esta Situação da Troca " + dto.descricao + " já está cadastrada!");
                        }
                        else
                        {
                            objSituacao = existente;
                            objSituacao.descricao = dto.descricao;
                            objSituacao.ativo = dto.ativo;
                            objSituacao.data_hora_alteracao = DateTime.Now;
                            objSituacao.cs_colaborador_alteracao = cs_colaborador_logado;
                        }
                    }
                    else
                    {
                        objSituacao = new SituacaoTroca()
                        {
                            descricao = dto.descricao,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };
                    }
                }
                else
                {
                    objSituacao = _repository.FindByID(dto.id.ToString());

                    if (objSituacao != null)
                    {
                        objSituacao.descricao = dto.descricao;
                        objSituacao.ativo = dto.ativo;
                        objSituacao.data_hora_alteracao = DateTime.Now;
                        objSituacao.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Situação da Troca não encontrada para efetuar a atualização.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objSituacao, logOperacaoId).id;
                else
                    _repository.Update(objSituacao, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para Situação da Troca não foram preenchidos");
            }
        }
    }
}