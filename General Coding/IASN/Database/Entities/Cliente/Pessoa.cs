using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T572_Pessoa
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
    public class Pessoa
    {
        #region Atributos
        private int codPessoa;
        private int codUsuario;
        private EstadoCivil codEstCiv;
        private string nomePes;
        private string sexo;
        private DateTime? dataNasc;
        private Pais codPais;
        private Estado codEstado;
        private Cidade cidade;
        private string endereco;
        private string bairro;
        private int? cep;
        private string ddd;
        private int? fone;
        private string rg;
        private string orgExpRG;
        private DateTime? dataEmisRG;
        private string cpf;
        private string email;
        private int? fax;
        private int? codPaisNasc;
        private int indAtivo;
        private int tipoPessoa;
        private int? vip;
        private Bairro codBairro;
        private Rua codRua;
        private string numEndereco;
        private string compEndereco;
        private int? indTeleAtendente;
        private string instCons;
        private int? celular;
        private string traineEnergia;
        private string codSebraeNA;
        private int? indAlteraSinco;
        private string logUsuario;
        #endregion

        #region Propriedades
        public Estado CodEstado
        {
            get { return codEstado; }
            set { codEstado = value; }
        }
        public Pais CodPais
        {
            get { return codPais; }
            set { codPais = value; }
        }
        public int CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public int CodPessoa
        {
            get { return codPessoa; }
            set { codPessoa = value; }
        }
        public EstadoCivil CodEstCiv
        {
            get { return codEstCiv; }
            set { codEstCiv = value; }
        }
        public string NomePes
        {
            get { return nomePes; }
            set { nomePes = value; }
        }
        public string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }
        public DateTime? DataNasc
        {
            get { return dataNasc; }
            set { dataNasc = value; }
        }
        public Cidade Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }
        public string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }
        public string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }
        public int? Cep
        {
            get { return cep; }
            set { cep = value; }
        }
        public string Ddd
        {
            get { return ddd; }
            set { ddd = value; }
        }
        public int? Fone
        {
            get { return fone; }
            set { fone = value; }
        }
        public string Rg
        {
            get { return rg; }
            set { rg = value; }
        }
        public string OrgExpRG
        {
            get { return orgExpRG; }
            set { orgExpRG = value; }
        }
        public DateTime? DataEmisRG
        {
            get { return dataEmisRG; }
            set { dataEmisRG = value; }
        }
        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }        
        public int? CodPaisNasc
        {
            get { return codPaisNasc; }
            set { codPaisNasc = value; }
        }
        public int IndAtivo
        {
            get { return indAtivo; }
            set { indAtivo = value; }
        }
        public int TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        public int? Vip
        {
            get { return vip; }
            set { vip = value; }
        }
        public string CompEndereco
        {
            get { return compEndereco; }
            set { compEndereco = value; }
        }        
        public string NumEndereco
        {
            get { return numEndereco; }
            set { numEndereco = value; }
        }
        public string InstCons
        {
            get { return instCons; }
            set { instCons = value; }
        }
        public int? IndTeleAtendente
        {
            get { return indTeleAtendente; }
            set { indTeleAtendente = value; }
        }
        public string TraineEnergia
        {
            get { return traineEnergia; }
            set { traineEnergia = value; }
        }
        public string CodSebraeNA
        {
            get { return codSebraeNA; }
            set { codSebraeNA = value; }
        }
        public int? IndAlteraSinco
        {
            get { return indAlteraSinco; }
            set { indAlteraSinco = value; }
        }        
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion
        
        #region Construtores
        public Pessoa()
            : this(-1)
        { }
        public Pessoa(int codPessoa)
        {
            this.codPessoa = codPessoa;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Pessoa.PessoaInc";
        private const string SPUPDATE = "Pessoa.PessoaAlt";
        private const string SPDELETE = "Pessoa.PessoaDel";
        private const string SPSELECTID = "Pessoa.PessoaSelId";
        private const string SPSELECTPAG = "Pessoa.PessoaSelPag";
        private const string SPSELECTPAGCRE = "Pessoa.PessoaSelPagCRE";
        private const string SPSELECTPESSOAID = "Pessoa.PessoaCombo";
        private const string SPSELECTPESSOAGES = "Pessoa.PessoaGestor";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codPessoa";
        private const string PARMCURSOR = "curPessoa";
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*2*/ new OracleParameter( "codEstCiv", OracleType.Int32),
                    /*3*/ new OracleParameter( "nomePes", OracleType.VarChar),
                    /*4*/ new OracleParameter( "sexo", OracleType.VarChar),
                    /*5*/ new OracleParameter( "dataNasc", OracleType.DateTime),
                    /*6*/ new OracleParameter( "codPais", OracleType.Int32),
                    /*7*/ new OracleParameter( "codEstado", OracleType.Int32),
                    /*8*/ new OracleParameter( "cidade", OracleType.VarChar),
                    /*9*/ new OracleParameter( "endereco", OracleType.VarChar),
                    /*10*/ new OracleParameter( "bairro", OracleType.VarChar),
                    /*11*/ new OracleParameter( "cep", OracleType.Int32),
                    /*12*/ new OracleParameter( "ddd", OracleType.VarChar),
                    /*13*/ new OracleParameter( "fone", OracleType.Int32),
                    /*14*/ new OracleParameter( "rg", OracleType.VarChar),                     
                    /*15*/ new OracleParameter( "orgExpRG", OracleType.VarChar),
                    /*16*/ new OracleParameter( "dataEmisRG", OracleType.DateTime),
                    /*17*/ new OracleParameter( "cpf", OracleType.VarChar),
                    /*18*/ new OracleParameter( "email", OracleType.VarChar),
                    /*19*/ new OracleParameter( "fax", OracleType.Int32),
                    /*20*/ new OracleParameter( "codPaisNasc", OracleType.Int32),
                    /*21*/ new OracleParameter( "indAtivo", OracleType.Int32),
                    /*22*/ new OracleParameter( "tipoPessoa", OracleType.Int32),
                    /*23*/ new OracleParameter( "vip", OracleType.Int32),
                    /*24*/ new OracleParameter( "codBairro", OracleType.Int32),
                    /*25*/ new OracleParameter( "codRua", OracleType.Int32),
                    /*26*/ new OracleParameter( "numEndereco", OracleType.VarChar),
                    /*27*/ new OracleParameter( "compEndereco", OracleType.VarChar),
                    /*28*/ new OracleParameter( "indTeleAtendente", OracleType.Int32),
                    /*29*/ new OracleParameter( "instCons", OracleType.VarChar),
                    /*30*/ new OracleParameter( "celular", OracleType.Int32),
                    /*31*/ new OracleParameter( "traineEnergia", OracleType.VarChar),
                    /*32*/ new OracleParameter( "codSebraeNA", OracleType.VarChar),
                    /*33*/ new OracleParameter( "indAlteraSinco", OracleType.Int32),
                    /*34*/ new OracleParameter( "usuarioAlt", OracleType.VarChar)
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
            parms[0].Value = this.codPessoa;
            parms[1].Value = this.codUsuario;
            parms[2].Value = this.codEstCiv;
            parms[3].Value = this.nomePes;
            parms[4].Value = this.sexo;
            parms[5].Value = this.dataNasc;
            parms[6].Value = this.codPais;
            parms[7].Value = this.codEstado;
            parms[8].Value = this.cidade;
            parms[9].Value = this.endereco;
            parms[10].Value = this.bairro;
            parms[11].Value = this.cep;
            parms[12].Value = this.ddd;
            parms[13].Value = this.fone;
            parms[14].Value = this.rg;
            parms[15].Value = this.orgExpRG;
            parms[16].Value = this.dataEmisRG;
            parms[17].Value = this.cpf;
            parms[18].Value = this.email;
            parms[19].Value = this.fax;
            parms[20].Value = this.codPaisNasc;
            parms[21].Value = this.indAtivo;
            parms[22].Value = this.tipoPessoa;
            parms[23].Value = this.vip;
            parms[24].Value = this.codBairro;
            parms[25].Value = this.codRua;
            parms[26].Value = this.numEndereco;
            parms[27].Value = this.compEndereco;
            parms[28].Value = this.indTeleAtendente;
            parms[29].Value = this.instCons;
            parms[30].Value = this.celular;
            parms[31].Value = this.traineEnergia;
            parms[32].Value = this.codSebraeNA;
            parms[33].Value = this.indAlteraSinco;
            parms[34].Value = this.logUsuario;
            if (this.codPessoa < 0)
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
                        codPessoa = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codPessoa = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
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
                                                                  new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlPessoaCombo
        public static OracleDataReader LoadDataDdlPessoaCombo()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPESSOAID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Pessoa</returns>
        public static Pessoa GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Pessoa pessoa = new Pessoa();
            try
            {
                if (dr.Read())
                {
                    pessoa.codPessoa = Convert.ToInt32(dr["A572_cd_pes"]);
                    pessoa.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A598_cd_est_civ"] != DBNull.Value && dr["A598_cd_est_civ"] != DBNull.Value && dr["A598_cd_est_civ"].ToString() != "")
                        pessoa.codEstCiv = new EstadoCivil(Convert.ToInt32(dr["A598_cd_est_civ"]));
                    pessoa.nomePes = Convert.ToString(dr["A572_nome"]);
                    pessoa.sexo = Convert.ToString(dr["A572_sexo"]);
                    if (dr["A572_dt_nasc"] != DBNull.Value && dr["A572_dt_nasc"] != DBNull.Value && dr["A572_dt_nasc"].ToString() != "")
                        pessoa.dataNasc = Convert.ToDateTime(dr["A572_dt_nasc"]);
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        pessoa.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        pessoa.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        pessoa.cidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    pessoa.endereco = Convert.ToString(dr["A572_endereco"]);
                    pessoa.bairro = Convert.ToString(dr["A572_bairro"]);
                    if (dr["A572_cep"] != DBNull.Value && dr["A572_cep"] != DBNull.Value && dr["A572_cep"].ToString() != "")
                        pessoa.cep = Convert.ToInt32(dr["A572_cep"]);
                    pessoa.ddd = Convert.ToString(dr["A572_ddd"]);
                    if (dr["A572_fone"] != DBNull.Value && dr["A572_fone"] != DBNull.Value && dr["A572_fone"].ToString() != "")
                        pessoa.fone = Convert.ToInt32(dr["A572_fone"]);
                    pessoa.rg = Convert.ToString(dr["A572_rg"]);
                    pessoa.orgExpRG = Convert.ToString(dr["A572_org_exp_rg"]);
                    if (dr["A572_dt_emis_rg"] != DBNull.Value && dr["A572_dt_emis_rg"] != DBNull.Value && dr["A572_dt_emis_rg"].ToString() != "")
                        pessoa.dataEmisRG = Convert.ToDateTime(dr["A572_dt_emis_rg"]);
                    pessoa.cpf = Convert.ToString(dr["A572_cpf"]);
                    pessoa.email = Convert.ToString(dr["A572_e_mail"]);
                    if (dr["A572_fax"] != DBNull.Value && dr["A572_fax"] != DBNull.Value && dr["A572_fax"].ToString() != "")
                        pessoa.fax = Convert.ToInt32(dr["A572_fax"]);
                    if (dr["A035_cd_pais_nac"] != DBNull.Value && dr["A035_cd_pais_nac"] != DBNull.Value && dr["A035_cd_pais_nac"].ToString() != "")
                        pessoa.codPaisNasc = Convert.ToInt32(dr["A035_cd_pais_nac"]);
                    pessoa.indAtivo = Convert.ToInt32(dr["A572_ind_ativo"]);
                    pessoa.tipoPessoa = Convert.ToInt32(dr["A572_tipo_Pessoa"]);
                    if (dr["A572_vip"] != DBNull.Value && dr["A572_vip"] != DBNull.Value && dr["A572_vip"].ToString() != "")
                        pessoa.vip = Convert.ToInt32(dr["A572_vip"]);
                    if (dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"].ToString() != "")
                        pessoa.codBairro = new Bairro(Convert.ToInt32(dr["A616_cd_bairro"]));
                    if (dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"].ToString() != "")
                        pessoa.codRua = new Rua(Convert.ToInt32(dr["A615_cd_rua"]));
                    pessoa.numEndereco = Convert.ToString(dr["A572_nmr_end"]);
                    pessoa.compEndereco = Convert.ToString(dr["A572_comp_end"]);
                    if (dr["A572_ind_teleatendente"] != DBNull.Value && dr["A572_ind_teleatendente"] != DBNull.Value && dr["A572_ind_teleatendente"].ToString() != "")
                        pessoa.indTeleAtendente = Convert.ToInt32(dr["A572_ind_teleatendente"]);
                    pessoa.instCons = Convert.ToString(dr["A572_inst_cons"]);
                    if (dr["A572_celular"] != DBNull.Value && dr["A572_celular"] != DBNull.Value && dr["A572_celular"].ToString() != "")
                        pessoa.celular = Convert.ToInt32(dr["A572_celular"]);
                    pessoa.traineEnergia = Convert.ToString(dr["A572_traine_energia"]);
                    pessoa.codSebraeNA = Convert.ToString(dr["A012_cd_sebraena"]);
                    if (dr["A572_ind_altera_sinco"] != DBNull.Value && dr["A572_ind_altera_sinco"] != DBNull.Value && dr["A572_ind_altera_sinco"].ToString() != "")
                        pessoa.indAlteraSinco = Convert.ToInt32(dr["A572_ind_altera_sinco"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        pessoa.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pessoa = new Pessoa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return pessoa;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Pessoa</returns>
        public static Pessoa GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Pessoa pessoa = new Pessoa();
            try
            {
                if (dr.Read())
                {
                    pessoa.codPessoa = Convert.ToInt32(dr["A572_cd_pes"]);
                    pessoa.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["A598_cd_est_civ"] != DBNull.Value && dr["A598_cd_est_civ"] != DBNull.Value && dr["A598_cd_est_civ"].ToString() != "")
                        pessoa.codEstCiv = new EstadoCivil(Convert.ToInt32(dr["A598_cd_est_civ"]));
                    pessoa.nomePes = Convert.ToString(dr["A572_nome"]);
                    pessoa.sexo = Convert.ToString(dr["A572_sexo"]);
                    if (dr["A572_dt_nasc"] != DBNull.Value && dr["A572_dt_nasc"] != DBNull.Value && dr["A572_dt_nasc"].ToString() != "")
                        pessoa.dataNasc = Convert.ToDateTime(dr["A572_dt_nasc"]);
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        pessoa.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        pessoa.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        pessoa.cidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    pessoa.endereco = Convert.ToString(dr["A572_endereco"]);
                    pessoa.bairro = Convert.ToString(dr["A572_bairro"]);
                    if (dr["A572_cep"] != DBNull.Value && dr["A572_cep"] != DBNull.Value && dr["A572_cep"].ToString() != "")
                        pessoa.cep = Convert.ToInt32(dr["A572_cep"]);
                    pessoa.ddd = Convert.ToString(dr["A572_ddd"]);
                    if (dr["A572_fone"] != DBNull.Value && dr["A572_fone"] != DBNull.Value && dr["A572_fone"].ToString() != "")
                        pessoa.fone = Convert.ToInt32(dr["A572_fone"]);
                    pessoa.rg = Convert.ToString(dr["A572_rg"]);
                    pessoa.orgExpRG = Convert.ToString(dr["A572_org_exp_rg"]);
                    if (dr["A572_dt_emis_rg"] != DBNull.Value && dr["A572_dt_emis_rg"] != DBNull.Value && dr["A572_dt_emis_rg"].ToString() != "")
                        pessoa.dataEmisRG = Convert.ToDateTime(dr["A572_dt_emis_rg"]);
                    pessoa.cpf = Convert.ToString(dr["A572_cpf"]);
                    pessoa.email = Convert.ToString(dr["A572_e_mail"]);
                    if (dr["A572_fax"] != DBNull.Value && dr["A572_fax"] != DBNull.Value && dr["A572_fax"].ToString() != "")
                        pessoa.fax = Convert.ToInt32(dr["A572_fax"]);
                    if (dr["A035_cd_pais_nac"] != DBNull.Value && dr["A035_cd_pais_nac"] != DBNull.Value && dr["A035_cd_pais_nac"].ToString() != "")
                        pessoa.codPaisNasc = Convert.ToInt32(dr["A035_cd_pais_nac"]);
                    pessoa.indAtivo = Convert.ToInt32(dr["A572_ind_ativo"]);
                    pessoa.tipoPessoa = Convert.ToInt32(dr["A572_tipo_Pessoa"]);
                    if (dr["A572_vip"] != DBNull.Value && dr["A572_vip"] != DBNull.Value && dr["A572_vip"].ToString() != "")
                        pessoa.vip = Convert.ToInt32(dr["A572_vip"]);
                    if (dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"].ToString() != "")
                        pessoa.codBairro = new Bairro(Convert.ToInt32(dr["A616_cd_bairro"]));
                    if (dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"].ToString() != "")
                        pessoa.codRua = new Rua(Convert.ToInt32(dr["A615_cd_rua"]));
                    pessoa.numEndereco = Convert.ToString(dr["A572_nmr_end"]);
                    pessoa.compEndereco = Convert.ToString(dr["A572_comp_end"]);
                    if (dr["A572_ind_teleatendente"] != DBNull.Value && dr["A572_ind_teleatendente"] != DBNull.Value && dr["A572_ind_teleatendente"].ToString() != "")
                        pessoa.indTeleAtendente = Convert.ToInt32(dr["A572_ind_teleatendente"]);
                    pessoa.instCons = Convert.ToString(dr["A572_inst_cons"]);
                    if (dr["A572_celular"] != DBNull.Value && dr["A572_celular"] != DBNull.Value && dr["A572_celular"].ToString() != "")
                        pessoa.celular = Convert.ToInt32(dr["A572_celular"]);
                    pessoa.traineEnergia = Convert.ToString(dr["A572_traine_energia"]);
                    pessoa.codSebraeNA = Convert.ToString(dr["A012_cd_sebraena"]);
                    if (dr["A572_ind_altera_sinco"] != DBNull.Value && dr["A572_ind_altera_sinco"] != DBNull.Value && dr["A572_ind_altera_sinco"].ToString() != "")
                        pessoa.indAlteraSinco = Convert.ToInt32(dr["A572_ind_altera_sinco"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        pessoa.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pessoa = new Pessoa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return pessoa;
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

        public static Context.Paginacao LoadDataPaginacaoCRE(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao, int CidadeEvento, string PeriodoIni, string PeriodoFin)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter("CidadeEvento", OracleType.Int32),
			    new OracleParameter("PeriodoIni", OracleType.VarChar,50),
                new OracleParameter("PeriodoFin", OracleType.VarChar,50),
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
              };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Value = CidadeEvento;
            parms[5].Value = PeriodoIni;
            parms[6].Value = PeriodoFin;
            parms[7].Direction = ParameterDirection.Output;
            parms[8].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGCRE, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[8].Value);
            return paginacao;
        }

        #endregion

        #region AlteraSenha
        public static bool AlteraSenha(string usuario, string senha, string novasenha)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter("usuario", OracleType.VarChar),
                                                              new OracleParameter("senha", OracleType.VarChar),
                                                              new OracleParameter("novasenha", OracleType.VarChar)};

            param[0].Value = usuario;
            param[1].Value = senha;
            param[2].Value = novasenha;

            Context.DataBase.ExecuteReader(CommandType.StoredProcedure, "Pessoa.PessoaAltSenha", param);
            
            return false;
        }
        #endregion

        #region LoadDataDdlPessoa
        public static OracleDataReader LoadDataDdlPessoa()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPESSOAGES, param);
            return dr;
        }

        #endregion

        #endregion

    }
}
