using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderInquiryFollowUp
    {
        /// <summary>
        /// serviceOrderInquiryFollowUp_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>      
        public decimal serviceOrder_id { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste parceiro
        /// </summary>   
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>       
        public string userRegistration_id { get; set; }

        /// <summary>
        /// CauseNotSent
        /// Causa não enviada
        /// </summary>       
        public string causeNotSent { get; set; }

        /// <summary>
        /// sentSomewhereElseWhere
        /// Enviada em outro lugar onde
        /// </summary>       
        public string sentSomewhereElseWhere { get; set; }

        /// <summary>
        /// comments
        /// Comentários
        /// </summary>        
        public string comments { get; set; }

        /// <summary>
        /// dateComplete
        /// Data Completa
        /// </summary>
        public DateTime? dateComplete { get; set; }

        /// <summary>
        /// userFollowUp_id
        /// Usuário Acompanhamento ID
        /// </summary>
        public string userFollowUp_id { get; set; }

        public int? id_old { get; set; }

        public virtual ServiceOrder serviceOrder { get; set; }
    }
}
