using Database.Models.Gestao;
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
    public class LojaService : BaseService<Lojas>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly LojaRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public LojaService(LojaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastrar Loja no banco de dados
        /// </summary>
        /// <param name="dto">Objeto Loja DTO para manipulação de registros de Loja</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado no sistema</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto da Loja cadastrada</returns>
        public LojasDTO Gravar(LojasDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.nome) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                Lojas objLoja;
                if (string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.nome);
                    if (existente != null)
                    {
                        if (existente.ativo)
                        {
                            throw new Exception("Esta Loja " + dto.nome + " já está cadastrada!");
                        }
                        else
                        {
                            objLoja = existente;
                            objLoja.codigo = dto.codigo;
                            objLoja.complemento = dto.complemento;
                            objLoja.localizacao = dto.localizacao;
                            objLoja.nome = dto.nome;
                            objLoja.observacao = dto.observacao;
                            objLoja.status_loja = dto.status_loja;
                            objLoja.ativo = dto.ativo;
                            objLoja.data_hora_alteracao = DateTime.Now;
                            objLoja.cs_colaborador_alteracao = cs_colaborador_logado;
                        }
                    }
                    else
                    {
                        objLoja = new Lojas()
                        {
                            codigo = dto.codigo,
                            complemento = dto.complemento,
                            localizacao = dto.localizacao,
                            nome = dto.nome,
                            observacao = dto.observacao,
                            status_loja = dto.status_loja,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };
                    }
                }
                else
                {
                    objLoja = _repository.FindByID(dto.id.ToString());

                    if (objLoja != null)
                    {
                        objLoja.codigo = dto.codigo;
                        objLoja.complemento = dto.complemento;
                        objLoja.localizacao = dto.localizacao;
                        objLoja.nome = dto.nome;
                        objLoja.observacao = dto.observacao;
                        objLoja.status_loja = dto.status_loja;
                        objLoja.ativo = dto.ativo;
                        objLoja.data_hora_alteracao = DateTime.Now;
                        objLoja.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Loja não encontrada para efetuar a atualização.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objLoja, logOperacaoId).id;
                else
                    _repository.Update(objLoja, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para a Loja não foram preenchidos");
            }
        }

        public ColaboradoresLojasDTO GravarVinculo(ColaboradoresLojasDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.cs) &&
                !string.IsNullOrEmpty(dto.loja_id) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                var existente = _repository.VerificarSeExisteVinculo(dto.cs, dto.loja_id);
                if (existente != null)
                    throw new Exception("Este Colaborador já está vinculado a esta Loja.");

                return _repository.GravarAtualizarVinculo(dto, cs_colaborador_logado, logOperacaoId);
            }
            else
            {
                throw new InvalidOperationException("Os campos para vinculo de colaborador e Loja não foram preenchidos");
            }
        }

        public IEnumerable<ColaboradoresLojasDTO> ObterColaboradoresLoja(string lojaId, string cs)
        {
            return _repository.ObterColaboradoresLoja(lojaId, cs);
        }

        public void DesvincularColaboradorLoja(string id, string cs_colaborador_logado, Guid logOperacaoId)
        {
            _repository.DesvincularColaboradorLoja(id, cs_colaborador_logado, logOperacaoId);
        }
    }
}