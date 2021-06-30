using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 27/08/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class Encaminha // T090_ENCAMIN 
    {
        #region Atributos
        private int numSeqEnc;
        private Cliente codCliente;
        private ContatoCliente numContato;
        private PendenciaCli numPendencia;
        private Usuario codUsuario;
        private DateTime dtaEnc;
        private DateTime? dtaSol;
        private string dscSol;
        private string dscFonteSol;
        private string dscTemSol;
        private int indSolucao;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int NumSeqEnc
        {
            get { return numSeqEnc; }
            set { numSeqEnc = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public ContatoCliente NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }
        public PendenciaCli NumPendencia
        {
            get { return numPendencia; }
            set { numPendencia = value; }
        }
        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public DateTime DtaEnc
        {
            get { return dtaEnc; }
            set { dtaEnc = value; }
        }
        public DateTime? DtaSol
        {
            get { return dtaSol; }
            set { dtaSol = value; }
        }
        public string DscSol
        {
            get { return dscSol; }
            set { dscSol = value; }
        }
        public string DscFonteSol
        {
            get { return dscFonteSol; }
            set { dscFonteSol = value; }
        }
        public string DscTemSol
        {
            get { return dscTemSol; }
            set { dscTemSol = value; }
        }
        public int IndSolucao
        {
            get { return indSolucao; }
            set { indSolucao = value; }
        }

        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public Encaminha()
            : this(-1)
        { }
        public Encaminha(int numSeqEnc)
        {
            this.numSeqEnc = numSeqEnc;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Encaminha.EncaminhaInc";
        private const string SPUPDATE = "Encaminha.EncaminhaAlt";
        private const string SPDELETE = "Encaminha.EncaminhaDel";
        private const string SPSELECTID = "Encaminha.EncaminhaSelId";
        private const string SPSELECTPAG = "Encaminha.EncaminhaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMnumSeqEnc = "numSeqEnc";
        private const string PARMcodCliente = "codCliente";
        private const string PARMnumContato = "numContato";
        private const string PARMnumPendencia = "numPendencia";
        private const string PARMCURSOR = "curEncaminha";
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
                    /*0*/ new OracleParameter(PARMnumSeqEnc, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodCliente, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMnumContato, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMnumPendencia, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*5*/ new OracleParameter( "dtaEnc", OracleType.DateTime),
                    /*6*/ new OracleParameter( "dtaSol", OracleType.DateTime),
                    /*7*/ new OracleParameter( "dscSol", OracleType.LongVarChar),
                    /*8*/ new OracleParameter( "dscFonteSol", OracleType.VarChar),
                    /*9*/ new OracleParameter( "dscTemSol", OracleType.VarChar),
                    /*10*/ new OracleParameter( "indSolucao", OracleType.Int32),
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
            parms[0].Value = this.numSeqEnc;
            parms[1].Value = this.codCliente.CodCLIENTE;
            parms[2].Value = this.numContato.NumContato;
            parms[3].Value = this.numPendencia.NumPendencia;
            parms[4].Value = this.codUsuario.CodUsuario;
            parms[5].Value = this.dtaEnc;
            parms[6].Value = DBNull.Value;
            parms[7].Value = "";
            parms[8].Value = ""; 
            parms[9].Value = "";
            parms[10].Value = DBNull.Value;

            if (this.dtaSol != null)
                {parms[6].Value = this.dtaSol;}
            if (this.dscSol != null)
                {parms[7].Value = this.dscSol + " ";}
            if (this.dscFonteSol != null)
                {parms[8].Value = this.dscFonteSol;}
            if (this.dscTemSol != null)
                {parms[9].Value = this.dscTemSol;}
            if (this.indSolucao != null)
                { parms[10].Value = this.indSolucao; }

            parms[11].Value = this.logUsuario;
            if (this.numSeqEnc < 0)
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
                        numSeqEnc = Convert.ToInt32(cmd.Parameters[PARMnumSeqEnc].Value);
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
                numSeqEnc = Convert.ToInt32(cmd.Parameters[PARMnumSeqEnc].Value);
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
        /// <param name="numSeqEnc">Código do Registro</param>
        public static void Delete(int numSeqEnc, int codCliente, int numContato, int numPendencia)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumSeqEnc, OracleType.Int32, 4),
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                new OracleParameter(PARMnumContato, OracleType.Int32, 4),
                new OracleParameter(PARMnumPendencia, OracleType.Int32, 4)
            };
            parms[0].Value = numSeqEnc;
            parms[1].Value = codCliente;
            parms[2].Value = numContato;
            parms[3].Value = numPendencia;

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
        /// <param name="numSeqEnc">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int numSeqEnc, int codCliente, int numContato, int numPendencia, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumSeqEnc, OracleType.Int32, 4),
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                new OracleParameter(PARMnumContato, OracleType.Int32, 4),
                new OracleParameter(PARMnumPendencia, OracleType.Int32, 4)
            };
                parms[0].Value = numSeqEnc;
                parms[1].Value = codCliente;
                parms[2].Value = numContato;
                parms[3].Value = numPendencia;

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
        /// <param name="numSeqEnc">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numSeqEnc, int codCliente, int numContato, int numPendencia)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumSeqEnc, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumContato, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumPendencia, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numSeqEnc;
            param[1].Value = codCliente;
            param[2].Value = numContato;
            param[3].Value = numPendencia;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="numSeqEnc">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numSeqEnc, int codCliente, int numContato, int numPendencia, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumSeqEnc, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumContato, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumPendencia, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numSeqEnc;
            param[1].Value = codCliente;
            param[2].Value = numContato;
            param[3].Value = numPendencia;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="numSeqEnc">Código do Registro</param>
        /// <returns>Encaminha</returns>
        public static Encaminha GetDataRow(int numSeqEnc, int codCliente, int numContato, int numPendencia)
        {
            OracleDataReader dr = LoadDataDr(numSeqEnc, codCliente, numContato, numPendencia);
            Encaminha Encaminha = new Encaminha();
            try
            {
                if (dr.Read())
                {
                    Encaminha.numSeqEnc = Convert.ToInt32(dr["A090_num_seq_enc"]);
                    Encaminha.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    Encaminha.numContato = new ContatoCliente(Convert.ToInt32(dr["A014_num_cont"]));
                    Encaminha.numPendencia = new PendenciaCli(Convert.ToInt32(dr["A015_num_pend"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        Encaminha.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["A090_dt_enc"] != DBNull.Value && dr["A090_dt_enc"] != DBNull.Value && dr["A090_dt_enc"].ToString() != "")
                        Encaminha.dtaEnc = Convert.ToDateTime(dr["A090_dt_enc"]);
                    if (dr["A090_dt_sol"] != DBNull.Value && dr["A090_dt_sol"] != DBNull.Value && dr["A090_dt_sol"].ToString() != "")
                        Encaminha.dtaSol = Convert.ToDateTime(dr["A090_dt_sol"]);
                    if (dr["A090_dsc_sol"] != DBNull.Value && dr["A090_dsc_sol"] != DBNull.Value && dr["A090_dsc_sol"].ToString() != "")
                        Encaminha.dscSol = Convert.ToString(dr["A090_dsc_sol"]);
                    if (dr["A090_fonte_sol"] != DBNull.Value && dr["A090_fonte_sol"] != DBNull.Value && dr["A090_fonte_sol"].ToString() != "")
                        Encaminha.dscFonteSol = Convert.ToString(dr["A090_fonte_sol"]);
                    if (dr["A090_tem_sol"] != DBNull.Value && dr["A090_tem_sol"] != DBNull.Value && dr["A090_tem_sol"].ToString() != "")
                        Encaminha.dscTemSol = Convert.ToString(dr["A090_tem_sol"]);
                    if (dr["A090_ind_solucao"] != DBNull.Value && dr["A090_ind_solucao"] != DBNull.Value && dr["A090_ind_solucao"].ToString() != "")
                        Encaminha.indSolucao = Convert.ToInt32(dr["A090_ind_solucao"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Encaminha.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Encaminha = new Encaminha();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Encaminha;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="numSeqEnc">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Encaminha</returns>
        public static Encaminha GetDataRow(int numSeqEnc, int codCliente, int numContato, int numPendencia, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numSeqEnc, codCliente, numContato, numPendencia, trans);
            Encaminha Encaminha = new Encaminha();
            try
            {
                if (dr.Read())
                {
                    Encaminha.numSeqEnc = Convert.ToInt32(dr["A090_num_seq_enc"]);
                    Encaminha.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    Encaminha.numContato = new ContatoCliente(Convert.ToInt32(dr["A014_num_cont"]));
                    Encaminha.numPendencia = new PendenciaCli(Convert.ToInt32(dr["A015_num_pend"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        Encaminha.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["A090_dt_enc"] != DBNull.Value && dr["A090_dt_enc"] != DBNull.Value && dr["A090_dt_enc"].ToString() != "")
                        Encaminha.dtaEnc = Convert.ToDateTime(dr["A090_dt_enc"]);
                    if (dr["A090_dt_sol"] != DBNull.Value && dr["A090_dt_sol"] != DBNull.Value && dr["A090_dt_sol"].ToString() != "")
                        Encaminha.dtaSol = Convert.ToDateTime(dr["A090_dt_sol"]);
                    if (dr["A090_dsc_sol"] != DBNull.Value && dr["A090_dsc_sol"] != DBNull.Value && dr["A090_dsc_sol"].ToString() != "")
                        Encaminha.dscSol = Convert.ToString(dr["A090_dsc_sol"]);
                    if (dr["A090_fonte_sol"] != DBNull.Value && dr["A090_fonte_sol"] != DBNull.Value && dr["A090_fonte_sol"].ToString() != "")
                        Encaminha.dscFonteSol = Convert.ToString(dr["A090_fonte_sol"]);
                    if (dr["A090_tem_sol"] != DBNull.Value && dr["A090_tem_sol"] != DBNull.Value && dr["A090_tem_sol"].ToString() != "")
                        Encaminha.dscTemSol = Convert.ToString(dr["A090_tem_sol"]);
                    if (dr["A090_ind_solucao"] != DBNull.Value && dr["A090_ind_solucao"] != DBNull.Value && dr["A090_ind_solucao"].ToString() != "")
                        Encaminha.indSolucao = Convert.ToInt32(dr["A090_ind_solucao"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Encaminha.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Encaminha = new Encaminha();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Encaminha;
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