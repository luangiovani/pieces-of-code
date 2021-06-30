using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;
using System.Net.Mail;
using System.Net;
using LinhaSebrae;

namespace Classes
{
    public class EmailDll
    {
        #region Metodos Específicos

        #region EnviarEmailDll
        public static void EnviarEmailDll(string EmailDestino, string EmailOrigem, string EmailTitulo, string EmailCorpo)
        {
            clsFechamento _email = new clsFechamento();
            bool Enviook = _email.MandaEmail(EmailOrigem.ToString(), EmailDestino.ToString(), EmailTitulo.ToString(), EmailCorpo.ToString(), 1);
            Enviook = Enviook;
        }

        #endregion

        #region interface
        public interface LinhaSebrae
        {
            void clsFechamento();
        }
        #endregion


        #endregion
    }
}