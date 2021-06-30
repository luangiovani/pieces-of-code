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
    public class AvaliacaoService : BaseService<Avaliacao>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly AvaliacaoRepository _repository;

        /// <summary>
        /// Objeto do Repositório de manipulação de recomendações
        /// </summary>
        private readonly RecomendacaoRepository _recomendacaoRepository;

        /// <summary>
        /// Objeto do Repositório de manipulação de recomendações
        /// </summary>
        private readonly ColaboradorRepository _colaboradorRepository;

        /// <summary>
        /// Objeto do Repositório de manipulação de Expiração de Pontos para Colaborador
        /// </summary>
        private readonly ExpiracaoPontosColaboradorRepository _expiracaoPontosColaboradorRepository;

        private readonly ConfiguracaoExpiracaoRepository _configuracaoExpiracaoRepository;

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
        public AvaliacaoService(AvaliacaoRepository repository, RecomendacaoRepository recomendacaoRepository,
            ColaboradorRepository colaboradorRepository, ExpiracaoPontosColaboradorRepository expiracaoPontosColaboradorRepository,
            TipoRecomendacaoRepository tipoRecomendacaoRepository, ConfiguracaoExpiracaoRepository configuracaoExpiracaoRepository,
            VerbaRepository verbaRepository, SendEmailService emailService) : base(repository)
        {
            _repository = repository;
            _recomendacaoRepository = recomendacaoRepository;
            _colaboradorRepository = colaboradorRepository;
            _expiracaoPontosColaboradorRepository = expiracaoPontosColaboradorRepository;
            _configuracaoExpiracaoRepository = configuracaoExpiracaoRepository;
            _verbaRepository = verbaRepository;
            _emailService = emailService;
            _tipoRecomendacaoRepository = tipoRecomendacaoRepository;
        }

        /// <summary>
        /// Cadastrar uma nova Avaliação no banco de dados
        /// </summary>
        /// <param name="dto">Objeto com as informações da Avaliação para inserção/atualização</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado que está realizando a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto aplicacao cadastrado</returns>
        public AvaliacaoDTO Gravar(AvaliacaoDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.justificativa) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                Avaliacao objAvaliacao;
                if (string.IsNullOrEmpty(dto.id.ToString()))
                {
                    objAvaliacao = new Avaliacao()
                    {
                        justificativa = dto.justificativa,
                        cs_colaborador_avaliador = dto.cs_colaborador_avaliador,
                        recomendacao_id = dto.recomendacao_id,
                        situacao_avaliacao_id = dto.situacao_avaliacao_id,
                        ativo = true,
                        data_avaliacao = DateTime.Now,
                        data_hora_alteracao = DateTime.Now,
                        cs_colaborador_alteracao = cs_colaborador_logado
                    };
                }
                else
                {
                    objAvaliacao = _repository.FindByID(dto.id.ToString());

                    if (objAvaliacao != null)
                    {
                        objAvaliacao.justificativa = dto.justificativa;
                        objAvaliacao.cs_colaborador_avaliador = dto.cs_colaborador_avaliador;
                        objAvaliacao.recomendacao_id = dto.recomendacao_id;
                        objAvaliacao.situacao_avaliacao_id = dto.situacao_avaliacao_id;
                        objAvaliacao.ativo = true;
                        objAvaliacao.data_avaliacao = DateTime.Now;
                        objAvaliacao.data_hora_alteracao = DateTime.Now;
                        objAvaliacao.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Avaliação não encontrada para analisar.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objAvaliacao, logOperacaoId).id;
                else
                    _repository.Update(objAvaliacao, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para Avaliação não foram preenchidos");
            }
        }

        /// <summary>
        /// Realizar a Avaliação de uma Recomendação
        /// </summary>
        /// <param name="dto">Objeto com a informação da Avaliação da Recomendação</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado que está realizando a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto de Avaliação</returns>
        public AvaliarDTO Avaliar(AvaliarDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (!string.IsNullOrEmpty(dto.id.ToString()) &&
                !string.IsNullOrEmpty(dto.justificativa))
            {
                #region Verificar a recomendação

                var objAvaliacao = _repository.FindByID(dto.id.ToString());
                string situacao_avaliacao_id = string.Empty;
                string justificativa = string.Empty;
                string cs_colaborador_alteracao = string.Empty;
                DateTime? data_hora_alteracao = null;
                DateTime? data_avaliacao = null;               

                if (objAvaliacao != null)
                {
                    var recomendacao = _recomendacaoRepository.FindByID(objAvaliacao.recomendacao_id);
                    var tipoRecomendacao = recomendacao != null ? _tipoRecomendacaoRepository.FindByID(recomendacao.tipo_recomendacao_id).nome : "";
                    if (recomendacao == null)
                    {
                        _repository.Remove(objAvaliacao.id.ToString(), cs_colaborador_logado, logOperacaoId);
                        throw new InvalidOperationException("Não existe recomendação para esta Avaliação, a mesma foi inutilizada.");
                    }

                    if (cs_colaborador_logado == recomendacao.cs_colaborador)
                    {
                        throw new Exception("Não é possível avaliar uma recomendação realizada para você.");
                    }

                    situacao_avaliacao_id = objAvaliacao.situacao_avaliacao_id;
                    justificativa = objAvaliacao.justificativa;
                    cs_colaborador_alteracao = objAvaliacao.cs_colaborador_alteracao;
                    data_hora_alteracao = objAvaliacao.data_hora_alteracao;
                    data_avaliacao = objAvaliacao.data_avaliacao;
                    try
                    {
                        objAvaliacao.situacao_avaliacao_id = dto.aprovar ? SituacaoAvaliacaoEnum.Aprovada : SituacaoAvaliacaoEnum.Reprovada;
                        objAvaliacao.justificativa = dto.justificativa;
                        objAvaliacao.cs_colaborador_alteracao = cs_colaborador_logado;
                        objAvaliacao.data_hora_alteracao = DateTime.Now;
                        objAvaliacao.data_avaliacao = DateTime.Now;

                        _repository.Update(objAvaliacao, logOperacaoId);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ocorreu um erro ao tentar atualizar a Avaliação.: " + ex.Message);
                    }

                    try
                    {
                        #region Obter Avaliações

                        var listaAvaliacoes = _recomendacaoRepository.AvaliacoesRecomendacao(objAvaliacao.recomendacao_id);
                        var avalAprovadas = listaAvaliacoes.Where(a => a.situacao_avaliacao_id == SituacaoAvaliacaoEnum.Aprovada);
                        var avalReprovadas = listaAvaliacoes.Where(a => a.situacao_avaliacao_id == SituacaoAvaliacaoEnum.Reprovada);

                        #endregion

                        bool aprovada = listaAvaliacoes.Count() == avalAprovadas.Count();

                        /// Recomendação Aprovada, todas as avaliações foram aprovadas
                        if (aprovada)
                        {
                            string situacao_recomendacao_id = recomendacao.situacao_recomendacao_id;

                            #region Atualiza Situação da Recomendação

                            recomendacao.situacao_recomendacao_id = SituacaoRecomendacaoEnum.Aprovada;
                            try
                            {
                                _recomendacaoRepository.Update(recomendacao, logOperacaoId);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Ocorreu um erro ao tentar Atualizar a Situação da Recomendação.: " + ex.Message);
                            }

                            #endregion

                            try
                            {
                                #region Atualiza pontos Colaborador
                                var colab = _colaboradorRepository.FindByCS(recomendacao.cs_colaborador);
                                decimal quantidade_pontos = colab.quantidade_pontos;
                                colab.quantidade_pontos += recomendacao.qtde_pontos;
                                try
                                {
                                    _colaboradorRepository.AtualizarPontosColaborador(colab, cs_colaborador_logado, logOperacaoId);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Ocorreu um erro ao tentar Atualizar a Quantidade de Pontos do Colaborador.: " + ex.Message);
                                }
                                #endregion

                                #region Insere a Expiração de Pontos do Colaborador

                                try
                                {
                                    var configExpiracao = _configuracaoExpiracaoRepository.FindAll().FirstOrDefault();

                                    var dtHoje = DateTime.Now;

                                    if (configExpiracao != null)
                                    {
                                        var dQtde = Convert.ToDouble(configExpiracao.qtde_expiracao);

                                        if (dQtde > 0)
                                        {
                                            switch (configExpiracao.tipo_expiracao)
                                            {
                                                case "H":
                                                    dtHoje = dtHoje.AddHours(dQtde);
                                                    break;
                                                case "D":
                                                    dtHoje = dtHoje.AddDays(dQtde);
                                                    break;
                                                case "S":
                                                    dtHoje = dtHoje.AddDays(dQtde * 7);
                                                    break;
                                                case "M":
                                                    dtHoje = dtHoje.AddMonths((int)dQtde);
                                                    break;
                                                case "A":
                                                    dtHoje = dtHoje.AddYears((int)dQtde);
                                                    break;
                                            }

                                            _expiracaoPontosColaboradorRepository.Add(new ExpiracaoPontosColaborador()
                                            {
                                                ativo = true,
                                                cs_colaborador_criacao = cs_colaborador_logado,
                                                data_expiracao = dtHoje,
                                                data_hora_criacao = DateTime.Now,
                                                recomendacao_id = objAvaliacao.recomendacao_id
                                            }, logOperacaoId);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    colab.quantidade_pontos = quantidade_pontos;
                                    try
                                    {
                                        _colaboradorRepository.AtualizarPontosColaborador(colab, cs_colaborador_logado, logOperacaoId);
                                    }
                                    catch { }

                                    throw new Exception("Ocorreu um erro ao tentar Gravar a expiração de pontos do Colaborador.: " + ex.Message);
                                }

                                #endregion

                                #region Envia Email ao Colaborador 

                                try
                                {
                                    if (!string.IsNullOrEmpty(colab.email))
                                    {
                                        string corpoEmail = Templates.HTML_RECOMENDACAO_APROVADA;
                                        corpoEmail = corpoEmail.Replace("#DESTINATARIO#", colab.nome);
                                        corpoEmail = corpoEmail.Replace("#PONTOS#", recomendacao.qtde_pontos.ToString());
                                        corpoEmail = corpoEmail.Replace("#MOTIVO#", (!string.IsNullOrEmpty(tipoRecomendacao) ? tipoRecomendacao : ""));
                                        corpoEmail = corpoEmail.Replace("#JUSTIFICATIVA#", recomendacao.justificativa);
                                        _emailService.SendAsync(corpoEmail, "Novo Reconhecimento para Colaborador", colab.email);
                                    }
                                }
                                catch
                                {
                                }

                                #endregion
                            }
                            catch (Exception ex)
                            {
                                recomendacao.situacao_recomendacao_id = situacao_recomendacao_id;
                                try
                                {
                                    _recomendacaoRepository.Update(recomendacao, logOperacaoId);
                                }
                                catch { }
                                throw ex;
                            }
                        }
                        /// Recomendação Reprovada
                        else if (avalReprovadas.Count() > 0)
                        {
                            #region Atualiza Situação da Recomendação
                            recomendacao.situacao_recomendacao_id = SituacaoRecomendacaoEnum.Reprovada;
                            try
                            {
                                _recomendacaoRepository.Update(recomendacao, logOperacaoId);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Ocorreu um erro ao tentar Atualizar a Situação da Recomendação.: " + ex.Message);
                            }
                            #endregion

                            #region Atualizar Colaborador Solicitante

                            var solicitante = _colaboradorRepository.FindByCS(recomendacao.cs_colaborador_solicitante);

                            if (solicitante != null)
                            {
                                #region Obter saldo de Verba do Gestor

                                var saldoVerba = _verbaRepository.ObterSaldoVerbaGestor(solicitante.cs);

                                if (saldoVerba != null)
                                {
                                    _verbaRepository.AtualizarSaldoVerbaGestor(saldoVerba.id.ToString(), recomendacao.qtde_pontos);
                                }

                                #endregion
                            }

                            #endregion
                        }

                    }
                    catch (Exception ex)
                    {
                        objAvaliacao.situacao_avaliacao_id = situacao_avaliacao_id;
                        objAvaliacao.justificativa = justificativa;
                        objAvaliacao.cs_colaborador_alteracao = cs_colaborador_alteracao;
                        objAvaliacao.data_hora_alteracao = data_hora_alteracao;
                        objAvaliacao.data_avaliacao = data_avaliacao;
                        _repository.Update(objAvaliacao, logOperacaoId);

                        throw ex;
                    }

                    return dto;
                }
                else
                    throw new InvalidOperationException("A Avaliação não foi localizada no banco de dados");

                #endregion
            }
            else
                throw new InvalidOperationException("Os campos para Avaliação não foram preenchidos");
        }

        /// <summary>
        /// Recupera a Listagem de Avaliações Pendentes de Análise pelo Gestor
        /// </summary>
        /// <param name="cs_gestor">CS do Gestor que está listando as Avaliações</param>
        /// <returns>Listagem de Avaliações do Gestor que estão Pendentes</returns>
        public IEnumerable<AvaliacoesPendentesGestorDTO> ListarAvaliacoesPendendesGestor(string cs_gestor)
        {
            return _repository.ListarAvaliacoesPendendesGestor(cs_gestor);
        }

        public IEnumerable<AvaliacoesRealizadasGestorDTO> ListarAvaliacoesRealizadasGestor(string cs_gestor)
        {
            return _repository.ListarAvaliacoesRealizadasGestor(cs_gestor);
        }
    }
}