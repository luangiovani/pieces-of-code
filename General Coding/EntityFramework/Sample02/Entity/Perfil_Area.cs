using System;
using System.Collections.Generic;

namespace Framework.Database.Entity
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Configuracao da Entidade Perfil_Area, para mapeamento na tabela Perfil_Area no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class Perfil_Area
    {
        /// <summary>
        /// Perfil ID
        /// Id interno do perfil que será utilizado nos relacionamentos e controle
        /// </summary>
        public string id_perfil { get; set; }

        /// <summary>
        /// Area ID
        /// Id interno da área será utilizado nos relacionamentos e controle
        /// </summary>
        public Guid id_area { get; set; }

        /// <summary>
        /// Visualizar
        /// Indicador se o perfil tem acesso a visualizações da Área
        /// </summary>
        public bool ind_visualizar { get; set; }

        /// <summary>
        /// Cadastrar
        /// Indicador se o perfil tem acesso a realizar cadastros da Área
        /// </summary>
        public bool ind_cadastrar { get; set; }

        /// <summary>
        /// Excluir
        /// Indicador se o perfil tem acesso a realizar exclusões da Área
        /// </summary>
        public bool ind_excluir { get; set; }

        /// <summary>
        /// Data de Cadastro
        /// Data que foi realizado o vínculo entre um Perfil e uma área
        /// </summary>
        public DateTime dt_cadastro { get; set; }

        /// <summary>
        /// Perfil
        /// Entidade Perfil do relacionamento
        /// </summary>
        public virtual ApplicationRole perfil { get; set; }

        /// <summary>
        /// Area
        /// Entidade Área do relacionamento
        /// </summary>
        public virtual Area area { get; set; }
    }
}