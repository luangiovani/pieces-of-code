using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Model
{
    /// <summary>
    /// reverter as traduçoes das classes acima
    /// </summary>
    public class EquipmentsSellViewModel
    {
        

        [Display(Name = "Quantidade")]
        public string Amount { get; set; }

        [Display(Name = "Marca")]
        public string Make { get; set; }

        [Display(Name = "Modelo")]
        public string Model { get; set; }

        [Display(Name = "Tamanho")]
        public string Size { get; set; }

        [Display(Name = "Valor")]
        public string SalePrice { get; set; }

        public int media_id { get; set; }
        public decimal serviceOrder_id { get; set; }
        
        [Key]
        public int equipament_id { get; set; }
        public DateTime dateRegistration { get; set; }
        public string userRegistration_id { get; set; }




        public static implicit operator EquipmentsSellViewModel(EquipmentsSell obj)
        {

            return new EquipmentsSellViewModel
            {
                Amount = obj.Amount,
                Make = obj.Make,
                Model = obj.Model,
                Size = obj.Size,
                SalePrice = obj.SalePrice,
                media_id = obj.media_id,
                serviceOrder_id = obj.serviceOrder_id,
                equipament_id = obj.equipament_id,
                dateRegistration = obj.dateRegistration,
                userRegistration_id = obj.userRegistration_id
            };
        }

        public static implicit operator EquipmentsSell(EquipmentsSellViewModel obj)
        {

            return new EquipmentsSell
            {
                Amount = obj.Amount,
                Make = obj.Make,
                Model = obj.Model,
                Size = obj.Size,
                SalePrice = obj.SalePrice,
                media_id = obj.media_id,
                serviceOrder_id = obj.serviceOrder_id,
                equipament_id = obj.equipament_id,
                dateRegistration = obj.dateRegistration,
                userRegistration_id = obj.userRegistration_id

            };

        }

    }
    
}
