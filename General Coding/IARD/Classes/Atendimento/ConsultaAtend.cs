using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/07/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class ConsultaAtend // T013_CONSULTA
    {
        #region Atributos
        private Atendimento numAtend;
        private TipoConsultaAtend codTipoConsAt;
        private int codUsuario;
        private int codEscrSebrae;
        private int? numTotConBi;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int? NumTotConBi
        {
            get { return numTotConBi; }
            set { numTotConBi = value; }
        }
        public Atendimento NumAtend
        {
            get { return numAtend; }
            set { numAtend = value; }
        }
        public TipoConsultaAtend CodTipoConsAt
        {
            get { return codTipoConsAt; }
            set { codTipoConsAt = value; }
        }
        public int CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public int CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ConsultaAtend()
            : this(-1)
        { }
        public ConsultaAtend(int numAtend)
        {
            this.numAtend = new Atendimento();
            this.codTipoConsAt = new TipoConsultaAtend();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ConsultaAtend.ConsultaAtendInc";
        private const string SPUPDATE = "ConsultaAtend.ConsultaAtendAlt";
        private const string SPDELETE = "ConsultaAtend.ConsultaAtendDel";
        private const string SPSELECTID = "ConsultaAtend.ConsultaAtendSelId";
        private const string SPSELECTPAG = "ConsultaAtend.ConsultaAtendSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMnumAtend = "numAtend";
        private const string PARMcodTipoConsAt = "codTipoConsAt";
        private const string PARMcodUsuario = "codUsuario";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMCURSOR = "curConsultaAtend";
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
                    /*0*/ new OracleParameter(PARMnumAtend, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodUsuario, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter( "numTotConBi", OracleType.Int32),
                    /*5*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.numAtend.NumAtend;
            parms[1].Value = this.codTipoConsAt.CodTipoConsAt;
            parms[2].Value = this.codUsuario;
            parms[3].Value = this.codEscrSebrae;
            if (this.numTotConBi != null && this.numTotConBi.ToString() != "")
            {
                parms[4].Value = this.numTotConBi;
            }
            else 
            {
                parms[4].Value = DBNull.Value;
            }
            
            parms[5].Value = this.logUsuario;
            if (this.numAtend.NumAtend < 0)
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
                        //numAtend = Convert.ToInt32(cmd.Parameters[PARMnumAtend].Value);
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
                //numAtend = Convert.ToInt32(cmd.Parameters[PARMnumAtend].Value);
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
        /// <param name="numAtend">Código do Registro</param>
        public static void Delete(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4),
                new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4),
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4)
            };
            parms[0].Value = numAtend;
            parms[1].Value = codTipoConsAt;
            parms[2].Value = codUsuario;
            parms[3].Value = codEscrSebrae;

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
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4),
                new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4),
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4)
            };
                parms[0].Value = numAtend;
                parms[1].Value = codTipoConsAt;
                parms[2].Value = codUsuario;
                parms[3].Value = codEscrSebrae;

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
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codTipoConsAt;
            param[2].Value = codUsuario;
            param[3].Value = codEscrSebrae;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codTipoConsAt;
            param[2].Value = codUsuario;
            param[3].Value = codEscrSebrae;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>ConsultaAtend</returns>
        public static ConsultaAtend GetDataRow(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codTipoConsAt, codUsuario, codEscrSebrae);
            ConsultaAtend ConsultaAtend = new ConsultaAtend();
            try
            {
                if (dr.Read())
                {
                    ConsultaAtend.numAtend = new Atendimento(Convert.ToInt32(dr["A001_num_atend"]));
                    if (dr["A005_cd_tp_cons"] != DBNull.Value && dr["A005_cd_tp_cons"] != DBNull.Value && dr["A005_cd_tp_cons"].ToString() != "")
                        ConsultaAtend.codTipoConsAt = new TipoConsultaAtend(Convert.ToInt32(dr["A005_cd_tp_cons"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        ConsultaAtend.CodUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        ConsultaAtend.CodEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    if (dr["A013_tot_con_BI"] != DBNull.Value && dr["A013_tot_con_BI"] != DBNull.Value && dr["A013_tot_con_BI"].ToString() != "")
                        ConsultaAtend.numTotConBi = Convert.ToInt32(dr["A013_tot_con_BI"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ConsultaAtend.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ConsultaAtend = new ConsultaAtend();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ConsultaAtend;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ConsultaAtend</returns>
        public static ConsultaAtend GetDataRow(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codTipoConsAt, codUsuario, codEscrSebrae, trans);
            ConsultaAtend ConsultaAtend = new ConsultaAtend();
            try
            {
                if (dr.Read())
                {
                    ConsultaAtend.numAtend = new Atendimento(Convert.ToInt32(dr["A001_num_atend"]));
                    if (dr["A005_cd_tp_cons"] != DBNull.Value && dr["A005_cd_tp_cons"] != DBNull.Value && dr["A005_cd_tp_cons"].ToString() != "")
                        ConsultaAtend.codTipoConsAt = new TipoConsultaAtend(Convert.ToInt32(dr["A005_cd_tp_cons"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        ConsultaAtend.CodUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        ConsultaAtend.CodEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    if (dr["A013_tot_con_BI"] != DBNull.Value && dr["A013_tot_con_BI"] != DBNull.Value && dr["A013_tot_con_BI"].ToString() != "")
                        ConsultaAtend.numTotConBi = Convert.ToInt32(dr["A013_tot_con_BI"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ConsultaAtend.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ConsultaAtend = new ConsultaAtend();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ConsultaAtend;
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