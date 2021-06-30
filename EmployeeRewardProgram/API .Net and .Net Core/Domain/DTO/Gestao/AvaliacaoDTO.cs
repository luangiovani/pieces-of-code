using System;

namespace Domain.DTO.Gestao
{
    public class AvaliacaoDTO
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
        /// Identificador da Recomendação que esta avaliação pertence
        /// </summary>
        public string recomendacao_id { get; set; }

        /// <summary>
        /// Identificador do colaborador gestor que irá aprovar/reprovar a recomendação
        /// </summary>
        public string cs_colaborador_avaliador { get; set; }

        /// <summary>
        /// Nome do colaborador gestor que irá aprovar/reprovar a recomendação
        /// </summary>
        public string nome_colaborador_avaliador { get; set; }

        /// <summary>
        /// Email do Colaborador Avaliador
        /// </summary>
        public string email_colaborador_avaliador { get; set; }

        /// <summary>
        /// Cargo do Colaborador Avaliador
        /// </summary>
        public string cargo_avaliador { get; set; }

        /// <summary>
        /// Status da Avaliação
        /// </summary>
        public string situacao_avaliacao { get; set; }

        /// <summary>
        /// Status da Avaliação
        /// </summary>
        public string situacao_avaliacao_id { get; set; }

        /// <summary>
        /// Data que foi efetuada a avaliação
        /// </summary>
        public DateTime data_avaliacao { get; set; }

        /// <summary>
        /// Data que foi efetuada a avaliação
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// Informa uma justificativa para a avaliação
        /// </summary>
        public string justificativa { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }
    }

    public class AvaliacoesPendentesGestorDTO
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
        /// Data que a Recomendação foi realizada
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// Identificador do colaborador que recebeu a recomendação
        /// </summary>
        public string cs { get; set; }

        /// <summary>
        /// Nome do colaborador que recebeu a recomendação
        /// </summary>
        public string colaborador { get; set; }

        /// <summary>
        /// Motivo da Recomendação
        /// </summary>
        public string motivo { get; set; }

        /// <summary>
        /// Quantidade de Pontos que vale a Recomendação
        /// </summary>
        public decimal pontos { get; set; }
    }

    public class AvaliacoesRealizadasGestorDTO
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
        /// Data que a Avaliação foi realizada
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// Identificador do colaborador que recebeu a recomendação avaliada
        /// </summary>
        public string cs { get; set; }

        /// <summary>
        /// Nome do colaborador que recebeu a recomendação avaliada
        /// </summary>
        public string colaborador { get; set; }

        /// <summary>
        /// Motivo da Recomendação avaliada
        /// </summary>
        public string motivo { get; set; }

        /// <summary>
        /// Quantidade de Pontos que vale a Recomendação avaliada
        /// </summary>
        public decimal pontos { get; set; }

        /// <summary>
        /// Situação da Avaliação da Recomendação
        /// </summary>
        public string situacao { get; set; }
    }

    public class AvaliarDTO
    {
        public Guid? id { get; set; }

        public bool aprovar { get; set; }

        public string justificativa { get; set; }
    }
}
