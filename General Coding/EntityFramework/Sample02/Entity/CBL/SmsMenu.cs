using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class SmsMenu
    {
        /// <summary>
        /// ServiceOrderNotes ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int sms_id { get; set; }

        /// <summary>
        /// ordem
        /// ordem das perguntas
        /// </summary>
        [Required]
        public int ordem { get; set; }

        /// <summary>
        /// Pergunta
        /// Pergunta da sms
        /// </summary>
        [Required]
        public string pergunta { get; set; }

        /// <summary>
        /// Resposta
        /// Resposta sms
        [Required]
        /// </summary>
        public string resposta { get; set; }

        /// <summary>
        /// Resposta Automatica
        /// Resposta Automatica
        /// </summary>
        public Boolean respostaAutomatica { get; set; }


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


        
    }
}
