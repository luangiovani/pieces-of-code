using System;
using System.ComponentModel.DataAnnotations;
using Framework.Database.Entity;

namespace Framework.Domain.Model
{
    public class PerfilAreaViewModel
    {
        [Key]
        [Display(Name = "Code")]
        [ScaffoldColumn(false)]
        public string id_perfil { get; set; }

        [Key]
        [Display(Name = "Area Code")]
        [ScaffoldColumn(false)]
        public Guid id_area { get; set; }

        [Display(Name = "Can View")]
        public bool ind_visualizar { get; set; }

        [Display(Name = "Add New")]
        public bool ind_cadastrar { get; set; }

        [Display(Name = "Can Delete")]
        public bool ind_excluir { get; set; }

        [Display(Name = "Data Cadastro")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [ScaffoldColumn(false)]
        public DateTime dt_cadastro { get; set; }
    }
}