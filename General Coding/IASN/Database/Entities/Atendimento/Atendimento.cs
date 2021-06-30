using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T001_ATEND
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
    public class Atendimento
    {
        #region Atributos
        private int numAtend;
        private Usuario codUsuario;
        private EscritorioSebrae codEscrSebrae;
        private Cliente codCliente;
        private ContatoCliente numContato;
        private string codTipAtend;
        private int? numAnoRef;
        private int? numMesRef;
        private DateTime dtaAtendimento;
        private string strTempoAtendimento;
        private string strObservacao;
        private int? numTotAtBi;
        private int? numSequencia;
        private FonteInform codFonte;
        private Pais codPais;
        private Estado codEstado;
        private Cidade codCidade;
        private Setorial codSetor;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int NumAtend
        {
            get { return numAtend; }
            set { numAtend = value; }
        }

        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }

        public EscritorioSebrae CodEscrSebrae
        {
            get { return codEscrSebrae; }
            set { codEscrSebrae = value; }
        }

        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }

        public ContatoCliente NumContato
        {
            get { return numContato; }
            set { numContato = value; }
        }

        public string CodTipAtend
        {
            get { return codTipAtend; }
            set { codTipAtend = value; }
        }

        public int? NumAnoRef
        {
            get { return numAnoRef; }
            set { numAnoRef = value; }
        }

        public int? NumMesRef
        {
            get { return numMesRef; }
            set { numMesRef = value; }
        }

        public DateTime DtaAtendimento
        {
            get { return dtaAtendimento; }
            set { dtaAtendimento = value; }
        }

        public string StrTempoAtendimento
        {
            get { return strTempoAtendimento; }
            set { strTempoAtendimento = value; }
        }

        public string StrObservacao
        {
            get { return strObservacao; }
            set { strObservacao = value; }
        }

        public int? NumTotAtBi
        {
            get { return numTotAtBi; }
            set { numTotAtBi = value; }
        }

        public int? NumSequencia
        {
            get { return numSequencia; }
            set { numSequencia = value; }
        }

        public FonteInform CodFonte
        {
            get { return codFonte; }
            set { codFonte = value; }
        }

        public Pais CodPais
        {
            get { return codPais; }
            set { codPais = value; }
        }

        public Estado CodEstado
        {
            get { return codEstado; }
            set { codEstado = value; }
        }

        public Cidade CodCidade
        {
            get { return codCidade; }
            set { codCidade = value; }
        }

        public Setorial CodSetor
        {
            get { return codSetor; }
            set { codSetor = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public Atendimento()
            : this(-1)
        { }
        public Atendimento(int numAtend)
        {
            this.numAtend = numAtend;
            this.codUsuario = new Usuario();
            this.codEscrSebrae = new EscritorioSebrae();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Atendimento.AtendimentoInc";
        private const string SPUPDATE = "Atendimento.AtendimentoAlt";
        private const string SPDELETE = "Atendimento.AtendimentoDel";
        private const string SPSELECTID = "Atendimento.AtendimentoSelId";
        private const string SPSELECTPAG = "Atendimento.AtendimentoSelPag";
        private const string SPSELECTHISTORICO = "Atendimento.AtendimentoHistorico";
        private const string SPSELECTGRAFICO = "Atendimento.AtendimentoGraficoSelId";
        private const string SPSELECTGRAFICOCLI = "Atendimento.AtendimentoGraficoCliSelId";
        private const string SPSELECTRELATORIO = "Atendimento.AtendimentoRelatorioSelId";
        private const string SPSELECTRELProjetos = "Atendimento.AtRelProjetos";
        private const string SPSELECTGENERICO = "Atendimento.AtendimentoGenericoSelId";
        
        #endregion

        #region Parametros Oracle
        private const string PARMnumAtend = "numAtend";
        private const string PARMcodUsuario = "codUsuario";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMCURSOR = "curAtendimento";
        private const string PARMcodCliente = "codCliente";
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
                    /*0*/ new OracleParameter(PARMnumAtend, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodUsuario, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "codCliente", OracleType.Int32),
                    /*4*/ new OracleParameter( "numContato", OracleType.Int32),
                    /*5*/ new OracleParameter( "codTipAtend", OracleType.VarChar),
                    /*6*/ new OracleParameter( "numAnoRef", OracleType.Int32),
                    /*7*/ new OracleParameter( "numMesRef", OracleType.Int32),
                    /*8*/ new OracleParameter( "dtaAtendimento", OracleType.DateTime),
                    /*9*/ new OracleParameter( "strTempoAtendimento", OracleType.VarChar),
                    /*0*/ new OracleParameter( "strObservacao", OracleType.VarChar),
                    /*1*/ new OracleParameter( "numTotAtBi", OracleType.Int32),
                    /*2*/ new OracleParameter( "numSequencia", OracleType.Int32),
                    /*3*/ new OracleParameter( "codFonte", OracleType.Int32),
                    /*4*/ new OracleParameter( "codPais", OracleType.Int32),
                    /*5*/ new OracleParameter( "codEstado", OracleType.Int32),
                    /*6*/ new OracleParameter( "codCidade", OracleType.Int32),
                    /*18*/ new OracleParameter( "logUsuario", OracleType.VarChar)

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
            parms[0].Value = this.numAtend;
            parms[1].Value = this.codUsuario.CodUsuario;
            parms[2].Value = this.codEscrSebrae.CodEscrSebrae;
            parms[3].Value = this.CodCliente.CodCLIENTE;
            parms[4].Value = this.numContato.NumContato;
            parms[5].Value = this.codTipAtend;
            parms[6].Value = DBNull.Value;
            parms[7].Value = DBNull.Value;
            if (this.numAnoRef != null)
            { parms[6].Value = this.numAnoRef; }
            if (this.numMesRef != null)
            { parms[7].Value = this.numMesRef; }
            parms[8].Value = DBNull.Value;
            if (this.dtaAtendimento != null)
            { parms[8].Value = this.dtaAtendimento; }
            parms[9].Value = "";
            if (this.strTempoAtendimento != null)
            { parms[9].Value = this.strTempoAtendimento; }
            parms[10].Value = "";
            if (this.strObservacao != null)
            {parms[10].Value = this.strObservacao;}
            parms[11].Value = this.logUsuario;
            parms[11].Value = DBNull.Value;
            if (this.numTotAtBi != null)
            {parms[11].Value = this.numTotAtBi;}
            parms[12].Value = DBNull.Value;
            if (this.numSequencia != null)
            {parms[12].Value = this.numSequencia;}
            
            parms[13].Value = DBNull.Value;
            if (this.codFonte != null)
            {parms[13].Value = this.codFonte;}
            
            parms[14].Value = DBNull.Value;
            if (this.codPais != null)
            {parms[14].Value = this.codPais;}
            
            parms[15].Value = DBNull.Value;
            if (this.codEstado != null)
            {parms[15].Value = this.codEstado;}

            parms[16].Value = DBNull.Value;
            if (this.codCidade != null)
            {parms[16].Value = this.codCidade;}
            /*
            parms[17].Value = DBNull.Value;
            if (this.codSetor != null)
            {parms[17].Value = this.codSetor;}
            */
            parms[17].Value = this.logUsuario;
            if (this.numAtend < 0)
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
                        numAtend = Convert.ToInt32(cmd.Parameters[PARMnumAtend].Value);
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
                numAtend = Convert.ToInt32(cmd.Parameters[PARMnumAtend].Value);
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
        /// <param name="numAtend">Código do Registro</param>
        public static void Delete(int numAtend, int codUsuario, int codEscrSebrae)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4)
            };
            parms[0].Value = numAtend;
            parms[1].Value = codUsuario;
            parms[2].Value = codEscrSebrae;
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
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int numAtend, int codUsuario, int codEscrSebrae, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMnumAtend, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4) 
            };
                parms[0].Value = numAtend;
                parms[1].Value = codUsuario;
                parms[2].Value = codEscrSebrae;
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
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numAtend, int codUsuario, int codEscrSebrae)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codUsuario;
            param[2].Value = codEscrSebrae;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int numAtend, int codUsuario, int codEscrSebrae, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMnumAtend, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numAtend;
            param[1].Value = codUsuario;
            param[2].Value = codEscrSebrae;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>Atendimento</returns>
        public static Atendimento GetDataRow(int numAtend, int codUsuario, int codEscrSebrae)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codUsuario, codEscrSebrae);
            Atendimento Atendimento = new Atendimento();
            try
            {
                if (dr.Read())
                {
                    Atendimento.numAtend = Convert.ToInt32(dr["A001_num_atend"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        Atendimento.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        Atendimento.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));

                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        Atendimento.codCliente = new Cliente( Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"].ToString() != "")
                        Atendimento.numContato = new ContatoCliente( Convert.ToInt32(dr["A014_num_cont"]));
                    if (dr["A054_cd_tp_at"] != DBNull.Value && dr["A054_cd_tp_at"] != DBNull.Value && dr["A054_cd_tp_at"].ToString() != "")
                        Atendimento.codTipAtend = Convert.ToString(dr["A054_cd_tp_at"]);
                    if (dr["A001_ano_ref"] != DBNull.Value && dr["A001_ano_ref"] != DBNull.Value && dr["A001_ano_ref"].ToString() != "")
                        Atendimento.numAnoRef = Convert.ToInt32(dr["A001_ano_ref"]);
                    if (dr["A001_mes_ref"] != DBNull.Value && dr["A001_mes_ref"] != DBNull.Value && dr["A001_mes_ref"].ToString() != "")
                        Atendimento.numMesRef = Convert.ToInt32(dr["A001_mes_ref"]);
                    if (dr["A001_dt_atend"] != DBNull.Value && dr["A001_dt_atend"] != DBNull.Value && dr["A001_dt_atend"].ToString() != "")
                        Atendimento.dtaAtendimento = Convert.ToDateTime(dr["A001_dt_atend"]);
                    if (dr["A001_tem_atend"] != DBNull.Value && dr["A001_tem_atend"] != DBNull.Value && dr["A001_tem_atend"].ToString() != "")
                        Atendimento.strTempoAtendimento = Convert.ToString(dr["A001_tem_atend"]);
                    if (dr["A001_obs"] != DBNull.Value && dr["A001_obs"] != DBNull.Value && dr["A001_obs"].ToString() != "")
                        Atendimento.strObservacao = Convert.ToString(dr["A001_obs"]);
                    if (dr["A001_tot_at_BI"] != DBNull.Value && dr["A001_tot_at_BI"] != DBNull.Value && dr["A001_tot_at_BI"].ToString() != "")
                        Atendimento.numTotAtBi = Convert.ToInt32(dr["A001_tot_at_BI"]);
                    if (dr["A001_sequence"] != DBNull.Value && dr["A001_sequence"] != DBNull.Value && dr["A001_sequence"].ToString() != "")
                        Atendimento.numSequencia = Convert.ToInt32(dr["A001_sequence"]);
                    if (dr["A044_cd_fonte"] != DBNull.Value && dr["A044_cd_fonte"] != DBNull.Value && dr["A044_cd_fonte"].ToString() != "")
                        Atendimento.codFonte = new FonteInform( Convert.ToInt32(dr["A044_cd_fonte"]));
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        Atendimento.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        Atendimento.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        Atendimento.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A171_cd_setor"] != DBNull.Value && dr["A171_cd_setor"] != DBNull.Value && dr["A171_cd_setor"].ToString() != "")
                        Atendimento.codSetor = new Setorial(Convert.ToInt32(dr["A171_cd_setor"]));

                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Atendimento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Atendimento = new Atendimento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Atendimento;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Atendimento</returns>
        public static Atendimento GetDataRow(int numAtend, int codUsuario, int codEscrSebrae, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(numAtend, codUsuario, codEscrSebrae, trans);
            Atendimento Atendimento = new Atendimento();
            try
            {
                if (dr.Read())
                {
                    Atendimento.numAtend = Convert.ToInt32(dr["A001_num_atend"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        Atendimento.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"] != DBNull.Value && dr["A004_cd_escr"].ToString() != "")
                        Atendimento.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));

                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        Atendimento.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"] != DBNull.Value && dr["A014_num_cont"].ToString() != "")
                        Atendimento.numContato.NumContato = Convert.ToInt32(dr["A014_num_cont"]);
                    if (dr["A054_cd_tp_at"] != DBNull.Value && dr["A054_cd_tp_at"] != DBNull.Value && dr["A054_cd_tp_at"].ToString() != "")
                        Atendimento.codTipAtend = Convert.ToString(dr["A054_cd_tp_at"]);
                    if (dr["A001_ano_ref"] != DBNull.Value && dr["A001_ano_ref"] != DBNull.Value && dr["A001_ano_ref"].ToString() != "")
                        Atendimento.numAnoRef = Convert.ToInt32(dr["A001_ano_ref"]);
                    if (dr["A001_mes_ref"] != DBNull.Value && dr["A001_mes_ref"] != DBNull.Value && dr["A001_mes_ref"].ToString() != "")
                        Atendimento.numMesRef = Convert.ToInt32(dr["A001_mes_ref"]);
                    if (dr["A001_dt_atend"] != DBNull.Value && dr["A001_dt_atend"] != DBNull.Value && dr["A001_dt_atend"].ToString() != "")
                        Atendimento.dtaAtendimento = Convert.ToDateTime(dr["A001_dt_atend"]);
                    if (dr["A001_tem_atend"] != DBNull.Value && dr["A001_tem_atend"] != DBNull.Value && dr["A001_tem_atend"].ToString() != "")
                        Atendimento.strTempoAtendimento = Convert.ToString(dr["A001_tem_atend"]);
                    if (dr["A001_obs"] != DBNull.Value && dr["A001_obs"] != DBNull.Value && dr["A001_obs"].ToString() != "")
                        Atendimento.strObservacao = Convert.ToString(dr["A001_obs"]);
                    if (dr["A001_tot_at_BI"] != DBNull.Value && dr["A001_tot_at_BI"] != DBNull.Value && dr["A001_tot_at_BI"].ToString() != "")
                        Atendimento.numTotAtBi = Convert.ToInt32(dr["A001_tot_at_BI"]);
                    if (dr["A001_sequence"] != DBNull.Value && dr["A001_sequence"] != DBNull.Value && dr["A001_sequence"].ToString() != "")
                        Atendimento.numSequencia = Convert.ToInt32(dr["A001_sequence"]);
                    if ( dr["A044_cd_fonte"] != DBNull.Value && dr["A044_cd_fonte"].ToString() != "")
                        Atendimento.codFonte = new FonteInform(Convert.ToInt32(dr["A044_cd_fonte"]));
                    if ( dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        Atendimento.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if ( dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        Atendimento.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if ( dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        Atendimento.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if  (dr["A171_cd_setor"] != DBNull.Value && dr["A171_cd_setor"].ToString() != "")
                        Atendimento.codSetor = new Setorial(Convert.ToInt32(dr["A171_cd_setor"]));

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Atendimento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Atendimento = new Atendimento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Atendimento;
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

        #region LoadDataDrHistorico
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrHistorico(int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codCliente;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTHISTORICO, param);
            return dr;
        }
        #endregion

        #region LoadDataDrAssunto
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrAssunto(int numSequencial)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("numSequencial", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numSequencial;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, "Atendimento.AtendimentoAssuntoSelId", param);
            return dr;
        }
        #endregion

        #region loaddatadrmetricas
        public static OracleDataReader LoadDataDrMetricas(int numSequencial)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("numSequencial", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = numSequencial;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, "Atendimento.AtendimentoMetricasSelId", param);
            return dr;
        }
        #endregion

        #region LoadDataDrGrafico
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrGrafico()
        {
            OracleParameter[] param = new OracleParameter[] { 
 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTGRAFICO, param);
            return dr;
        }
        #endregion

        #region LoadDataDrGraficoCliente
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="numAtend">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrGraficoCliente(int mes, int ano)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("nAno", OracleType.Int32, 4),
                    new OracleParameter("nMes", OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = ano;
            param[1].Value = mes;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTGRAFICOCLI, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrRelatorio(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("sWhere", OracleType.VarChar),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTRELATORIO, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrRelProjetos(string sWhereAt, string sWhereEv)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("sWhereAT", OracleType.VarChar),
                    new OracleParameter("sWhereEV", OracleType.VarChar),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = sWhereAt;
            param[1].Value = sWhereEv;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTRELProjetos, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrGenerico(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("sWhere", OracleType.VarChar),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTGENERICO, param);
            return dr;
        }

        #endregion

        #endregion

    }
}