using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/07/2007 
//-- Autor :  Daniel -- Melhorias Honorato 2010

namespace Classes
{
    public class InstrutorEvento // T031 a125_cd_instr 
    {
        #region Atributos
        private Eventos codEvento;
        private Instrutor codInstrutor;
        private DateTime datInicioInstrutor;
        private int? codCondicaoPagamento;
        private DateTime datFimInstrutor;
        private decimal? qtdHoraInstrutor;
        private decimal? qtdHoraConsultoria;
        private decimal? medAvaliacao;
        private DateTime? datInstrutorLeu;
        private int? indMostraExtranet;
        private int? assCertificado;
        private int? indTrainee;
        private string nomObservação;
        private decimal? vlrHora;
        private int? indOrdem;
        private decimal? vlrHoraInstrutor;
        private decimal? vlrHoraConsultoria;
        private Cliente codCliente; 
        private string logUsuario;
        #endregion

        #region Propriedades
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public Instrutor CodInstrutor
        {
            get { return codInstrutor; }
            set { codInstrutor = value; }
        }
        public DateTime DatInicioInstrutor
        {
            get { return datInicioInstrutor; }
            set { datInicioInstrutor = value; }
        }
        public int? CodCondicaoPagamento
        {
            get { return codCondicaoPagamento; }
            set { codCondicaoPagamento = value; }
        }
        public DateTime DatFimInstrutor
        {
            get { return datFimInstrutor; }
            set { datFimInstrutor = value; }
        }
        public decimal? QtdHoraInstrutor
        {
            get { return qtdHoraInstrutor; }
            set { qtdHoraInstrutor = value; }
        }
        public decimal? QtdHoraConsultoria
        {
            get { return qtdHoraConsultoria; }
            set { qtdHoraConsultoria = value; }
        }
        public decimal? MedAvaliacao
        {
            get { return medAvaliacao; }
            set { medAvaliacao = value; }
        }
        public DateTime? DatInstrutorLeu
        {
            get { return datInstrutorLeu; }
            set { datInstrutorLeu = value; }
        }
        public int? IndMostraExtranet
        {
            get { return indMostraExtranet; }
            set { indMostraExtranet = value; }
        }
        public int? AssCertificado
        {
            get { return assCertificado; }
            set { assCertificado = value; }
        }
        public int? IndTrainee
        {
            get { return indTrainee; }
            set { indTrainee = value; }
        }
        public int? IndOrdem
        {
            get { return indOrdem; }
            set { indOrdem = value; }
        }
        public string NomObservação
        {
            get { return nomObservação; }
            set { nomObservação = value; }
        }
        public decimal? VlrHora
        {
            get { return vlrHora; }
            set { vlrHora = value; }
        }
        public decimal? VlrHoraInstrutor
        {
            get { return vlrHoraInstrutor; }
            set { vlrHoraInstrutor = value; }
        }
        public decimal? VlrHoraConsultoria
        {
            get { return vlrHoraConsultoria; }
            set { vlrHoraConsultoria = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public InstrutorEvento()
            : this(-1)
        { }
        public InstrutorEvento(int codEvento)
        {
            this.codEvento = new Eventos();
            this.codInstrutor = new Instrutor();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "InstrutorEvento.InstrutorEventoInc";
        private const string SPUPDATE = "InstrutorEvento.InstrutorEventoAlt";
        private const string SPDELETE = "InstrutorEvento.InstrutorEventoDel";
        private const string SPSELECTID = "InstrutorEvento.InstrutorEventoSelId";
        private const string SPSELECTPAG = "InstrutorEvento.InstrutorEventoSelPag";
        private const string SPSELECTEXISTE = "InstrutorEvento.InstrutorEventoSelExistente";
        private const string SPSELECTCONTRATO = "InstrutorEvento.InstrutorEventoSelContrato";
        //Tirando da DLL
        private const string SPTConsultarInstrutor = "InstrutorEvento.TConsultarInstrutor";
        
        #endregion

        #region Parametros Oracle
        private const string PARMCODEVENTO = "codEvento";
        private const string PARMCODINSTRUTOR = "codInstrutor";
        private const string PARMCURSOR = "curInstrutorEvento";
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
                    /*0*/ new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODINSTRUTOR, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "datInicioInstrutor", OracleType.DateTime),
                    /*3*/ new OracleParameter( "codCondicaoPagamento", OracleType.Int32),
                    /*4*/ new OracleParameter( "datFimInstrutor", OracleType.DateTime),
                    
                    /*5*/ new OracleParameter( "qtdHoraInstrutor", OracleType.Float),
                    /*6*/ new OracleParameter( "qtdHoraConsultoria", OracleType.Float),
                    /*7*/ new OracleParameter( "medAvaliacao", OracleType.Float),
                    /*8*/ new OracleParameter( "datInstrutorLeu", OracleType.DateTime),
                    /*9*/ new OracleParameter( "indMostraExtranet", OracleType.Int32),
                    /*10*/ new OracleParameter( "assCertificado", OracleType.Int32),
                    /*11*/ new OracleParameter( "indTrainee", OracleType.Int32),
                    /*12*/ new OracleParameter( "indOrdem", OracleType.Int32),
                    /*13*/ new OracleParameter( "nomObservação", OracleType.VarChar),
                    /*14*/ new OracleParameter( "vlrHora", OracleType.Float),
                    /*15*/ new OracleParameter( "vlrHoraInstrutor", OracleType.Float),
                    /*16*/ new OracleParameter( "vlrHoraConsultoria", OracleType.Float),
                    /*17*/ new OracleParameter( "codCliente", OracleType.Int32),
                    /*18*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[1].Value = this.codInstrutor.CodInstrutor.CodPessoa;
            parms[2].Value = this.datInicioInstrutor;
            parms[3].Value = DBNull.Value;
            if (this.codCondicaoPagamento.HasValue)
            { parms[3].Value = this.codCondicaoPagamento; }
            parms[4].Value = this.datFimInstrutor;
            parms[5].Value = DBNull.Value;
            if (this.qtdHoraInstrutor != null)
            { parms[5].Value = this.qtdHoraInstrutor; }
            parms[6].Value = DBNull.Value;
            if (this.qtdHoraConsultoria != null)
            { parms[6].Value = this.qtdHoraConsultoria; }
            parms[7].Value = DBNull.Value;
            if (this.medAvaliacao != null)
            { parms[7].Value = this.medAvaliacao; }
            parms[8].Value = DBNull.Value;
            if (this.datInstrutorLeu != null)
            { parms[8].Value = this.datInstrutorLeu; }
            parms[9].Value = DBNull.Value;
            if (this.indMostraExtranet != null)
            { parms[9].Value = this.indMostraExtranet; }
            parms[10].Value = DBNull.Value;
            if (this.assCertificado != null)
            { parms[10].Value = this.assCertificado; }
            parms[11].Value = DBNull.Value;
            if (this.indTrainee != null)
            { parms[11].Value = this.indTrainee; }
            parms[12].Value = DBNull.Value;
            if (this.indOrdem != null)
            { parms[12].Value = this.indOrdem; }
            parms[13].Value = "";
            if (this.nomObservação != null)
            { parms[13].Value = this.nomObservação.ToUpper(); }
            parms[14].Value = DBNull.Value;
            if (this.vlrHora != null)
            { parms[14].Value = this.vlrHora; }
            parms[15].Value = DBNull.Value;
            if (this.vlrHoraInstrutor != null)
            { parms[15].Value = this.vlrHoraInstrutor; }
            parms[16].Value = DBNull.Value;
            if (this.vlrHoraConsultoria != null)
            { parms[16].Value = this.vlrHoraConsultoria; }
            parms[17].Value = DBNull.Value;
            if (this.codCliente != null && this.codCliente.CodCLIENTE != -1)
            { parms[17].Value = this.codCliente.CodCLIENTE; }
            parms[18].Value = this.logUsuario.ToUpper();

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
        public static void Delete(int codEvento, int codInstrutor)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODINSTRUTOR, OracleType.Int32, 4)
            };
            parms[0].Value = codEvento;
            parms[1].Value = codInstrutor;
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
        public static void Delete(int codEvento, int codInstrutor, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODINSTRUTOR, OracleType.Int32, 4)
            };
                parms[0].Value = codEvento;
                parms[1].Value = codInstrutor;
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
        public static OracleDataReader LoadDataDr(int codEvento, int codInstrutor)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODINSTRUTOR, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = codInstrutor;
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
        public static OracleDataReader LoadDataDr(int codEvento, int codInstrutor, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODINSTRUTOR, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Value = codInstrutor;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        //
        //busca dados que era DLL
        public static OracleDataReader LoadDataTConsultarInstrutor(string CdEvento, string DtInicio, string DtFim, string Entidade)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("strCdEvento", OracleType.VarChar,100),
                    new OracleParameter("strDtInicio", OracleType.VarChar,100),
                    new OracleParameter("strDtFim", OracleType.VarChar,100),
                    new OracleParameter("strEntidade", OracleType.VarChar,100),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = CdEvento;
            param[1].Value = DtInicio;
            param[2].Value = DtFim;
            param[3].Value = Entidade;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPTConsultarInstrutor, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>InstrutorEvento</returns>
        public static InstrutorEvento GetDataRow(int codEvento, int codInstrutor)
        {
            OracleDataReader dr = LoadDataDr(codEvento, codInstrutor);
            InstrutorEvento ie = new InstrutorEvento();
            try
            {
                if (dr.Read())
                {
                    ie.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    ie.codInstrutor.CodInstrutor = new Pessoa(Convert.ToInt32(dr["a125_cd_instr"]));
                    ie.datInicioInstrutor = Convert.ToDateTime(dr["a031_dt_ini_inst"]);
                    ie.codCondicaoPagamento = Convert.ToInt32(dr["a2204_cd_cond_pgto"]);
                    ie.datFimInstrutor = Convert.ToDateTime(dr["a031_dt_fim_inst"]);
                    if (dr["a031_qtd_hr_inst"] != DBNull.Value)
                    { ie.qtdHoraInstrutor = Convert.ToDecimal(dr["a031_qtd_hr_inst"]); }
                    if (dr["a031_qtd_hr_cons"] != DBNull.Value)
                    { ie.qtdHoraConsultoria = Convert.ToDecimal(dr["a031_qtd_hr_cons"]); }
                    if (dr["a031_md_aval"] != DBNull.Value)
                    { ie.medAvaliacao = Convert.ToDecimal(dr["a031_md_aval"]); }
                    if (dr["a031_dt_instrutor_leu"] != DBNull.Value)
                    { ie.datInstrutorLeu = Convert.ToDateTime(dr["a031_dt_instrutor_leu"]); }
                    if (dr["a031_ind_mostra_extranet"] != DBNull.Value)
                    { ie.indMostraExtranet = Convert.ToInt32(dr["a031_ind_mostra_extranet"]); }
                    if (dr["a031_assina_certif"] != DBNull.Value)
                    { ie.assCertificado = Convert.ToInt32(dr["a031_assina_certif"]); }
                    if (dr["a031_ind_trainee"] != DBNull.Value)
                    { ie.indTrainee = Convert.ToInt32(dr["a031_ind_trainee"]); }
                    if (dr["a031_ind_ordem"] != DBNull.Value)
                    { ie.indOrdem = Convert.ToInt32(dr["a031_ind_ordem"]); }
                    ie.nomObservação = Convert.ToString(dr["a031_observacao"]);
                    if (dr["a031_valor_hora"] != DBNull.Value)
                    { ie.vlrHora = Convert.ToDecimal(dr["a031_valor_hora"]); }
                    if (dr["a031_vlr_hr_inst"] != DBNull.Value)
                    { ie.vlrHoraInstrutor = Convert.ToDecimal(dr["a031_vlr_hr_inst"]); }
                    if (dr["a031_vlr_hr_cons"] != DBNull.Value)
                    { ie.vlrHoraConsultoria = Convert.ToDecimal(dr["a031_vlr_hr_cons"]); }
                    if (dr["a012_cd_cli"] != DBNull.Value)
                    { ie.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"])); }
                    if (dr["usu_inc_alt"] != DBNull.Value)
                    { ie.logUsuario = Convert.ToString(dr["usu_inc_alt"]); }
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ie = new InstrutorEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ie;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>InstrutorEvento</returns>
        public static InstrutorEvento GetDataRow(int codEvento, int codInstrutor, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codEvento, codInstrutor, trans);
            InstrutorEvento ie = new InstrutorEvento();
            try
            {
                if (dr.Read())
                {
                    ie.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    ie.codInstrutor.CodInstrutor = new Pessoa(Convert.ToInt32(dr["a125_cd_instr"]));
                    ie.datInicioInstrutor = Convert.ToDateTime(dr["a031_dt_ini_inst"]);
                    ie.codCondicaoPagamento = Convert.ToInt32(dr["a2204_cd_cond_pgto"]);
                    ie.datFimInstrutor = Convert.ToDateTime(dr["a031_dt_fim_inst"]);
                    if (dr["a031_qtd_hr_inst"] != DBNull.Value)
                    { ie.qtdHoraInstrutor = Convert.ToDecimal(dr["a031_qtd_hr_inst"]); }
                    if (dr["a031_qtd_hr_cons"] != DBNull.Value)
                    { ie.qtdHoraConsultoria = Convert.ToDecimal(dr["a031_qtd_hr_cons"]); }
                    if (dr["a031_md_aval"] != DBNull.Value)
                    { ie.medAvaliacao = Convert.ToDecimal(dr["a031_md_aval"]); }
                    if (dr["a031_dt_instrutor_leu"] != DBNull.Value)
                    { ie.datInstrutorLeu = Convert.ToDateTime(dr["a031_dt_instrutor_leu"]); }
                    if (dr["a031_ind_mostra_extranet"] != DBNull.Value)
                    { ie.indMostraExtranet = Convert.ToInt32(dr["a031_ind_mostra_extranet"]); }
                    if (dr["a031_assina_certif"] != DBNull.Value)
                    { ie.assCertificado = Convert.ToInt32(dr["a031_assina_certif"]); }
                    if (dr["a031_ind_trainee"] != DBNull.Value)
                    { ie.indTrainee = Convert.ToInt32(dr["a031_ind_trainee"]); }
                    if (dr["a031_ind_ordem"] != DBNull.Value)
                    { ie.indOrdem = Convert.ToInt32(dr["a031_ind_ordem"]); }
                    ie.nomObservação = Convert.ToString(dr["a031_observacao"]);
                    if (dr["a031_valor_hora"] != DBNull.Value)
                    { ie.vlrHora = Convert.ToDecimal(dr["a031_valor_hora"]); }
                    if (dr["a031_vlr_hr_inst"] != DBNull.Value)
                    { ie.vlrHoraInstrutor = Convert.ToDecimal(dr["a031_vlr_hr_inst"]); }
                    if (dr["a031_vlr_hr_cons"] != DBNull.Value)
                    { ie.vlrHoraConsultoria = Convert.ToDecimal(dr["a031_vlr_hr_cons"]); }
                    if (dr["a012_cd_cli"] != DBNull.Value)
                    { ie.codCliente = new Cliente(Convert.ToInt32(dr["a012_cd_cli"])); }
                    if (dr["usu_inc_alt"] != DBNull.Value)
                    { ie.logUsuario = Convert.ToString(dr["usu_inc_alt"]); }
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ie = new InstrutorEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ie;
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

        #region GetNomeInstrutor
        /// <summary>
        /// metodo para encontrar nome de instrutor de um evento.
        /// </summary>
        /// <param name="codEvento"></param>
        /// <returns>string nomeInstrutor</returns>
        public static string GetNomeInstrutor(int codEvento)
        {
            string nomeInstrutor = "";
            Paginacao ins = InstrutorEvento.LoadDataPaginacao(" and a022_cd_ev = " + codEvento, 1, 5, "nvl(a031_assina_certif,0) desc");
            if (ins.DataReader.Read())
            {
                nomeInstrutor = ins.DataReader["A572_nome"].ToString();
            }
            ins.DataReader.Close();

            return nomeInstrutor;
        }
        #endregion

        #region LoadDataDrExistePag
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CodInstrutor"></param>
        /// <param name="CodEventos"></param>
        /// <param name="DataIncInst"></param>
        /// <param name="DataFimInst"></param>
        /// <returns>dr q deve ser fechado no final</returns>
        public static OracleDataReader LoadDataDrExistePag(string CodInstrutor, string CodEventos, string DataIncInst, string DataFimInst)
        { 
            string where = " AND a125_cd_instr = " + CodInstrutor 
                            + " AND a022_cd_ev <> " +  CodEventos
                            + " AND a031_dt_ini_inst >= '" + DataIncInst + "'"
                            + " AND a031_dt_fim_inst <= '" + DataFimInst + "'";
            Paginacao ie = InstrutorEvento.LoadDataPaginacao(where, 1, 100, "2"); 
             

            return ie.DataReader;
        }
        #endregion

        #region LoadDataDrExiste

        public static OracleDataReader LoadDataDrExiste(int codInstrutor, int codEvento, DateTime dataInicial, DateTime dataFinal)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("codInstrutor", OracleType.Int32),
			    new OracleParameter("codEvento", OracleType.Int32),
			    new OracleParameter("dataInicial", OracleType.DateTime),
			    new OracleParameter("dataFinal", OracleType.DateTime),
                new OracleParameter(PARMCURSOR, OracleType.Cursor) 
              };

            parms[0].Value = codInstrutor;
            parms[1].Value = codEvento;
            parms[2].Value = dataInicial;
            parms[3].Value = dataFinal;
            parms[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTEXISTE, parms);
                        
            return dr;
        }

        public static OracleDataReader LoadDataDrContrato(int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEvento;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTCONTRATO, param);
            return dr;
        }

        #endregion

        #endregion

    }
}
