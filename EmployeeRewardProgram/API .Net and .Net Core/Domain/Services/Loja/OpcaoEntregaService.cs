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
    public class OpcaoEntregaService : BaseService<OpcaoEntrega>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly OpcaoEntregaRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public OpcaoEntregaService(OpcaoEntregaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public OpcaoEntregaDTO Gravar(OpcaoEntregaDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.label) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                OpcaoEntrega objOpcaoEntrega = null;
                if (string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.label);
                    if (existente != null)
                    {
                        if (existente.ativo)
                        {
                            throw new Exception("Esta Opção de Entrega.: " + dto.label + " já está cadastrada!");
                        }
                        else
                        {
                            objOpcaoEntrega = existente;
                            objOpcaoEntrega.label = dto.label;
                            objOpcaoEntrega.label_colaborador = dto.label_colaborador;
                            objOpcaoEntrega.label_loja = dto.label_loja;
                            objOpcaoEntrega.ativo = dto.ativo;
                            objOpcaoEntrega.data_hora_alteracao = DateTime.Now;
                            objOpcaoEntrega.cs_colaborador_alteracao = cs_colaborador_logado;
                        }
                    }
                    else
                    {
                        objOpcaoEntrega = new OpcaoEntrega()
                        {
                            label = dto.label,
                            label_colaborador = dto.label_colaborador,
                            label_loja = dto.label_loja,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };
                    }
                }
                else
                {
                    objOpcaoEntrega = _repository.FindByID(dto.id.ToString());

                    if (objOpcaoEntrega != null)
                    {
                        objOpcaoEntrega.label = dto.label;
                        objOpcaoEntrega.label_colaborador = dto.label_colaborador;
                        objOpcaoEntrega.label_loja = dto.label_loja;
                        objOpcaoEntrega.ativo = dto.ativo;
                        objOpcaoEntrega.data_hora_alteracao = DateTime.Now;
                        objOpcaoEntrega.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Opção de entrega não encontrado para efetuar a atualização.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objOpcaoEntrega, logOperacaoId).id;
                else
                    _repository.Update(objOpcaoEntrega, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para a Opção de Entrega não foram preenchidos");
            }
        }
    }
}