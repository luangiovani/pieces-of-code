using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 5/11/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class ProjetoAcaoNa // T774_SOL_OBJ_AE
    {
        #region Atributos
        private string dscCodSol;
        private string dscCobObj;
        private string dscCodAb;
        private string dscCodAe;
        private string logUsuario;
        #endregion

        #region Propriedades
        public string DscCodSol
        {
          get { return dscCodSol; }
          set { dscCodSol = value; }
        }
        public string DscCobObj
        {
          get { return dscCobObj; }
          set { dscCobObj = value; }
        }
        public string DscCodAb
        {
          get { return dscCodAb; }
          set { dscCodAb = value; }
        }
        public string DscCodAe
        {
          get { return dscCodAe; }
          set { dscCodAe = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ProjetoAcaoNa()
            : this("")
        { }
        public ProjetoAcaoNa(string dscCodSol)
        {
            this.dscCodSol = dscCodSol;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ProjetoAcaoNa.ProjetoAcaoNaInc";
        private const string SPUPDATE = "ProjetoAcaoNa.ProjetoAcaoNaAlt";
        private const string SPDELETE = "ProjetoAcaoNa.ProjetoAcaoNaDel";
        private const string SPSELECTID = "ProjetoAcaoNa.ProjetoAcaoNaSelId";
        private const string SPSELECTPAG = "ProjetoAcaoNa.ProjetoAcaoNaSelPag";
        private const string SPSELECTPAG2010 = "ProjetoAcaoNa.ProjetoAcaoNaSelPag2010";
        private const string SPSELECTProjetoPAG2010 = "ProjetoAcaoNa.ProjetoNaSelPag2010";
        private const string SPSELECTPROJETOSELID = "ProjetoAcaoNa.ProjetoSelId";
        private const string SPSELECTACAOSELID = "ProjetoAcaoNa.AcaoSelId";
        #endregion

        #region Parametros Oracle
        private const string PARMdscCodSol = "dscCodSol";
        private const string PARMdscCobObj = "dscCobObj";
        private const string PARMdscCodAb = "dscCodAb";
        private const string PARMdscCodAe = "dscCodAe";
        private const string PARMCURSOR = "curProjetoAcaoNa";
        #endregion

        #region Metodos


        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMdscCodSol, OracleType.VarChar, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMdscCobObj, OracleType.VarChar, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMdscCodAb, OracleType.VarChar, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMdscCodAe, OracleType.VarChar, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.dscCodSol;
            parms[1].Value = this.dscCobObj;
            parms[2].Value = this.dscCodAb;
            parms[3].Value = this.dscCodAe;
            parms[4].Value = this.logUsuario;
            if (this.dscCodSol == "" )
            {
                parms[0].Direction = ParameterDirection.Output;
            }
            else
            {
                parms[0].Direction = ParameterDirection.Input;
            }
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
                        dscCodSol = Convert.ToString(cmd.Parameters[PARMdscCodSol].Value);
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
                dscCodSol = Convert.ToString(cmd.Parameters[PARMdscCodSol].Value);
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
        /// <param name="dscCodSol">Código do Registro</param>
        public static void Delete(string dscCodSol, string dscCobObj, string dscCodAb, string dscCodAe)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMdscCodSol, OracleType.VarChar, 4),
                new OracleParameter(PARMdscCobObj, OracleType.VarChar, 4),
                new OracleParameter(PARMdscCodAb, OracleType.VarChar, 4),
                new OracleParameter(PARMdscCodAe, OracleType.VarChar, 4)
            };
            parms[0].Value = dscCodSol;
            parms[1].Value = dscCobObj;
            parms[2].Value = dscCodAb;
            parms[3].Value = dscCodAe;

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
        /// <param name="dscCodSol">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(string dscCodSol, string dscCobObj, string dscCodAb, string dscCodAe, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMdscCodSol, OracleType.VarChar, 4),
                new OracleParameter(PARMdscCobObj, OracleType.VarChar, 4),
                new OracleParameter(PARMdscCodAb, OracleType.VarChar, 4),
                new OracleParameter(PARMdscCodAe, OracleType.VarChar, 4)
            };
                parms[0].Value = dscCodSol;
                parms[1].Value = dscCobObj;
                parms[2].Value = dscCodAb;
                parms[3].Value = dscCodAe;

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
        /// <param name="dscCodSol">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string dscCodSol, string dscCobObj, string dscCodAb, string dscCodAe)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMdscCodSol, OracleType.VarChar, 4), 
                    new OracleParameter(PARMdscCobObj, OracleType.VarChar, 4), 
                    new OracleParameter(PARMdscCodAb, OracleType.VarChar, 4), 
                    new OracleParameter(PARMdscCodAe, OracleType.VarChar, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = dscCodSol;
            param[1].Value = dscCobObj;
            param[2].Value = dscCodAb;
            param[3].Value = dscCodAe;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="dscCodSol">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(string dscCodSol, string dscCobObj, string dscCodAb, string dscCodAe, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMdscCodSol, OracleType.VarChar, 4), 
                    new OracleParameter(PARMdscCobObj, OracleType.VarChar, 4), 
                    new OracleParameter(PARMdscCodAb, OracleType.VarChar, 4), 
                    new OracleParameter(PARMdscCodAe, OracleType.VarChar, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = dscCodSol;
            param[1].Value = dscCobObj;
            param[2].Value = dscCodAb;
            param[3].Value = dscCodAe;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="dscCodSol">Código do Registro</param>
        /// <returns>ProjetoAcaoNa</returns>
        public static ProjetoAcaoNa GetDataRow(string dscCodSol, string dscCobObj, string dscCodAb, string dscCodAe)
        {
            OracleDataReader dr = LoadDataDr(dscCodSol, dscCobObj, dscCodAb, dscCodAe);
            ProjetoAcaoNa ProjetoAcaoNa = new ProjetoAcaoNa();
            try
            {
                if (dr.Read())
                {
                    ProjetoAcaoNa.dscCodSol = Convert.ToString(dr["dscCodSol"]);
                    ProjetoAcaoNa.dscCobObj = Convert.ToString(dr["dscCobObj"]);
                    ProjetoAcaoNa.dscCodAb = Convert.ToString(dr["dscCodAb"]);
                    ProjetoAcaoNa.dscCodAe = Convert.ToString(dr["dscCodAe"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProjetoAcaoNa.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProjetoAcaoNa = new ProjetoAcaoNa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProjetoAcaoNa;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="dscCodSol">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ProjetoAcaoNa</returns>
        public static ProjetoAcaoNa GetDataRow(string dscCodSol, string dscCobObj, string dscCodAb, string dscCodAe, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(dscCodSol, dscCobObj, dscCodAb, dscCodAe, trans);
            ProjetoAcaoNa ProjetoAcaoNa = new ProjetoAcaoNa();
            try
            {
                if (dr.Read())
                {
                    ProjetoAcaoNa.dscCodSol = Convert.ToString(dr["dscCodSol"]);
                    ProjetoAcaoNa.dscCobObj = Convert.ToString(dr["dscCobObj"]);
                    ProjetoAcaoNa.dscCodAb = Convert.ToString(dr["dscCodAb"]);
                    ProjetoAcaoNa.dscCodAe = Convert.ToString(dr["dscCodAe"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProjetoAcaoNa.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProjetoAcaoNa = new ProjetoAcaoNa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProjetoAcaoNa;
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
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
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

        public static Paginacao LoadDataPaginacao2010(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
              };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG2010, parms);

            paginacao.DataReader = dr;
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static Paginacao LoadDataPaginacaoprojeto2010(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
              };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTProjetoPAG2010, parms);

            paginacao.DataReader = dr;
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        #endregion


        #endregion

        #region Métodos Específicos

        #region LoadDataProjeto

        public static OracleDataReader LoadDataProjeto()
        {
            OracleParameter[] parms = new OracleParameter[] {  
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
              };

            parms[0].Direction = ParameterDirection.Output;


            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPROJETOSELID, parms);

            return dr;
        }
        #endregion



        #region LoadDataAcao

        public static OracleDataReader LoadDataAcao(string codProjeto)
        {
            OracleParameter[] parms = new OracleParameter[] {                
                new OracleParameter("dscCodSol", OracleType.VarChar),
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };            

            parms[0].Value = codProjeto;
            parms[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTACAOSELID, parms);

            return dr;
        }
        #endregion
        
        #endregion
    }
}