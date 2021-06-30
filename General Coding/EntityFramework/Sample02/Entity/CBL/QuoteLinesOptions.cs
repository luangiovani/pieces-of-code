using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class QuoteLinesOptions
    {
        /// <summary>
        /// Quote Line Option ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int quoteLineOption_id { get; set; }

        /// <summary>
        /// Name
        /// Nome da opção de diagnóstico do orçamento encontrado
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição do diagnóstico encontrado
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Active
        /// Indicador de status do diagnóstico
        /// </summary>
        //public bool active { get; set; }

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

        public int? id_old { get; set; }
    }
}
