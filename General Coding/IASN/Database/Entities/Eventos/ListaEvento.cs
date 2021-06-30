using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T290_LISTA_EVENTO
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
    public class ListaEvento
    {
        #region Atributos
        private int codItem;
        private Cliente codCliente;
        private Eventos codEvento;
        private GrupoReserva codGrupoReserva;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodItem
        {
            get { return codItem; }
            set { codItem = value; }
        }
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
        public GrupoReserva CodGrupoReserva
        {
            get { return codGrupoReserva; }
            set { codGrupoReserva = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ListaEvento()
            : this(-1)
        { }
        public ListaEvento(int codItem)
        {
            this.codItem = codItem;
            this.codCliente = new Cliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ListaEvento.ListaEventoInc";
        private const string SPUPDATE = "ListaEvento.ListaEventoAlt";
        private const string SPDELETE = "ListaEvento.ListaEventoDel";
        private const string SPSELECTID = "ListaEvento.ListaEventoSelId";
        private const string SPSELECTPAG = "ListaEvento.ListaEventoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodItem = "codItem";
        private const string PARMcodCliente = "codCliente";
        private const string PARMCURSOR = "curListaEvento";
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
                    /*0*/ new OracleParameter(PARMcodItem, OracleType.Int32) ,
                    /*1*/ new OracleParameter(PARMcodCliente, OracleType.Int32) ,
                    /*2*/ new OracleParameter( "codEvento", OracleType.Int32),
                    /*3*/ new OracleParameter( "codGrupoReserva", OracleType.Int32),
                    /*4*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codItem;
            parms[1].Value = this.CodCliente.CodCLIENTE;
            parms[2].Value = this.CodEvento.CodEvento;
            parms[3].Value = this.CodGrupoReserva.CodGrupoReserva;
            parms[4].Value = this.logUsuario;
            //if (this.codItem < 0)
            //{
            //    parms[0].Direction = ParameterDirection.Output;
            //}
            //else
            //{
                parms[0].Direction = ParameterDirection.Input;
            //}
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
                        codItem = Convert.ToInt32(cmd.Parameters[PARMcodItem].Value);
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
                codItem = Convert.ToInt32(cmd.Parameters[PARMcodItem].Value);
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
        /// <param name="codItem">Código do Registro</param>
        public static void Delete(int codItem, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodItem, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = codItem;
            parms[1].Value = codCliente;
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
        /// <param name="codItem">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codItem, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodItem, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
                parms[0].Value = codItem;
                parms[1].Value = codCliente;
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
        /// <param name="codItem">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codItem, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodItem, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codItem;
            param[1].Value = codCliente;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codItem">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codItem, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodItem, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codItem;
            param[1].Value = codCliente;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codItem">Código do Registro</param>
        /// <returns>ListaEvento</returns>
        public static ListaEvento GetDataRow(int codItem, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(codItem, codCliente);
            ListaEvento ListaEvento = new ListaEvento();
            try
            {
                if (dr.Read())
                {
                    ListaEvento.codItem = Convert.ToInt32(dr["A289_cd_item"]);
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        ListaEvento.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    ListaEvento.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    ListaEvento.codGrupoReserva = new GrupoReserva(Convert.ToInt32(dr["A196_cd_grupo_res"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ListaEvento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ListaEvento = new ListaEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ListaEvento;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codItem">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ListaEvento</returns>
        public static ListaEvento GetDataRow(int codItem, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codItem, codCliente, trans);
            ListaEvento ListaEvento = new ListaEvento();
            try
            {
                if (dr.Read())
                {
                    ListaEvento.codItem = Convert.ToInt32(dr["A289_cd_item"]);
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        ListaEvento.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    ListaEvento.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    ListaEvento.codGrupoReserva = new GrupoReserva(Convert.ToInt32(dr["A196_cd_grupo_res"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ListaEvento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ListaEvento = new ListaEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ListaEvento;
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

    }
}
