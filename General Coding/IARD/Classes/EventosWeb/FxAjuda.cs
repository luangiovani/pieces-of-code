using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 31/10/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class FxAjuda // T2203_FX_AJUDA
    {
        #region Atributos
        private Grupo2201 codGrupo2201;
        private int codInicioFx;
        private int codFimFx;
        private Usuario codUsuario;
        private int vlrAjuda;
		
        private string logUsuario;
        #endregion

        #region Propriedades
        public Grupo2201 CodGrupo2201
        {
          get { return codGrupo2201; }
          set { codGrupo2201 = value; }
        }
        public int CodInicioFx
        {
          get { return codInicioFx; }
          set { codInicioFx = value; }
        }
        public int CodFimFx
        {
          get { return codFimFx; }
          set { codFimFx = value; }
        }
        public Usuario CodUsuario
        {
          get { return codUsuario; }
          set { codUsuario = value; }
        }
        public int VlrAjuda
        {
          get { return vlrAjuda; }
          set { vlrAjuda = value; }
        }
        public string LogUsuario 
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public FxAjuda()
            : this(-1)
        { }
        public FxAjuda(int codInicioFx)
        {
            this.codGrupo2201 = new Grupo2201();
            this.codInicioFx = codInicioFx;
            //this.codFimFx = codFimFx;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "FxAjuda.FxAjudaInc";
        private const string SPUPDATE = "FxAjuda.FxAjudaAlt";
        private const string SPDELETE = "FxAjuda.FxAjudaDel";
        private const string SPSELECTID = "FxAjuda.FxAjudaSelId";
        private const string SPSELECTPAG = "FxAjuda.FxAjudaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodGrupo2201 = "codGrupo2201";
        private const string PARMcodInicioFx = "codInicioFx";
        private const string PARMcodFimFx = "codFimFx";
        private const string PARMCURSOR = "curFxAjuda";
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
                    /*0*/ new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodInicioFx, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodFimFx, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*6*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*7*/ new OracleParameter( "vlrAjuda", OracleType.Int32),
                    /*11*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codGrupo2201;
            parms[1].Value = this.codInicioFx;
            parms[2].Value = this.codFimFx;
            parms[3].Value = this.codUsuario;
            parms[4].Value = this.vlrAjuda;
            parms[5].Value = this.logUsuario;
            if (this.codGrupo2201.CodGrupo2201 < 0)
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
                        //codGrupo2201 = new Grupo2201(Convert.ToInt32(cmd.Parameters[PARMcodGrupo2201].Value));
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
                //codGrupo2201 = new Grupo2201(Convert.ToInt32(cmd.Parameters[PARMcodGrupo2201].Value));
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
        /// <param name="codGrupo2201">Código do Registro</param>
        public static void Delete(int codGrupo2201, int codInicioFx, int codFimFx)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodInicioFx, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodFimFx, OracleType.Int32, 4)
            };
            parms[0].Value = codGrupo2201;
            parms[1].Value = codInicioFx;
            parms[2].Value = codFimFx;
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
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codGrupo2201, int codInicioFx, int codFimFx, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodInicioFx, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodFimFx, OracleType.Int32, 4) 
            };
                parms[0].Value = codGrupo2201;
                parms[1].Value = codInicioFx;
                parms[2].Value = codFimFx;
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
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codGrupo2201, int codInicioFx, int codFimFx)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodInicioFx, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodFimFx, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo2201;
            param[1].Value = codInicioFx;
            param[2].Value = codFimFx;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codGrupo2201, int codInicioFx, int codFimFx, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodInicioFx, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodFimFx, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo2201;
            param[1].Value = codInicioFx;
            param[2].Value = codFimFx;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <returns>FxAjuda</returns>
        public static FxAjuda GetDataRow(int codGrupo2201, int codInicioFx, int codFimFx)
        {
            OracleDataReader dr = LoadDataDr(codGrupo2201, codInicioFx, codFimFx);
            FxAjuda FxAjuda = new FxAjuda();
            try
            {
                if (dr.Read())
                {
                    FxAjuda.codGrupo2201 = new Grupo2201(Convert.ToInt32(dr["A2201_cd_grupo"]));
                    FxAjuda.codInicioFx = Convert.ToInt32(dr["A2203_inicio_fx"]);
                    FxAjuda.codFimFx = Convert.ToInt32(dr["A2203_fim_fx"]);
                    FxAjuda.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    FxAjuda.vlrAjuda = Convert.ToInt32(dr["A2203_valor_ajuda"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        FxAjuda.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                FxAjuda = new FxAjuda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return FxAjuda;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>FxAjuda</returns>
        public static FxAjuda GetDataRow(int codGrupo2201, int codInicioFx, int codFimFx, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codGrupo2201, codInicioFx, codFimFx, trans);
            FxAjuda FxAjuda = new FxAjuda();
            try
            {
                if (dr.Read())
                {
                    FxAjuda.codGrupo2201 = new Grupo2201(Convert.ToInt32(dr["A2201_cd_grupo"]));
                    FxAjuda.codInicioFx = Convert.ToInt32(dr["A2203_inicio_fx"]);
                    FxAjuda.codFimFx = Convert.ToInt32(dr["A2203_fim_fx"]);
                    FxAjuda.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    FxAjuda.vlrAjuda = Convert.ToInt32(dr["A2203_valor_ajuda"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        FxAjuda.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                FxAjuda = new FxAjuda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return FxAjuda;
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
        #endregion


        #endregion

    }
}