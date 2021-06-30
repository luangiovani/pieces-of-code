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
    public class EmailRodizio // T1145_EMAIL_RODIZIO
    {
        #region Atributos
        private TituloEvento codTituloEv;
        private Pessoa codUsuario;
        private int indInc;
        private int indAlt;
        private int indExc;
        private int indPen;
        private int indEnvia;
        private string logUsuario;
        #endregion

        #region Propriedades
        public TituloEvento CodTituloEv
        {
          get { return codTituloEv; }
          set { codTituloEv = value; }
        }
        public Pessoa CodUsuario
        {
          get { return codUsuario; }
          set { codUsuario = value; }
        }
        public int IndInc
        {
          get { return indInc; }
          set { indInc = value; }
        }
        public int IndAlt
        {
          get { return indAlt; }
          set { indAlt = value; }
        }
        public int IndExc
        {
          get { return indExc; }
          set { indExc = value; }
        }
        public int IndPen
        {
          get { return indPen; }
          set { indPen = value; }
        }
        public int IndEnvia
        {
          get { return indEnvia; }
          set { indEnvia = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public EmailRodizio()
            : this(-1)
        { }
        public EmailRodizio(int codTituloEv)
        {
            this.codTituloEv = new TituloEvento();
            this.codUsuario = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "EmailRodizio.EmailRodizioInc";
        private const string SPUPDATE = "EmailRodizio.EmailRodizioAlt";
        private const string SPDELETE = "EmailRodizio.EmailRodizioDel";
        private const string SPSELECTID = "EmailRodizio.EmailRodizioSelId";
        private const string SPSELECTPAG = "EmailRodizio.EmailRodizioSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodTituloEv = "codTituloEv";
        private const string PARMcodUsuario = "codUsuario";
        private const string PARMCURSOR = "curEmailRodizio";
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
                    /*0*/ new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodUsuario, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "indInc", OracleType.Int32),
                    /*3*/ new OracleParameter( "indAlt", OracleType.Int32),
                    /*4*/ new OracleParameter( "indExc", OracleType.Int32),
                    /*5*/ new OracleParameter( "indPen", OracleType.Int32),
                    /*6*/ new OracleParameter( "indEnvia", OracleType.Int32),
                    /*7*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codTituloEv.CodTituloEv;
            parms[1].Value = this.codUsuario.CodPessoa;
            parms[2].Value = this.indInc;
            parms[3].Value = this.indAlt;
            parms[4].Value = this.indExc;
            parms[5].Value = this.indPen;
            parms[6].Value = this.indEnvia;
            parms[7].Value = this.logUsuario;
            if (this.codTituloEv.CodTituloEv < 0)
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
                        codTituloEv.CodTituloEv = Convert.ToInt32(cmd.Parameters[PARMcodTituloEv].Value);
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
                codTituloEv.CodTituloEv = Convert.ToInt32(cmd.Parameters[PARMcodTituloEv].Value);
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
        /// <param name="codTituloEv">Código do Registro</param>
        public static void Delete(int codTituloEv, int codUsuario)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4)
            };
            parms[0].Value = codTituloEv;
            parms[1].Value = codUsuario;
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
        /// <param name="codTituloEv">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codTituloEv, int codUsuario, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4)
            };
                parms[0].Value = codTituloEv;
                parms[1].Value = codUsuario;
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
        /// <param name="codTituloEv">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codTituloEv, int codUsuario)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codTituloEv;
            param[1].Value = codUsuario;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codTituloEv">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codTituloEv, int codUsuario, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codTituloEv;
            param[1].Value = codUsuario;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codTituloEv">Código do Registro</param>
        /// <returns>EmailRodizio</returns>
        public static EmailRodizio GetDataRow(int codTituloEv, int codUsuario)
        {
            OracleDataReader dr = LoadDataDr(codTituloEv, codUsuario);
            EmailRodizio EmailRodizio = new EmailRodizio();
            try
            {
                if (dr.Read())
                {
                    EmailRodizio.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    if (dr["A572_cd_pes"] != DBNull.Value && dr["A572_cd_pes"] != DBNull.Value && dr["A572_cd_pes"].ToString() != "")
                        EmailRodizio.codUsuario = new Pessoa(Convert.ToInt32(dr["A572_cd_pes"]));
                    EmailRodizio.indInc = Convert.ToInt32(dr["A1145_ind_inc"]);
                    EmailRodizio.indAlt = Convert.ToInt32(dr["A1145_ind_alt"]);
                    EmailRodizio.indExc = Convert.ToInt32(dr["A1145_ind_exc"]);
                    EmailRodizio.indPen = Convert.ToInt32(dr["A1145_ind_pen"]);
                    EmailRodizio.indEnvia = Convert.ToInt32(dr["A1145_ind_envia"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        EmailRodizio.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EmailRodizio = new EmailRodizio();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EmailRodizio;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codTituloEv">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>EmailRodizio</returns>
        public static EmailRodizio GetDataRow(int codTituloEv, int codUsuario, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codTituloEv, codUsuario, trans);
            EmailRodizio EmailRodizio = new EmailRodizio();
            try
            {
                if (dr.Read())
                {
                    EmailRodizio.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    if (dr["A572_cd_pes"] != DBNull.Value && dr["A572_cd_pes"] != DBNull.Value && dr["A572_cd_pes"].ToString() != "")
                        EmailRodizio.codUsuario = new Pessoa(Convert.ToInt32(dr["A572_cd_pes"]));
                    EmailRodizio.indInc = Convert.ToInt32(dr["A1145_ind_inc"]);
                    EmailRodizio.indAlt = Convert.ToInt32(dr["A1145_ind_alt"]);
                    EmailRodizio.indExc = Convert.ToInt32(dr["A1145_ind_exc"]);
                    EmailRodizio.indPen = Convert.ToInt32(dr["A1145_ind_pen"]);
                    EmailRodizio.indEnvia = Convert.ToInt32(dr["A1145_ind_envia"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        EmailRodizio.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EmailRodizio = new EmailRodizio();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EmailRodizio;
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