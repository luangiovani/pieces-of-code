using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Database.Entity
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Configuracao da Entidade Area, para mapeamento na tabela Area no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class Area
    {
        /// <summary>
        /// Area ID
        /// Id interno da área será utilizado nos relacionamentos e controle único
        /// </summary>
        public Guid id_area { get; set; }

        /// <summary>
        /// Area ID
        /// Id interno da área mãe ou superior que será utilizado nos relacionamentos e controle
        /// </summary>
        public Guid? id_area_mae { get; set; }

        /// <summary>
        /// Nome
        /// Nome da área para menu
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Ordem
        /// Ordenação de Menu, ordem que aparece na tela
        /// </summary>
        public int ordem { get; set; }

        /// <summary>
        /// Controller
        /// Controladora, por onde passarão as requisições de área
        /// </summary>
        public string controller { get; set; }

        /// <summary>
        /// Action
        /// Ação específica da área
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// Help
        /// Texto de ajuda e explicação da área
        /// </summary>
        public string help { get; set; }

        /// <summary>
        /// Data de Cadastro
        /// Data que a área foi adicionada no sistema
        /// </summary>
        public DateTime dt_cadastro { get; set; }

        /// <summary>
        /// Area Mãe/Superior
        /// Área que está no nível acima da área em questão
        /// </summary>
        public virtual Area area_mae { get; set; }

        /// <summary>
        /// Areas filhas / inferiores
        /// Lista de áreas que estão abaixo da área em questão
        /// </summary>
        public virtual ICollection<Area> area_filha { get; set; }

        /// <summary>
        /// Perfis da Área
        /// Lista de Perfis que estão vinculadas a esta área, ou seja, que possuem acesso a área
        /// </summary>
        public virtual ICollection<Perfil_Area> perfil_area { get; set; }
    }
}