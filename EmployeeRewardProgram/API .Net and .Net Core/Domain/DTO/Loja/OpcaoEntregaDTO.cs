using System;

namespace Domain.DTO.Loja
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
    /// Objeto de Transferência da Entidade OpcaoEntrega
    /// </summary
    public class OpcaoEntregaDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Label para aparecer quando for selecionar a opção de entrega
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// Label para esta opção quando o colaborador estiver lendo
        /// </summary>
        public string label_colaborador { get; set; }

        /// <summary>
        /// Label para esta opção quando a loja estiver lendo
        /// </summary>
        public string label_loja { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }
    }
}
