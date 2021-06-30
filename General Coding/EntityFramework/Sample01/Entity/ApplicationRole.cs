using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TAJ.Database.Entity
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<Perfil_Area> perfil_area { get; set; }
        //public virtual ICollection<ApplicationUserRole> perfil_usuario  { get; set; }

        public ApplicationRole() : base() { }

        public ApplicationRole(string name) : base(name)
        {
            
        }
    }
}