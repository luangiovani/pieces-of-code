using Database.Models.Loja;
using Domain.DTO.Loja;
using Domain.Repositories.Loja;
using System;

namespace Domain.Services.Loja
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
    public class TipoOpcaoService : BaseService<TipoOpcao>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly TipoOpcaoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public TipoOpcaoService(TipoOpcaoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gravar ou Atualizar um novo Tipo de Opção
        /// </summary>
        /// <param name="dto">Objeto com as informações do Tipo de Opção para inserção/atualização</param>
        /// <param name="cs_colaborador_logado">Código do colaborador que está enviando as informações</param>
        /// <param name="logOperacaoId">ID do Log da operação desta transação</param>
        /// <returns>Objeto Tipo de Opção com as informações inseridas/atualizadas</returns>
        public TipoOpcaoDTO Gravar(TipoOpcaoDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null && 
                (!string.IsNullOrEmpty(dto.nome) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                TipoOpcao objTipoOpcao = null;
                if (string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.nome);
                    if (existente != null)
                    {
                        if (existente.ativo)
                        {
                            throw new Exception("Este Tipo de Opção " + dto.nome + " já está cadastrado!");
                        }
                        else
                        {
                            objTipoOpcao = existente;
                            objTipoOpcao.nome = dto.nome;
                            objTipoOpcao.descricao = dto.descricao;
                            objTipoOpcao.ativo = dto.ativo;
                            objTipoOpcao.data_hora_alteracao = DateTime.Now;
                            objTipoOpcao.cs_colaborador_alteracao = cs_colaborador_logado;
                        }
                    }
                    else
                    {
                        objTipoOpcao = new TipoOpcao()
                        {
                            nome = dto.nome,
                            descricao = dto.descricao,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };
                    }
                }
                else
                {
                    objTipoOpcao = _repository.FindByID(dto.id.ToString());

                    if (objTipoOpcao != null)
                    {
                        objTipoOpcao.nome = dto.nome;
                        objTipoOpcao.descricao = dto.descricao;
                        objTipoOpcao.ativo = dto.ativo;
                        objTipoOpcao.data_hora_alteracao = DateTime.Now;
                        objTipoOpcao.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Tipo de Opção não encontrado para efetuar a atualização.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objTipoOpcao, logOperacaoId).id;
                else
                    _repository.Update(objTipoOpcao, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para o Tipo de Opção não foram preenchidos");
            }
        }
    }
}
