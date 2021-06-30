using Domain.DTO.Gestao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.DTO.Loja
{
    public class SolicitacaoCompraDTO
    {
        public Guid? id { get; set; }

        public int qtde { get; set; }

        public string opcao_entrega_id { get; set; }

        public string meio_de_compra_id { get; set; }

        public string loja_id { get; set; }

        public string observacoes { get; set; }

        public string produto_id { get; set; }

        public string cs_colaborador { get; set; }
    }

    public class CompraDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Sequencial do registro na tabela (ordenação)
        /// </summary>
        public int sequencial { get; set; }

        /// <summary>
        /// Opção de Entrega que o Colaborador escolheu
        /// </summary>
        public string opcao_entrega_id { get; set; }

        /// <summary>
        /// Loja em que o Colaborador efetuou a troca de pontos
        /// </summary>
        public string loja_id { get; set; }

        /// <summary>
        /// Colaborador que realizou a troca
        /// </summary>
        public string cs_colaborador { get; set; }

        /// <summary>
        /// Colaborador que atendeu ou vendeu na Loja
        /// </summary>
        public string cs_colaborador_loja { get; set; }

        /// <summary>
        /// Quantidade total de pontos da compra
        /// </summary>
        public decimal total_pontos { get; set; }

        /// <summary>
        /// Valor total da compra
        /// </summary>
        public decimal total_valor { get; set; }

        /// <summary>
        /// Taxa de conversão de pontos em valor
        /// </summary>
        public decimal taxa_conversao { get; set; }

        /// <summary>
        /// Situação da Compra
        /// </summary>
        public string situacao_compra_id { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }

        /// <summary>
        /// Data e Hora da Criação do registro
        /// </summary>
        public DateTime? data_hora_criacao { get; set; }

        /// <summary>
        /// Usuário de Criação do registro
        /// </summary>
        public int? cs_colaborador_criacao { get; set; }

        /// <summary>
        /// Data e Hora da Alteração do registro
        /// </summary>
        public DateTime? data_hora_alteracao { get; set; }

        /// <summary>
        /// Usuário de Alteração do registro
        /// </summary>
        public string cs_colaborador_alteracao { get; set; }
    }

    public class CompraTrocaDTO
    {
        public Guid id { get; set; }

        public int sequencial { get; set; }

        public string cs_colaborador { get; set; }

        public string colaborador { get; set; }

        public string situacao { get; set; }

        public decimal pontos { get; set; }

        public string data { get; set; }
    }

    public class TrocasPendentesDTO
    {
        public TrocasPendentesDTO()
        {
            TrocasPendentes = new List<CompraTrocaDTO>();
            TrocasPendentesDeEnvio = new List<CompraTrocaDTO>();
        }

        public int quantidadePendente
        {
            get
            {
                return TrocasPendentes.Count();
            }
        }

        public int quantidadePendenteDeEnvio
        {
            get
            {
                return TrocasPendentesDeEnvio.Count();
            }
        }

        public IEnumerable<CompraTrocaDTO> TrocasPendentes { get; set; }

        public IEnumerable<CompraTrocaDTO> TrocasPendentesDeEnvio { get; set; }
    }

    public class DetalheTrocaItemsVariacoesDTO {

        public string descricao { get; set; }

        public string valor { get; set; }
    }

    public class DetalheTrocaItemsDTO
    {
        public DetalheTrocaItemsDTO()
        {
            Variacoes = new List<DetalheTrocaItemsVariacoesDTO>();
        }

        public Guid id { get; set; }

        public int sequencial { get; set; }

        public int ordem { get; set; }

        public string nome { get; set; }

        public string observacao { get; set; }

        public decimal valor_pontos { get; set; }

        public decimal valor_monetario { get; set; }

        public string imagem { get; set; }

        public IEnumerable<DetalheTrocaItemsVariacoesDTO> Variacoes { get; set; }
    }

    public class DetalheTrocaDTO
    {
        public DetalheTrocaDTO()
        {
            Items = new List<DetalheTrocaItemsDTO>();
        }

        public Guid? id { get; set; }

        public int sequencial { get; set; }

        public string data { get; set; }

        public decimal total_pontos { get; set; }

        public decimal total_valor { get; set; }

        public decimal taxa_conversao { get; set; }

        public string cs_colaborador { get; set; }

        public string colaborador { get; set; }

        public string descricao { get; set; }

        public string loja { get; set; }

        public string label_loja { get; set; }

        public string loja_id { get; set; }

        public string situacao { get; set; }

        public string situacao_compra_id { get; set; }

        public string justificativa { get; set; }

        public string informacoes_complementares { get; set; }

        public IEnumerable<DetalheTrocaItemsDTO> Items { get; set; }
    }

    public class CombosTrocaDTO
    {
        public CombosTrocaDTO()
        {
            Lojas = new List<LojasDTO>();
            Opcoes = new List<OpcaoEntregaDTO>();
            VariacoesProduto = new List<VariacoesProdutoDTO>();
        }

        public IEnumerable<LojasDTO> Lojas { get; set; }

        public IEnumerable<OpcaoEntregaDTO> Opcoes { get; set; }

        public IEnumerable<VariacoesProdutoDTO> VariacoesProduto { get; set; }
    }

    public class MudaSituacaoDTO
    {
        public string compra_id { get; set; }

        public string situacao_compra_id { get; set; }

        public string justificativa { get; set; }

        public string informacoes_complementares { get; set; }
    }

    public class RelatorioComprasDTO
    {
        public string faturamento_id { get; set; }

        public string compra_id { get; set; }

        public string loja { get; set; }

        public string compra { get; set; }

        public string produto { get; set; }

        public string produtoDescricao { get; set; }

        public decimal total_pontos { get; set; }

        public decimal total_valor { get; set; }

        public string data { get; set; }

        public string situacao_compra { get; set; }

        public string situacao_compra_id { get; set; }

        public string cs { get; set; }

        public string colaborador { get; set; }

        public int pago { get; set; }

        public string label_pago {
            get
            {
                return pago == -1 ? "Não faturado" : (pago == 1 ? "Pago" : "Não Pago");
            }
        }
    }
}
