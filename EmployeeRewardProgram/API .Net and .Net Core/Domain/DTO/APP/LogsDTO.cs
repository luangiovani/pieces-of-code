using System;

namespace Domain.DTO.APP
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Implementação dos Objetos para Abstração entre a Camada de Serviços e a Camada de Acesso a Dados
    /// </atividades>
    /// <summary>
    /// Objeto de Transferência da Entidade Aplicacao
    /// </summary>
    public class LogsDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Sequencial do registro na tabela (ordenação)
        /// </summary>
        public int sequencial { get; set; }

        /// <summary>
        /// Colaborador que está logado
        /// </summary>
        public string cs_colaborador { get; set; }

        /// <summary>
        /// Nome do Colaborador
        /// </summary>
        public string nome_colaborador { get; set; }

        /// <summary>
        /// Indicador da aplicação em que foi realizada a operação
        /// </summary>
        public string aplicacao_id { get; set; }

        /// <summary>
        /// Data e Hora que iniciou a operação
        /// </summary>
        public DateTime data_hora_inicio { get; set; }

        /// <summary>
        /// Data e Hora que Finalizou a operação
        /// </summary>
        public DateTime? data_hora_fim { get; set; }

        /// <summary>
        /// Referência da Operação que está sendo executada
        /// </summary>
        public string operacao { get; set; }

        /// <summary>
        /// Observação da Operação
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// Indicador se ocorreu algum erro na operação
        /// </summary>
        public string erro { get; set; }
    }

    public class LogsBuscaDTO
    {
        public DateTime? dtIni {
            get
            {
                var dt = DateTime.ParseExact(dataInicial, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None);
                if (!string.IsNullOrEmpty(dataInicial))
                    return dt;
                else
                    return null;
            }
        }

        public DateTime? dtFim {
            get
            {
                var dt = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None);
                if (!string.IsNullOrEmpty(dataFinal))
                {
                    dt = dt.AddHours(23- dt.Hour);
                    dt = dt.AddMinutes(59 - dt.Minute);
                    dt = dt.AddSeconds(59 - dt.Second);
                    return dt;
                }
                else
                    return null;
            }
        }

        public string dataInicial { get; set; } 

        public string dataFinal { get; set; }

        public string operacao { get; set; }
        public string observacoes { get; set; }
    }
}
