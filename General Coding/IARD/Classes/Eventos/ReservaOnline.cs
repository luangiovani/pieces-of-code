using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

namespace Classes
{
    public class ReservaOnline/// t943_reserva_online
    {
        #region Atributos
        private int codReservaOnline;
        private Eventos codEvento;
        private Cliente codCliente;
        private int? codClienteJur;
        private ContatoCliente numContatoCli;
        private int indImportado;
        private DateTime dtaIncAlt;
        private string  usuIncAlt;
     
        #endregion
        
        #region Propriedades
         public int CodReservaOnline
        {
            get { return codReservaOnline; }
            set { codReservaOnline = value; }
        }
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public int? CodClienteJur
        {
            get { return codClienteJur; }
            set { codClienteJur = value; }
        }
        public ContatoCliente NumContatoCli
        {
            get { return numContatoCli; }
            set { numContatoCli = value; }
        }
        public int DndImportado
        {
            get { return indImportado; }
            set { indImportado = value; }
        }
        public DateTime DtaIncAlt
        {
            get { return dtaIncAlt; }
            set { dtaIncAlt = value; }
        }
        public string UsuIncAlt
        {
            get { return usuIncAlt; }
            set { usuIncAlt = value; }
        }
        #endregion

        #region Construtores
        public ReservaOnline()
            : this(-1)
        { }
        public ReservaOnline(int codReservaOnline)
        {
            this.codReservaOnline = codReservaOnline;
            this.codEvento = new Eventos();
            this.codCliente = new Cliente();
            this.codClienteJur = new int();
            this.numContatoCli = new ContatoCliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ReservaOnline.ReservaOnlineInc";
        private const string SPUPDATE = "ReservaOnline.ReservaOnlineAlt";
        private const string SPSELECTID = "ReservaOnline.ReservaOnlineSelId";
        private const string SPSELECTPAG = "ReservaOnline.ReservaOnlineSelPag";
        private const string SPDELETE = "ReservaOnline.ReservaOnlineDel";
        private const string SPSELECTCONT = "ReservaOnline.ReservaOnlineSelCont";
        private const string SPSELECTMAX = "ReservaOnline.ReservaOnlineMax";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codReservaOnline";
        private const string PARMCURSOR = "curReservaOnline";
        #endregion

        #region Metodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "codEvento", OracleType.Int32),
                    /*2*/ new OracleParameter( "codCliente", OracleType.Int32),
                    /*2*/ new OracleParameter( "codClienteJur", OracleType.Int32),
                    /*3*/ new OracleParameter( "numContatoCli", OracleType.Int32),
                    /*4*/ new OracleParameter( "indImportado", OracleType.Int32),
                    /*5*/ new OracleParameter( "usuIncAlt", OracleType.VarChar)
                };

                // Criando cache dos parameters 
                DataBase.CacheParameters(SPINSERT, parms);
            //}
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codReservaOnline;
            parms[1].Value = this.CodEvento.CodEvento;
            parms[2].Value = this.CodCliente.CodCLIENTE ;
            parms[3].Value = this.CodClienteJur;
            parms[4].Value = this.numContatoCli.NumContato;
            parms[5].Value = this.indImportado;
            parms[6].Value = this.usuIncAlt;


            if (this.codReservaOnline < 0)
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
                        codReservaOnline = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codReservaOnline = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
            parms[0].Value = codigo;
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
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
                parms[0].Value = codigo;
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
        public static OracleDataReader LoadDataDr(int codigo, int eve)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter("codEvento", OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Value = eve;
            param[2].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codigo, int eve, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter("codEvento", OracleType.Int32, 4),
                                                                  new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Eventos</returns>
        public static ReservaOnline GetDataRow(int codigo, int eve)
        {
            OracleDataReader dr = LoadDataDr(codigo, eve);
            ReservaOnline reOn = new ReservaOnline();
            try
            {
                if (dr.Read())
                {
                    reOn.codReservaOnline = Convert.ToInt32(dr["A943_COD_RES"]);
                    reOn.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    reOn.codCliente = new Cliente(Convert.ToInt32(dr["A012_CD_CLI"]));
                    if (dr["A012_CD_CLI_jur"].ToString() != "")
                        reOn.codClienteJur = Convert.ToInt32(dr["A012_CD_CLI_jur"]);

                    reOn.numContatoCli = new ContatoCliente(Convert.ToInt32(dr["A014_NUM_CONT"]));
                    if (dr["A943_IND_IMPORTADO"].ToString() != "")
                        reOn.indImportado = Convert.ToInt32(dr["A943_IND_IMPORTADO"]);
                    reOn.usuIncAlt = Convert.ToString(dr["USU_INC_ALT"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                reOn = new ReservaOnline();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return reOn;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Eventos</returns>
        public static ReservaOnline GetDataRow(int codigo, int eve, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, eve, trans);
            ReservaOnline reOn = new ReservaOnline();
            try
            {
                if (dr.Read())
                {
                    reOn.codReservaOnline = Convert.ToInt32(dr["codReservaOnline"]);
                    reOn.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    reOn.codCliente = new Cliente(Convert.ToInt32(dr["A012_CD_CLI"]));
                    if (dr["A012_CD_CLI_jur"].ToString() != "")
                        reOn.codClienteJur = Convert.ToInt32(dr["A012_CD_CLI_jur"]);

                    reOn.numContatoCli = new ContatoCliente(Convert.ToInt32(dr["A014_NUM_CONT"]));
                    if (dr["A143_IND_IMPORTADO"].ToString() != "")
                        reOn.indImportado = Convert.ToInt32(dr["A143_IND_IMPORTADO"]);
                    reOn.usuIncAlt = Convert.ToString(dr["USU_INC_ALT"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                reOn = new ReservaOnline();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return reOn;
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

        #region LoadDataPaginacaoCont


        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cláusula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">Número da página que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada página</param>
        /// <param name="ExpressaoOrdenacao">Expressão de ordenação</param>
        /// <returns>Instância do objeto Paginação, contendo um DataReader e o total de registros</returns>
        /// 
        public static Paginacao LoadDataPaginacaoCont(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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


            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTCONT, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #region LoadDataDrMax

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrMax(int codigoEve)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codEvento", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigoEve;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTMAX, param);
            return dr;
        }
        #endregion


        #endregion
    }
}