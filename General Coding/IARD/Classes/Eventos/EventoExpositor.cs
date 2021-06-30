using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/07/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class EventoExpositor // T697_EVENTO_EXPOSITOR  
    {
        #region Atributos
        private Eventos codEvento;
        private Contato numContato;
        private Cliente codCliente;
        private int? indEnviado;
        private string logUsuario;
        #endregion

        #region Propriedades
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public Contato NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public int? IndEnviado
        {
            get { return indEnviado; }
            set { indEnviado = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public EventoExpositor()
            : this(-1)
        { }
        public EventoExpositor(int codEvento)
        {
            this.codEvento = new Eventos();
            this.numContato = new  Contato();
            this.codCliente = new Cliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "EventoExpositor.EventoExpositorInc";
        private const string SPUPDATE = "EventoExpositor.EventoExpositorAlt";
        private const string SPDELETE = "EventoExpositor.EventoExpositorDel";
        private const string SPSELECTID = "EventoExpositor.EventoExpositorSelId";
        private const string SPSELECTPAG = "EventoExpositor.EventoExpositorSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODEVENTO = "codEvento";
        private const string PARMNUMCONTATO = "numContato";
        private const string PARMCODCLIENTE = "codCliente";
        private const string PARMCURSOR = "curEventoExpositor";
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
                    /*0*/ new OracleParameter(PARMCODEVENTO, OracleType.Int32, 8, ParameterDirection.Input.ToString()) ,
                    /*1*/ new OracleParameter(PARMNUMCONTATO, OracleType.Int32, 8, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 8, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "indEnviado", OracleType.Int32),
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
            parms[0].Value = this.codEvento.CodEvento;
            parms[1].Value = this.numContato.NomContato;
            parms[2].Value = this.codCliente.CodCLIENTE;
            parms[3].Value = DBNull.Value;
            if (this.indEnviado!=null)
            { parms[3].Value = this.indEnviado; }            
            parms[4].Value = this.logUsuario;
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
                        codEvento = new Eventos(Convert.ToInt32(cmd.Parameters[PARMCODEVENTO].Value));
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
                codEvento = new Eventos(Convert.ToInt32(cmd.Parameters[PARMCODEVENTO].Value));
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
        public static void Delete(int codEvento, int numContato, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMNUMCONTATO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 4)
            };
            parms[0].Value = codEvento;
            parms[1].Value = numContato;
            parms[2].Value = codCliente;
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
        public static void Delete(int codEvento, int numContato, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMNUMCONTATO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 4) 
            };
                parms[0].Value = codEvento;
                parms[1].Value = numContato;
                parms[2].Value = codCliente;
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
        public static OracleDataReader LoadDataDr(int codEvento, int numContato, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4), 
                    new OracleParameter(PARMNUMCONTATO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = numContato;
            param[2].Value = codCliente;
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
        public static OracleDataReader LoadDataDr(int codEvento, int numContato, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4), 
                    new OracleParameter(PARMNUMCONTATO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = numContato;
            param[2].Value = codCliente;
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
        /// <returns>EventoExpositor</returns>
        public static EventoExpositor GetDataRow(int codEvento, int numContato, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(codEvento, numContato, codCliente);
            EventoExpositor EventoExpositor = new EventoExpositor();
            try
            {
                if (dr.Read())
                {
                    EventoExpositor.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    EventoExpositor.numContato = new Contato(Convert.ToInt32(dr["a014_num_cont"]));
                    EventoExpositor.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"]));
                    if (dr["a697_ind_enviado"] != DBNull.Value)
                        EventoExpositor.indEnviado = Convert.ToInt32(dr["a697_ind_enviado"]);
                    EventoExpositor.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EventoExpositor = new EventoExpositor();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EventoExpositor;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>EventoExpositor</returns>
        public static EventoExpositor GetDataRow(int codEvento, int numContato, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codEvento, numContato, codCliente, trans);
            EventoExpositor EventoExpositor = new EventoExpositor();
            try
            {
                if (dr.Read())
                {
                    EventoExpositor.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    EventoExpositor.numContato = new Contato(Convert.ToInt32(dr["a014_num_cont"]));
                    EventoExpositor.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"]));
                    if (dr["a697_ind_enviado"] != DBNull.Value)
                        EventoExpositor.indEnviado = Convert.ToInt32(dr["a697_ind_enviado"]);
                    EventoExpositor.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EventoExpositor = new EventoExpositor();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EventoExpositor;
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
