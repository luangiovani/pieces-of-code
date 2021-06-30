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
    public class TaxaConversaoService : BaseService<TaxaConversao>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly TaxaConversaoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public TaxaConversaoService(TaxaConversaoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna a taxa de conversão atual
        /// </summary>
        /// <returns></returns>
        public TaxaConversao ObterAtiva()
        {
            return _repository.ObterAtivo();
        }

        /// <summary>
        /// Inativa a Taxa de Conversão que está ativa
        /// Insere a nova taxa de Conversão
        /// </summary>
        /// <param name="vlrTaxa"></param>
        /// <param name="cs_colaborador_logado"></param>
        /// <param name="logId"></param>
        public TaxaConversao InserirNovaTaxa(decimal vlrTaxa, string cs_colaborador_logado, Guid logId)
        {
            var taxaAtual = _repository.ObterAtivo();

            if (taxaAtual != null)
                Remove(taxaAtual.id.ToString(), cs_colaborador_logado, logId);

            var taxa = new TaxaConversao()
            {
                cs_colaborador_criacao = cs_colaborador_logado,
                data_hora_criacao = DateTime.Now,
                ativo = true,
                fator = vlrTaxa,
                nome = "Nova Taxa " + vlrTaxa.ToString(),
                valor_moeda = 1
            };

            return _repository.Add(taxa, logId);
        }

        /// <summary>
        /// Cadastrar uma nova Taxa de Conversão no banco de dados
        /// </summary>
        /// <param name="dto">Objeto com as informações da Taxa de Conversão para inserção/atualização</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado que está realizando a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto Taxa de Conversão cadastrado</returns>
        public TaxaConversaoDTO Gravar(TaxaConversaoDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (dto.valor_moeda > 0 &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                TaxaConversao objTaxaConversao;
                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.valor_moeda, dto.fator);
                    if (existente != null)
                    {
                        if (existente.ativo)
                        {
                            throw new Exception("Esta Taxa de Conversão " + dto.nome + " já está cadastrada!");
                        }
                        else
                        {
                            objTaxaConversao = existente;
                            objTaxaConversao.nome = dto.nome;
                            objTaxaConversao.ativo = dto.ativo;
                            objTaxaConversao.data_hora_alteracao = DateTime.Now;
                            objTaxaConversao.cs_colaborador_alteracao = cs_colaborador_logado;
                            dto.id = objTaxaConversao.id;
                        }
                    }
                    else
                    {
                        objTaxaConversao = new TaxaConversao()
                        {
                            valor_moeda = dto.valor_moeda,
                            fator = dto.fator,
                            nome = dto.nome,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };
                    }
                }
                else
                {
                    objTaxaConversao = _repository.FindByID(dto.id.ToString());

                    if (objTaxaConversao != null)
                    {
                        objTaxaConversao.nome = dto.nome;
                        objTaxaConversao.valor_moeda = dto.valor_moeda;
                        objTaxaConversao.fator = dto.fator;
                        objTaxaConversao.ativo = dto.ativo;
                        objTaxaConversao.data_hora_alteracao = DateTime.Now;
                        objTaxaConversao.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Taxa de Conversão não encontrada para efetuar a atualização.");
                }

                /// Obtém a Ataxa Atual para Inativar depois
                var atual = ObterAtiva();
                

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString())) {
                    dto.id = _repository.Add(objTaxaConversao, logOperacaoId).id;
                }
                else
                    _repository.Update(objTaxaConversao, logOperacaoId);

                if (atual != null)
                {
                    /// Inativa a Atual para que não ocorra duas taxas ativas ao mesmo tempo
                    atual.ativo = false;
                    _repository.Update(atual, logOperacaoId);
                }

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para Taxa de Conversão não foram preenchidos");
            }
        }
    }
}
