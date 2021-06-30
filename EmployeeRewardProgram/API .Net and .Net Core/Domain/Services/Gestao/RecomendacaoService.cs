using Database.Models.Gestao;
using Domain.DTO.Gestao;
using Domain.Helpers;
using Domain.Repositories.Gestao;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public class RecomendacaoService : BaseService<Recomendacao>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly RecomendacaoRepository _repository;

        /// <summary>
        /// Objeto do Reposirório de manipulação dos dados de Colaboradores
        /// </summary>
        private readonly ColaboradorRepository _colaboradorRepository;

        /// <summary>
        /// Objeto do Repositório de manipulação dos dados de Avaliações de Recomendações
        /// </summary>
        private readonly AvaliacaoRepository _avaliacaoRepository;

        /// <summary>
        /// Objeto do Repositório de manipulação do Saldo de Verbas para o Gestor
        /// </summary>
        private readonly VerbaRepository _verbaRepository;

        private readonly TipoRecomendacaoRepository _tipoRecomendacaoRepository;

        /// <summary>
        /// Injeção do Serviço para Envio de Emails relacionados a comunicações de Recomendações
        /// </summary>
        private readonly SendEmailService _emailService;
        
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public RecomendacaoService(RecomendacaoRepository repository, ColaboradorRepository colaboradorRepository, TipoRecomendacaoRepository tipoRecomendacaoRepository,
            AvaliacaoRepository avaliacaoRepository, VerbaRepository verbaRepository, SendEmailService emailService) : base(repository)
        {
            _repository = repository;
            _colaboradorRepository = colaboradorRepository;
            _tipoRecomendacaoRepository = tipoRecomendacaoRepository;
            _avaliacaoRepository = avaliacaoRepository;
            _verbaRepository = verbaRepository;
            _emailService = emailService;
        }

        public RecomendacaoDTO Recomendar(RecomendacaoDTO recomendacao, string cs_colaborador_logado, Guid logId)
        {
            var colaborador = _colaboradorRepository.FindByCS(recomendacao.cs);
            var colaboradorSolic = _colaboradorRepository.FindByCS(recomendacao.cs_solicitante);
            if (colaborador != null && colaborador.elegivel == "Sim" && colaboradorSolic != null)
            {
                var colaboradorGestor = _colaboradorRepository.FindByCS(colaborador.cs_superior_imediato);
                var tipoRecomendacao = _tipoRecomendacaoRepository.FindByID(recomendacao.tipo_recomendacao);

                if (colaborador.cs == recomendacao.cs_solicitante)
                {
                    throw new Exception("Não é possível recomendar pontos a seu favor");
                }

                var listaAvaliacoes = GerarAvaliacoes("0", recomendacao.cs, recomendacao.cs_solicitante);

                if (listaAvaliacoes.Count > 0)
                {
                    Recomendacao rec = new Recomendacao()
                    {
                        cs_colaborador = recomendacao.cs,
                        cs_colaborador_solicitante = recomendacao.cs_solicitante,
                        justificativa = recomendacao.justificativa,
                        qtde_pontos = Convert.ToDecimal(recomendacao.qtde_pontos),
                        tipo_recomendacao_id = recomendacao.tipo_recomendacao,
                        situacao_recomendacao_id = SituacaoRecomendacaoEnum.EmAnalise,
                        subordinado = colaborador.cs_superior_imediato == recomendacao.cs_solicitante,
                        ativo = true,
                        data_hora_criacao = DateTime.Now,
                        cs_colaborador_criacao = cs_colaborador_logado
                    };

                    rec = _repository.Add(rec, logId);
                    recomendacao.id = rec.id;

                    if (!string.IsNullOrEmpty(recomendacao.id.ToString()))
                    {
                        #region Avaliações

                        var avaliadorDiretor = listaAvaliacoes.Where(g => g.cargo_avaliador.TrimStart().TrimEnd().Contains("Diretor")).FirstOrDefault();
                        bool enviaEmailDiretor = avaliadorDiretor != null;

                        /// Tem mais de 1 Avaliador
                        if (listaAvaliacoes.Count() > 1 && enviaEmailDiretor)
                        {
                            listaAvaliacoes.Remove(listaAvaliacoes.Where(g => g.cargo_avaliador.TrimStart().TrimEnd().Contains("Diretor")).FirstOrDefault());
                            enviaEmailDiretor = false;
                        }

                        foreach (var aval in listaAvaliacoes)
                        {
                            try
                            {
                                _avaliacaoRepository.Add((new Avaliacao()
                                {
                                    ativo = aval.ativo,
                                    cs_colaborador_avaliador = aval.cs_colaborador_avaliador,
                                    data_avaliacao = aval.data_avaliacao,
                                    justificativa = aval.justificativa,
                                    recomendacao_id = recomendacao.id.ToString(),
                                    situacao_avaliacao_id = aval.situacao_avaliacao_id
                                }), logId);
                            }
                            catch (Exception exAval)
                            {
                                throw new Exception("Erro ao Gravar Avaliação para Recomendação.: " + exAval.Message);
                            } 
                        }

                        #endregion

                        #region Debitar Saldo de Verba do Solicitante

                        var saldoVerba = _verbaRepository.ObterSaldoVerbaGestor(recomendacao.cs_solicitante);
                        if (saldoVerba != null)
                        {
                            if (saldoVerba.quantidade_pontos >= Convert.ToDecimal(recomendacao.qtde_pontos))
                            {
                                saldoVerba.quantidade_pontos -= Convert.ToDecimal(recomendacao.qtde_pontos);
                                _verbaRepository.AtualizarSaldoVerbaGestor(saldoVerba.id.ToString(), saldoVerba.quantidade_pontos);
                            }
                            else
                            {

                                var listaAvaliacoesGravadas = _repository.AvaliacoesRecomendacao(recomendacao.id.ToString());
                                foreach (var aval in listaAvaliacoesGravadas)
                                {
                                    try
                                    {
                                        _avaliacaoRepository.Remove(aval.id.ToString(), cs_colaborador_logado, logId);
                                    }
                                    catch (Exception exAval)
                                    {
                                        throw new Exception("Erro ao Remover Avaliação para Recomendação.: " + exAval.Message);
                                    }
                                }

                                throw new Exception("Recomendação não gravada no banco de dados - Seu saldo de pontos.: " + 
                                    saldoVerba.quantidade_pontos.ToString() + 
                                    " é inferior a quantidade de pontos da recomendação.: " + recomendacao.qtde_pontos);
                            }
                        }

                        #endregion

                        #region Enviar Emails
                        try
                        {
                            //Não travar no Envio de Email
                            string corpoEmail = Templates.HTML_EMAIL_NOVA_RECOMENDACAO;

                            if (enviaEmailDiretor && avaliadorDiretor != null)
                            {
                                corpoEmail = corpoEmail.Replace("#DESTINATARIO#", avaliadorDiretor.nome_colaborador_avaliador);
                                corpoEmail = corpoEmail.Replace("#CODIGO#", rec.sequencial.ToString());
                                corpoEmail = corpoEmail.Replace("#DATA#", rec.data_hora_criacao.ToString("dd/MM/yyyy"));
                                corpoEmail = corpoEmail.Replace("#CSSOLIC#", colaboradorSolic.cs);
                                corpoEmail = corpoEmail.Replace("#NOMESOLIC#", colaboradorSolic.nome);
                                corpoEmail = corpoEmail.Replace("#CS#", colaborador.cs);
                                corpoEmail = corpoEmail.Replace("#NOME#", colaborador.nome);
                                corpoEmail = corpoEmail.Replace("#CSGESTOR#", colaboradorGestor.cs);
                                corpoEmail = corpoEmail.Replace("#NOMEGESTOR#", colaboradorGestor.nome);
                                corpoEmail = corpoEmail.Replace("#PONTOS#", recomendacao.qtde_pontos.ToString());
                                corpoEmail = corpoEmail.Replace("#MOTIVO#", (tipoRecomendacao != null ? tipoRecomendacao.nome : ""));
                                corpoEmail = corpoEmail.Replace("#JUSTIFICATIVA#", recomendacao.justificativa);
                                corpoEmail = corpoEmail.Replace("#TEXTOAPROVAADICIONAL#", "O colaborador <strong>" + colaborador.nome + "</strong> recebeu uma nova recomendação, não será necessário sua intervenção.");
                                _emailService.SendAsync(corpoEmail, "Novo Reconhecimento para Colaborador", avaliadorDiretor.email_colaborador_avaliador);
                            }

                            foreach (var aval in listaAvaliacoes)
                            {
                                if (enviaEmailDiretor && aval.email_colaborador_avaliador == avaliadorDiretor.email_colaborador_avaliador)
                                    continue;

                                corpoEmail = corpoEmail = Templates.HTML_EMAIL_NOVA_RECOMENDACAO;
                                corpoEmail = corpoEmail.Replace("#DESTINATARIO#", aval.nome_colaborador_avaliador);
                                corpoEmail = corpoEmail.Replace("#CODIGO#", rec.sequencial.ToString());
                                corpoEmail = corpoEmail.Replace("#DATA#", rec.data_hora_criacao.ToString("dd/MM/yyyy"));
                                corpoEmail = corpoEmail.Replace("#CSSOLIC#", colaboradorSolic.cs);
                                corpoEmail = corpoEmail.Replace("#NOMESOLIC#", colaboradorSolic.nome);
                                corpoEmail = corpoEmail.Replace("#CS#", colaborador.cs);
                                corpoEmail = corpoEmail.Replace("#NOME#", colaborador.nome);
                                corpoEmail = corpoEmail.Replace("#CSGESTOR#", colaboradorGestor.cs);
                                corpoEmail = corpoEmail.Replace("#NOMEGESTOR#", colaboradorGestor.nome);
                                corpoEmail = corpoEmail.Replace("#PONTOS#", recomendacao.qtde_pontos.ToString());
                                corpoEmail = corpoEmail.Replace("#MOTIVO#", (tipoRecomendacao != null ? tipoRecomendacao.nome : ""));
                                corpoEmail = corpoEmail.Replace("#JUSTIFICATIVA#", recomendacao.justificativa);
                                corpoEmail = corpoEmail.Replace("#TEXTOAPROVAADICIONAL#", "O colaborador <strong>" + colaborador.nome + "</strong> recebeu uma nova recomendação, por favor acesse seu painel e avalie esta recomendação!");

                                _emailService.SendAsync(corpoEmail, "Novo Reconhecimento para Colaborador", aval.email_colaborador_avaliador);
                            }
                        }
                        catch
                        {
                        }

                        #endregion
                    }
                    else
                        throw new Exception("Recomendação não gravada no banco de dados.");

                    return recomendacao;
                }
                else
                {
                    throw new Exception("Colaborador sem gestor que possa Avaliar - CS.: " + recomendacao.cs);
                }

            }
            else
            {
                if (colaborador != null && colaborador.elegivel != "Sim")
                    throw new Exception("Colaborador não elegível - CS.: " + recomendacao.cs);
                else
                    throw new Exception("Colaborador não localizado pelo CS.: " + recomendacao.cs);
            }
        }

        public List<AvaliacaoDTO> GerarAvaliacoes(string recomendacaoId = "", string csColaborador = "", string csSolicitante = "")
        {
            List<AvaliacaoDTO> listaAvaliacoes = new List<AvaliacaoDTO>();

            var colaborador = _colaboradorRepository.FindByCS(csColaborador);
            var solicitante = _colaboradorRepository.FindByCS(csSolicitante);
            
            if (colaborador != null && solicitante != null)
            {
                /// Se o Colaborador não tem Superior, algo está errado
                if (String.IsNullOrEmpty(colaborador.cs_superior_imediato))
                {
                    throw new Exception("Colaborador não tem Gestor");
                }

                ///
                /// Se o solicitante é o próprio gestor do Colaborador, quem aprova é seu Gestor
                /// Ou superior próximo que seja Gerente ou Gerente Executivo
                ///
                if (colaborador.cs_superior_imediato == csSolicitante)
                {
                    #region Solicitante com Gestor
                    /// Solicitante tem que ter Superior ou ter um cargo de Diretor em diante
                    if (!string .IsNullOrEmpty(solicitante.cs_superior_imediato) ||
                        (solicitante.cargo.TrimStart().TrimEnd().Contains("Diretor") || (solicitante.cargo.TrimStart().TrimEnd().Contains("Presidente"))))
                    {
                        #region Superior é Presidente ou Vice
                        /// Superior imediato do Colaborador é o Solicitante e o cargo do Solicitante é Presidente ou VP
                        /// Já entra a avaliação como aprovada diretamente
                        if (solicitante.cargo.TrimStart().TrimEnd().Contains("Presidente"))
                        {
                            listaAvaliacoes.Add(new AvaliacaoDTO()
                            {
                                ativo = true,
                                cargo_avaliador = solicitante.cargo,
                                cs_colaborador_avaliador = solicitante.cs,
                                nome_colaborador_avaliador = solicitante.nome,
                                email_colaborador_avaliador = solicitante.email,
                                justificativa = "Aprovado Automaticamente - Superior imediato do Colaborador é o Solicitante e o cargo do Solicitante é Presidente ou Vice Presidente",
                                data_avaliacao = DateTime.Now,
                                recomendacao_id = recomendacaoId,
                                situacao_avaliacao_id = SituacaoAvaliacaoEnum.Aprovada
                            });
                        }
                        #endregion
                        #region Verifica Avaliadores
                        else
                        {
                            #region Verifica o Cargo dos Gestores Avaliadores

                            /// Obtém os Gestores do Solicitante como Avaliadores
                            var listaAvaliadores = _colaboradorRepository.ListarGestoresAvaliadores(csSolicitante);

                            /// Parte da premissa que o Cargo que está Recomendando é Coordenador no mínimo
                            listaAvaliadores = listaAvaliadores.Where(g => g.cargo.Contains("Gerente") || g.cargo.Contains("Diretor"));

                            if (listaAvaliadores.Count() > 0)
                            {
                                foreach (var gestorAvaliador in listaAvaliadores)
                                {
                                    var objAvaliacao = new AvaliacaoDTO()
                                    {
                                        ativo = true,
                                        cs_colaborador_avaliador = gestorAvaliador.cs_gestor,
                                        cargo_avaliador = gestorAvaliador.cargo,
                                        email_colaborador_avaliador = gestorAvaliador.email,
                                        nome_colaborador_avaliador = gestorAvaliador.nome,
                                        justificativa = string.Empty,
                                        data_avaliacao = DateTime.Now,
                                        recomendacao_id = recomendacaoId,
                                        situacao_avaliacao_id = SituacaoAvaliacaoEnum.Pendente
                                    };

                                    if (!listaAvaliacoes.Contains(objAvaliacao))
                                        listaAvaliacoes.Add(objAvaliacao);
                                }
                            }
                            else
                            {
                                throw new Exception("O Gestor do colaborador é o próprio solicitante, " +
                                "sendo assim, o superior do solicitante deve aprovar a recomendação, " +
                                "porém o superior deste Gestor.: " + solicitante.nome + " não foi localizado no sistema ou " + 
                                "o cargo do superior deste gestor não é Gerente ou Diretor");
                            }

                            #endregion
                        }
                        #endregion

                    }
                    #endregion
                    #region Exceção Sem Gestor
                    else
                    {
                        throw new Exception("O Gestor do colaborador é o próprio solicitante, " +
                                 "sendo assim, o superior do solicitante deve aprovar a recomendação, " +
                                 "porém o superior deste Gestor.: " + solicitante.nome + " não foi localizado no sistema ou " +
                                 "o cargo deste gestor não é Diretor, Presidente ou Vice Presidente");
                    }
                    #endregion
                }
                else
                {
                    #region Verifica o Cargo dos Gestores Avaliadores

                    /// Obtém os Gestores do Solicitante como Avaliadores
                    var listaAvaliadores = _colaboradorRepository.ListarGestoresAvaliadores(colaborador.cs);

                    /// Entre os Avaliadores do Colaborador é necessário que sejam um dos cargos
                    /// Coordenador, Gerente ou Diretor
                    listaAvaliadores = listaAvaliadores.Where(g => g.cargo.Contains("Coordenador") || g.cargo.Contains("Gerente") || g.cargo.Contains("Diretor"));

                    if (listaAvaliadores.Count() > 0)
                    {
                        foreach (var gestorAvaliador in listaAvaliadores)
                        {
                            var objAvaliacao = new AvaliacaoDTO()
                            {
                                ativo = true,
                                cs_colaborador_avaliador = gestorAvaliador.cs_gestor,
                                cargo_avaliador = gestorAvaliador.cargo,
                                email_colaborador_avaliador = gestorAvaliador.email,
                                nome_colaborador_avaliador = gestorAvaliador.nome,
                                justificativa = string.Empty,
                                data_avaliacao = DateTime.Now,
                                recomendacao_id = recomendacaoId,
                                situacao_avaliacao_id = SituacaoAvaliacaoEnum.Pendente
                            };

                            if (!listaAvaliacoes.Contains(objAvaliacao))
                                listaAvaliacoes.Add(objAvaliacao);
                        }
                    }
                    else
                    {
                        throw new Exception("Não foi possível localizar o Gestor que seja Coordenador, Gerente ou Diretor para avaliar esta recomendação");
                    }

                    #endregion
                }
            }
            else
                throw new Exception("Colaborador não localizado pelo CS.: " + csColaborador);

            return listaAvaliacoes;
        }

        public List<SituacaoRecomendacaoDTO> StatusRecomendacoesColaborador(string cs)
        {
            return _repository.StatusRecomendacoesColaborador(cs).ToList();
        }

        public List<SituacaoRecomendacaoDTO> StatusRecomendacoesGestor(string cs)
        {
            return _repository.StatusRecomendacoesGestor(cs).ToList();
        }

        public DetalhesStatusRecomendacaoModelDTO DetalheRecomendacao(string id)
        {
            return _repository.DetalheRecomendacao(id);
        }

        public IndicadorQuantitativoRecomendacoesDTO ObterQuantitativo()
        {
            return _repository.ObterQuantitativo();
        }
    }
}