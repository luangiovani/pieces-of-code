using System.Collections.Generic;
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
    /// Objeto de Transferência da Entidade Departamento
    /// </summary
    public class AtribuicaoDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Identificador da Recomendacao desta atribuicao
        /// </summary>
        public int? recomendacao_id { get; set; }

        /// <summary>
        /// Colaborador que está recebendo a atribuição
        /// </summary>
        public int colaborador_id { get; set; }

        /// <summary>
        /// Colaborador que solicitou a atribuição de pontos
        /// </summary>
        public int colaborador_solicitante_id { get; set; }

        /// <summary>
        /// Quantidade de Pontos para atribuição
        /// </summary>
        public decimal qtde_pontos { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }
    }

    public class ExtratoAtribuicaoDTO
    {
        public string gestor { get; set; }

        public string colaborador { get; set; }

        public decimal total_atribuido { get; set; }
    }

    public class ExtratoAtribuicoesDTO
    {
        public IEnumerable<ExtratoAtribuicaoDTO> realizadas { get; set; }

        public IEnumerable<ExtratoAtribuicaoDTO> recebidas { get; set; }

        public ExtratoAtribuicoesDTO()
        {
            realizadas = new List<ExtratoAtribuicaoDTO>();
            recebidas = new List<ExtratoAtribuicaoDTO>();
        }
    }
}
