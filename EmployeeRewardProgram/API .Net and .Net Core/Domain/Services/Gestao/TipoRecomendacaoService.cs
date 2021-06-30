using Database.Models.Gestao;
using Domain.DTO;
using Domain.DTO.Gestao;
using Domain.Repositories.Gestao;
using System;
using System.Collections.Generic;

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
    public class TipoRecomendacaoService : BaseService<TipoRecomendacao>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly TipoRecomendacaoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public TipoRecomendacaoService(TipoRecomendacaoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public TipoRecomendacaoDTO Gravar(TipoRecomendacaoDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.nome) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                TipoRecomendacao objTipoRecomendacao;
                if (string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.nome);
                    if (existente != null)
                    {
                        if (existente.ativo)
                        {
                            throw new Exception("Este Tipo de Recomendação" + dto.nome + " já está cadastrado!");
                        }
                        else
                        {
                            objTipoRecomendacao = existente;
                            objTipoRecomendacao.nome = dto.nome;
                            objTipoRecomendacao.descricao = dto.descricao;
                            /// Escolha 1 | Fixa 2
                            /// Se for Escolha, pontos serão informados manualmente, senão
                            objTipoRecomendacao.pontos_fixos = (dto.tipopontuacao == 1 ? 0 : dto.pontosfixos);
                            objTipoRecomendacao.tipo_pontuacao = dto.tipopontuacao;
                            objTipoRecomendacao.ativo = dto.ativo;
                            objTipoRecomendacao.data_hora_alteracao = DateTime.Now;
                            objTipoRecomendacao.cs_colaborador_alteracao = cs_colaborador_logado;
                        }
                    }
                    else
                    {
                        objTipoRecomendacao = new TipoRecomendacao()
                        {
                            descricao = dto.descricao,
                            nome = dto.nome,
                            pontos_fixos = (dto.tipopontuacao == 1 ? 0 : dto.pontosfixos),
                            tipo_pontuacao = dto.tipopontuacao,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };
                    }
                }
                else
                {
                    objTipoRecomendacao = _repository.FindByID(dto.id.ToString());

                    if (objTipoRecomendacao != null)
                    {
                        objTipoRecomendacao.nome = dto.nome;
                        objTipoRecomendacao.descricao = dto.descricao;
                        objTipoRecomendacao.pontos_fixos = (dto.tipopontuacao == 1 ? 0 : dto.pontosfixos);
                        objTipoRecomendacao.tipo_pontuacao = dto.tipopontuacao;
                        objTipoRecomendacao.ativo = dto.ativo;
                        objTipoRecomendacao.data_hora_alteracao = DateTime.Now;
                        objTipoRecomendacao.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Tipo de Recomendação não encontrada para efetuar a atualização.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objTipoRecomendacao, logOperacaoId).id;
                else
                    _repository.Update(objTipoRecomendacao, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para Tipo de Recomendação não foram preenchidos");
            }

        }

        public IEnumerable<BaseDTO> ListarTipoRecomendacaoCombo()
        {
            return _repository.ListarTipoRecomendacaoCombo();
        }

        public IEnumerable<TipoRecomendacao> ListarTodosAtivos()
        {
            return _repository.ListarTodosAtivos();
        }
    }
}