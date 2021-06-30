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
    /// Objeto de Transferência da Entidade TipoRecomendacao
    /// </summary
    public class TipoRecomendacaoDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Nome da Entidade que este DTO pertence
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Descrição do Tipo de Recomendação
        /// </summary>
        public string descricao { get; set; }

        /// <summary>
        /// Quantidade fixa de pontuação que tem este Tipo de Recomendação
        /// </summary>
        public decimal pontosfixos { get; set; }

        /// <summary>
        /// Indica o Tipo de Pontuação se Fixa ou Excolha
        /// </summary>
        public int tipopontuacao { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }
    }
}