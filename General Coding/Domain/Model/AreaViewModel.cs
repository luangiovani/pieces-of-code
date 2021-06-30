using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Model
{
    public class AreaViewModel
    {
        public AreaViewModel()
        {
            dt_cadastro = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }

        [Key]
        [Display(Name = "Code")]
        [ScaffoldColumn(false)]
        public Guid? id_area { get; set; }

        [Display(Name = "Code Area Mother")]
        [ScaffoldColumn(false)]
        public Guid? id_area_mae { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Area")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string nome { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Sort")]
        public int ordem { get; set; }

        [Display(Name = "Controller")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string controller { get; set; }

        [Display(Name = "Action")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string action { get; set; }

        [Display(Name = "Help")]
        public string help { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Date register")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dt_cadastro { get; set; }

        [Display(Name = "Mother Area")]
        public virtual AreaViewModel area_mae { get; set; }

        [Display(Name = "Sub Areas")]
        public virtual ICollection<AreaViewModel> area_filha { get; set; }

        [Display(Name = "Profiles Area")]
        public virtual ICollection<PerfilAreaViewModel> perfil_area { get; set; }
    }

    public class PerfilAreasViewModel
    {
        public Guid AreaId { get; set; }
        public string Area { get; set; }
        public bool Permissao { get; set; }
        public bool PermissaoV { get; set; }
        public bool PermissaoC { get; set; }
        public bool PermissaoE { get; set; }
        public virtual ICollection<PerfilAreasViewModel> AreasFilhas { get; set; }
    }
}