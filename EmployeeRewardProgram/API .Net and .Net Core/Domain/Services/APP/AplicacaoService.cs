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
    public class AplicacaoService : BaseService<Aplicacao>
    {
        /// <summary>
        /// Repositório de banco de dados
        /// </summary>
        private readonly AplicacaoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public AplicacaoService(AplicacaoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastrar uma nova Aplicacao no banco de dados
        /// </summary>
        /// <param name="dto">Objeto com as informações da Aplicação para inserção/atualização</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado que está realizando a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto aplicacao cadastrado</returns>
        public AplicacaoDTO Gravar(AplicacaoDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.descricao) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                Aplicacao objAplicacao;
                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.descricao);
                    if (existente != null)
                    {
                        if (existente.ativo)
                        {
                            throw new Exception("Esta Aplicação " + dto.descricao + " já está cadastrada!");
                        }
                        else
                        {
                            objAplicacao = existente;
                            objAplicacao.descricao = dto.descricao;
                            objAplicacao.ativo = dto.ativo;
                            objAplicacao.data_hora_alteracao = DateTime.Now;
                            objAplicacao.cs_colaborador_alteracao = cs_colaborador_logado;
                            dto.id = objAplicacao.id;
                        }
                    }
                    else
                    {
                        objAplicacao = new Aplicacao()
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
                    objAplicacao = _repository.FindByID(dto.id.ToString());

                    if (objAplicacao != null)
                    {
                        objAplicacao.descricao = dto.descricao;
                        objAplicacao.ativo = dto.ativo;
                        objAplicacao.data_hora_alteracao = DateTime.Now;
                        objAplicacao.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Aplicação não encontrada para efetuar a atualização.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objAplicacao, logOperacaoId).id;
                else
                    _repository.Update(objAplicacao, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para Aplicação não foram preenchidos");
            }
        }
    }
}
