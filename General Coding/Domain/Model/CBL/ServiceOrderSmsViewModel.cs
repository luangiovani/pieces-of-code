using Framework.Database.Entity;
using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderSmsViewModel
    {
        /// <summary>
        /// ServiceOrderNotes ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "ServiceOrderSms ID")]
        public int serviceOrderSms_id { get; set; }

       
        /// <summary>
        /// serviceOrder_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Service Order ID")]
        public decimal serviceOrder_id { get; set; }


        /// <summary>
        /// Description
        /// Descrição da nota
        /// </summary>
        [Key]
        [Display(Name = "Sms")]
        public string mensagem { get; set; }
        
        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
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

       
        public string tipoSms { get; set; }

        /// <summary>
        /// nome
        /// nome de quem enviou o sms
        /// </summary>
        public string nome { get; set; }
        
        public string retornosSms { get; set; }

        /// <summary>
        /// User
        /// Usuário
        /// </summary>
        [Display(Name = "User")]
        public virtual UsuarioViewModel user { get; set; }


        public string numeroDestinatario { get; set; }


        public static implicit operator ServiceOrderSmsViewModel(ServiceOrderSms obj)
        {

            return new ServiceOrderSmsViewModel
            {
                serviceOrderSms_id = obj.serviceOrderSms_id,
                serviceOrder_id = obj.serviceOrder_id,
                mensagem = obj.mensagem,
                userRegistration_id = obj.userRegistration_id,
                dateRegistration = obj.dateRegistration,
                tipoSms = obj.tipoSms,
                nome = obj.nome,
                retornosSms = obj.retornosSms,
                numeroDestinatario = obj.numeroDestinatario
            };
        }

        public static implicit operator ServiceOrderSms(ServiceOrderSmsViewModel obj)
        {

            return new ServiceOrderSms
            {
                serviceOrderSms_id = obj.serviceOrderSms_id,
                serviceOrder_id = obj.serviceOrder_id,
                mensagem = obj.mensagem,
                userRegistration_id = obj.userRegistration_id,
                dateRegistration = obj.dateRegistration,
                tipoSms = obj.tipoSms,
                nome = obj.nome,
                retornosSms = obj.retornosSms,
                numeroDestinatario = obj.numeroDestinatario

            };

        }


    }
}
