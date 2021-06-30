using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 16/05/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class DetalheRP // T034_DETALHE_RP 
    {
        #region Atributos
        private int codDetalheRP;
        private GrupoRP codGrupoRP;
        private SubgrupoRP codSubGrupoRP;
        private string dscDetalheRP;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodDetalheRP
        {
            get { return codDetalheRP; }
            set { codDetalheRP = value; }
        }
        public GrupoRP CodGrupoRP
        {
            get { return codGrupoRP; }
            set { codGrupoRP = value; }
        }
        public SubgrupoRP CodSubGrupoRP
        {
            get { return codSubGrupoRP; }
            set { codSubGrupoRP = value; }
        }
        public string DscDetalheRP
        {
            get { return dscDetalheRP; }
            set { dscDetalheRP = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public DetalheRP()
            : this(-1)
        { }
        public DetalheRP(int codDetalheRP)
        {
            this.codDetalheRP = codDetalheRP;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "DetalheRP.DetalheRPInc";
        private const string SPUPDATE = "DetalheRP.DetalheRPAlt";
        private const string SPDELETE = "DetalheRP.DetalheRPDel";
        private const string SPSELECTID = "DetalheRP.DetalheRPSelId";
        private const string SPSELECTPAG = "DetalheRP.DetalheRPSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codDetalheRP";
        private const string PARMCODIGO2 = "codGrupoRP";
        private const string PARMCODIGO3 = "codSubGrupoRP";
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODIGO2, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMCODIGO3, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "dscDetalheRP", OracleType.VarChar),
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
            parms[0].Value = this.codDetalheRP;
            parms[1].Value = this.codGrupoRP;
            parms[2].Value = this.codSubGrupoRP;
            parms[3].Value = this.dscDetalheRP;
            parms[4].Value = this.logUsuario;
            if (this.codDetalheRP < 0)
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
                        codDetalheRP = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codDetalheRP = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static void Delete(int codigo, int codigo2, int codigo3)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODIGO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODIGO2, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODIGO3, OracleType.Int32, 4)
            };
            parms[0].Value = codigo;
            parms[1].Value = codigo2;
            parms[2].Value = codigo3;
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
        public static void Delete(int codigo, int codigo2, int codigo3, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODIGO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODIGO2, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODIGO3, OracleType.Int32, 4) 
            };
                parms[0].Value = codigo;
                parms[1].Value = codigo2;
                parms[2].Value = codigo3;
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
        public static OracleDataReader LoadDataDr(int codigo, int codigo2, int codigo3)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODIGO2, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODIGO3, OracleType.Int32, 4), 
                    new OracleParameter("curDetalheRP", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Value = codigo2;
            param[2].Value = codigo3;
            param[3].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codigo, int codigo2, int codigo3, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODIGO2, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODIGO3, OracleType.Int32, 4), 
                    new OracleParameter("curDetalheRP", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Value = codigo2;
            param[2].Value = codigo3;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DetalheRP</returns>
        public static DetalheRP GetDataRow(int codigo, int codigo2, int codigo3)
        {
            OracleDataReader dr = LoadDataDr(codigo, codigo2, codigo3);
            DetalheRP detalheRP = new DetalheRP();
            try
            {
                if (dr.Read())
                {
                    detalheRP.codDetalheRP = Convert.ToInt32(dr["A034_cd_det_rp"]);
                    if (dr["A069_cd_gru_rp"] != DBNull.Value && dr["A069_cd_gru_rp"] != DBNull.Value && dr["A069_cd_gru_rp"].ToString() != "")
                        detalheRP.codGrupoRP = new GrupoRP(Convert.ToInt32(dr["A069_cd_gru_rp"]));
                    if (dr["A070_cd_sgr_rp"] != DBNull.Value && dr["A070_cd_sgr_rp"] != DBNull.Value && dr["A070_cd_sgr_rp"].ToString() != "")
                        detalheRP.codSubGrupoRP = new SubgrupoRP(Convert.ToInt32(dr["A070_cd_sgr_rp"]));
                    detalheRP.dscDetalheRP = Convert.ToString(dr["A034_dsc_det_rp"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        detalheRP.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                detalheRP = new DetalheRP();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return detalheRP;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DetalheRP</returns>
        public static DetalheRP GetDataRow(int codigo, int codigo2, int codigo3, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, codigo2, codigo3, trans);
            DetalheRP detalheRP = new DetalheRP();
            try
            {
                if (dr.Read())
                {
                    detalheRP.codDetalheRP = Convert.ToInt32(dr["A034_cd_det_rp"]);
                    if (dr["A069_cd_gru_rp"] != DBNull.Value && dr["A069_cd_gru_rp"] != DBNull.Value && dr["A069_cd_gru_rp"].ToString() != "")
                        detalheRP.codGrupoRP = new GrupoRP(Convert.ToInt32(dr["A069_cd_gru_rp"]));
                    if (dr["A070_cd_sgr_rp"] != DBNull.Value && dr["A070_cd_sgr_rp"] != DBNull.Value && dr["A070_cd_sgr_rp"].ToString() != "")
                        detalheRP.codSubGrupoRP = new SubgrupoRP(Convert.ToInt32(dr["A070_cd_sgr_rp"]));
                    detalheRP.dscDetalheRP = Convert.ToString(dr["A034_dsc_det_rp"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        detalheRP.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                detalheRP = new DetalheRP();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return detalheRP;
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
                new OracleParameter("curDetalheRP", OracleType.Cursor),
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
