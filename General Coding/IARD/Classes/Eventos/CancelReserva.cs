using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 10/07/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class CancelReserva // T1134_CancelReserva  
    {
        #region Atributos
        private Cliente codCliente;
        private Eventos codEvento;
        private int numContato;
        private Usuario codUsuario;
        private DateTime? dataCancel;
        private string nomeMotivo;
        private string logUsuario;
        #endregion

        #region Propriedades
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public int NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }
        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public DateTime? DataCancel
        {
            get { return dataCancel; }
            set { dataCancel = value; }
        }
        public string NomeMotivo
        {
            get { return nomeMotivo; }
            set { nomeMotivo = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public CancelReserva()
            : this(-1)
        { }
        public CancelReserva(int codCliente)
        {
            this.codCliente = new Cliente();
            this.codEvento = new Eventos();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "CancelReserva.CancelReservaInc";
        private const string SPUPDATE = "CancelReserva.CancelReservaAlt";
        private const string SPDELETE = "CancelReserva.CancelReservaDel";
        private const string SPSELECTID = "CancelReserva.CancelReservaSelId";
        private const string SPSELECTPAG = "CancelReserva.CancelReservaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODCLIENTE = "codCliente";
        private const string PARMCODEVENTO = "codEvento";
        private const string PARMCURSOR = "curCancelReserva";
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
                    /*0*/ new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODEVENTO, OracleType.Int32, 8, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "numContato", OracleType.Int32),
                    /*4*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*3*/ new OracleParameter( "dataCancel", OracleType.DateTime),
                    /*5*/ new OracleParameter( "nomeMotivo", OracleType.VarChar),
                    /*8*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codCliente.CodCLIENTE;
            parms[1].Value = this.codEvento.CodEvento;
            parms[2].Value = this.numContato;
            parms[3].Value = this.codUsuario.CodUsuario;
            parms[4].Value = DBNull.Value;
            if (this.dataCancel!=null)
            { parms[4].Value = this.dataCancel; }
            parms[5].Value = this.nomeMotivo;
            parms[6].Value = this.logUsuario;
            if (this.codCliente.CodCLIENTE < 0)
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
                        codCliente = new Cliente(Convert.ToInt32(cmd.Parameters[PARMCODCLIENTE].Value));
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
                codCliente = new Cliente(Convert.ToInt32(cmd.Parameters[PARMCODCLIENTE].Value));
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
        /// <param name="codCliente">Código do Registro</param>
        public static void Delete(int codCliente, int codEvento)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4)
            };
            parms[0].Value = codCliente;
            parms[1].Value = codEvento;
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
        /// <param name="codCliente">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codCliente, int codEvento, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4)
            };
                parms[0].Value = codCliente;
                parms[1].Value = codEvento;
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
        /// <param name="codCliente">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codCliente, int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codCliente;
            param[1].Value = codEvento;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codCliente">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codCliente, int codEvento, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codCliente;
            param[1].Value = codEvento;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codCliente">Código do Registro</param>
        /// <returns>CancelReserva</returns>
        public static CancelReserva GetDataRow(int codCliente, int codEvento)
        {
            OracleDataReader dr = LoadDataDr(codCliente, codEvento);
            CancelReserva pe = new CancelReserva();
            try
            {
                if (dr.Read())
                {
                    pe.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"]));
                    pe.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    pe.numContato = Convert.ToInt32(dr["a014_num_cont"]);
                    pe.codUsuario = new Usuario(Convert.ToInt32(dr["a052_cd_usuario"]));
                    if (dr["a1134_dta_canc"]!=DBNull.Value)
                    { pe.dataCancel = Convert.ToDateTime(dr["a1134_dta_canc"]); }                    
                    pe.nomeMotivo = Convert.ToString(dr["a1134_motivo"]);
                    pe.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pe = new CancelReserva();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return pe;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codCliente">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>CancelReserva</returns>
        public static CancelReserva GetDataRow(int codCliente, int codEvento, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codCliente, codEvento, trans);
            CancelReserva pe = new CancelReserva();
            try
            {
                if (dr.Read())
                {
                    pe.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"]));
                    pe.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    pe.numContato = Convert.ToInt32(dr["a014_num_cont"]);
                    pe.codUsuario = new Usuario(Convert.ToInt32(dr["a052_cd_usuario"]));
                    if (dr["a1134_dta_canc"] != DBNull.Value)
                    { pe.dataCancel = Convert.ToDateTime(dr["a1134_dta_canc"]); }
                    pe.nomeMotivo = Convert.ToString(dr["a1134_motivo"]);
                    pe.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pe = new CancelReserva();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return pe;
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
