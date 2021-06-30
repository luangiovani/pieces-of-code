using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T924_res_complemento
    /// </summary>
    /// <autor>
    /// Luan Giovani Cassini Fernandes
    /// </autor>
    /// <data>
    /// 07/05/2018
    /// </data>
    /// <atividade>
    /// https://esfera.teamworkpm.net/#tasks/17108514
    /// </atividade>
    public class EventoCredenResposta
    {
        #region Atributos
        private int codEvento;
        private int numSequencia;
        private int numContato;
        private int codCliente;
        private string dscResposta;
        #endregion

        #region Propriedades
        public int CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public int NumSequencia
        {
            get { return numSequencia; }
            set { numSequencia = value; }
        }
        public int NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }

        public int CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public string DscResposta
        {
            get { return dscResposta; }
            set { dscResposta = value; }
        }

        #endregion

        #region Construtores
        public EventoCredenResposta()
            : this(-1)
        { }
        public EventoCredenResposta(int codEvento)
        {
            this.codEvento = codEvento;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Ev_Res_Comp.Ev_Res_CompInc";
        private const string SPUPDATE = "Ev_Res_Comp.Ev_Res_CompAlt";
        private const string SPDELETE = "Ev_Res_Comp.Ev_Res_CompDel";
        private const string SPSELECTID = "Ev_Res_Comp.Ev_Res_CompSelId";
        private const string SPSELECTPAG = "Ev_Res_Comp.Ev_Res_CompSelPag";
        private const string SPSELECTRESPOSTA = "Ev_Res_Comp.Ev_Res_CompSelId";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGOEVENTO = "codEvento";
        private const string PARMCODIGONUMSEQ = "numSequencia";
        private const string PARMCODIGONUMCONTATO = "numContato";
        private const string PARMCODIGOCODCLIENT = "codCliente";   
        private const string PARMCURSOR = "curEv_Res_Comp";
        #endregion

        #region Metodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = Context.DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                /*0*/ new OracleParameter(PARMCODIGOEVENTO, OracleType.Int32, 2, ParameterDirection.InputOutput.ToString()) ,
                /*1*/ new OracleParameter( "numSequencia", OracleType.Int32),
                /*2*/ new OracleParameter( "numContato", OracleType.Int32),
                /*3*/ new OracleParameter( "codCliente", OracleType.Int32),
                /*4*/ new OracleParameter( "dscResposta", OracleType.VarChar)
            };

                // Criando cache dos parameters 
                Context.DataBase.CacheParameters(SPINSERT, parms);
            }
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codEvento;
            parms[1].Value = this.numSequencia;
            parms[2].Value = this.numContato;
            parms[3].Value = this.codCliente;
            parms[4].Value = this.dscResposta.ToUpper();
            if (this.codEvento < 0)
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

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGOEVENTO].Value);
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
                OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identificação do registro inserido.
                codEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGOEVENTO].Value);
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
            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
        public static void Delete(int codEvento, int numSequencia, int numContato, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                    new OracleParameter(PARMCODIGOEVENTO, OracleType.Int32, 2) ,
                    new OracleParameter(PARMCODIGONUMSEQ, OracleType.Int32, 2) ,
                    new OracleParameter(PARMCODIGONUMCONTATO, OracleType.Int32, 2) ,
                    new OracleParameter(PARMCODIGOCODCLIENT, OracleType.Int32, 2) 
                };

            parms[0].Value = codEvento;
            parms[1].Value = numSequencia;
            parms[2].Value = numContato;
            parms[3].Value = codCliente;

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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
        public static void Delete(int codEvento, int numSequencia, int numContato, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                    new OracleParameter(PARMCODIGOEVENTO, OracleType.Int32, 2) ,
                    new OracleParameter(PARMCODIGONUMSEQ, OracleType.Int32, 2) ,
                    new OracleParameter(PARMCODIGONUMCONTATO, OracleType.Int32, 2) ,
                    new OracleParameter(PARMCODIGOCODCLIENT, OracleType.Int32, 2) 
                };

                parms[0].Value = codEvento;
                parms[1].Value = numSequencia;
                parms[2].Value = numContato;
                parms[3].Value = codCliente;

                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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
        public static OracleDataReader LoadDataDr(int codEvento, int numSequencia, int numContato, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGOEVENTO, OracleType.Int32, 8),
                new OracleParameter(PARMCODIGONUMSEQ, OracleType.Int32, 8),
                new OracleParameter(PARMCODIGONUMCONTATO, OracleType.Int32, 8),
                new OracleParameter(PARMCODIGOCODCLIENT, OracleType.Int32, 8), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codEvento;
            param[1].Value = numSequencia;
            param[2].Value = numContato;
            param[3].Value = codCliente;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codEvento, int numSequencia, int numContato, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {   
                new OracleParameter(PARMCODIGOEVENTO, OracleType.Int32, 8),
                new OracleParameter(PARMCODIGONUMSEQ, OracleType.Int32, 8),
                new OracleParameter(PARMCODIGONUMCONTATO, OracleType.Int32, 8),
                new OracleParameter(PARMCODIGOCODCLIENT, OracleType.Int32, 8), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codEvento;
            param[1].Value = numSequencia;
            param[2].Value = numContato;
            param[3].Value = codCliente;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>EventoCredenResposta</returns>
        public static EventoCredenResposta GetDataRow(int codEvento, int numSequencia, int numContato, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(codEvento, numSequencia, numContato, codCliente);
            EventoCredenResposta EventoCredenResposta = new EventoCredenResposta();
            try
            {
                if (dr.Read())
                {
                    EventoCredenResposta.codEvento = Convert.ToInt32(dr["A022_cd_ev"]);
                    if (dr["A923_num_sequencia"] != DBNull.Value)
                        EventoCredenResposta.numSequencia = Convert.ToInt32(dr["A923_num_sequencia"]);
                    if (dr["A014_num_cont"] != DBNull.Value)
                        EventoCredenResposta.numContato = Convert.ToInt32(dr["A014_num_cont"]);
                    if (dr["A012_cd_cli"] != DBNull.Value)
                        EventoCredenResposta.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A924_dsc_resposta"] != DBNull.Value && dr["A924_dsc_resposta"] != DBNull.Value && dr["A924_dsc_resposta"].ToString() != "")
                        EventoCredenResposta.dscResposta = Convert.ToString(dr["A924_dsc_resposta"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EventoCredenResposta = new EventoCredenResposta();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EventoCredenResposta;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>EventoCredenResposta</returns>
        public static EventoCredenResposta GetDataRow(int codEvento, int numSequencia, int numContato, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codEvento, numSequencia, numContato, codCliente);
            EventoCredenResposta EventoCredenResposta = new EventoCredenResposta();
            try
            {
                if (dr.Read())
                {
                    EventoCredenResposta.codEvento = Convert.ToInt32(dr["A022_cd_ev"]);
                    if (dr["A923_num_sequencia"] != DBNull.Value)
                        EventoCredenResposta.numSequencia = Convert.ToInt32(dr["A923_num_sequencia"]);
                    if (dr["A014_num_cont"] != DBNull.Value)
                        EventoCredenResposta.numContato = Convert.ToInt32(dr["A014_num_cont"]);
                    if (dr["A012_cd_cli"] != DBNull.Value)
                        EventoCredenResposta.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A924_dsc_resposta"] != DBNull.Value && dr["A924_dsc_resposta"] != DBNull.Value && dr["A924_dsc_resposta"].ToString() != "")
                        EventoCredenResposta.dscResposta = Convert.ToString(dr["A924_dsc_resposta"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EventoCredenResposta = new EventoCredenResposta();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EventoCredenResposta;
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
        public static Context.Paginacao LoadDataPaginacao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

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


            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG, parms);
 
            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion


        #region Metodos Especificos

        #region LoadDataResposta
        public static OracleDataReader LoadDataResposta(int codEvento, int numSequencia, int numContato, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] {  
                    new OracleParameter("codEvento", OracleType.Int32),
                    new OracleParameter("numSequencia", OracleType.Int32),
                    new OracleParameter("numContato", OracleType.Int32),
                    new OracleParameter("codCliente", OracleType.Int32),
                    new OracleParameter("curEv_Res_Comp", OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = numSequencia;
            param[2].Value = numContato;
            param[3].Value = codCliente;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTRESPOSTA, param);

            return dr;
        }

        #endregion

        #endregion

    }
}