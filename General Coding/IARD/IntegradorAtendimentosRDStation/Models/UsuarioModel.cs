using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntegradorAtendimentosRDStation.Models
{
    public class UsuarioModel
    {
        [DisplayName("Login")]
        [Required(ErrorMessage="Informe o Login")]
        public string Login { get; set; }
        
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "Informe a Senha")]
        public string Senha { get; set; }
    }
}