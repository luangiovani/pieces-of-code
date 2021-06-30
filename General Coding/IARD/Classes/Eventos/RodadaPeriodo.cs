using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Rodada Periodo
//-- Data : 24/10/2011
//-- Autor :  Denis Douglas Cavalheiro
namespace Classes
{
    public class RodadaPeriodo
    {
        #region Atributos
        private int seqRodadaPeriodo;
        private Rodada codRodada;
        private DateTime dtPeriodo;
        private string hrInicio;
        private string hrFinal;
        private int tpoPausa;
        private string horarioPausas;
        #endregion

        #region Propriedades
        public int SeqRodadaPeriodo
        {
            get { return seqRodadaPeriodo; }
            set { seqRodadaPeriodo = value; }
        }
        public Rodada CodRodada
        {
            get { return codRodada; }
            set { codRodada = value; }
        }
        public DateTime DtPeriodo
        {
            get { return dtPeriodo; }
            set { dtPeriodo = value; }
        }
        public string HrInicio
        {
            get { return hrInicio; }
            set { hrInicio = value; }
        }
        public string HrFinal
        {
            get { return hrFinal; }
            set { hrFinal = value; }
        }
        public int TpoPausa
        {
            get { return tpoPausa; }
            set { tpoPausa = value; }
        }
        public string HorarioPausas
        {
            get { return horarioPausas; }
            set { horarioPausas = value; }
        }
        #endregion

