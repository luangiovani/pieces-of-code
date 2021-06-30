using Database.Models.Gestao;
using Domain.DTO.APP;
using Domain.DTO.Gestao;
using Domain.Repositories.Gestao;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
    public class ColaboradorService : BaseService<Colaborador>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly ColaboradorRepository _repository;

        private readonly RecomendacaoRepository _recomendacaoRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public ColaboradorService(ColaboradorRepository repository,
                                  RecomendacaoRepository recomendacaoRepository) : base(repository)
        {
            _repository = repository;
            _recomendacaoRepository = recomendacaoRepository;
        }

        /// <summary>
        /// Listar os colaboradores subordinados (meuTime = true) ou não do Gestor (meuTime = false)
        /// </summary>
        /// <param name="gestorId">CS do Gestor que está sendo listados os colaboradores</param>
        /// <param name="meuTime">Variável condicional que demonstra se os colaboradores listados são subordinados ao Gestor ou não</param>
        /// <returns>Lista de Colaboradores</returns>
        public IEnumerable<Colaborador> ListarColaboradores(string gestorId, bool meuTime, bool comTrocas)
        {
            if (comTrocas)
                return _repository.ListarColaboradoresComTrocas(gestorId, meuTime);
            else
                return _repository.ListarColaboradores(gestorId, meuTime);
        }

        public IEnumerable<Colaborador> ListarColaboradores()
        {
            return _repository.ListarColaboradores();
        }

        public IEnumerable<GestoresDTO> ListarGestores()
        {
            return _repository.ListarGestores();
        }

        /// <summary>
        /// Obter registro pelo ID informado
        /// </summary>
        /// <param name="id">Id do registro que se deseja obter o detalhamento</param>
        /// <returns>Objeto Model localizado pelo Id Informado</returns>
        public virtual Colaborador FindByCS(string cs)
        {
            Regex regexObj = new Regex(@"[^\d]");
            cs = regexObj.Replace(cs, "");
            return _repository.FindByCS(cs);
        }

        /// <summary>
        /// Relatório da Pontuação por Colaborador
        /// </summary>
        /// <param name="cs_gestor">Código CS do Colaborador Gestor</param>
        /// <returns>Listagem de Pontuação por Colaboradores</returns>
        public virtual IEnumerable<RelatorioPontuacaoDTO> RelatorioPontuacao(string cs_gestor)
        {
            List<RelatorioPontuacaoDTO> listaRetorno = new List<RelatorioPontuacaoDTO>();

            var lista = _repository.RelatorioPontuacao(cs_gestor);

           string csLido = string .Empty;
            RelatorioPontuacaoDTO objRelatorio = null;
            decimal totalPontos = 0;

            foreach (var item in lista)
            {
               string csAtual = item.cs_colaborador;

                if (csAtual != csLido)
                {
                    if (!string .IsNullOrEmpty(csLido))
                        listaRetorno.Add(objRelatorio);

                    objRelatorio = item;
                    totalPontos = Convert.ToDecimal(item.qtde_pontos);

                    csLido = csAtual;
                }
                else
                {
                    totalPontos += Convert.ToDecimal(item.qtde_pontos);
                    objRelatorio.qtde_pontos = totalPontos.ToString();
                }
            }

            if (!listaRetorno.Contains(objRelatorio))
                listaRetorno.Add(objRelatorio);

            return listaRetorno;
        }

        public virtual RelatorioPontuacaoDTO DetalheRelatorioPontuacao(string cs_colaborador, string cs_gestor)
        {
            var objRelatorio = _repository.DetalheRelatorioPontuacao(cs_colaborador, cs_gestor);

            if (objRelatorio != null)
            {
                objRelatorio.Relatorio = _recomendacaoRepository.ObterRecomendacoesColaborador(cs_colaborador, cs_gestor);
            }

            return objRelatorio;
        }

        public ExtratoColaboradorDTO ExtratoColaborador(string cs_colaborador)
        {
            return _repository.ExtratoColaborador(cs_colaborador);
        }

        public IEnumerable<ExtratoColaboradorDTO> ExtratoColaboradoresGestor(string cs_gestor)
        {
            return _repository.ExtratoColaboradoresGestor(cs_gestor);
        }

        public IEnumerable<RecomendacoesColaboradorDTO> ObterRecomendacoes(string cs_colaborador)
        {
            return _repository.ObterRecomendacoes(cs_colaborador);
        }

        public ColaboradorTrocaDTO ObterExtratoParaTroca(string cs_colaborador)
        {
            return _repository.ObterExtratoParaTroca(cs_colaborador);
        }

        public UsuarioDTO ObterUsuarioOpcoesMenu(string cs, string aplicacao_id)
        {
            return _repository.ObterUsuarioOpcoesMenu(cs, aplicacao_id);
        }

        public void AtualizarPerfil(PerfilColaboradorDTO dto, string cs_colaborador, Guid logOperacaoId)
        {
            _repository.AtualizarPerfil(dto, cs_colaborador, logOperacaoId);
        }

        public IEnumerable<GestorAvaliadorDTO> ListarGestoresAvaliadores(string cs_colaborador)
        {
            return _repository.ListarGestoresAvaliadores(cs_colaborador);
        }

        public IEnumerable<ComprasRealizadasCompradorDTO> ListarComprasColaboradores(string cs)
        {
            return _repository.ListarComprasColaboradores(cs);
        }

        public IEnumerable<ComprasRealizadasCompradorDTO> ListarComprasColaboradoresGestor(string cs_gestor)
        {
            return _repository.ListarComprasColaboradoresGestor(cs_gestor);
        }

        public Colaborador ObterColaboradorLoja(string cs, string loja_id)
        {
            return _repository.ObterColaboradorLoja(cs, loja_id);
        }
    }
}