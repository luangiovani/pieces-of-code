using System;

namespace Domain.DTO.Gestao
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
    /// Objeto de Transferência da Entidade Loja
    /// </summary
    public class LojasDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Nome da Loja
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Status da Loja
        /// </summary>
        public string status_loja { get; set; }

        /// <summary>
        /// Código da Loja
        /// </summary>
        public string codigo { get; set; }

        /// <summary>
        /// Observações da Loja
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// Complemento da Loja
        /// </summary>
        public string complemento { get; set; }

        /// <summary>
        /// Localização da Loja
        /// </summary>
        public string localizacao { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }
    }

    public class ColaboradoresLojasDTO
    {
        public string id { get; set; }

        public int sequencial { get; set; }

        public string cs { get; set; }

        public string nome { get; set; }

        public string email { get; set; }

        public string loja_id { get; set; }

        public string loja { get; set; }

        public string cs_colaborador_logado { get; set; }
    }
}
