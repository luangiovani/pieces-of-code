using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderRecoveryFollowUp
    {
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
        /// Media Status
        /// Estado de mídia
        /// </summary>
        public string mediaStatus { get; set; }

        /// <summary>
        /// rateOurService
        /// Taxa do Nosso Serviço
        /// </summary>
        public string rateOurService { get; set; }

        /// <summary>
        /// would Be Reference
        /// Seria referenciado
        /// </summary>
        public string wouldBeReference { get; set; }

        /// <summary>
        /// Send Letter Reference
        /// Enviar Carta de Referência
        /// </summary>
        public string sendLetterReference { get; set; }

        /// <summary>
        /// Date Complete
        /// Data completa
        /// </summary>
        public DateTime? dateComplete { get; set; }

        /// <summary>
        /// IntroFaxed
        /// Introdução de fax em...
        /// </summary>
        public DateTime? introFaxed { get; set; }

        /// <summary>
        /// emailSent
        /// E-mail enviado
        /// </summary>
        public DateTime? emailSent { get; set; }

        /// <summary>
        /// comments
        /// Comentários
        /// </summary>
        public string comments { get; set; }

        public int? id_old { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        public virtual ServiceOrder serviceOrder { get; set; }
       
    }
}
