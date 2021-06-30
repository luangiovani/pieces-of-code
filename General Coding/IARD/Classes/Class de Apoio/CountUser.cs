using System;
using System.Collections.Generic;
using System.Text;
using Classes;

namespace Classes
{
    public class CountUser
    {
        #region AddAssociado
        /// <summary>
        /// Adiciona associado a session do usuario
        /// </summary>
        /// <param name="codAssociadoParam"></param>
        /// <param name="UserCount"></param>
        /// <returns></returns>
        public static string AddAssociado(string sessionID, int codAssociadoParam, string UserCount)
        {
            //chama carregamento de associado
            string varUsers = "";
            string users = UserCount;
            string codAssociado = codAssociadoParam.ToString();

            foreach (string var in users.Split('@'))
            {
                if (var.Trim().Length > 0)
                {
                    string id = "";
                    if (var.Split('#').Length > 1)
                        id = var.Split('#')[1].ToString();
                    if (sessionID == id)
                    {
                        varUsers += codAssociado + "#" + id + "@";
                    }
                    else
                    {
                        varUsers += var + "@";
                    }
                }
            }
            return varUsers;
        }
        #endregion

        #region
        /// <summary>
        /// conta numeros de users por associado
        /// </summary>
        /// <param name="codAssociadoParam"></param>
        /// <param name="UserCount"></param>
        /// <returns></returns>
        public static int CountAssociado(int codAssociadoParam, string UserCount)
        {
            //conta numeros de users por associado
            int cont = 0;
            string users = UserCount;
            string codAssociado = codAssociadoParam.ToString();

            foreach (string var in users.Split('@'))
            {
                if (var.Trim().Length > 0)
                {
                    string id = "";
                    if (var.Split('#').Length > 1)
                        id = var.Split('#')[0].ToString();
                    if (codAssociado.Trim() == id.Trim())
                    {
                        cont++;
                    }
                }
            }
            return cont;
        }
        #endregion

        #region
        /// <summary>
        /// Remove a session que foi expirada
        /// </summary>
        /// <param name="codAssociadoParam"></param>
        /// <param name="UserCount"></param>
        /// <returns></returns>
        public static String RemoverSessao(string sessionID, string UserCount)
        {

            string varUsers = "";
            string users = UserCount;

            foreach (string var in users.Split('@'))
            {
                if (var.Trim().Length > 0)
                {
                    string id = "";
                    if (var.Split('#').Length > 1)
                        id = var.Split('#')[1].ToString();
                    if (sessionID != id)
                    {
                        varUsers += var + "@";
                    }
                }
            }
            return varUsers;
        }
        #endregion
    }
}
