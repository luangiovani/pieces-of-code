using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 10/11/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class OrdemServico // T798_ORDEM_SERV    
    {
        #region Atributos
        private Eventos codEvento;
        private int codOrdem;
        private int numAnoOrdem;
        private Instrutor codInstrutor;
        private DateTime dtaCancelamento;
        private int indCancelado;
        private string logUsuario;
        #endregion

        #region Propriedades

        public int NumAnoOrdem
        {
            get { return numAnoOrdem; }
            set { numAnoOrdem = value; }
        }
        public Instrutor CodInstrutor
        {
            get { return codInstrutor; }
            set { codInstrutor = value; }
        }
        public DateTime DtaCancelamento
        {
            get { return dtaCancelamento; }
            set { dtaCancelamento = value; }
        }
        public int IndCancelado
        {
            get { return indCancelado; }
            set { indCancelado = value; }
        }
        public int CodOrdem
        {
            get { return codOrdem; }
            set { codOrdem = value; }
        }
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public string LogUsuario 
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public OrdemServico()
            : this(-1)
        { }
        public OrdemServico(int codOrdem)
        {
            //this.codEvento = codEvento; 
            this.codOrdem = codOrdem; 
            //this.numAnoOrdem = new EscritorioSebrae();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "OrdemServico.OrdemServicoInc";
        private const string SPUPDATE = "OrdemServico.OrdemServicoAlt";
        private const string SPDELETE = "OrdemServico.OrdemServicoDel";
        private const string SPSELECTID = "OrdemServico.OrdemServicoSelId";
        private const string SPSELECTPAG = "OrdemServico.OrdemServicoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodEvento = "codEvento";
        private const string PARMcodOrdem = "codOrdem";
        private const string PARMnumAnoOrdem = "numAnoOrdem";
        private const string PARMCURSOR = "curOrdemServico";
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
                    /*0*/ new OracleParameter(PARMcodEvento, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodOrdem, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMnumAnoOrdem, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "codInstrutor", OracleType.Int32),
                    /*4*/ new OracleParameter( "dtaCancelamento", OracleType.DateTime),
                    /*6*/ new OracleParameter( "indCancelado", OracleType.Int32),
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
            parms[0].Value = this.codEvento;
            parms[1].Value = this.codOrdem;
            parms[2].Value = this.numAnoOrdem;
            parms[3].Value = this.codInstrutor;
            parms[4].Value = this.dtaCancelamento;
            parms[5].Value = this.indCancelado;
		
            parms[6].Value = this.logUsuario;
            if (this.codEvento.CodEvento < 0)
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
                        codOrdem = Convert.ToInt32(cmd.Parameters[PARMcodOrdem].Value);
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
                codOrdem = Convert.ToInt32(cmd.Parameters[PARMcodOrdem].Value);
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
        public static void Delete(int codEvento, int codOrdem, int numAnoOrdem)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodOrdem, OracleType.Int32, 4)  ,
                new OracleParameter(PARMnumAnoOrdem, OracleType.Int32, 4)
            };
            parms[0].Value = codEvento;
            parms[1].Value = codOrdem;
            parms[2].Value = numAnoOrdem;
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
        public static void Delete(int codEvento, int codOrdem, int numAnoOrdem, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodOrdem, OracleType.Int32, 4)  ,
                new OracleParameter(PARMnumAnoOrdem, OracleType.Int32, 4) 
            };
                parms[0].Value = codEvento;
                parms[1].Value = codOrdem;
                parms[2].Value = numAnoOrdem;
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
        public static OracleDataReader LoadDataDr(int codEvento, int codOrdem, int numAnoOrdem)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodOrdem, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumAnoOrdem, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = codOrdem;
            param[2].Value = numAnoOrdem;
            param[3].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codEvento, int codOrdem, int numAnoOrdem, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodOrdem, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumAnoOrdem, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = codOrdem;
            param[2].Value = numAnoOrdem;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>OrdemServico</returns>
        public static OrdemServico GetDataRow(int codEvento, int codOrdem, int numAnoOrdem)
        {
            OracleDataReader dr = LoadDataDr(codEvento, codOrdem, numAnoOrdem);
            OrdemServico OrdemServico = new OrdemServico();
            try
            {
                if (dr.Read())
                {
                    OrdemServico.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    OrdemServico.codOrdem = Convert.ToInt32(dr["A798_cd_ordem"]);
                    OrdemServico.numAnoOrdem = Convert.ToInt32(dr["A798_ano_ordem"]);
                    OrdemServico.codInstrutor.CodInstrutor.CodPessoa = Convert.ToInt32(dr["A125_cd_instr"]);
                    if (dr["A798_dt_cancel"] != DBNull.Value && dr["A798_dt_cancel"].ToString() != "")
                        OrdemServico.dtaCancelamento = Convert.ToDateTime(dr["A798_dt_cancel"]);
                    OrdemServico.indCancelado = Convert.ToInt32(dr["A798_ind_cancel"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        OrdemServico.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                OrdemServico = new OrdemServico();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return OrdemServico;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>OrdemServico</returns>
        public static OrdemServico GetDataRow(int codEvento, int codOrdem, int numAnoOrdem, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codEvento, codOrdem, numAnoOrdem, trans);
            OrdemServico OrdemServico = new OrdemServico();
            try
            {
                if (dr.Read())
                {
                    OrdemServico.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    OrdemServico.codOrdem = Convert.ToInt32(dr["A798_cd_ordem"]);
                    OrdemServico.numAnoOrdem = Convert.ToInt32(dr["A798_ano_ordem"]);
                    OrdemServico.codInstrutor.CodInstrutor.CodPessoa = Convert.ToInt32(dr["A125_cd_instr"]);
                    if (dr["A798_dt_cancel"] != DBNull.Value && dr["A798_dt_cancel"].ToString() != "")
                        OrdemServico.dtaCancelamento = Convert.ToDateTime(dr["A798_dt_cancel"]);
                    OrdemServico.indCancelado = Convert.ToInt32(dr["A798_ind_cancel"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        OrdemServico.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                OrdemServico = new OrdemServico();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return OrdemServico;
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