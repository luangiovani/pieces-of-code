using Domain.DTO.Loja;
using System;
using System.Collections.Generic;

namespace Domain.DTO.Gestao
{
    public class ColaboradoresDTO
    {
    }

    public class TrocasExtratoColaboradorDTO
    {
        public Guid id { get; set; }

        public string nome { get; set; }

        public decimal valor { get; set; }

        public decimal pontos { get; set; }

        public string data { get; set; }
    }

    public class ExtratoColaboradorDTO
    {
        public ExtratoColaboradorDTO()
        {
            Trocas = new List<TrocasExtratoColaboradorDTO>();
        }

        public Guid id { get; set; }

        public string cs { get; set; }

        public string nome { get; set; }

        public decimal tkusRecebidos { get; set; }

        public decimal tkusUtilizados { get; set; }

        public decimal tkusDisponiveis { get; set; }

        public decimal tkusPendentes { get; set; }

        public decimal expirados { get; set; }

        public decimal aExpirar { get; set; }

        public int recs { get; set; }

        public IEnumerable<TrocasExtratoColaboradorDTO> Trocas { get; set; }
    }

    public class ColaboradorTrocaDTO
    {
        public ColaboradorTrocaDTO()
        {
            Produtos = new List<ProdutosColaboradorTrocaDTO>();
        }

        public Guid id { get; set; }

        public string cs { get; set; }

        public string nome { get; set; }

        public decimal tkusRecebidos { get; set; }

        public decimal tkusUtilizados { get; set; }

        public decimal tkusDisponiveis { get; set; }

        public IEnumerable<ProdutosColaboradorTrocaDTO> Produtos { get; set; }
    }

    public class PerfilColaboradorDTO
    {
        public string colaborador_id { get; set; }

        public string perfil_id { get; set; }

        public string perfil { get; set; }
    }

    public class GestoresDTO
    {
        public string cs { get; set; }

        public string nome { get; set; }
    }

    public class GestorAvaliadorDTO
    {
        public string cs_gestor { get; set; }

        public string cargo { get; set; }

        public string email { get; set; }

        public string nome { get; set; }

        public int nivel { get; set; }
    }

    public class ItemCompraDTO
    {
        public Guid? id { get; set; }

        public int sequencial { get; set; }

        public string compra_id { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public decimal valor_pontos { get; set; }

        public decimal valor_monetario { get; set; }

        public string observacao { get; set; }
    }

    public class ComprasRealizadasCompradorDTO
    {
        public ComprasRealizadasCompradorDTO()
        {
            Items = new List<ItemCompraDTO>();
        }

        public Guid? id { get; set; }

        public int sequencial { get; set; }

        public string cs { get; set; }

        public string nome { get; set; }

        public string data { get; set; }

        public decimal pontos { get; set; }

        public decimal valor { get; set; }

        public string situacao { get; set; }

        public string situacao_id { get; set; }

        public string produtos { get; set; }

        public string informacoes_complementares { get; set; }

        public List<ItemCompraDTO> Items { get; set; }
    }

    public class DashboardDTO
    {
        public DashboardDTO()
        {
            extrato = new ExtratoColaboradorDTO();
            recomendacoesAprovadas = new List<SituacaoRecomendacaoDTO>();
            recomendacoesPendentes = new List<SituacaoRecomendacaoDTO>();
            comprasRealizadas = new List<ComprasRealizadasCompradorDTO>();
        }

        public ExtratoColaboradorDTO extrato { get; set; }

        public IEnumerable<SituacaoRecomendacaoDTO> recomendacoesPendentes { get; set; }

        public IEnumerable<SituacaoRecomendacaoDTO> recomendacoesAprovadas { get; set; }

        public IEnumerable<ComprasRealizadasCompradorDTO> comprasRealizadas { get; set; }
    }
}
