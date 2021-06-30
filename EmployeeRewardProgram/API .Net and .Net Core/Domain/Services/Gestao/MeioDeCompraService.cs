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
    public class MeioDeCompraService : BaseService<MeioDeCompra>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly MeioDeCompraRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public MeioDeCompraService(MeioDeCompraRepository repository) : base(repository)
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
        public MeioDeCompraDTO Gravar(MeioDeCompraDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.nome) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                MeioDeCompra objMeioDeCompra;
                if (string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.nome);
                    if (existente != null)
                    {
                        if (existente.ativo)
                        {
                            throw new Exception("Este Meio de Compra " + dto.nome + " já está cadastrado!");
                        }
                        else
                        {
                            objMeioDeCompra = existente;
                            objMeioDeCompra.nome = dto.nome;
                            objMeioDeCompra.ativo = dto.ativo;
                            objMeioDeCompra.data_hora_alteracao = DateTime.Now;
                            objMeioDeCompra.cs_colaborador_alteracao = cs_colaborador_logado;
                        }
                    }
                    else
                    {
                        objMeioDeCompra = new MeioDeCompra()
                        {
                            nome = dto.nome,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };
                    }
                }
                else
                {
                    objMeioDeCompra = _repository.FindByID(dto.id.ToString());

                    if (objMeioDeCompra != null)
                    {
                        objMeioDeCompra.nome = dto.nome;
                        objMeioDeCompra.ativo = dto.ativo;
                        objMeioDeCompra.data_hora_alteracao = DateTime.Now;
                        objMeioDeCompra.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Meio de Compra não encontrado para efetuar a atualização.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objMeioDeCompra, logOperacaoId).id;
                else
                    _repository.Update(objMeioDeCompra, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para o Meio de Compra não foram preenchidos");
            }
        }
    }
}
