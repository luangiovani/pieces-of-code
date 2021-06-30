using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Framework.Database.Entity.CBL;

namespace Framework.Database.Entity
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Configuracao da Entidade ApplicationRole (Perfil de Usuário), para mapeamento na tabela Pefil no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class ApplicationRole : IdentityRole
    {
        public int? id_old { get; set; }

        public bool? indSendSms { get; set; }

        /// <summary>
        /// Perfil x Area
        /// Lista de áreas que o perfil tem permissão de acesso
        /// </summary>
        public virtual ICollection<Perfil_Area> perfil_area { get; set; }
        //public virtual ICollection<ApplicationUserRole> perfil_usuario  { get; set; }

        public virtual ICollection<RoleLocations> roleLocations { get; set; }

        /// <summary>
        /// Construtor
        /// Construtor de inicialização padrão
        /// </summary>
        public ApplicationRole() : base() { }

        /// <summary>
        /// Construtor
        /// Construtor para inicializar o perfil de acordo com o nome do perfil passado
        /// e assim obter as informaçoes do Vínculo entre Perfil e Área de acesso
        /// </summary>
        /// <param name="name">Nome do Perfil para Inicializar e Obter as Informações</param>
        public ApplicationRole(string name) : base(name)
        {
            
        }
    }
}