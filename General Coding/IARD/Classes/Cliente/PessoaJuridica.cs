using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/05/2007 
//-- Autor :  Daniel melhorias 10/09/08 rodrigo

namespace Classes
{
    public class PessoaJuridica /// T039_pessoa_jur
    {
        #region Atributos
        private int codCliente;
        private string fantasia;
        private string cnpj;
        private string porte;
        private string inscEstad;
        private int? anoFund;
        private int? mesFund;
        private int numEmprego;
        private string inscMunic;
        private int codSubGrupoCae;
        private int codAtivCna;
        private string codSetor;
        private int codGruCnae;
        private int? codSetor171;
        private string codCnae20;
        private string tpoPj;
        private string logUsuario;
        #endregion

        #region Propriedades

        public string CodCnae20
        {
            get { return codCnae20; }
            set { codCnae20 = value; }
        }

        public string Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }
        public string Fantasia
        {
            get { return fantasia; }
            set { fantasia = value; }
        }
        public int CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public string Porte
        {
            get { return porte; }
            set { porte = value; }
        }
        public string InscEstad
        {
            get { return inscEstad; }
            set { inscEstad = value; }
        }
        public int? AnoFund
        {
            get { return anoFund; }
            set { anoFund = value; }
        }
        public int? MesFund
        {
            get { return mesFund; }
            set { mesFund = value; }
        }
        public int NumEmprego
        {
            get { return numEmprego; }
            set { numEmprego = value; }
        }
        public string InscMunic
        {
            get { return inscMunic; }
            set { inscMunic = value; }
        }
        public int CodSubGrupoCae
        {
            get { return codSubGrupoCae; }
            set { codSubGrupoCae = value; }
        }
        public int CodAtivCna
        {
            get { return codAtivCna; }
            set { codAtivCna = value; }
        }
        public string CodSetor
        {
            get { return codSetor; }
            set { codSetor = value; }
        }
        public int CodGruCnae
        {
            get { return codGruCnae; }
            set { codGruCnae = value; }
        }
        public int? CodSetor171
        {
            get { return codSetor171; }
            set { codSetor171 = value; }
        }
        public string TpoPj
        {
            get { return tpoPj; }
            set { tpoPj = value; }
        } 
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "PessoaJuridica.PessoaJuridicaInc";
        private const string SPUPDATE = "PessoaJuridica.PessoaJuridicaAlt";
        private const string SPDELETE = "PessoaJuridica.PessoaJuridicaDel";
        private const string SPSELECTID = "PessoaJuridica.PessoaJuridicaSelId";
        private const string SPSELECTPAG = "PessoaJuridica.PessoaJuridicaSelPag";
        private const string SPSELECTBuscarCnpj = "PessoaJuridica.BuscarCnpj";
        private const string SPSELECTBuscarCPR = "PessoaJuridica.BuscarCPR";
        private const string SPSELECTBuscarDadosEmpCont = "PessoaJuridica.BuscarDadosEmpCont";
        private const string SPSELECTBuscarCNAE20 = "PessoaJuridica.BuscarCNAE20";
        private const string SPSELECTBuscarCPFEmpresa = "PessoaJuridica.BuscarCPFEmpresa";
        //para entidades 
        private const string SPInsertEntidade = "PessoaJuridica.InsertPjEntidade";
        private const string SPSELECTIDEntidade = "PessoaJuridica.EntidadeSelId";
        private const string SPSELECTIDEntidadePAG = "PessoaJuridica.EntidadeSelPag";
        private const string SPSELECTIDAcaoEntidade = "PessoaJuridica.AcaoEntidadeSelId";
        private const string SPSELECTIDLoginEntidade = "PessoaJuridica.EntidadeLoginSelId";
        private const string SPInsertAcaoEntidade = "PessoaJuridica.InsertPjAcaoEntidade";

        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codCliente";
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32),
                    /*1*/ new OracleParameter( "Fantasia", OracleType.VarChar),
                    /*2*/ new OracleParameter( "cgc", OracleType.VarChar),
                    /*3*/ new OracleParameter( "porte", OracleType.VarChar),
                    /*4*/ new OracleParameter( "inscEstad", OracleType.VarChar),
                    /*5*/ new OracleParameter( "anoFund", OracleType.Int32),
                    /*6*/ new OracleParameter( "mesFund", OracleType.Int32),
                    /*7*/ new OracleParameter( "numEmprego", OracleType.Int32),
                    /*8*/ new OracleParameter( "inscMunic", OracleType.VarChar),
                    /*9*/ new OracleParameter( "codSubGrupoCae", OracleType.Int32),
                    /*10*/ new OracleParameter( "codAtivCna", OracleType.Int32),
                    /*11*/ new OracleParameter( "codSetor", OracleType.VarChar),
                    /*12*/ new OracleParameter( "codGruCnae", OracleType.Int32),
                    /*13*/ new OracleParameter( "codSetor171", OracleType.Int32),
                           new OracleParameter( "codCnae20", OracleType.VarChar),
                    /*15*/ new OracleParameter( "tpoPj", OracleType.VarChar),
                    /*16*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codCliente;
            parms[1].Value = this.Fantasia.ToUpper();
            parms[2].Value = "";
            if (this.cnpj != null && this.cnpj.ToString() != "")
            { parms[2].Value = this.cnpj; }
            parms[3].Value = this.porte.ToUpper();
            parms[4].Value = "";
            if (this.inscEstad != null && this.inscEstad.ToString() != "")
            { parms[4].Value = this.inscEstad.ToUpper(); }
            if (this.anoFund != null && this.anoFund.ToString() != "")
            { parms[5].Value = this.anoFund; }
            else
            { parms[5].Value = DBNull.Value; }
            if (this.mesFund != null && this.mesFund.ToString() != "")
            { parms[6].Value = this.mesFund; }
            else
            { parms[6].Value = DBNull.Value; }
            parms[7].Value = this.numEmprego;
            parms[8].Value = "";
            if (this.inscMunic != null && this.inscMunic.ToString() != "")
            { parms[8].Value = this.inscMunic.ToUpper(); }
            parms[9].Value = this.codSubGrupoCae;
            parms[10].Value = this.codAtivCna;
            parms[11].Value = this.codSetor.ToUpper();
            parms[12].Value = this.codGruCnae;
            if (this.codSetor171 != null && this.codSetor171.ToString() != "")
            { parms[13].Value = this.codSetor171; }
            else
            { parms[13].Value = DBNull.Value; }

            parms[14].Value = "";
            if (this.codCnae20 != null && this.codCnae20.ToString() != "")
            { parms[14].Value = codCnae20.ToUpper(); }

            parms[15].Value = "";
            if (this.tpoPj != null && this.tpoPj.ToString() != "")
            { parms[15].Value = tpoPj.ToUpper(); }
            parms[16].Value = this.logUsuario.ToUpper();

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
                        codCliente = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                CodCliente = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter("curPessoaJuridica", OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        //para entidade
        public static OracleDataReader LoadDataDrEntidade(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDEntidade, param);
            return dr;
        }

        public static Paginacao LoadDataEntidadePaginacao(string Where)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
                new OracleParameter("curPessoaJuridica", OracleType.Cursor)
              };

            parms[0].Value = Where;
            parms[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDEntidadePAG, parms);

            paginacao.DataReader = dr;
            return paginacao;
        }

        public static OracleDataReader LoadDataDrAcaoEntidade(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDAcaoEntidade, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrLoginEntidade(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDLoginEntidade, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>PessoaJuridica</returns>
        public static PessoaJuridica GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            PessoaJuridica pessoaJuridica = new PessoaJuridica();
            try
            {
                if (dr.Read())
                {
                    pessoaJuridica.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    pessoaJuridica.Fantasia = Convert.ToString(dr["A039_nm_fant"]);
                    if (dr["A039_cgc"] != DBNull.Value && dr["A039_cgc"] != DBNull.Value && dr["A039_cgc"].ToString() != "")                        
                    pessoaJuridica.cnpj = Convert.ToString(dr["A039_cgc"]);
                    pessoaJuridica.porte = Convert.ToString(dr["A039_porte"]);
                    pessoaJuridica.inscEstad = Convert.ToString(dr["A039_ie"]);
                    if (dr["A039_ano_fund"] != DBNull.Value && dr["A039_ano_fund"] != DBNull.Value && dr["A039_ano_fund"].ToString() != "")
                        pessoaJuridica.anoFund = Convert.ToInt32(dr["A039_ano_fund"]);
                    if (dr["A039_mes_fund"] != DBNull.Value && dr["A039_mes_fund"] != DBNull.Value && dr["A039_mes_fund"].ToString() != "")
                        pessoaJuridica.mesFund = Convert.ToInt32(dr["A039_mes_fund"]);
                    pessoaJuridica.numEmprego = Convert.ToInt32(dr["A039_num_empreg"]);
                    pessoaJuridica.inscMunic = Convert.ToString(dr["A039_inscr_munic"]);
                    if (dr["A335_cod_sub_grupo_cnae"] != DBNull.Value && dr["A335_cod_sub_grupo_cnae"].ToString() != "")
                        pessoaJuridica.codSubGrupoCae = Convert.ToInt32(dr["A335_cod_sub_grupo_cnae"]);
                    if (dr["A336_cd_ativ_cnae"] != DBNull.Value && dr["A336_cd_ativ_cnae"].ToString() != "")
                        pessoaJuridica.codAtivCna = Convert.ToInt32(dr["A336_cd_ativ_cnae"]);
                    if (dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"].ToString() != "")
                        pessoaJuridica.codSetor = Convert.ToString(dr["A048_cd_setor"]);
                    if (dr["A334_cd_gru_cnae"] != DBNull.Value && dr["A334_cd_gru_cnae"].ToString() != "")
                        pessoaJuridica.codGruCnae = Convert.ToInt32(dr["A334_cd_gru_cnae"]);
                    if (dr["A171_cd_setor"] != DBNull.Value && dr["A171_cd_setor"] != DBNull.Value && dr["A171_cd_setor"].ToString() != "")
                        pessoaJuridica.codSetor171 = Convert.ToInt32(dr["A171_cd_setor"]);
                    if (dr["A039_tpo_pj"] != DBNull.Value && dr["A039_tpo_pj"].ToString() != "") 
                        pessoaJuridica.tpoPj = Convert.ToString(dr["A039_tpo_pj"]);
                    pessoaJuridica.codCnae20 = "";
                    if (dr["A936_COD_CNAE"] != DBNull.Value && dr["A936_COD_CNAE"] != DBNull.Value && dr["A936_COD_CNAE"].ToString() != "")
                        pessoaJuridica.codCnae20 = Convert.ToString(dr["A936_COD_CNAE"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        pessoaJuridica.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pessoaJuridica = new PessoaJuridica();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return pessoaJuridica;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>PessoaJuridica</returns>
        public static PessoaJuridica GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            PessoaJuridica pessoaJuridica = new PessoaJuridica();
            try
            {
                if (dr.Read())
                {
                    pessoaJuridica.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    pessoaJuridica.Fantasia = Convert.ToString(dr["A039_nm_fant"]);
                    if (dr["A039_cgc"] != DBNull.Value && dr["A039_cgc"] != DBNull.Value && dr["A039_cgc"].ToString() != "")
                        pessoaJuridica.cnpj = Convert.ToString(dr["A039_cgc"]);
                    pessoaJuridica.porte = Convert.ToString(dr["A039_porte"]);
                    pessoaJuridica.inscEstad = Convert.ToString(dr["A039_ie"]);
                    if (dr["A039_ano_fund"] != DBNull.Value && dr["A039_ano_fund"] != DBNull.Value && dr["A039_ano_fund"].ToString() != "")
                        pessoaJuridica.anoFund = Convert.ToInt32(dr["A039_ano_fund"]);
                    if (dr["A039_mes_fund"] != DBNull.Value && dr["A039_mes_fund"] != DBNull.Value && dr["A039_mes_fund"].ToString() != "")
                        pessoaJuridica.mesFund = Convert.ToInt32(dr["A039_mes_fund"]);
                    pessoaJuridica.numEmprego = Convert.ToInt32(dr["A039_num_empreg"]);
                    pessoaJuridica.inscMunic = Convert.ToString(dr["A039_inscr_munic"]);
                    if (dr["A335_cod_sub_grupo_cnae"] != DBNull.Value && dr["A335_cod_sub_grupo_cnae"].ToString() != "")
                        pessoaJuridica.codSubGrupoCae = Convert.ToInt32(dr["A335_cod_sub_grupo_cnae"]);
                    if (dr["A336_cd_ativ_cnae"] != DBNull.Value && dr["A336_cd_ativ_cnae"].ToString() != "")
                        pessoaJuridica.codAtivCna = Convert.ToInt32(dr["A336_cd_ativ_cnae"]);
                    if (dr["A048_cd_setor"] != DBNull.Value && dr["A048_cd_setor"].ToString() != "")
                        pessoaJuridica.codSetor = Convert.ToString(dr["A048_cd_setor"]);
                    if (dr["A334_cd_gru_cnae"] != DBNull.Value && dr["A334_cd_gru_cnae"].ToString() != "")
                        pessoaJuridica.codGruCnae = Convert.ToInt32(dr["A334_cd_gru_cnae"]);
                    if (dr["A171_cd_setor"] != DBNull.Value && dr["A171_cd_setor"] != DBNull.Value && dr["A171_cd_setor"].ToString() != "")
                        pessoaJuridica.codSetor171 = Convert.ToInt32(dr["A171_cd_setor"]);
                    if (dr["A039_tpo_pj"] != DBNull.Value && dr["A039_tpo_pj"].ToString() != "")
                        pessoaJuridica.tpoPj = Convert.ToString(dr["A039_tpo_pj"]);
                    if (dr["A936_COD_CNAE"] != DBNull.Value && dr["A936_COD_CNAE"] != DBNull.Value && dr["A936_COD_CNAE"].ToString() != "")
                        pessoaJuridica.codCnae20 = Convert.ToString(dr["A936_COD_CNAE"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        pessoaJuridica.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                pessoaJuridica = new PessoaJuridica();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return pessoaJuridica;
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
                new OracleParameter("curPessoaJuridica", OracleType.Cursor),
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

        //
        public static OracleDataReader InsertEntidade(string sCliente, string sTipo, string sConvenio, string dVigenciaini, string dVigenciafim, string sRepresentante, string sRegional, string stpEscritorio, string sFederacao, string sCalcula, string sUsuario)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("sCliente", OracleType.VarChar,100),
                    new OracleParameter("sTipo", OracleType.VarChar,100),
                    new OracleParameter("sConvenio", OracleType.VarChar,100),
                    new OracleParameter("dVigenciaini", OracleType.VarChar,100),
                    new OracleParameter("dVigenciafim", OracleType.VarChar,100),
                    new OracleParameter("sRepresentante", OracleType.VarChar,100),
                    new OracleParameter("sRegional", OracleType.VarChar,100),
                    new OracleParameter("stpEscritorio", OracleType.VarChar,100),
                    new OracleParameter("sFederacao", OracleType.VarChar,100),
                    new OracleParameter("sCalcula", OracleType.VarChar,100),
                    new OracleParameter("sUsuario", OracleType.VarChar,100),
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };
            param[0].Value = sCliente;
            param[1].Value = sTipo;
            param[2].Value = sConvenio;
            param[3].Value = dVigenciaini;
            param[4].Value = dVigenciafim;
            param[5].Value = sRepresentante;
            param[6].Value = sRegional;
            param[7].Value = stpEscritorio;
            param[8].Value = sFederacao;
            param[9].Value = sCalcula;
            param[10].Value = sUsuario;
            param[11].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPInsertEntidade, param);
            return dr;
        }

        public static OracleDataReader InsertAcaoEntidade(string sCliente, string sTitulo, string dExecucao, string sInsert)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("sCliente", OracleType.VarChar,100),
                    new OracleParameter("sTitulo", OracleType.VarChar,100),
                    new OracleParameter("dExecucao", OracleType.VarChar,100),
                    new OracleParameter("sInsert", OracleType.VarChar,100),
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };
            param[0].Value = sCliente;
            param[1].Value = sTitulo;
            param[2].Value = dExecucao;
            param[3].Value = sInsert;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPInsertAcaoEntidade, param);
            return dr;
        }
        #endregion

        #endregion

        #region Métodos Específicos

        #region LoadDataDrBuscarCnpj
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrBuscarCnpj(string Cnpj)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codCnpj", OracleType.VarChar), 
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };

            param[0].Value = Cnpj;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarCnpj, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrBuscarCPR(string CPR)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codCPR", OracleType.VarChar), 
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };

            param[0].Value = CPR;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarCPR, param);
            return dr;
        }

        public static OracleDataReader BuscarDadosEmpCont(int codCliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codCliente", OracleType.Int32 ), 
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };

            param[0].Value = codCliente;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarDadosEmpCont, param);
            return dr;
        }
        //
        public static int GetCodPess(string varCNPJ)
        {
            int res = -1;
            OracleDataReader pesJur = PessoaJuridica.LoadDataDrBuscarCnpj(varCNPJ);
            if (pesJur.Read())
            {
                res = Convert.ToInt32(pesJur["a012_cd_cli"]);
            }
            pesJur.Close();
            return res;
        }
        #endregion

        #region LoadDataDrBuscarCNAE20
        public static OracleDataReader LoadDataDrBuscarCNAE20(string CNAE)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codCnae", OracleType.VarChar), 
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };

            param[0].Value = CNAE;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarCNAE20, param);
            return dr;
        }
        #endregion

        #region LoadDataDrBuscarCPFEmpresa
        public static OracleDataReader LoadDataDrBuscarCPFEmpresa(string CPF)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("numCPF", OracleType.VarChar), 
                    new OracleParameter("curPessoaJuridica", OracleType.Cursor)
                };

            param[0].Value = CPF;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTBuscarCPFEmpresa, param);
            return dr;
        }
        #endregion

        #endregion
    }
}
