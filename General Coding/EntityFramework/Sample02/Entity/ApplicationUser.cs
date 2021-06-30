using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Framework.Database.Entity.CBL;

namespace Framework.Database.Entity
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Configuracao da Entidade ApplicationUser (Usuário), para mapeamento na tabela Usuario no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Nome
        /// Nome completo do Usuário
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Telefone
        /// Número Telefone do Usuário
        /// </summary>
        public string telefone { get; set; }

        /// <summary>
        /// Celular
        /// Número do Telefone Celular do Usuário
        /// </summary>
        public string celular { get; set; }

        /// <summary>
        /// Ativo
        /// Indicador de Status do Usuário
        /// </summary>
        public bool ativo { get; set; }

        /// <summary>
        /// URL File
        /// Url do Arquivo do Documento
        /// </summary>
        public string url_file { get; set; }

        /// <summary>
        /// Data de cadastro
        /// Data em que o usuário foi criado no sistema
        /// </summary>
        public DateTime dt_cadastro { get; set; }

        public int? id_old { get; set; }

        public int location_id { get; set; }

        public virtual Locations location { get; set; }

        public ICollection<Notes> notes { get; set; }

        public ICollection<ServiceOrder> serviceOrders { get; set; }

        public ICollection<ServiceOrder> serviceOrdersAssigned { get; set; }

        public ICollection<LabNotes> labNotes { get; set; }
        //public ICollection<ServiceOrderCloud> serviceOrderClouds { get; set; }

        public string tokenCode { get; set; }

        /// <summary>
        /// Construtor
        /// Construtor padrão para inicializar a entidade usuário
        /// </summary>
        public ApplicationUser()
        {
        }

        /// <summary>
        /// Gerar Identidade de usuário assíncronamente
        /// </summary>
        /// <param name="manager">Usuário a ser gerenciado</param>
        /// <returns>Identidade de Usuário</returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}