using System;
using System.Data.OracleClient; 
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T262_DAD_AD_CONT
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
    public class DadoAdicionalContato
    {
        #region Atributos
        private int codContato;
        private Estado codEstadoNatural;
        private Cidade codCidadeNatural;
        private Pais codPais;
        private Estado codEstado;
        private Cidade codCidade;
        private GrauEscolaridade codGrauEscolaridade;
        private string numRg;
        private string dscLogradouroEnd;
        private string dscBairroEnd;
        private int? cepEndereco;
        private string nomCursoGraduacao;
        private string nomPosGraduacao;
        private string numCpf;
        private DateTime? dtaNascimento;
        private string emailContato;
        private string tpoSexo;
        private Pais codPaisNatural;
        private string dscOrgaoRg;
        private Bairro codBairro;
        private Usuario codUsuario;
        private Rua codRua;
        private string numEndereco;
        private string nomComplementoEnd;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodContato
        {
            get { return codContato; }
            set { codContato = value; }
        }
        public Estado CodEstado
        {
            get { return codEstado; }
            set { codEstado = value; }
        }
        public string TpoSexo
        {
            get { return tpoSexo; }
            set { tpoSexo = value; }
        }
        public string DscLogradouroEnd
        {
            get { return dscLogradouroEnd; }
            set { dscLogradouroEnd = value; }
        }
        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public Estado CodEstadoNatural
        {
            get { return codEstadoNatural; }
            set { codEstadoNatural = value; }
        }
        public string EmailContato
        {
            get { return emailContato; }
            set { emailContato = value; }
        }
        public GrauEscolaridade CodGrauEscolaridade
        {
            get { return codGrauEscolaridade; }
            set { codGrauEscolaridade = value; }
        }
        public string NomCursoGraduacao
        {
            get { return nomCursoGraduacao; }
            set { nomCursoGraduacao = value; }
        }
        public string NomPosGraduacao
        {
            get { return nomPosGraduacao; }
            set { nomPosGraduacao = value; }
        }
        public Pais CodPaisNatural
        {
            get { return codPaisNatural; }
            set { codPaisNatural = value; }
        }
        public Cidade CodCidadeNatural
        {
            get { return codCidadeNatural; }
            set { codCidadeNatural = value; }
        }
        public DateTime? DtaNascimento
        {
            get { return dtaNascimento; }
            set { dtaNascimento = value; }
        }
        public string NumRg
        {
            get { return numRg; }
            set { numRg = value; }
        }
        public string DscOrgaoRg
        {
            get { return dscOrgaoRg; }
            set { dscOrgaoRg = value; }
        }
        public string NumCpf
        {
            get { return numCpf; }
            set { numCpf = value; }
        }
        public int? CepEndereco
        {
            get { return cepEndereco; }
            set { cepEndereco = value; }
        }        
        public string NumEndereco
        {
            get { return numEndereco; }
            set { numEndereco = value; }
        }
        public string DscBairroEnd
        {
            get { return dscBairroEnd; }
            set { dscBairroEnd = value; }
        }
        public string NomComplementoEnd
        {
            get { return nomComplementoEnd; }
            set { nomComplementoEnd = value; }
        }
        public Pais CodPais
        {
            get { return codPais; }
            set { codPais = value; }
        }
        public Cidade CodCidade
        {
            get { return codCidade; }
            set { codCidade = value; }
        }
        public Rua CodRua
        {
            get { return codRua; }
            set { codRua = value; }
        }
        public Bairro CodBairro
        {
            get { return codBairro; }
            set { codBairro = value; }
        }  
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "DadoAdicionalContato.DadoAdicionalContatoInc";
        private const string SPUPDATE = "DadoAdicionalContato.DadoAdicionalContatoAlt";
        private const string SPDELETE = "DadoAdicionalContato.DadoAdicionalContatoDel";
        private const string SPSELECTID = "DadoAdicionalContato.DadoAdicionalContatoSelId";
        private const string SPSELECTPAG = "DadoAdicionalContato.DadoAdicionalContatoSelPag";
        private const string SPSELECTBuscarCpf = "DadoAdicionalContato.BuscarCpf";
        private const string SPSELECTBuscarCliente = "DadoAdicionalContato.BuscarCliContato";
        private const string SPSELECTBuscaCPFSelPAG = "DadoAdicionalContato.BuscaCPFSelPag";
        private const string SPSELECTBuscarNome = "DadoAdicionalContato.BuscarNome";
        private const string SPSELECTBuscarLoginSenha = "DadoAdicionalContato.BuscarLoginSenha";
        private const string SPSELECTBuscarLogin = "DadoAdicionalContato.BuscarLogin";
        private const string SPSELECTBuscaNomeSelPAG = "DadoAdicionalContato.BuscaNomeSelPag";
        private const string SPSELECTBuscaNomeSelPAGCREDENCIADO = "DadoAdicionalContato.BuscaNomeCredSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codContato";
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32),
                    /*1*/ new OracleParameter( "tpoSexo", OracleType.VarChar),
                    /*2*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*3*/ new OracleParameter( "codEstadoNatural", OracleType.Int32),
                    /*4*/ new OracleParameter( "emailContato", OracleType.VarChar),
                    /*5*/ new OracleParameter( "codGrauEscolaridade", OracleType.Int32),                     
                    /*6*/ new OracleParameter( "nomCursoGraduacao", OracleType.VarChar),
                    /*7*/ new OracleParameter( "nomPosGraduacao", OracleType.VarChar),
                    /*8*/ new OracleParameter( "codPaisNatural", OracleType.Int32),
                    /*9*/ new OracleParameter( "codCidadeNatural", OracleType.Int32),
                    /*10*/ new OracleParameter( "dtaNascimento", OracleType.DateTime),
                    /*11*/ new OracleParameter( "numRg", OracleType.VarChar),
                    /*12*/ new OracleParameter( "dscOrgaoRg", OracleType.VarChar),
                    /*13*/ new OracleParameter( "numCpf", OracleType.VarChar),
                    /*14*/ new OracleParameter( "cepEndereco", OracleType.Int32),
                    /*15*/ new OracleParameter( "dscLogradouroEnd", OracleType.VarChar),
                    /*16*/ new OracleParameter( "numEndereco", OracleType.VarChar),
                    /*17*/ new OracleParameter( "dscBairroEnd", OracleType.VarChar),
                    /*18*/ new OracleParameter( "nomComplementoEnd", OracleType.VarChar),
                    /*19*/ new OracleParameter( "codPais", OracleType.Int32),
                    /*20*/ new OracleParameter( "codCidade", OracleType.Int32),
                    /*21*/ new OracleParameter( "codRua", OracleType.Int32),
                    /*22*/ new OracleParameter( "codEstado", OracleType.Int32),
                    /*23*/ new OracleParameter( "codBairro", OracleType.Int32),
                    /*24*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[1].Value = "";
            parms[2].Value = DBNull.Value;
            parms[3].Value = DBNull.Value;
            parms[4].Value = "";
            parms[5].Value = DBNull.Value;
            parms[6].Value = "";
            parms[7].Value = "";
            parms[8].Value = DBNull.Value;
            parms[9].Value = DBNull.Value;
            parms[10].Value = DBNull.Value;
            parms[11].Value = "";
            parms[12].Value = "";
            parms[13].Value = "";
            parms[14].Value = DBNull.Value;
            parms[15].Value = "";
            parms[16].Value = "";
            parms[17].Value = "";
            parms[18].Value = "";
            parms[19].Value = DBNull.Value;
            parms[20].Value = DBNull.Value;
            parms[21].Value = DBNull.Value;
            parms[22].Value = DBNull.Value;
            parms[23].Value = DBNull.Value;
            parms[24].Value = "";


            parms[0].Value = this.codContato;
            if (this.tpoSexo != null)
                parms[1].Value = this.tpoSexo.ToUpper();

            if (this.codUsuario != null)
                parms[2].Value = this.codUsuario.CodUsuario;

            if (this.codEstadoNatural != null)
                parms[3].Value = this.codEstadoNatural.CodEstado;

            if (this.emailContato != null)
                parms[4].Value = this.emailContato.ToLower();

            if (this.codGrauEscolaridade != null)
                parms[5].Value = this.codGrauEscolaridade.CodGrauEscolaridade;

            if (this.nomCursoGraduacao != null)
                parms[6].Value = this.nomCursoGraduacao.ToUpper();

            if (this.nomPosGraduacao != null)
                parms[7].Value = this.nomPosGraduacao.ToUpper();

            if (this.codPaisNatural != null)
                parms[8].Value = this.codPaisNatural.CodPais;

            if (this.codCidadeNatural != null)
                parms[9].Value = this.codCidadeNatural.CodCidade;

            if (this.dtaNascimento != null)
                parms[10].Value = this.dtaNascimento;

            if (this.numRg != null)
                parms[11].Value = this.numRg.ToUpper();

            if (this.dscOrgaoRg != null)
                parms[12].Value = this.dscOrgaoRg.ToUpper();

            if (this.numCpf != null)
                parms[13].Value = this.numCpf.ToUpper();

            if (this.cepEndereco != null)
                parms[14].Value = this.cepEndereco;

            if (this.dscLogradouroEnd != null)
                parms[15].Value = this.dscLogradouroEnd.ToUpper();

            if (this.numEndereco != null)
                parms[16].Value = this.numEndereco.ToUpper();

            if (this.dscBairroEnd != null)
                parms[17].Value = this.dscBairroEnd.ToUpper();

            if (this.nomComplementoEnd != null)
                parms[18].Value = this.nomComplementoEnd.ToUpper();

            if (this.codPais != null)
                parms[19].Value = this.codPais.CodPais;

            if (this.codCidade != null)
                parms[20].Value = this.codCidade.CodCidade;

            if (this.codRua != null)
                parms[21].Value = this.codRua.CodRua;

            if (this.codEstado != null)
                parms[22].Value = this.codEstado.CodEstado;

            if (this.codBairro != null)
                parms[23].Value = this.codBairro.CodBairro;

            parms[24].Value = this.logUsuario.ToUpper();


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

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codContato = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                CodContato = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
            parms[0].Value = codigo;
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
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
                parms[0].Value = codigo;
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
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>dac</returns>
        public static DadoAdicionalContato GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            DadoAdicionalContato dac = new DadoAdicionalContato();
            try
            {
                if (dr.Read())
                {
                    dac.codContato = Convert.ToInt32(dr["A261_cd_cont"]);
                    if (dr["A021_cd_est_nat"] != DBNull.Value && dr["A021_cd_est_nat"] != DBNull.Value && dr["A021_cd_est_nat"].ToString() != "")
                        dac.codEstadoNatural = new Estado(Convert.ToInt32(dr["A021_cd_est_nat"]));
                    if (dr["A011_cd_cid_nat"] != DBNull.Value && dr["A011_cd_cid_nat"] != DBNull.Value && dr["A011_cd_cid_nat"].ToString() != "")
                        dac.codCidadeNatural = new Cidade(Convert.ToInt32(dr["A011_cd_cid_nat"]));
                    if (dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"].ToString() != "")
                        dac.codPais = new Pais(Convert.ToInt32(dr["A035_cd_Pais"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        dac.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        dac.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A263_cd_esc"] != DBNull.Value && dr["A263_cd_esc"] != DBNull.Value && dr["A263_cd_esc"].ToString() != "")
                        dac.codGrauEscolaridade = new GrauEscolaridade(Convert.ToInt32(dr["A263_cd_esc"]));
                    dac.numRg = Convert.ToString(dr["A262_num_rg"]);
                    dac.dscLogradouroEnd = Convert.ToString(dr["A262_end_res"]);
                    dac.dscBairroEnd = Convert.ToString(dr["A262_bairro"]);
                    if (dr["A262_cep"] != DBNull.Value && dr["A262_cep"] != DBNull.Value && dr["A262_cep"].ToString() != "")
                        dac.cepEndereco = Convert.ToInt32(dr["A262_cep"]);
                    dac.nomCursoGraduacao = Convert.ToString(dr["A262_curso_grad"]);
                    dac.nomCursoGraduacao = Convert.ToString(dr["A262_curso_pos_grad"]);

                    if (dr["A262_num_cpf"] != DBNull.Value && dr["A262_num_cpf"] != DBNull.Value && dr["A262_num_cpf"].ToString() != "")
                        dac.numCpf = Convert.ToString(dr["A262_num_cpf"]);

                    if (dr["A262_dt_nasc"] != DBNull.Value && dr["A262_dt_nasc"] != DBNull.Value && dr["A262_dt_nasc"].ToString() != "")
                        dac.dtaNascimento = Convert.ToDateTime(dr["A262_dt_nasc"]);

                    dac.emailContato = Convert.ToString(dr["A262_email"]).ToLower();
                    dac.tpoSexo = Convert.ToString(dr["A262_sexo"]);
                    if (dr["A035_cd_Pais_nat"] != DBNull.Value && dr["A035_cd_Pais_nat"] != DBNull.Value && dr["A035_cd_Pais_nat"].ToString() != "")
                        dac.codPaisNatural = new Pais(Convert.ToInt32(dr["A035_cd_Pais_nat"]));
                    dac.dscOrgaoRg = Convert.ToString(dr["A262_org_exp_rg"]);
                    if (dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"].ToString() != "")
                        dac.codBairro = new Bairro(Convert.ToInt32(dr["A616_cd_bairro"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        dac.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"].ToString() != "")
                        dac.codRua = new Rua(Convert.ToInt32(dr["A615_cd_rua"]));
                    dac.numEndereco = Convert.ToString(dr["A262_nmr_end"]);
                    dac.nomComplementoEnd = Convert.ToString(dr["A262_comp_end"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        dac.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                dac = new DadoAdicionalContato();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return dac;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>dac</returns>
        public static DadoAdicionalContato GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            DadoAdicionalContato dac = new DadoAdicionalContato();
            try
            {
                if (dr.Read())
                {
                    dac.codContato = Convert.ToInt32(dr["A261_cd_cont"]);
                    if (dr["A021_cd_est_nat"] != DBNull.Value && dr["A021_cd_est_nat"] != DBNull.Value && dr["A021_cd_est_nat"].ToString() != "")
                        dac.codEstadoNatural = new Estado(Convert.ToInt32(dr["A021_cd_est_nat"]));
                    if (dr["A011_cd_cid_nat"] != DBNull.Value && dr["A011_cd_cid_nat"] != DBNull.Value && dr["A011_cd_cid_nat"].ToString() != "")
                        dac.codCidadeNatural = new Cidade(Convert.ToInt32(dr["A011_cd_cid_nat"]));
                    if (dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"] != DBNull.Value && dr["A035_cd_Pais"].ToString() != "")
                        dac.codPais = new Pais(Convert.ToInt32(dr["A035_cd_Pais"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        dac.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        dac.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A263_cd_esc"] != DBNull.Value && dr["A263_cd_esc"] != DBNull.Value && dr["A263_cd_esc"].ToString() != "")
                        dac.codGrauEscolaridade = new GrauEscolaridade(Convert.ToInt32(dr["A263_cd_esc"]));
                    dac.numRg = Convert.ToString(dr["A262_num_rg"]);
                    dac.dscLogradouroEnd = Convert.ToString(dr["A262_end_res"]);
                    dac.dscBairroEnd = Convert.ToString(dr["A262_bairro"]);
                    if (dr["A262_cep"] != DBNull.Value && dr["A262_cep"] != DBNull.Value && dr["A262_cep"].ToString() != "")
                        dac.cepEndereco = Convert.ToInt32(dr["A262_cep"]);
                    dac.nomCursoGraduacao = Convert.ToString(dr["A262_curso_grad"]);
                    dac.nomCursoGraduacao = Convert.ToString(dr["A262_curso_pos_grad"]);

                    if (dr["A262_num_cpf"] != DBNull.Value && dr["A262_num_cpf"] != DBNull.Value && dr["A262_num_cpf"].ToString() != "")
                        dac.numCpf = Convert.ToString(dr["A262_num_cpf"]);

                    if (dr["A262_dt_nasc"] != DBNull.Value && dr["A262_dt_nasc"] != DBNull.Value && dr["A262_dt_nasc"].ToString() != "")
                        dac.dtaNascimento = Convert.ToDateTime(dr["A262_dt_nasc"]);
                    dac.emailContato = Convert.ToString(dr["A262_email"]).ToLower();
                    dac.tpoSexo = Convert.ToString(dr["A262_sexo"]);
                    if (dr["A035_cd_Pais_nat"] != DBNull.Value && dr["A035_cd_Pais_nat"] != DBNull.Value && dr["A035_cd_Pais_nat"].ToString() != "")
                        dac.codPaisNatural = new Pais(Convert.ToInt32(dr["A035_cd_Pais_nat"]));
                    dac.dscOrgaoRg = Convert.ToString(dr["A262_org_exp_rg"]);
                    if (dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"].ToString() != "")
                        dac.codBairro = new Bairro(Convert.ToInt32(dr["A616_cd_bairro"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        dac.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"].ToString() != "")
                        dac.codRua = new Rua(Convert.ToInt32(dr["A615_cd_rua"]));
                    dac.numEndereco = Convert.ToString(dr["A262_nmr_end"]);
                    dac.nomComplementoEnd = Convert.ToString(dr["A262_comp_end"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        dac.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                dac = new DadoAdicionalContato();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return dac;
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
                new OracleParameter("curDadoAdicionalContato", OracleType.Cursor),
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

        #region Métodos Especificos

        #region LoadDataDrBuscarLogin
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrBuscarLogin(string Login)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codLogin", OracleType.VarChar), 
                    new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
                };

            param[0].Value = Login;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarLogin, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrSPBuscarLogin(string Login, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter("codLogin", OracleType.VarChar),
                new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
};
            param[0].Value = Login;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTBuscarLogin, param);
            return dr;
        }

        #endregion

        #region LoadDataDrBuscarLoginSenha
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrBuscarLoginSenha(string LoginSenha, string Senha)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codLogin", OracleType.VarChar), 
                    new OracleParameter("codSenha", OracleType.VarChar), 
                    new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
                };

            param[0].Value = LoginSenha;
            param[1].Value = Senha;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarLoginSenha, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrSPBuscarLoginSenha(string LoginSenha, string Senha, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter("codLogin", OracleType.VarChar),
                new OracleParameter("codSenha", OracleType.VarChar),
                new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
};
            param[0].Value = LoginSenha;
            param[1].Value = Senha;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTBuscarLoginSenha, param);
            return dr;
        }

        #endregion

        #region LoadDataDrBuscarCpf
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrBuscarCpf(string Cpf)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codCpf", OracleType.VarChar), 
                    new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
                };

            param[0].Value = Cpf;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarCpf, param);
            return dr;
        } 

        #endregion

        #region LoadDataDrBuscarCodCliente
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrBuscarCodCliente(string codCli)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codA012", OracleType.VarChar), 
                    new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
                };

            param[0].Value = codCli;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarCliente, param);
            return dr;
        }
 

        #endregion
                
        #region LoadDataBuscaCpfPaginacao
        /// <summary>
        /// Passagem de cpf para agrupamento do banco
        /// </summary>
        /// <param name="Where"></param>
        /// <returns></returns> 
        public static Context.Paginacao LoadDataBuscaCpfPaginacao(string Where)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000), 
                new OracleParameter("curBusca", OracleType.Cursor), 
              };

            parms[0].Value = Where;
            parms[1].Direction = ParameterDirection.Output;


            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscaCPFSelPAG, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            //paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion

        #region LoadDataDrBuscarNome
        /// <summary>
        /// Encontra cod chave pelo nome
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrBuscarNome(string Nome)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("nomCli", OracleType.VarChar), 
                    new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
                };

            param[0].Value = Nome.ToUpper();
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarNome, param);
            return dr;
        }

        /// <summary>
        /// Encontra cod chave pelo nome para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrBuscarNome(string Nome, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter("nomCli", OracleType.VarChar),
                new OracleParameter("curDadoAdicionalContato", OracleType.Cursor)
};
            param[0].Value = Nome.ToUpper();
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTBuscarNome, param);
            return dr;
        }
        #endregion

        #region LoadDataBuscaNomePaginacao
        /// <summary>
        /// Metodo para completar nomes
        /// </summary>
        /// <param name="Where"></param>
        /// <returns></returns>
        public static Context.Paginacao LoadDataBuscaNomePaginacao(string Where)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000), 
                new OracleParameter("curBusca", OracleType.Cursor), 
              };

            parms[0].Value = Where;
            parms[1].Direction = ParameterDirection.Output;


            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscaNomeSelPAG, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            //paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion

        #region LoadDataBuscaNomePagCredenciado
        /// <summary>
        /// Metodo para completar nomes
        /// </summary>
        /// <param name="Where"></param>
        /// <returns></returns>
        public static Context.Paginacao LoadDataBuscaNomePagCredenciado(string Where)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000), 
                new OracleParameter("curBusca", OracleType.Cursor), 
              };

            parms[0].Value = Where;
            parms[1].Direction = ParameterDirection.Output;


            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscaNomeSelPAGCREDENCIADO, parms);


            paginacao.DataReader = dr; 
            return paginacao;
        }
        #endregion 
 
        
        #region LoadDataDrBuscarNome
        /// <summary>
        /// passa nome completo do cli e retorna cpf 
        /// para preencher a tela no carregar valores
        /// </summary>
        /// <param name="Nome"></param>
        /// <returns></returns>
        public static string GetCpfPorNomeCli(string Nome)
        {
            string cpf = "";
            OracleDataReader dr = DadoAdicionalContato.LoadDataDrBuscarNome(Nome);
            if (dr.Read())
            {
                ClienteIntegral clint;
                ClienteParcial cliPar;
                DadoAdicionalContato dad;

                int CodCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                string tipo = dr["Tipo"].ToString();
                int CodContato = Convert.ToInt32(dr["A261_cd_cont"]);


                if (tipo == "C" || tipo == "J")
                {
                    //clint = ClienteIntegral.GetDataRow(CodCliente);
                    dad = DadoAdicionalContato.GetDataRow(CodContato);
                    cpf = dad.NumCpf;
                }
                else if (tipo == "P")
                {
                    cliPar = ClienteParcial.GetDataRow(CodCliente);
                    if (cliPar.NumCpfCNPJ != null)
                        cpf = cliPar.NumCpfCNPJ;
                }
            }
            dr.Close();
            return cpf;
        }

        #endregion

        #region GetCodPorNomeCli
        /// <summary>
        /// passa nome completo do cli e retorna CodCliente 
        /// para preencher a tela no carregar valores
        /// </summary>
        /// <param name="Nome"></param>
        /// <returns>Codigo Cliente</returns>
        public static int GetCodPorNomeCli(string Nome)
        {
            int cod = 0;
            OracleDataReader dr = DadoAdicionalContato.LoadDataDrBuscarNome(Nome);
            if (dr.Read())
            {
                cod = Convert.ToInt32(dr["A012_cd_cli"]);
            }
            dr.Close();
            return cod;
        }

        #endregion
        #endregion

    }
}
