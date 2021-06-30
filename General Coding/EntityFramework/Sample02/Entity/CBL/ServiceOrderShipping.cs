using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderShipping
    {
        /// <summary>
        /// Service Order Shipping ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public decimal serviceOrder_id { get; set; }

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
        /// shipCompany
        /// Recipiente da Empresa
        /// </summary>   
        public string shipCompany { get; set; }

        /// <summary>
        /// shipAddress
        /// Endereço do Recipiente 
        /// </summary>   
        public string shipAddress { get; set; }

        /// <summary>
        /// shipCity_id
        /// Recipiente da Cidade
        /// </summary>   
        public int? shipCity_id { get; set; }

        /// <summary>
        /// ship Postal ZipCode
        /// Código Postal
        /// </summary>   
        public string shipPostalZipCode { get; set; }

        /// <summary>
        /// Ship District
        /// Distrito de recipiente
        /// </summary>  
        public string shipDistrict { get; set; }

        /// <summary>
        /// Ship Contact
        /// Distrito de Contato
        /// </summary>   
        public string shipContact { get; set; }

        /// <summary>
        /// Ship Telephone
        /// Distrito de telefone
        /// </summary> 
        public string shipTelephone { get; set; }

        /// <summary>
        /// Ship Email
        /// Distrito de e-mail
        /// </summary>   
        public string shipEmail { get; set; }

        /// <summary>
        /// Ship Method
        /// Método de envio, Método de navio
        /// </summary>
        public string shipMethod { get; set; }

        /// <summary>
        /// Ship Account Number
        /// Número de Conta navio
        /// </summary>   
        public string shipAccountNumber { get; set; }

        /// <summary>
        /// Ship Tracking Number
        /// Número de Rastreamento
        /// </summary>  
        public string shipTrackingNumber { get; set; }

        /// <summary>
        /// Ship Media Status
        /// Estado de Envio de mídia 
        /// </summary>   
        public string shipMediaStatus { get; set; }

        /// <summary>
        /// Ship Media Date 
        /// Envio de mídia Taxa
        /// </summary> 
        public DateTime? shipMediaDate { get; set; }

        /// <summary>
        /// Ship Data Shipped
        /// Dados do navio Enviado
        /// </summary> 
        public DateTime? shipDataShipped { get; set; }

        /// <summary>
        /// Ship Instructions
        /// Instruções de navio/ instruções de envio
        /// </summary> 
        public string shipInstructions { get; set; }

        /// <summary>
        /// Ship Contents
        /// Índice de navios / índice de envio
        /// </summary> 
        public string shipContents { get; set; }

        /// <summary>
        /// Ship Pre Recovery Info
        /// Envio de Pré Informações Recuperação
        /// </summary> 
        public string shipPreRecoveryInfo { get; set; }

        public int? id_old { get; set; }

        public virtual ServiceOrder serviceOrder { get; set; }

        public virtual City city { get; set; }
    }
}