        #region Construtores
        public RodadaPeriodo()
            : this(-1)
        { }
        public RodadaPeriodo(int seqRodadaPeriodo)
        {
            this.codRodada = new Rodada();
            this.seqRodadaPeriodo = seqRodadaPeriodo;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Rodada_Periodo.Rodada_PeriodoInc";
        private const string SPUPDATE = "Rodada_Periodo.Rodada_PeriodoAlt";
        private const string SPDELETE = "Rodada_Periodo.Rodada_PeriodoDel";
        private const string SPSELECTID = "Rodada_Periodo.Rodada_PeriodoSelId";
        private const string SPSELECTPAG = "Rodada_Periodo.Rodada_PeriodoSelPag";
        private const string SPSELECTROD = "Rodada_Periodo.RODADA_PERIODOSelRod";
        #endregion

        #region Parametros Oracle
        private const string PARMcodRodada = "codRodada";
        private const string PARMseqRodadaPeriodo = "seqRodadaPeriodo";
        private const string PARMdtPeriodo = "dtPeriodo";
        private const string PARMhrInicio = "hrInicio";
        private const string PARMhrFinal = "hrFinal";
        private const string PARMtpoPausa = "tpoPausa";
        private const string PARMhorarioPausas = "horarioPausas";
        private const string PARMCURSOR = "curRODADA_PERIODO";
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
                    /*0*/ new OracleParameter(PARMseqRodadaPeriodo, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodRodada, OracleType.Int32) ,
                    /*2*/ new OracleParameter(PARMdtPeriodo, OracleType.DateTime) ,
                    /*3*/ new OracleParameter(PARMhrInicio, OracleType.VarChar) ,
                    /*4*/ new OracleParameter(PARMhrFinal, OracleType.VarChar) ,
                    /*5*/ new OracleParameter(PARMtpoPausa, OracleType.Int32) ,
                    /*6*/ new OracleParameter(PARMhorarioPausas, OracleType.VarChar) 
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
            parms[0].Value = this.seqRodadaPeriodo;
            parms[1].Value = this.codRodada.CodRodada;
            parms[2].Value = this.dtPeriodo;
            parms[3].Value = this.hrInicio;
            parms[4].Value = this.hrFinal;
            parms[5].Value = this.tpoPausa;
            parms[6].Value = this.horarioPausas;

            if (this.seqRodadaPeriodo < 0)
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
                        seqRodadaPeriodo = Convert.ToInt32(cmd.Parameters[PARMseqRodadaPeriodo].Value);
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
                seqRodadaPeriodo = Convert.ToInt32(cmd.Parameters[PARMseqRodadaPeriodo].Value);
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
        /// <param name="codEvento">Código do Registro</param>
        public static void Delete(int codRodada, int seqRodadaPeriodo)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4),
                new OracleParameter(PARMseqRodadaPeriodo, OracleType.Int32, 4)
            };
            parms[0].Value = codRodada;
            parms[1].Value = seqRodadaPeriodo;
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
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codRodada, int seqRodadaPeriodo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4)  ,
                new OracleParameter(PARMseqRodadaPeriodo, OracleType.Int32, 4)
            };
                parms[0].Value = codRodada;
                parms[1].Value = 0;
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
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codRodada, int seqRodadaPeriodo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMseqRodadaPeriodo, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Value = seqRodadaPeriodo;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codRodada, int seqRodadaPeriodo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMseqRodadaPeriodo, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };
            param[0].Value = codRodada;
            param[1].Value = seqRodadaPeriodo;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>Sessao</returns>
        public static RodadaPeriodo GetDataRow(int codRodada, int seqRodadaPeriodo)
        {
            OracleDataReader dr = LoadDataDr(codRodada, seqRodadaPeriodo);
            RodadaPeriodo rodadaPeriodo = new RodadaPeriodo();
            try
            {
                if (dr.Read())
                {
                    rodadaPeriodo.codRodada = new Rodada(Convert.ToInt32(dr["A930_cd_rodada"]));
                    if (dr["A935_seq_periodo"] != DBNull.Value && dr["A935_seq_periodo"] != DBNull.Value && dr["A935_seq_periodo"].ToString() != "")
                        rodadaPeriodo.seqRodadaPeriodo = Convert.ToInt32(dr["A935_seq_periodo"]);
                    rodadaPeriodo.dtPeriodo = Convert.ToDateTime(dr["A935_dta_periodo"]);
                    rodadaPeriodo.hrInicio = Convert.ToString(dr["A935_hr_inicial"]);
                    rodadaPeriodo.hrFinal = Convert.ToString(dr["A935_hr_final"]);
                    rodadaPeriodo.tpoPausa = Convert.ToInt32(dr["A935_tpo_pausa"]);
                    rodadaPeriodo.horarioPausas = Convert.ToString(dr["A935_horario_pausas"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodadaPeriodo = new RodadaPeriodo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodadaPeriodo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Sessao</returns>
        public static RodadaPeriodo GetDataRow(int codRodada, int seqRodadaPeriodo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codRodada, seqRodadaPeriodo, trans);
            RodadaPeriodo rodadaPeriodo = new RodadaPeriodo();
            try
            {
                if (dr.Read())
                {
                    rodadaPeriodo.codRodada = new Rodada(Convert.ToInt32(dr["A930_cd_rodada"]));
                    if (dr["A935_seq_periodo"] != DBNull.Value && dr["A935_seq_periodo"] != DBNull.Value && dr["A935_seq_periodo"].ToString() != "")
                        rodadaPeriodo.seqRodadaPeriodo = Convert.ToInt32(dr["A935_seq_periodo"]);
                    rodadaPeriodo.dtPeriodo = Convert.ToDateTime(dr["A935_dta_periodo"]);
                    rodadaPeriodo.hrInicio = Convert.ToString(dr["A935_hr_inicial"]);
                    rodadaPeriodo.hrFinal = Convert.ToString(dr["A935_hr_final"]);
                    rodadaPeriodo.tpoPausa = Convert.ToInt32(dr["A935_tpo_pausa"]);
                    rodadaPeriodo.horarioPausas = Convert.ToString(dr["A935_horario_pausas"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodadaPeriodo = new RodadaPeriodo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodadaPeriodo;
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

        #region LoadDataDr

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrRodada(int codRodada)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTROD, param);
            return dr;
        }
        #endregion

        #endregion
    }
}
