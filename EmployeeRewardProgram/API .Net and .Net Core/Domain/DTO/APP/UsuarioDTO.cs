using System;
using System.Collections.Generic;

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
    /// Objeto de Transferência da Entidade Usuario
    /// </summary>
    public class UsuarioDTO
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public UsuarioDTO()
        {
            Menus = new List<MenuUsuarioDTO>();
        }

        /// <summary>
        /// O Colaborador a quem este usuário pertence
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Código CS do Colaborador
        /// </summary>
        public string cs { get; set; }

        /// <summary>
        /// Nome do Usuário
        /// </summary>
        public string nome { get; set; }
        
        /// <summary>
        /// Perfil de Acesso do Colaborador
        /// </summary>
        public string perfil { get; set; }

        /// <summary>
        /// Id do Perfil de Acesso do Colaborador
        /// </summary>
        public string perfilId { get; set; }

        /// <summary>
        /// Email do Usuário para recuperação de senha se for o caso
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Token de autenticação
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// Listagem de Menus de acesso do Usuário
        /// </summary>
        public IEnumerable<MenuUsuarioDTO> Menus { get; set; }
    }
}
