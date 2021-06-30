using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
using System.Collections;
using System.Web;  
using Classes;
using System.Data;
using System.Data.OracleClient;
using System.Security.Cryptography; 
using System.Runtime.InteropServices;
using System.IO;

//-- Classe Classes Sebrae
//-- Data : 03/09/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class Criptografia
    {
        #region EncryptString
        /// <summary>
        /// Método para encriptografar.
        /// </summary>
        /// <param name="senha"></param>
        /// <returns></returns>
        public string EncryptString(string senha)
        {
            Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(senha);
            string encryptedConnectionString = Convert.ToBase64String(b);
            return encryptedConnectionString;
        }
        #endregion

        #region DecryptString
        /// <summary>
        /// Método que descriptografa
        /// </summary>
        /// <param name="criptog"></param>
        /// <returns></returns>
        public string DecryptString(string criptog)
        {
            Byte[] b = Convert.FromBase64String(criptog);
            string decryptedConnectionString = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return decryptedConnectionString;
        }
        #endregion


    }
}
