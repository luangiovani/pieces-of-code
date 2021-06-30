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
    public class ParticipanteEvento // T037_partic_ev
    {
        #region Atributos
        private Cliente codCliente;
        private Eventos codEvento;
        private int numContato;
        private DateTime? dataInscricao;
        private int? indAprovado;
        private int? indCerticadoE;
        private int? codGrupoReserva;
        private string nomeCracha;
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
        public int? IndAprovado
        {
            get { return indAprovado; }
            set { indAprovado = value; }
        }
        public DateTime? DataInscricao
        {
            get { return dataInscricao; }
            set { dataInscricao = value; }
        }
        public int? IndCerticadoE
        {
            get { return indCerticadoE; }
            set { indCerticadoE = value; }
        }
        public int? CodGrupoReserva
        {
            get { return codGrupoReserva; }
            set { codGrupoReserva = value; }
        }
        public string NomeCracha
        {
            get { return nomeCracha; }
            set { nomeCracha = value; }
        }
        public int? CodParticipanteEv
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
        public ParticipanteEvento()
            : this(-1)
        { }
        public ParticipanteEvento(int codCliente)
        {
            this.codCliente = new Cliente();
            this.codEvento = new Eventos();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ParticipanteEvento.ParticipanteEventoInc";
        private const string SPUPDATE = "ParticipanteEvento.ParticipanteEventoAlt";
        private const string SPDELETE = "ParticipanteEvento.ParticipanteEventoDel";
        private const string SPSELECTID = "ParticipanteEvento.ParticipanteEventoSelId";
        private const string SPSELECTPAG = "ParticipanteEvento.ParticipanteEventoSelPag";
        private const string SPSELECTPAGRODADA = "ParticipanteEvento.ParticipanteRodadaSelPag";
        private const string SPCONFIRMRESERVA = "ParticipanteEvento.ConfirmaParticipEv";
        private const string SPTODOSPARTICIPANTES = "ParticipanteEvento.TodosParticipantesEvSelId";
        private const string SPTODOSKITS = "TituloEvento.TodosKitsSelId";
        private const string SPTODOSPARTICIPANTESRODADA = "ParticipanteEvento.TodosParticipantesRODADASelId";
        private const string SPTODOSPARTICIANCORARODADA = "ParticipanteEvento.TodosParticipantesANCORASelId";
        private const string SPREPLCONS = "ParticipanteEvento.TodosReplConsSelId";
        private const string SPSELECTCONTATO = "ParticipanteEvento.ParticipanteContatoEventoSel";
        #endregion

        #region Parametros Oracle
        private const string PARMCODCLIENTE = "codCliente";
        private const string PARMCODEVENTO = "codEvento";
        private const string PARMCURSOR = "curParticipanteEvento";
        private const string PARMNUMCONTATO = "numContato";
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
                    /*0*/ new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 8) ,
                    /*1*/ new OracleParameter(PARMCODEVENTO, OracleType.Int32, 8) ,
                    /*2*/ new OracleParameter( "numContato", OracleType.Int32),
                    /*3*/ new OracleParameter( "dataInscricao", OracleType.DateTime),
                    /*4*/ new OracleParameter( "indAprovado", OracleType.Int32),
                    /*5*/ new OracleParameter( "indCerticadoE", OracleType.Int32),
                    /*6*/ new OracleParameter( "codGrupoReserva", OracleType.Int32),
                    /*7*/ new OracleParameter( "nomeCracha", OracleType.VarChar),
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
            parms[3].Value = DBNull.Value;
            if (this.dataInscricao != null)
            { parms[3].Value = this.dataInscricao; }
            parms[4].Value = DBNull.Value;
            if (this.indAprovado != null)
            { parms[4].Value = this.indAprovado; }
            parms[5].Value = DBNull.Value;
            if (this.indCerticadoE != null)
            { parms[5].Value = this.indCerticadoE; }
            parms[6].Value = DBNull.Value;
            if (this.codGrupoReserva != null)
            { parms[6].Value = this.codGrupoReserva; }
            parms[7].Value = this.nomeCracha;
            parms[8].Value = this.logUsuario;

            parms[0].Direction = ParameterDirection.Input;

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
        public static void Delete(int codCliente, int numContato, int codEvento)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 8)  ,
                new OracleParameter(PARMNUMCONTATO, OracleType.Int32, 8),
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 8)
            };
            parms[0].Value = codCliente;
            parms[1].Value = numContato;
            parms[2].Value = codEvento;
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
        public static void Delete(int codCliente, int numContato, int codEvento, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 8)  ,
                new OracleParameter(PARMNUMCONTATO, OracleType.Int32, 8),
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 8)
            };
                parms[0].Value = codCliente;
                parms[1].Value = numContato;
                parms[2].Value = codEvento;
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
        public static OracleDataReader LoadDataDr(int codCliente, int codEvento, int numContato)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                    new OracleParameter(PARMNUMCONTATO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codCliente;
            param[1].Value = codEvento;
            param[2].Value = numContato;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTCONTATO, param);
            return dr;
        }



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
        /// <returns>ParticipanteEvento</returns>
        public static ParticipanteEvento GetDataRow(int codCliente, int codEvento)
        {
            OracleDataReader dr = LoadDataDr(codCliente, codEvento);
            ParticipanteEvento pe = new ParticipanteEvento();
            try
            {
                if (dr.Read())
                {
                    pe.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"]));
                    pe.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    pe.numContato = Convert.ToInt32(dr["a014_num_cont"]);
                    if (dr["a037_dt_inscr"] != DBNull.Value)
                    { pe.dataInscricao = Convert.ToDateTime(dr["a037_dt_inscr"]); }
                    if (dr["a037_ind_aprov"] != DBNull.Value)
                    { pe.indAprovado = Convert.ToInt32(dr["a037_ind_aprov"]); }
                    if (dr["a037_ind_certif_emitido"] != DBNull.Value)
                    { pe.indCerticadoE = Convert.ToInt32(dr["a037_ind_certif_emitido"]); }
                    if (dr["a196_cd_grupo_res"] != DBNull.Value)
                    { pe.codGrupoReserva = Convert.ToInt32(dr["a196_cd_grupo_res"]); }
                    if (dr["a037_nm_cracha"] != DBNull.Value)
                    { pe.nomeCracha = Convert.ToString(dr["a037_nm_cracha"]); }
                    pe.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }

            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pe = new ParticipanteEvento();
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
        /// <returns>ParticipanteEvento</returns>
        public static ParticipanteEvento GetDataRow(int codCliente, int codEvento, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codCliente, codEvento, trans);
            ParticipanteEvento pe = new ParticipanteEvento();
            try
            {
                if (dr.Read())
                {
                    pe.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"]));
                    pe.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    pe.numContato = Convert.ToInt32(dr["a014_num_cont"]);
                    if (dr["a037_dt_inscr"] != DBNull.Value)
                    { pe.dataInscricao = Convert.ToDateTime(dr["a037_dt_inscr"]); }
                    if (dr["a037_ind_aprov"] != DBNull.Value)
                    { pe.indAprovado = Convert.ToInt32(dr["a037_ind_aprov"]); }
                    if (dr["a037_ind_certif_emitido"] != DBNull.Value)
                    { pe.indCerticadoE = Convert.ToInt32(dr["a037_ind_certif_emitido"]); }
                    if (dr["a196_cd_grupo_res"] != DBNull.Value)
                    { pe.codGrupoReserva = Convert.ToInt32(dr["a196_cd_grupo_res"]); }
                    pe.nomeCracha = Convert.ToString(dr["a037_nm_cracha"]);
                    pe.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pe = new ParticipanteEvento();
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
        /// GetDataRow
        /// </summary>
        /// <param name="codCliente">Código do Registro</param>
        /// <returns>ParticipanteEvento</returns>
        public static ParticipanteEvento GetDataRow(int codCliente, int codEvento, int numContato)
        {
            OracleDataReader dr = LoadDataDr(codCliente, codEvento, numContato);
            ParticipanteEvento pe = new ParticipanteEvento();
            try
            {
                if (dr.Read())
                {
                    pe.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"]));
                    pe.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    pe.numContato = Convert.ToInt32(dr["a014_num_cont"]);
                    if (dr["a037_dt_inscr"] != DBNull.Value)
                    { pe.dataInscricao = Convert.ToDateTime(dr["a037_dt_inscr"]); }
                    if (dr["a037_ind_aprov"] != DBNull.Value)
                    { pe.indAprovado = Convert.ToInt32(dr["a037_ind_aprov"]); }
                    if (dr["a037_ind_certif_emitido"] != DBNull.Value)
                    { pe.indCerticadoE = Convert.ToInt32(dr["a037_ind_certif_emitido"]); }
                    if (dr["a196_cd_grupo_res"] != DBNull.Value)
                    { pe.codGrupoReserva = Convert.ToInt32(dr["a196_cd_grupo_res"]); }
                    if (dr["a037_nm_cracha"] != DBNull.Value)
                    { pe.nomeCracha = Convert.ToString(dr["a037_nm_cracha"]); }
                    pe.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }

            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pe = new ParticipanteEvento();
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

        public static Paginacao LoadDataPaginacaoRodada(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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


            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGRODADA, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        #endregion


        #endregion

        #region Métodos Específicos

        #region ConfirmaReserva
        /// <summary>
        /// da update em reserva de vaga
        /// </summary>
        /// <param name="codCliente"></param>
        /// <param name="numContato"></param>
        /// <param name="codEvento"></param>
        /// <param name="trans"></param>
        public static void ConfirmaReserva(int codCliente, int numContato, int codEvento, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODCLIENTE, OracleType.Int32, 8), 
                    new OracleParameter( "numContato",  OracleType.Int32, 8),
                    new OracleParameter(PARMCODEVENTO,  OracleType.Int32, 8)
                };

            param[0].Value = codCliente;
            param[1].Value = numContato;
            param[2].Value = codEvento;

            try
            {
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPCONFIRMRESERVA, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region MostraTodosParticipantes
        /// <summary>
        /// MostraTodosParticipantes
        /// </summary>
        /// <param name="codCliente">Código do Registro</param>
        public static OracleDataReader MostraTodosParticipantes(int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr;
            try
            {
                dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPTODOSPARTICIPANTES, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dr;
        }
        //kit
        public static OracleDataReader MostraTodosKITS(int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr;
            try
            {
                dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPTODOSKITS, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dr;
        }
        
        //Rodada
        public static OracleDataReader MostraTodosParticipantesRodada(int codEvento, int codRodada, int ancora)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                    new OracleParameter("codRodada", OracleType.Int32, 4),
                    new OracleParameter("indAncora", OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = codRodada;
            param[2].Value = ancora;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr;
            try
            {
                dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPTODOSPARTICIPANTESRODADA, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dr;
        }

        /// <summary>
        /// Tras todos os participantes do evento indicando se possuem ou não produtos relacionados com o ancora informado
        /// </summary>
        /// <param name="codAncora"></param>
        /// <param name="codRodada"></param>
        /// <param name="codEvento"></param>
        /// <returns></returns>
        public static OracleDataReader MostraTodosParticipantesAncoraRodada(int codAncora, int codRodada, int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter("codAncora", OracleType.Int32, 4),                    
                new OracleParameter("codRodada", OracleType.Int32, 4),
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codAncora;
            param[1].Value = codRodada;
            param[2].Value = codEvento;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr;
            try
            {
                dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPTODOSPARTICIANCORARODADA, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dr;
        }

        //Consultoria Replicacao 
        public static OracleDataReader MostraReplicacaCons(int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr;
            try
            {
                dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPREPLCONS, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dr;
        }

        #endregion

        #region GetDataParticipanteEvento
        /// <summary>
        /// atraves do evento ecolhido lista-se participantes
        /// </summary>
        /// <param name="CodEvento">para filtrar por evento</param>
        /// <returns>paginação com nomes de clientes</returns>

        public static Paginacao GetDataParticipanteEvento(string CodEvento)
        {
            string where = " AND T037.A022_cd_ev =" + CodEvento;
            Paginacao Concli = ParticipanteEvento.LoadDataPaginacao(where, 1, 10000, "2");

            return Concli;
        }

        /// <summary>
        /// atraves do evento ecolhido lista-se participantes
        /// </summary>
        /// <param name="CodEvento">para filtrar por evento</param>
        /// <param name="indPago">para trazer somente pagos</param>
        /// <returns>paginação com nomes de clientes somente pagos</returns>
        public static Paginacao GetDataParticipanteEvento(string CodEvento, bool indPago)
        {
            string where = " AND T037.A022_cd_ev =" + CodEvento;// +" AND pago";
            Paginacao Concli = ParticipanteEvento.LoadDataPaginacao(where, 1, 10000, "2");

            return Concli;
        }
        #endregion

        #endregion
    }
}
