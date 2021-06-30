using Database.Models.Gestao;
using Domain.DTO.Gestao;
using Domain.Helpers;
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
    public class VerbaService : BaseService<Verba>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly VerbaRepository _repository;

        /// <summary>
        /// Objeto do repositório de Colaboradores
        /// </summary>
        private readonly ColaboradorRepository _colaboradorRepository;

        /// <summary>
        /// Injeção do Serviço para Envio de Emails relacionados a comunicações de Recomendações
        /// </summary>
        private readonly SendEmailService _emailService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public VerbaService(VerbaRepository repository, SendEmailService emailService, ColaboradorRepository colaboradorRepository) : base(repository)
        {
            _repository = repository;
            _emailService = emailService;
            _colaboradorRepository = colaboradorRepository;
        }

        public ExtratoAtribuicoesDTO RelatorioDeExtratoDeAtribuicoes()
        {
            return _repository.RelatorioDeExtratoDeAtribuicoes();
        }

        /// <summary>
        /// Atribuir Verba para Gestor
        /// </summary>
        /// <param name="dto">Objeto com as informações da atribuição de verba para inserção/atualização</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado que está realizando a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto verba cadastrado</returns>
        public VerbaDTO Gravar(VerbaDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.cs_colaborador) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                Verba objVerba;
                var colaborador = _colaboradorRepository.FindByCS(dto.cs_colaborador);
                if (colaborador != null)
                {
                    if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    {
                        objVerba = new Verba()
                        {
                            cs_colaborador = dto.cs_colaborador,
                            data_atribuicao = DateTime.Now,
                            observacao = dto.observacao,
                            taxa_conversao = dto.taxa_conversao,
                            valor_moeda = dto.valor_moeda,
                            valor_pontos = dto.valor_pontos,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };
                    }
                    else
                    {
                        var existente = _repository.FindByID(dto.id.ToString());
                        if (existente != null)
                        {
                            objVerba = existente;
                            objVerba.ativo = dto.ativo;
                            objVerba.valor_moeda = dto.valor_moeda;
                            objVerba.valor_pontos = dto.valor_pontos;
                            objVerba.data_hora_alteracao = DateTime.Now;
                            objVerba.cs_colaborador_alteracao = cs_colaborador_logado;
                            dto.id = objVerba.id;
                        }
                        else
                            throw new InvalidOperationException("Verba não encontrada para efetuar a atualização.");
                    }

                    if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                        dto.id = _repository.Add(objVerba, logOperacaoId).id;
                    else
                        _repository.Update(objVerba, logOperacaoId);

                    #region Envia Email Verba

                    try
                    {
                        string corpoEmail = Templates.HTML_VERBA_GESTOR;

                        corpoEmail = corpoEmail.Replace("#DESTINATARIO#", colaborador.nome);
                        corpoEmail = corpoEmail.Replace("#PONTOS#", objVerba.valor_pontos.ToString());
                        _emailService.SendAsync(corpoEmail, "Atribuição de Verba para o Gestor", colaborador.email);
                    }
                    catch
                    {
                    }
                    
                    #endregion


                    return dto;
                }
                else
                {
                    throw new InvalidOperationException("Não foi possível identificar o Colaborador para atribuição de Verba.");
                }
            }
            else
            {
                throw new InvalidOperationException("Os campos para Verba não foram preenchidos");
            }
        }

        public ColaboradorGestorSaldoVerba ObterSaldoVerbaGestor(string cs_gestor)
        {
            return _repository.ObterSaldoVerbaGestor(cs_gestor);
        }

        public IEnumerable<VerbaDTO> Listar()
        {
            return _repository.Listar();
        }
    }
}