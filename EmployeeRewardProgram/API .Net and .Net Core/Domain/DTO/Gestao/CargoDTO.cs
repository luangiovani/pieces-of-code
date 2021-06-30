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
    /// Objeto de Transferência da Entidade Cargo
    /// </summary
    public class CargoDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Identificador do Perfil que corresponde a este cargo (quando houver)
        /// </summary>
        public int? perfil_id { get; set; }

        /// <summary>
        /// Código do Cargo no sistema de origem do registro
        /// </summary>
        public string codigo { get; set; }

        /// <summary>
        /// Nome do Cargo
        /// </summary>
        public string nomenclatura { get; set; }

        /// <summary>
        /// Descrição deste cargo ou atribuições do mesmo
        /// </summary>
        public string descricao { get; set; }

        /// <summary>
        /// Se é um cargo elegível
        /// </summary>
        public bool elegivel { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }

        /// <summary>
        /// Data e Hora da Criação do registro
        /// </summary>
        public DateTime data_hora_criacao { get; set; }

        /// <summary>
        /// Usuário de Criação do registro
        /// </summary>
       public string cs_colaborador_criacao { get; set; }

        /// <summary>
        /// Data e Hora da Alteração do registro
        /// </summary>
        public DateTime? data_hora_alteracao { get; set; }

        /// <summary>
        /// Usuário de Alteração do registro
        /// </summary>
        public string cs_colaborador_alteracao { get; set; }
    }
}
