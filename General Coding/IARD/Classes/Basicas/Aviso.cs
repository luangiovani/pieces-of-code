using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 25/03/2009 
//-- Autor :  Juliano

namespace Classes
{
    public class Aviso
    {
        #region Atributos
        private int codAviso;
        private DateTime dtaInclusao;
        private int indDes;
        private string dscNov;
        private string logusuario;
        #endregion

        #region Propriedades
        public int CodAviso
        {
            get { return codAviso; }
            set { codAviso = value; }
        }
        public DateTime DtaInclusao
        {
            get { return dtaInclusao; }
            set { dtaInclusao = value; }
        }
        public int IndDes
        {
            get { return indDes; }
            set { indDes = value; }
        }
        public string DscNov
        {
            get { return dscNov; }
            set { dscNov = value; }
        }
        public string Logusuario
        {
            get { return logusuario; }
            set { logusuario = value; }
        }
        #endregion

        #region Construtores
        public Aviso()
            : this(-1)
        { }
        public Aviso(int codAviso)
        {
            this.codAviso = codAviso;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Aviso.AvisoInc";
        private const string SPUPDATE = "Aviso.AvisoAlt";
        private const string SPDELETE = "Aviso.AvisoDel";
        private const string SPSELECTID = "Aviso.AvisoSelId";
        private const string SPSELECTPAG = "Aviso.AvisoSelPag";
        private const string SPDetalheInstrutor = "Aviso.DetalheInstrutorSelId";
        private const string SPDetalheProduto = "Aviso.DetalheProdutoSelId";
        
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codAviso";
        #endregion

        #region Métodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = null;// DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32),
                /*1*/ new OracleParameter( "dscAviso", OracleType.VarChar),
                /*2*/ new OracleParameter( "indDes", OracleType.Int32),
                /*3*/ new OracleParameter( "logUsuario", OracleType.VarChar)
            };

                // Criando cache dos parameters 
                DataBase.CacheParameters(SPINSERT, parms);
            }
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codAviso;
            parms[1].Value = this.dscNov;
            parms[2].Value = this.indDes;
            parms[3].Value = this.logusuario;

