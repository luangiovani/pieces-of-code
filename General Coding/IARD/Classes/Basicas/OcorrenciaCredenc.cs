using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 26/07/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class OcorrenciaCredenc  // T915_OCORRENCIA  
    {
        #region Atributos
        private int codOcorrencia;
        private Eventos codEvento;
        private Credenciado codCredenciado;
        private DateTime hraOcorrencia;
        private string tpoOcorrencia;
        private string txtObservacao;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodOcorrencia
        {
            get { return codOcorrencia; }
            set { codOcorrencia = value; }
        }
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public Credenciado CodCredenciado
        {
            get { return codCredenciado; }
            set { codCredenciado = value; }
        }
        public DateTime HraOcorrencia
        {
            get { return hraOcorrencia; }
            set { hraOcorrencia = value; }
        }
        public string TpoOcorrencia
        {
            get { return tpoOcorrencia; }
            set { tpoOcorrencia = value; }
        }
        public string TxtObservacao
        {
            get { return txtObservacao; }
            set { txtObservacao = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public OcorrenciaCredenc()
            : this(-1)
        { }
        public OcorrenciaCredenc(int codOcorrencia)
        {
            this.codOcorrencia = codOcorrencia;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "OcorrenciaCredenc.OcorrenciaCredencInc";
        private const string SPUPDATE = "OcorrenciaCredenc.OcorrenciaCredencAlt";
        private const string SPDELETE = "OcorrenciaCredenc.OcorrenciaCredencDel";
        private const string SPSELECTID = "OcorrenciaCredenc.OcorrenciaCredencSelId";
        private const string SPSELECTPAG = "OcorrenciaCredenc.OcorrenciaCredencSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codOcorrencia";
        private const string PARMCURSOR = "curOcorrenciaCredenc";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 2, ParameterDirection.InputOutput.ToString()) ,
                      new OracleParameter( "codEvento", OracleType.Int32),
                      new OracleParameter( "codCredenciado", OracleType.Int32),
                      new OracleParameter( "hraOcorrencia", OracleType.DateTime),
                      new OracleParameter( "tpoOcorrencia", OracleType.VarChar),
                      new OracleParameter( "txtObservacao", OracleType.VarChar),
                /*06*/new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codOcorrencia;
            parms[1].Value = this.CodEvento.CodEvento;
            parms[2].Value = this.CodCredenciado.CodCredenciado;
            parms[3].Value = this.hraOcorrencia;
            parms[4].Value = this.tpoOcorrencia.ToUpper();
            parms[5].Value = "";
            if (this.txtObservacao != null)
            { parms[5].Value = this.txtObservacao.ToUpper(); }
            parms[6].Value = this.logUsuario.ToUpper();
            if (this.codOcorrencia < 0)
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
                        codOcorrencia = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codOcorrencia = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
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
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
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

                new OracleParameter(PARMCODIGO, OracleType.Int32, 2), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
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
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2),
                                                              new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>OcorrenciaCredenc</returns>
        public static OcorrenciaCredenc GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            OcorrenciaCredenc OcorrenciaCredenc = new OcorrenciaCredenc();
            try
            {
                if (dr.Read())
                {
                    OcorrenciaCredenc.codOcorrencia = Convert.ToInt32(dr["A915_cod_ocorrencia"]);
                    OcorrenciaCredenc.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    OcorrenciaCredenc.codCredenciado.CodCredenciado = Convert.ToInt32(dr["A910_cod_credenciado"]);
                    if (dr["A915_hra_ocorrencia"] != DBNull.Value && dr["A915_hra_ocorrencia"] != DBNull.Value && dr["A915_hra_ocorrencia"].ToString() != "")
                        OcorrenciaCredenc.hraOcorrencia = Convert.ToDateTime(dr["A915_hra_ocorrencia"]);
                    if (dr["A915_tpo_ocorrencia"] != DBNull.Value && dr["A915_tpo_ocorrencia"] != DBNull.Value && dr["A915_tpo_ocorrencia"].ToString() != "")
                        OcorrenciaCredenc.tpoOcorrencia = Convert.ToString(dr["A915_tpo_ocorrencia"]);
                    if (dr["A915_txt_observacao"] != DBNull.Value && dr["A915_txt_observacao"] != DBNull.Value && dr["A915_txt_observacao"].ToString() != "")
                        OcorrenciaCredenc.txtObservacao = Convert.ToString(dr["A915_txt_observacao"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        OcorrenciaCredenc.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                OcorrenciaCredenc = new OcorrenciaCredenc();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return OcorrenciaCredenc;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>OcorrenciaCredenc</returns>
        public static OcorrenciaCredenc GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            OcorrenciaCredenc OcorrenciaCredenc = new OcorrenciaCredenc();
            try
            {
                if (dr.Read())
                {
                    OcorrenciaCredenc.codOcorrencia = Convert.ToInt32(dr["A915_cod_ocorrencia"]);
                    OcorrenciaCredenc.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    OcorrenciaCredenc.codCredenciado.CodCredenciado = Convert.ToInt32(dr["A910_cod_credenciado"]);
                    if (dr["A915_hra_ocorrencia"] != DBNull.Value && dr["A915_hra_ocorrencia"] != DBNull.Value && dr["A915_hra_ocorrencia"].ToString() != "")
                        OcorrenciaCredenc.hraOcorrencia = Convert.ToDateTime(dr["A915_hra_ocorrencia"]);
                    if (dr["A915_tpo_ocorrencia"] != DBNull.Value && dr["A915_tpo_ocorrencia"] != DBNull.Value && dr["A915_tpo_ocorrencia"].ToString() != "")
                        OcorrenciaCredenc.tpoOcorrencia = Convert.ToString(dr["A915_tpo_ocorrencia"]);
                    if (dr["A915_txt_observacao"] != DBNull.Value && dr["A915_txt_observacao"] != DBNull.Value && dr["A915_txt_observacao"].ToString() != "")
                        OcorrenciaCredenc.txtObservacao = Convert.ToString(dr["A915_txt_observacao"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        OcorrenciaCredenc.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                OcorrenciaCredenc = new OcorrenciaCredenc();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return OcorrenciaCredenc;
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
