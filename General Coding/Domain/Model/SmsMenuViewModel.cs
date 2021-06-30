using Framework.Database.Entity;
using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model
{
    public class SmsMenuViewModel
    {
        /// <summary>
        /// ServiceOrderNotes ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "sms id")]
        public int sms_id { get; set; }

        /// <summary>
        /// ordem
        /// 
        /// </summary>
        /// [Key]
        [Display(Name = "Código")]
        public int ordem { get; set; }

        /// <summary>
        /// pergunta
        /// 
        /// </summary>
        [Key]
        [Display(Name = "Pergunta")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string pergunta { get; set; }


        /// <summary>
        /// Resposta
        /// 
        /// </summary>
        [Key]
        [Display(Name = "Resposta")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string resposta { get; set; }

        /// <summary>
        /// Resposta
        /// 
        /// </summary>
        [Key]
        [Display(Name = "Resposta Automática")]
        public Boolean respostaAutomatica { get; set; }
        
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





        public static implicit operator SmsMenuViewModel(SmsMenu obj)
        {

            return new SmsMenuViewModel
            {
                sms_id = obj.sms_id,
                ordem = obj.ordem,
                pergunta = obj.pergunta,
                resposta = obj.resposta,
                respostaAutomatica = obj.respostaAutomatica,
                userRegistration_id = obj.userRegistration_id,
                dateRegistration = obj.dateRegistration
            };
        }

        public static implicit operator SmsMenu(SmsMenuViewModel obj)
        {

            return new SmsMenu
            {
                sms_id = obj.sms_id,
                ordem = obj.ordem,
                pergunta = obj.pergunta,
                resposta = obj.resposta,
                respostaAutomatica = obj.respostaAutomatica,
                userRegistration_id = obj.userRegistration_id,
                dateRegistration = obj.dateRegistration

            };

        }


    }
}
