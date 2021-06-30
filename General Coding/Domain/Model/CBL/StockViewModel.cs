using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class StockViewModel
    {
        [Key]
        [Display(Name = "Stock ID")]
        public int stock_id { get; set; }

        [Display(Name = "Media")]
        public int? media_id { get; set; }

        [Display(Name = "Component")]
        public int? component_id { get; set; }

        [Display(Name="Model")]
        [StringLength(2000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Required(ErrorMessage = "Equipment/Component is required")]
        public string material { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateOfMovement { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public decimal quantity { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type of Movement is required (In/Out)")]
        [StringLength(128, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string typeOfMovement { get; set; }

        [Display(Name = "Address")]
        [StringLength(2000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string stockAddress { get; set; }

        [Display(Name = "Location ID")]
        public int? location_id { get; set; }

        [Display(Name = "Service Order #")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? serviceOrder_id { get; set; }

        [Display(Name = "OS Series")]
        public string OS_Series { get; set; }

        [Display(Name = "Notes")]
        [StringLength(8000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string notes { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Register Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateRegistration { get; set; }

        [Required(ErrorMessage = "Register User is required")]
        [Display(Name = "Register User")]
        public string userRegistration_id { get; set; }

        [Required(ErrorMessage = "Update Date is required")]
        [Display(Name = "Update Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime lastUpdateDate { get; set; }

        [Required(ErrorMessage = "Update User is required")]
        [Display(Name = "Update User")]
        public string lastUpdateUser_id { get; set; }

        [Display(Name = "Compatibility")]
        public string compatibility { get; set; }

        public int? id_old { get; set; }

        [Display(Name = "Location")]
        public virtual LocationsViewModel Location { get; set; }

        [Display(Name = "Equipment")]
        public virtual MediaViewModel Media { get; set; }

        [Display(Name = "Component")]
        public virtual ComponentViewModel Component { get; set; }

        //utilizado somente na tela para inserção de novo registro na movimentação de estoque
        public ICollection<MediaViewModel> ListMedias { get; set; }

        #region Media Info

        [Display(Name = "Manufacturer")]
        [ScaffoldColumn(true)]
        public string MediaManufacturer { get; set; }

        [Display(Name = "Location")]
        [ScaffoldColumn(true)]
        public string MediaLocation { get; set; }

        [Display(Name = "Status")]
        [ScaffoldColumn(true)]
        public string MediaStatus { get; set; }

        [Display(Name = "Serial")]
        [ScaffoldColumn(true)]
        public string MediaSerial { get; set; }

        [Display(Name = "Part #")]
        [ScaffoldColumn(true)]
        public string MediaPartNo { get; set; }

        [Display(Name = "Revision #")]
        [ScaffoldColumn(true)]
        public string MediaRevisionNo { get; set; }

        [Display(Name = "Firmware #")]
        [ScaffoldColumn(true)]
        public string MediaFirmwareNo { get; set; }

        [Display(Name = "Size")]
        [ScaffoldColumn(true)]
        public string MediaSize { get; set; }

        [Display(Name = "Interface")]
        [ScaffoldColumn(true)]
        public string MediaInterface { get; set; }

        [Display(Name = "PCB ID")]
        [ScaffoldColumn(true)]
        public string MediaPCBId { get; set; }

        [Display(Name = "PCB")]
        [ScaffoldColumn(true)]
        public string MediaPCB { get; set; }

        [Display(Name = "MLC #")]
        [ScaffoldColumn(true)]
        public string MediaMLCNo { get; set; }

        [Display(Name = "MFG Date")]
        [ScaffoldColumn(true)]
        public string MediaMFGDate { get; set; }

        [Display(Name = "OEM #")]
        [ScaffoldColumn(true)]
        public string MediaOemNo { get; set; }

        [Display(Name = "Up Level #")]
        [ScaffoldColumn(true)]
        public string MediaUpLevelNo { get; set; }

        [Display(Name = "Series")]
        [ScaffoldColumn(true)]
        public string MediaSeries { get; set; }

        [Display(Name = "Condition")]
        [ScaffoldColumn(true)]
        public string MediaCondition { get; set; }

        [Display(Name = "Condition Info")]
        [ScaffoldColumn(true)]
        public string MediaConditionInfo { get; set; }

        [Display(Name = "DCM Site #")]
        [ScaffoldColumn(true)]
        public string MediaDCMSiteNo { get; set; }

        [Display(Name = "HDA")]
        [ScaffoldColumn(true)]
        public string MediaHDA { get; set; }

        [Display(Name = "Compatibility")]
        [ScaffoldColumn(true)]
        public string MediaCompatibility { get; set; }

        [Display(Name = "Made IN")]
        [ScaffoldColumn(true)]
        public string MediaMadeIn { get; set; }
        #endregion


        public static implicit operator StockViewModel(Stock obj)
        {
            return new StockViewModel
            {
                stock_id = obj.stock_id,
                media_id = obj.media_id,
                component_id = obj.component_id,
                material = obj.material,
                dateOfMovement = obj.dateOfMovement,
                quantity = obj.quantity,
                typeOfMovement = obj.typeOfMovement,
                stockAddress = obj.stockAddress,
                location_id = obj.location_id,
                serviceOrder_id = obj.serviceOrder_id,
                //////OS_Series                               = obj.OS_Series,
                notes = obj.notes,
                dateRegistration = obj.dateRegistration,
                userRegistration_id = obj.userRegistration_id,
                lastUpdateDate = obj.lastUpdateDate,
                lastUpdateUser_id = obj.lastUpdateUser_id,
                /////compatibility                            = obj.compatibility,
                id_old = obj.id_old,
                //LocationsViewModel Location                 = obj.//LocationsViewModel Location,
                //MediaViewModel Media                        = obj.//MediaViewModel Media,
                //ComponentViewModel Component                = obj.//ComponentViewModel Component,
                ////ICollection<MediaViewModel> ListMedias    = obj.////ICollection<MediaViewModel> ListMedias,
                //MediaManufacturer                           = obj.MediaManufacturer,
                //MediaLocation                               = obj.MediaLocation,
                //MediaStatus                                 = obj.MediaStatus,
                //MediaSerial                                 = obj.MediaSerial,
                //MediaPartNo                                 = obj.MediaPartNo,
                //MediaRevisionNo                             = obj.MediaRevisionNo,
                //MediaFirmwareNo                             = obj.MediaFirmwareNo,
                //MediaSize                                   = obj.MediaSize,
                //MediaInterface                              = obj.MediaInterface,
                //MediaPCBId                                  = obj.MediaPCBId,
                //MediaPCB                                    = obj.MediaPCB ,
                //MediaMLCNo                                  = obj.MediaMLCNo,
                //MediaMFGDate                                = obj.MediaMFGDate,
                //MediaOemNo                                  = obj.MediaOemNo,
                //MediaUpLevelNo                              = obj.MediaUpLevelNo,
                //MediaSeries                                 = obj.MediaSeries,
                //MediaCondition                              = obj.MediaCondition,
                //MediaConditionInfo                          = obj.MediaConditionInfo,
                //MediaDCMSiteNo                              = obj.MediaDCMSiteNo,
                //MediaHDA                                    = obj.MediaHDA,
                //MediaCompatibility                          = obj.MediaCompatibility,
                //MediaMadeIn                                 = obj.MediaMadeIn,


            };
        }

        public static implicit operator Stock(StockViewModel obj)
        {

            return new Stock
            {
                stock_id = obj.stock_id,
                media_id = obj.media_id,
                component_id = obj.component_id,
                material = obj.material,
                dateOfMovement = obj.dateOfMovement,
                quantity = obj.quantity,
                typeOfMovement = obj.typeOfMovement,
                stockAddress = obj.stockAddress,
                location_id = obj.location_id,
                serviceOrder_id = obj.serviceOrder_id,                
                notes = obj.notes,
                dateRegistration = obj.dateRegistration,
                userRegistration_id = obj.userRegistration_id,
                lastUpdateDate = obj.lastUpdateDate,
                lastUpdateUser_id = obj.lastUpdateUser_id,
                id_old = obj.id_old,


            };

        }

    }
}
