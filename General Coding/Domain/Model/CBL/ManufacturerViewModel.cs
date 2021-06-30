using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Mapeamento da Entidade Manufacturer (Fabricante), para gravação na tabela Fabricante no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class ManufacturerViewModel
    {
        /// <summary>
        /// Manufacturer ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Manufacturer ID")]
        public int manufacturer_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do fabricante
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição do Fabricante
        /// </summary>
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }

        /// <summary>
        /// Medias
        /// Lista de equipamentos do Fabricante
        /// </summary>
        [Display(Name = "Medias")]
        public virtual ICollection<MediaViewModel> medias { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste parceiro
        /// </summary>
        [Display(Name = "Register Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>

        public string userRegistration_id { get; set; }

        [Display(Name = "Models of Medias")]
        public virtual ICollection<MediaModelsViewModel> mediaModels { get; set; }


        public static implicit operator Manufacturer(ManufacturerViewModel obj)
        {

            return new Manufacturer
            {
                dateRegistration = obj.dateRegistration,
                description = obj.description,
                //id_old = obj.id_old,
                manufacturer_id = obj.manufacturer_id,
                //mediaModels = obj.mediaModels,
                //medias = obj.medias,
                name = obj.name,
                userRegistration_id = obj.userRegistration_id
            };
        }
        public static implicit operator ManufacturerViewModel(Manufacturer obj)
        {

            return new ManufacturerViewModel
            {
                dateRegistration = obj.dateRegistration,
                description = obj.description,
                //id_old = obj.id_old,
                manufacturer_id = obj.manufacturer_id,
                //mediaModels = obj.mediaModels,
                //medias = obj.medias,
                name = obj.name,
                userRegistration_id = obj.userRegistration_id
            };
        }

    }
}
