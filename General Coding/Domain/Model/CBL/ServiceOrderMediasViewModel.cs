using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderMediasViewModel
    {
        [Key]
        public int serviceOrderMedias_id { get; set; }

        [Display(Name="Service Order #")]
        public decimal serviceOrder_id { get; set; }

        [Display(Name = "Media ID")]
        public int media_id { get; set; }

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

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        //public virtual ServiceOrderViewModel serviceOrder { get; set; }

        /// <summary>
        /// Media
        /// Equipamento da Ordem de Serviço
        /// </summary>
        public virtual MediaViewModel media { get; set; }


        public static implicit operator ServiceOrderMediasViewModel(ServiceOrderMedias obj)
        {
            if (obj != null)
            {
                return new ServiceOrderMediasViewModel
                {
                    dateRegistration = obj.dateRegistration,
                    media = obj.media,
                    media_id = obj.media_id,
                    serviceOrder_id = obj.serviceOrder_id,
                    serviceOrderMedias_id = obj.serviceOrderMedias_id,
                    userRegistration_id = obj.userRegistration_id

                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator ServiceOrderMedias(ServiceOrderMediasViewModel obj)
        {
            if (obj != null)
            {
                return new ServiceOrderMedias
                {

                    dateRegistration = obj.dateRegistration,
                    media = obj.media,
                    media_id = obj.media_id,
                    serviceOrder_id = obj.serviceOrder_id,
                    serviceOrderMedias_id = obj.serviceOrderMedias_id,
                    userRegistration_id = obj.userRegistration_id


                };
            }
            else
            {
                return null;
            }
        }



    }
}
