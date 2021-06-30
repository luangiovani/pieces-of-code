using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 03/09/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class ConsTitEvento // T687_consulta_tit_evento 
    {
        #region Atributos
        private TabCons numAtend;
        private TabCons codTipoConsAt;
        private int codUsuario;
        private int codEscrSebrae;
        private TituloEvento codTituloEv;
        private TabCons numSeq;
        private TabCons codTabCons;
        private string logUsuario;
        #endregion

        #region Propriedades
        public TabCons NumAtend
        {
            get { return numAtend; }
            set { numAtend = value; }
        }
        public TabCons CodTipoConsAt
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
        public TituloEvento CodTituloEv
        {
            get { return codTituloEv; }
            set { codTituloEv = value; }
        }
        public TabCons NumSeq
        {
            get { return numSeq; }
            set { numSeq = value; }
        }
        public TabCons CodTabCons
        {
            get { return codTabCons; }
            set { codTabCons = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ConsTitEvento()
            : this(-1)
        { }
        public ConsTitEvento(int numAtend)
        {
            this.numAtend = new TabCons();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ConsTitEvento.ConsTitEventoInc";
        private const string SPUPDATE = "ConsTitEvento.ConsTitEventoAlt";
        private const string SPDELETE = "ConsTitEvento.ConsTitEventoDel";
        private const string SPSELECTID = "ConsTitEvento.ConsTitEventoSelId";
        private const string SPSELECTPAG = "ConsTitEvento.ConsTitEventoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMnumAtend = "numAtend";
        private const string PARMcodTipoConsAt = "codTipoConsAt";
        private const string PARMcodUsuario = "codUsuario";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMcodTituloEv = "codTituloEv";
        private const string PARMnumSeq = "numSeq";
        private const string PARMcodTabCons = "codTabCons";
        private const string PARMCURSOR = "curConsTitEvento";
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
			        /*4*/ new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
			        /*5*/ new OracleParameter(PARMnumSeq, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
			        /*6*/ new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4, ParameterDirection.Input.ToString()) ,			
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
            parms[0].Value = this.numAtend;
            parms[1].Value = this.codTipoConsAt;
            parms[2].Value = this.codUsuario;
            parms[3].Value = this.codEscrSebrae;
            parms[4].Value = this.codTituloEv;
            parms[5].Value = this.numSeq;
            parms[6].Value = this.codTabCons;

            parms[7].Value = this.logUsuario;
            if (this.numAtend.NumAtend.NumAtend.NumAtend < 0)
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
        public static void Delete(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, int codTituloEv, int numSeq, string codTabCons)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4),
                new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4),
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
                new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4),
                new OracleParameter(PARMnumSeq, OracleType.Int32, 4),
                new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4)			 
            };
            parms[0].Value = numAtend;
            parms[1].Value = codTipoConsAt;
            parms[2].Value = codUsuario;
            parms[3].Value = codEscrSebrae;
            parms[4].Value = codTituloEv;
            parms[5].Value = numSeq;
            parms[6].Value = codTabCons;
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
        public static void Delete(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, int codTituloEv, int numSeq, string codTabCons, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4),
                new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4),
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
		        new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4),
		        new OracleParameter(PARMnumSeq, OracleType.Int32, 4),
		        new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4)		
            };
                parms[0].Value = numAtend;
                parms[1].Value = codTipoConsAt;
                parms[2].Value = codUsuario;
                parms[3].Value = codEscrSebrae;
                parms[4].Value = codTituloEv;
                parms[5].Value = numSeq;
                parms[6].Value = codTabCons;

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
        public static OracleDataReader LoadDataDr(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, int codTituloEv, int numSeq, string codTabCons)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
				    new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4), 
				    new OracleParameter(PARMnumSeq, OracleType.Int32, 4), 
				    new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4), 				
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codTipoConsAt;
            param[2].Value = codUsuario;
            param[3].Value = codEscrSebrae;
            param[4].Value = codTituloEv;
            param[5].Value = numSeq;
            param[6].Value = codTabCons;
            param[7].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, int codTituloEv, int numSeq, string codTabCons, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTipoConsAt, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
			        new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4), 
			        new OracleParameter(PARMnumSeq, OracleType.Int32, 4), 
			        new OracleParameter(PARMcodTabCons, OracleType.VarChar, 4), 			
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codTipoConsAt;
            param[2].Value = codUsuario;
            param[3].Value = codEscrSebrae;
            param[4].Value = codTituloEv;
            param[5].Value = numSeq;
            param[6].Value = codTabCons;
            param[7].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>ConsTitEvento</returns>
        public static ConsTitEvento GetDataRow(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, int codTituloEv, int numSeq, string codTabCons)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codTipoConsAt, codUsuario, codEscrSebrae, codTituloEv, numSeq, codTabCons);
            ConsTitEvento ConsTitEvento = new ConsTitEvento();
            try
            {
                if (dr.Read())
                {
                    ConsTitEvento.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    ConsTitEvento.numAtend = new TabCons(Convert.ToInt32(dr["A001_num_atend"]));
                    ConsTitEvento.codTipoConsAt.CodTipoConsAt.CodTipoConsAt.CodTipoConsAt = Convert.ToInt32(dr["A005_cd_tp_cons"]);
                    ConsTitEvento.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    ConsTitEvento.codEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    ConsTitEvento.numSeq.NumSeq = Convert.ToInt32(dr["A110_num_seq"]);
                    ConsTitEvento.codTabCons.CodTabCons.CodTabCons = Convert.ToString(dr["A118_tab_cons"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ConsTitEvento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ConsTitEvento = new ConsTitEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ConsTitEvento;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ConsTitEvento</returns>
        public static ConsTitEvento GetDataRow(int numAtend, int codTipoConsAt, int codUsuario, int codEscrSebrae, int codTituloEv, int numSeq, string codTabCons, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codTipoConsAt, codUsuario, codEscrSebrae, codTituloEv, numSeq, codTabCons, trans);
            ConsTitEvento ConsTitEvento = new ConsTitEvento();
            try
            {
                if (dr.Read())
                {
                    ConsTitEvento.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    ConsTitEvento.numAtend = new TabCons(Convert.ToInt32(dr["A001_num_atend"]));
                    ConsTitEvento.codTipoConsAt.CodTipoConsAt.CodTipoConsAt.CodTipoConsAt = Convert.ToInt32(dr["A005_cd_tp_cons"]);
                    ConsTitEvento.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    ConsTitEvento.codEscrSebrae = Convert.ToInt32(dr["A004_cd_escr"]);
                    ConsTitEvento.numSeq.NumSeq = Convert.ToInt32(dr["A110_num_seq"]);
                    ConsTitEvento.codTabCons.CodTabCons.CodTabCons = Convert.ToString(dr["A118_tab_cons"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ConsTitEvento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ConsTitEvento = new ConsTitEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ConsTitEvento;
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