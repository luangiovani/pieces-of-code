using Database.Models.APP;
using Domain.DTO.APP;
using Domain.Repositories.APP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services.APP
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
    public class LogService
    {
        /// <summary>
        /// Objeto repositório para manipulação da tabela LogOperacoes
        /// </summary>
        private readonly LogOperacoesRepository _logOperacoresRepository;

        /// <summary>
        /// Objeto repositório para manipulação da tabela LogTransacoes
        /// </summary>
        private readonly LogTransacoesRepository _logTransacoesRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logOperacoresRepository">Injeção Objeto repositório para manipulação da tabela LogOperacoes</param>
        /// <param name="logTransacoesRepository">Injeção Objeto repositório para manipulação da tabela LogTransacoes</param>
        public LogService(LogOperacoesRepository logOperacoresRepository, LogTransacoesRepository logTransacoesRepository)
        {
            _logOperacoresRepository = logOperacoresRepository;
            _logTransacoesRepository = logTransacoesRepository;
        }

        /// <summary>
        /// Método para inicializar o Log de Operações
        /// </summary>
        /// <param name="cs_colaborador_logado">Colaborador que está logado e realizando a Operação</param>
        /// <param name="aplicacao_id">Aplicação que a operação está sendo executada</param>
        /// <param name="operacao">Operação que está sendo executada</param>
        /// <param name="observacoes">Observações da Operação que está sendo executada</param>
        /// <returns>Objeto do LogOperacoes</returns>
        public LogOperacoes LogInicioOperacao(string cs_colaborador_logado, Guid aplicacao_id, string operacao, string observacoes)
        {
            var log = new LogOperacoes()
            {
                aplicacao_id = aplicacao_id.ToString(),
                cs_colaborador = cs_colaborador_logado,
                data_hora_inicio = DateTime.Now,
                data_hora_fim = null,
                observacao = observacoes,
                operacao = operacao
            };

            return _logOperacoresRepository.Add(log);
        }

        /// <summary>
        /// Método para finalizar um Log de Operação
        /// </summary>
        /// <param name="logId">Id do Log de Operação que está sendo manipulado</param>
        /// <param name="erro">Erro que está sendo registrado no Log, quando houver</param>
        public void LogFimOperacao(Guid logId, string erro)
        {
            LogOperacoes log;

            try
            {
                log = _logOperacoresRepository.FindByID(logId.ToString());
                if (log != null)
                {
                    log.data_hora_fim = DateTime.Now;
                    log.erro = erro;
                    _logOperacoresRepository.Update(log);
                }
                else
                {
                    throw new Exception("Erro ao tentar obter o Log de Operações");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para inicializar o Log de Transações
        /// </summary>
        /// <param name="log_operacao_id">Id do Log de Operações a que esta transação faz parte</param>
        /// <param name="transacao">Transação que está sendo efetuada</param>
        /// <param name="observacoes">Observação da Transação</param>
        /// <returns></returns>
        public LogTransacoes LogInicioTransacao(Guid log_operacao_id, string transacao, string observacoes)
        {
            var log = new LogTransacoes()
            {
                log_operacao_id = log_operacao_id.ToString(),
                data_hora_inicio = DateTime.Now,
                data_hora_fim = null,
                observacao = observacoes,
                transacao = transacao
            };

           string logtId = _logTransacoesRepository.Add(log).id.ToString();
            return _logTransacoesRepository.FindByID(logtId);
        }

        public IEnumerable<LogsDTO> ListarLogs(LogsBuscaDTO dto)
        {
            if (dto != null)
            {
                return _logOperacoresRepository.ListarLogs(dto).Select(l =>
                {
                    return new LogsDTO()
                    {
                        aplicacao_id = l.aplicacao_id,
                        cs_colaborador = l.cs_colaborador,
                        nome_colaborador = l.nome_colaborador,
                        data_hora_fim = l.data_hora_fim,
                        data_hora_inicio = l.data_hora_inicio,
                        erro = l.erro,
                        id = l.id,
                        observacao = l.observacao,
                        operacao = l.operacao,
                        sequencial = l.sequencial
                    };
                });
            }
            else
            {
                throw new Exception("Informações para consulta de Logs não foram informadas");
            }
        }

        public IEnumerable<string> ListarOperacoes()
        {
            return _logOperacoresRepository.ListarOperacoes();
        }
        /// <summary>
        /// Método para finalizar um Log de Transação
        /// </summary>
        /// <param name="logId">Id do Log de Transação que está sendo manipulado</param>
        /// <param name="erro">Erro que está sendo registrado no Log, quando houver</param>
        public void LogFimTransacao(Guid logId, string erro)
        {
            LogTransacoes log;

            try
            {
                log = _logTransacoesRepository.FindByID(logId.ToString());
                if (log != null)
                {
                    log.data_hora_fim = DateTime.Now;
                    log.erro = erro;
                    _logTransacoesRepository.Update(log);
                }
                else
                {
                    throw new Exception("Erro ao tentar obter o Log de Transações");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
