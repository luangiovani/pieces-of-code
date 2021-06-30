using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 2010
//-- Autor :  Honorato

namespace Classes
{
    public class LC
    {
        #region Atributos

        #endregion

        #region Propriedades

        #endregion

        #region Construtores
        #endregion

        #region StoredProcedures
        #endregion

        #region Parametros Oracle
        #endregion

        #region Metodos
        public static void Busca()
        {
            string _comando = "select * from Sebrae.T001_atend_sinco ";
            OracleDataReader dr2 = Classes.DataBase.ExecuteReader(CommandType.Text, _comando);
            dr2.Close();
        }

        #endregion


    }
}