            parms[0].Direction = ParameterDirection.Input;

        }
        #endregion

        #region Insert

        /// <summary>
        /// Insert com tratamento de transação
        /// </summary>
        public void Insert()
        {
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);

            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codAviso = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                        cmd.Parameters.Clear();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        }

        /// <summary>
        /// Insert sem tratamento de transação
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Insert(OracleTransaction trans)
        {
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            try
            {
                OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identificação do registro inserido.
                codAviso = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Update

        /// <summary>
        /// Update com tratamento de transação
        /// </summary>
        public void Update()
        {
            // --------------------------------------------------------  
            // Obtendo e setando os parâmetros 
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            // -------------------------------------------------------- 
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        } // END UPDATE

        /// <summary>
        /// Update sem tratamento de transação
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Update(OracleTransaction trans)
        {
            // -------------------------------------------------------- 
            // Obtendo e setando os parâmetros 
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            // -------------------------------------------------------- 
            try
            {
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } // END UPDATE 
        #endregion

        #region Delete

        /// <summary>
        /// Delete com tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
            parms[0].Value = codigo;
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end DELETE

        /// <summary>
        /// Delete sem tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
                parms[0].Value = codigo;
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region LoadDataDr

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                new OracleParameter("curAviso", OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                              new OracleParameter("curAviso", OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrDetalheInstrutor(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                new OracleParameter("curAviso", OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPDetalheInstrutor, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrDetalheProduto(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                new OracleParameter("curAviso", OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPDetalheProduto, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>InstituiEntidade</returns>
        public static Aviso GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Aviso aviso = new Aviso();
            try
            {
                if (dr.Read())
                {
                    aviso.codAviso = Convert.ToInt32(dr["A016_cd_aviso"]);
                    if (dr["A016_dsc_nov"] != DBNull.Value && dr["A016_dsc_nov"] != DBNull.Value && dr["A016_dsc_nov"].ToString() != "")
                        aviso.dscNov = dr["A016_dsc_nov"].ToString();
                    if (dr["A016_dt_inc"] != DBNull.Value && dr["A016_dt_inc"] != DBNull.Value && dr["A016_dt_inc"].ToString() != "")
                        aviso.dtaInclusao = Convert.ToDateTime(dr["A016_dt_inc"]);
                    if (dr["A016_ind_des"] != DBNull.Value && dr["A016_ind_des"] != DBNull.Value && dr["A016_ind_des"].ToString() != "")
                        aviso.indDes = Convert.ToInt32(dr["A016_ind_des"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        aviso.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                aviso = new Aviso();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return aviso;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>InstituiEntidade</returns>
        public static Aviso GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Aviso aviso = new Aviso();
            try
            {
                if (dr.Read())
                {
                    aviso.codAviso = Convert.ToInt32(dr["A016_cd_aviso"]);
                    if (dr["A016_dsc_nov"] != DBNull.Value && dr["A016_dsc_nov"] != DBNull.Value && dr["A016_dsc_nov"].ToString() != "")
                        aviso.dscNov = dr["A016_dsc_nov"].ToString();
                    if (dr["A016_dt_inc"] != DBNull.Value && dr["A016_dt_inc"] != DBNull.Value && dr["A016_dt_inc"].ToString() != "")
                        aviso.dtaInclusao = Convert.ToDateTime(dr["A016_dt_inc"]);
                    if (dr["A016_ind_des"] != DBNull.Value && dr["A016_ind_des"] != DBNull.Value && dr["A016_ind_des"].ToString() != "")
                        aviso.indDes = Convert.ToInt32(dr["A016_ind_des"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        aviso.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                aviso = new Aviso();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return aviso;
        }
        #endregion

        #region LoadDataPaginacao

        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cláusula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">Número da página que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada página</param>
        /// <param name="ExpressaoOrdenacao">Expressão de ordenação</param>
        /// <returns>Instância do objeto Paginação, contendo um DataReader e o total de registros</returns>
        /// 
        public static Paginacao LoadDataPaginacao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
            new OracleParameter("sWhere", OracleType.VarChar,5000),
		    new OracleParameter("CurrentPage", OracleType.Int32),
		    new OracleParameter("PageSize", OracleType.Int32),
		    new OracleParameter("SortExpression", OracleType.VarChar,50),
            new OracleParameter("curAviso", OracleType.Cursor),
            new OracleParameter("nRegistro", OracleType.Int32)
          };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;


            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion

        #endregion

        #region ValidaDados

        #endregion

    }

    public class clsExtenso
    {

        private static string[] strUnidades = {
			"",
			"Um",
			"Dois",
			"Três",
			"Quatro",
			"Cinco",
			"Seis",
			"Sete",
			"Oito",
			"Nove",
			"Dez",
			"Onze",
			"Doze",
			"Treze",
			"Quatorze",
			"Quinze",
			"Dezesseis",
			"Dezessete",
			"Dezoito",
			"Dezenove"
		};
        private static string[] strDezenas = {
			"",
			"Dez",
			"Vinte",
			"Trinta",
			"Quarenta",
			"Cinqüenta",
			"Sessenta",
			"Setenta",
			"Oitenta",
			"Noventa"
		};
        private static string[] strCentenas = {
			"",
			"Cem",
			"Duzentos",
			"Trezentos",
			"Quatrocentos",
			"Quinhentos",
			"Seiscentos",
			"Setecentos",
			"Oitocentos",
			"Novecentos"
		};
        private static string strErrorString = "Valor fora da faixa";
        private static decimal decMin = Convert.ToDecimal("0,01");
        private static decimal decMax = 1000000000;
        private static string strMoeda = " Real ";
        private static string strMoedas = " Reais ";
        private static string strCentesimo = " Centavo ";

        private static string strCentesimos = " Centavos ";
        private static string ConversaoRecursiva(Int64 intNumero)
        {
            Int64 intResto = 0;

            if (intNumero==0)
                return "";

            if (((intNumero >= 1) && (intNumero <= 19)))
            {
                return strUnidades[intNumero];
            }
            else if (((intNumero == 20) || ((intNumero == 30) || ((intNumero == 40) || ((intNumero == 50) || ((intNumero == 60) || ((intNumero == 70) || ((intNumero == 80) || (intNumero == 90)))))))))
            {
                return (strDezenas[Math.DivRem(intNumero, 10, out intResto)] + " ");
            }
            else if ((((intNumero >= 21) && (intNumero <= 29)) || (((intNumero >= 31) && (intNumero <= 39)) || (((intNumero >= 41) && (intNumero <= 49)) || (((intNumero >= 51) && (intNumero <= 59)) || (((intNumero >= 61) && (intNumero <= 69)) || (((intNumero >= 71) && (intNumero <= 79)) || (((intNumero >= 81) && (intNumero <= 89)) || ((intNumero >= 91) && (intNumero <= 99))))))))))
            {
                return (strDezenas[Math.DivRem(intNumero, 10, out intResto)] + (" e " + ConversaoRecursiva((intNumero % 10))));
            }
            else if (((intNumero == 100) || ((intNumero == 200) || ((intNumero == 300) || ((intNumero == 400) || ((intNumero == 500) || ((intNumero == 600) || ((intNumero == 700) || ((intNumero == 800) || (intNumero == 900))))))))))
            {
                return (strCentenas[Math.DivRem(intNumero, 100, out intResto)] + " ");
            }
            else if (((intNumero >= 101) && (intNumero <= 199)))
            {
                return (" Cento e " + ConversaoRecursiva((intNumero % 100)));
            }
            else if ((((intNumero >= 201) && (intNumero <= 299)) || (((intNumero >= 301) && (intNumero <= 399)) || (((intNumero >= 401) && (intNumero <= 499)) || (((intNumero >= 501) && (intNumero <= 599)) || (((intNumero >= 601) && (intNumero <= 699)) || (((intNumero >= 701) && (intNumero <= 799)) || (((intNumero >= 801) && (intNumero <= 899)) || ((intNumero >= 901) && (intNumero <= 999))))))))))
            {
                return (strCentenas[Math.DivRem(intNumero, 100, out intResto)] + (" e " + ConversaoRecursiva((intNumero % 100))));
            }
            else if (((intNumero >= 1000) && (intNumero <= 999999)))
            {
                return (ConversaoRecursiva(Math.DivRem(intNumero, 1000, out intResto)) + (" Mil " + ConversaoRecursiva((intNumero % 1000))));
            }
            else if (((intNumero >= 1000000) && (intNumero <= 1999999)))
            {
                return (ConversaoRecursiva(Math.DivRem(intNumero, 1000000, out intResto)) + (" Milhão " + ConversaoRecursiva((intNumero % 1000000))));
            }
            else if (((intNumero >= 2000000) && (intNumero <= 999999999)))
            {
                return (ConversaoRecursiva(Math.DivRem(intNumero, 1000000, out intResto)) + (" Milhões " + ConversaoRecursiva((intNumero % 1000000))));
            }
            else if (((intNumero >= 1000000000) && (intNumero <= 1999999999)))
            {
                return (ConversaoRecursiva(Math.DivRem(intNumero, 1000000000, out intResto)) + (" Bilhão " + ConversaoRecursiva((intNumero % 1000000000))));
            }
            return (ConversaoRecursiva(Math.DivRem(intNumero, 1000000000, out intResto)) + (" Bilhões " + ConversaoRecursiva((intNumero % 1000000000))));
            return (ConversaoRecursiva(Math.DivRem(intNumero, 1000000000000L, out intResto)) + (" Trilhão " + ConversaoRecursiva(intNumero % 1000000000000L)));
            return (ConversaoRecursiva(Math.DivRem(intNumero, 1000000000000L, out intResto)) + (" Trilhões " + ConversaoRecursiva(intNumero % 1000000000000L)));
            return "";
        }

        private static string LimpaEspacos(string strTexto)
        {
            string strRetorno = "";
            bool booUltIs32 = false;
            foreach (char chrChar in strTexto)
            {
                if (chrChar.ToString() != " ")
                {
                    strRetorno = (strRetorno + chrChar);
                    booUltIs32 = false;
                }
                else if (!booUltIs32)
                {
                    strRetorno = (strRetorno + chrChar);
                    booUltIs32 = true;
                }
            }
            return strRetorno.Trim();
        }

        public static string NumeroParaExtenso(decimal decNumero)
        {
            string strRetorno = "";
            if (((decNumero >= decMin) && (decNumero <= decMax)))
            {
                Int64 intInteiro = Convert.ToInt64(Math.Truncate(decNumero));
                Int64 intCentavos = Convert.ToInt64(Math.Truncate(((decNumero - Math.Truncate(decNumero)) * 100)));
                if ((intInteiro <= 1))
                {
                    strRetorno = strRetorno + (ConversaoRecursiva(intInteiro)) + strMoeda;
                }
                else
                {
                    strRetorno = strRetorno + (ConversaoRecursiva(intInteiro)) + strMoedas;

                }
                if ((intCentavos > 0))
                {
                    if ((intCentavos == 1))
                    {
                        strRetorno = strRetorno + " e " + (ConversaoRecursiva(intCentavos)) + strCentesimo;
                    }
                    else
                    {
                        strRetorno = strRetorno + " e " + (ConversaoRecursiva(intCentavos)) + strCentesimos;
                    }
                }
            }
            else
            {
                //throw new Exception(strErrorString);
                strRetorno = "";
                return strRetorno;
            }
            return LimpaEspacos(strRetorno);
        }
    }

}
