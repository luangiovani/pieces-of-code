using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TAJ.Database.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string nome { get; set; }
        public string telefone { get; set; }
        public string celular { get; set; }
        public bool ativo { get; set; }
        public DateTime dt_cadastro { get; set; }
        public virtual ICollection<Usuario_Filial> usuario_filial { get; set; }
        //public virtual ICollection<ApplicationUserRole> perfil_usuario { get; set; }  

        public ApplicationUser()
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}