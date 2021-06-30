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
    public class ParticipanteEvFechado // T197_PARTIC_EV_FECH
    {
        #region Atributos
        private int numContato;
        private Eventos codEvento;
        private Cliente codCliente;
        private int codGrupoReserva;
        private DateTime? dtaInscricao;
        private int? indAprovado;
        private int? indCertifEmitido;
        private string nomCracha;
        private int? indPago;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodGrupoReserva
        {
          get { return codGrupoReserva; }
          set { codGrupoReserva = value; }
        }
        public DateTime? DtaInscricao
        {
          get { return dtaInscricao; }
          set { dtaInscricao = value; }
        }
        public int? IndAprovado
        {
          get { return indAprovado; }
          set { indAprovado = value; }
        }
        public int? IndCertifEmitido
        {
          get { return indCertifEmitido; }
          set { indCertifEmitido = value; }
        }
        public string NomCracha
        {
          get { return nomCracha; }
          set { nomCracha = value; }
        }
        public int? IndPago
        {
          get { return indPago; }
          set { indPago = value; }
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
        public int NumContato
        {
          get { return numContato; }
          set { numContato = value; }
        }
        public string LogUsuario 
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ParticipanteEvFechado()
            : this(-1)
        { }
        public ParticipanteEvFechado(int numContato)
        {
            this.numContato = numContato; 
            this.codEvento = new Eventos(); 
            this.codCliente = new Cliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ParticipanteEvFechado.ParticipanteEvFechadoInc";
        private const string SPUPDATE = "ParticipanteEvFechado.ParticipanteEvFechadoAlt";
        private const string SPDELETE = "ParticipanteEvFechado.ParticipanteEvFechadoDel";
        private const string SPSELECTID = "ParticipanteEvFechado.ParticipanteEvFechadoSelId";
        private const string SPSELECTPAG = "ParticipanteEvFechado.ParticipanteEvFechadoSelPag";
        private const string SPFECHAEVENTO = "ParticipanteEvFechado.FechaParticipEv";
        #endregion

        #region Parametros Oracle
        private const string PARMnumContato = "numContato";
        private const string PARMcodEvento = "codEvento";
        private const string PARMcodCliente = "codCliente";
        private const string PARMCURSOR = "curParticipanteEvFechado";
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
                    /*0*/ new OracleParameter(PARMnumContato, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodEvento, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodCliente, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "codGrupoReserva", OracleType.Int32),
                    /*4*/ new OracleParameter( "dtaInscricao", OracleType.DateTime),
                    /*5*/ new OracleParameter( "indAprovado", OracleType.Int32),
                    /*6*/ new OracleParameter( "indCertifEmitido", OracleType.Int32),
                    /*8*/ new OracleParameter( "nomCracha", OracleType.VarChar),
                    /*9*/ new OracleParameter( "indPago", OracleType.Int32),
                    /*0*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.numContato;
            parms[1].Value = this.codEvento.CodEvento;
            parms[2].Value = this.codCliente.CodCLIENTE;
            parms[3].Value = this.codGrupoReserva;
            parms[4].Value = this.dtaInscricao;

            parms[5].Value = DBNull.Value;
            parms[6].Value = DBNull.Value;
            parms[8].Value = DBNull.Value;

            if (this.indAprovado!=null)
            { parms[5].Value = this.indAprovado; }
            if (this.indCertifEmitido!=null)
            { parms[6].Value = this.indCertifEmitido; }
            parms[7].Value = this.nomCracha;
            if (this.indPago!=null)
            { parms[8].Value = this.indPago;}
            parms[9].Value = this.logUsuario;
            if (this.numContato < 0)
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
                        numContato = Convert.ToInt32(cmd.Parameters[PARMnumContato].Value);
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
                numContato = Convert.ToInt32(cmd.Parameters[PARMnumContato].Value);
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
        /// <param name="numContato">Código do Registro</param>
        public static void Delete(int numContato, int codEvento, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumContato, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = numContato;
            parms[1].Value = codEvento;
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
        /// <param name="numContato">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int numContato, int codEvento, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumContato, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4) 
            };
                parms[0].Value = numContato;
                parms[1].Value = codEvento;
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
        /// <param name="numContato">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numContato, int codEvento, int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumContato, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numContato;
            param[1].Value = codEvento;
            param[2].Value = codCliente;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }


        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="numContato">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numContato, int codEvento, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumContato, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numContato;
            param[1].Value = codEvento;
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
        /// <param name="numContato">Código do Registro</param>
        /// <returns>ParticipanteEvFechado</returns>
        public static ParticipanteEvFechado GetDataRow(int numContato, int codEvento, int codCliente)
        {
            OracleDataReader dr = LoadDataDr(numContato, codEvento, codCliente);
            ParticipanteEvFechado ParticipanteEvFechado = new ParticipanteEvFechado();
            try
            {
                if (dr.Read())
                {
                    ParticipanteEvFechado.numContato = Convert.ToInt32(dr["A014_num_cont"]);
                    ParticipanteEvFechado.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    ParticipanteEvFechado.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    ParticipanteEvFechado.codGrupoReserva = Convert.ToInt32(dr["A196_cd_grupo_res"]);
                    ParticipanteEvFechado.dtaInscricao = Convert.ToDateTime(dr["A197_dt_inscr"]);
                    ParticipanteEvFechado.indAprovado = Convert.ToInt32(dr["A197_ind_aprov"]);
                    if (dr["A197_ind_certif_emitido"] != DBNull.Value && dr["A197_ind_certif_emitido"] != DBNull.Value && dr["A197_ind_certif_emitido"].ToString() != "") 
                        ParticipanteEvFechado.indCertifEmitido = Convert.ToInt32(dr["A197_ind_certif_emitido"]);
                    ParticipanteEvFechado.nomCracha = Convert.ToString(dr["A197_nm_cracha"]);
                    ParticipanteEvFechado.indPago = Convert.ToInt32(dr["A197_ind_pago"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ParticipanteEvFechado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ParticipanteEvFechado = new ParticipanteEvFechado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ParticipanteEvFechado;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="numContato">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ParticipanteEvFechado</returns>
        public static ParticipanteEvFechado GetDataRow(int numContato, int codEvento, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numContato, codEvento, codCliente, trans);
            ParticipanteEvFechado ParticipanteEvFechado = new ParticipanteEvFechado();
            try
            {
                if (dr.Read())
                {
                    ParticipanteEvFechado.numContato = Convert.ToInt32(dr["A014_num_cont"]);
                    ParticipanteEvFechado.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    ParticipanteEvFechado.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    ParticipanteEvFechado.codGrupoReserva = Convert.ToInt32(dr["A196_cd_grupo_res"]);
                    ParticipanteEvFechado.dtaInscricao = Convert.ToDateTime(dr["A197_dt_inscr"]);
                    ParticipanteEvFechado.indAprovado = Convert.ToInt32(dr["A197_ind_aprov"]);
                    if (dr["A197_ind_certif_emitido"] != DBNull.Value && dr["A197_ind_certif_emitido"] != DBNull.Value && dr["A197_ind_certif_emitido"].ToString() != "")
                        ParticipanteEvFechado.indCertifEmitido = Convert.ToInt32(dr["A197_ind_certif_emitido"]);
                    ParticipanteEvFechado.nomCracha = Convert.ToString(dr["A197_nm_cracha"]);
                    ParticipanteEvFechado.indPago = Convert.ToInt32(dr["A197_ind_pago"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ParticipanteEvFechado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ParticipanteEvFechado = new ParticipanteEvFechado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ParticipanteEvFechado;
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


        #region Métodos Específicos 

        #region LoadDataParticipanteEvFechado
        /// <summary>
        /// atraves do evento ecolhido lista-se participantes
        /// </summary>
        /// <param name="CodEvento">para filtrar por evento</param>
        /// <returns>paginação com nomes de clientes</returns>

        public static Paginacao GetDataParticipanteEvFechado(string CodEvento)
        {
            string where = " AND T197.A022_cd_ev =" + CodEvento;
            Paginacao dr = ParticipanteEvFechado.LoadDataPaginacao(where, 1, 10000, "2");

            return dr;
        }

        /// <summary>
        /// atraves do evento ecolhido lista-se participantes
        /// </summary>
        /// <param name="CodEvento">para filtrar por evento</param>
        /// <param name="indPago">para trazer somente pagos</param>
        /// <returns>paginação com nomes de clientes somente pagos</returns>
        public static Paginacao LoadDataParticipanteEvento(string CodEvento, bool indPago)
        {
            string where = " AND T197.A022_cd_ev =" + CodEvento;// +" AND nvl(A197_ind_pago, 0)=1";
            Paginacao dr = ParticipanteEvFechado.LoadDataPaginacao(where, 1, 10000, "2");

            return dr;
        }
        #endregion

        #region FechamentoEvento
        /// <summary>
        /// da update em reserva de vaga
        /// </summary>
        /// <param name="codCliente"></param>
        /// <param name="numContato"></param>
        /// <param name="codEvento"></param>
        /// <param name="trans"></param>
        public static void FechamentoEvento(int codEvento, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter(PARMcodEvento,  OracleType.Int32, 8)
                };

            param[0].Value = codEvento;

            try
            {
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPFECHAEVENTO, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion


        #endregion

    }
}