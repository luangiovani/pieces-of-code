using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderSms
    {
        /// <summary>
        /// ServiceOrderNotes ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int serviceOrderSms_id { get; set; }
       
        /// <summary>
        /// serviceOrder_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
       
        public decimal serviceOrder_id { get; set; }

        /// <summary>
        /// Description
        /// Descrição da sms
        /// </summary>
        public string mensagem { get; set; }

        /// <summary>
        /// tipo
        /// tipo (promoção, envio Automatico CBL, Resposta Cliente, Resposta CBL)
        /// </summary>
        public string tipoSms { get; set; }

        /// <summary>
        /// nome
        /// nome de quem enviou o sms
        /// </summary>
        public string nome { get; set; }


        /// <summary>
        /// retornosSms
        /// retorno da zenvia
        /// </summary>
        public string retornosSms { get; set; }




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
                

        public string numeroDestinatario { get; set; }
        
    }
}
