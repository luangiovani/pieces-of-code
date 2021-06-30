using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class MediaModelsViewModel
    {
        [Key]
        [Display(Name = "Media Models ID")]
        public int mediaModels_id { get; set; }

        [Display(Name = "Manufacturer")]
        public int manufacturer_id { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Model is Required")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string model { get; set; }

        [Display(Name = "Compatibility")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string compatibility { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
        /// </summary>
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
        public string userRegistration_id { get; set; }

        [Display(Name = "Manufacturer")]
        public virtual ManufacturerViewModel manufacturer { get; set; }

        public static implicit operator MediaModels(MediaModelsViewModel obj)
        {

            return new MediaModels
            {
                compatibility = obj.compatibility,
                dateRegistration = obj.dateRegistration,
                manufacturer = obj.manufacturer,
                manufacturer_id = obj.manufacturer_id,
                mediaModels_id = obj.mediaModels_id,
                model = obj.model,
                userRegistration_id = obj.userRegistration_id
            };
        }

        public static implicit operator MediaModelsViewModel(MediaModels obj)
        {

            return new MediaModelsViewModel
            {
                compatibility = obj.compatibility,
                dateRegistration = obj.dateRegistration,
                manufacturer = obj.manufacturer,
                manufacturer_id = obj.manufacturer_id,
                mediaModels_id = obj.mediaModels_id,
                model = obj.model,
                userRegistration_id = obj.userRegistration_id
            };
        }


    }
}
