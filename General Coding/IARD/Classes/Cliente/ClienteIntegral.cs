using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/05/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class ClienteIntegral /// T018_CLI_CADAST  
    {
        #region Atributos
        private int codCliente;        
        private Pais codPais;
        private Estado codEstado;
        private Cidade codCidade;
        private Usuario codeUsuario;
        private int tpoCliente;
        private string nomeCliente;
        private string dscLogradouroEnd;
        private string dddTelefone;
        private string dddCelular;
        private Int64 numTelefone;
        private Int64? numTelefone2;
        private int? numFax;
        private string dscBairroEnd;
        private int? cepEndereco;
        private string emailCliente;
        private int? indDesativado;
        private int? indClienteEspecial;
        private int? tempoMedio;
        private string obsTelefone;
        private int? cliFor;
        private int? numcaixaPostal;
        private int? indClienteMaquina;
        private int? indClientePegn;
        private int? indDiskResolva;
        private Bairro codBairro;
        private string cepInternacional;
        private Rua codRua;
        private string dscComplementoEnd;
        private string numEnd;
        private string siteCliente;
        private int? indVeiculoComunicacao;
        private int? indClienteInformal;
        private int? indPot;
        private int? indSet;
        private int? indEntidadeCredenciada;
        private string logUsuario;
        private DateTime dataInclusao;
        private DateTime dataUltAlteracao;
        private DateTime dataEnvio;
        #endregion

        #region Propriedades
        public Pais CodPais
        {
            get { return codPais; }
            set { codPais = value; }
        }
        public int CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
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
        public Usuario CodeUsuario
        {
            get { return codeUsuario; }
            set { codeUsuario = value; }
        }
        public int TpoCliente
        {
            get { return tpoCliente; }
            set { tpoCliente = value; }
        }
        public string NomeCliente
        {
            get { return nomeCliente; }
            set { nomeCliente = value; }
        }
        public string DscLogradouroEnd
        {
            get { return dscLogradouroEnd; }
            set { dscLogradouroEnd = value; }
        }
        public string DddTelefone
        {
            get { return dddTelefone; }
            set { dddTelefone = value; }
        }

        public string DddCelular
        {
            get { return dddCelular; }
            set { dddCelular = value; }
        }
        public Int64 NumTelefone
        {
            get { return numTelefone; }
            set { numTelefone = value; }
        }
        public Int64? NumTelefone2
        {
            get { return numTelefone2; }
            set { numTelefone2 = value; }
        }
        public int? NumFax
        {
            get { return numFax; }
            set { numFax = value; }
        }
        public string DscBairroEnd
        {
            get { return dscBairroEnd; }
            set { dscBairroEnd = value; }
        }
        public int? CepEndereco
        {
            get { return cepEndereco; }
            set { cepEndereco = value; }
        }
        public string EmailCliente
        {
            get { return emailCliente; }
            set { emailCliente = value; }
        }
        public int? IndDesativado
        {
            get { return indDesativado; }
            set { indDesativado = value; }
        }
        public int? IndClienteEspecial
        {
            get { return indClienteEspecial; }
            set { indClienteEspecial = value; }
        }
        public int? TempoMedio
        {
            get { return tempoMedio; }
            set { tempoMedio = value; }
        }
        public string ObsTelefone
        {
            get { return obsTelefone; }
            set { obsTelefone = value; }
        }
        public int? CliFor
        {
            get { return cliFor; }
            set { cliFor = value; }
        }
        public int? NumcaixaPostal
        {
            get { return numcaixaPostal; }
            set { numcaixaPostal = value; }
        }
        public int? IndClienteMaquina
        {
            get { return indClienteMaquina; }
            set { indClienteMaquina = value; }
        }
        public int? IndClientePegn
        {
            get { return indClientePegn; }
            set { indClientePegn = value; }
        }
        public int? IndDiskResolva
        {
            get { return indDiskResolva; }
            set { indDiskResolva = value; }
        }
        public Bairro CodBairro
        {
            get { return codBairro; }
            set { codBairro = value; }
        }
        public string CepInternacional
        {
            get { return cepInternacional; }
            set { cepInternacional = value; }
        }
        public Rua CodRua
        {
            get { return codRua; }
            set { codRua = value; }
        }
        public string DscComplementoEnd
        {
            get { return dscComplementoEnd; }
            set { dscComplementoEnd = value; }
        }
        public string NumEnd
        {
            get { return numEnd; }
            set { numEnd = value; }
        }
        public string SiteCliente
        {
            get { return siteCliente; }
            set { siteCliente = value; }
        }
        public int? IndVeiculoComunicacao
        {
            get { return indVeiculoComunicacao; }
            set { indVeiculoComunicacao = value; }
        }
        public int? IndClienteInformal
        {
            get { return indClienteInformal; }
            set { indClienteInformal = value; }
        }
        public int? IndPot
        {
            get { return indPot; }
            set { indPot = value; }
        }
        public int? IndSet
        {
            get { return indSet; }
            set { indSet = value; }
        }
        public int? IndEntidadeCredenciada
        {
            get { return indEntidadeCredenciada; }
            set { indEntidadeCredenciada = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        public DateTime DataInclusao
        {
            get { return dataInclusao; }
            set { dataInclusao = value; }
        }
        public DateTime DataUltAlteracao
        {
            get { return dataUltAlteracao; }
            set { dataUltAlteracao = value; }
        }
        public DateTime DataEnvio
        {
            get { return dataEnvio; }
            set { dataEnvio = value; }
        }

        #endregion
        
        #region Construtores
        public ClienteIntegral()
            : this(-1)
        { }
        public ClienteIntegral(int codCliente)
        {
            this.codCliente = codCliente;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ClienteIntegral.ClienteIntegralInc";
        private const string SPUPDATE = "ClienteIntegral.ClienteIntegralAlt";
        private const string SPDELETE = "ClienteIntegral.ClienteIntegralDel";
        private const string SPSELECTID = "ClienteIntegral.ClienteIntegralSelId";
        private const string SPSELECTPAG = "ClienteIntegral.ClienteIntegralSelPag";
        private const string SPSELECTBUSCAUC = "ClienteIntegral.ClienteIntegralBuscaUc";

        private const string SPSELECTGRUPO = "ClienteIntegral.CSelectGrupo";
        private const string SPSELECTSubGRUPO = "ClienteIntegral.CSelectSubGrupo";
        private const string SPSELECTDetalheGRUPO = "ClienteIntegral.CSelectDetalheGrupo";
        private const string SPSELECTDadosEntidade = "ClienteIntegral.CSelectDadosEntidade";

        private const string SPSELECTSebraeEscritorio = "ClienteIntegral.CSEscritorio";
        private const string SPSELECTRegionalEscr = "ClienteIntegral.CSRegionalEscr";
        private const string SPSELECTFederacao = "ClienteIntegral.CSFederacao";
        private const string SPSELECTDadosSebrae = "ClienteIntegral.CSelectDadosSebrae";
        private const string SPSELECTDadosSebraeFed = "ClienteIntegral.CSelectDadosSebraeFed";
        //relatorios 2 iguais, datareader ou selpag
        private const string SPSELECTIDRelatorio  = "ClienteIntegral.ClienteSelIdRelatorio";
        private const string SPSELECTPAGRelatorio = "ClienteIntegral.ClienteSelPagRelatorio";

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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32) ,
                    /*1*/ new OracleParameter( "codPais", OracleType.Int32),
                    /*2*/ new OracleParameter( "codEstado", OracleType.Int32),
                    /*3*/ new OracleParameter( "codCidade", OracleType.Int32),
                    /*4*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*5*/ new OracleParameter( "tpoCliente", OracleType.Int32),
                    /*6*/ new OracleParameter( "nomeCliente", OracleType.VarChar),
                    /*7*/ new OracleParameter( "dscLogradouroEnd", OracleType.VarChar),
                    /*8*/ new OracleParameter( "dddTelefone", OracleType.VarChar),
                    /*9*/ new OracleParameter( "NumTelefone", OracleType.Number),
                    /*10*/ new OracleParameter( "NumTelefone2", OracleType.Number),
                    /*11*/ new OracleParameter( "numFax", OracleType.Int32),
                    /*12*/ new OracleParameter( "dscBairroEnd", OracleType.VarChar),
                    /*13*/ new OracleParameter( "cepEndereco", OracleType.Int32),
                    /*14*/ new OracleParameter( "emailCliente", OracleType.VarChar),                     
                    /*15*/ new OracleParameter( "indDesativado", OracleType.Int32),
                    /*16*/ new OracleParameter( "indClienteEspecial", OracleType.Int32),
                    /*17*/ new OracleParameter( "tempoMedio", OracleType.Int32),
                    /*18*/ new OracleParameter( "obsTelefone", OracleType.VarChar),
                    /*19*/ new OracleParameter( "cliFor", OracleType.Int32),
                    /*20*/ new OracleParameter( "numcaixaPostal", OracleType.Int32),
                    /*21*/ new OracleParameter( "indClienteMaquina", OracleType.Int32),
                    /*22*/ new OracleParameter( "indClientePegn", OracleType.Int32),
                    /*23*/ new OracleParameter( "indDiskResolva", OracleType.Int32),
                    /*24*/ new OracleParameter( "codBairro", OracleType.Int32),
                    /*25*/ new OracleParameter( "cepInternacional", OracleType.VarChar),
                    /*26*/ new OracleParameter( "codRua", OracleType.Int32),
                    /*27*/ new OracleParameter( "dscComplementoEnd", OracleType.VarChar),
                    /*28*/ new OracleParameter( "numEnd", OracleType.VarChar),
                    /*29*/ new OracleParameter( "siteCliente", OracleType.VarChar),
                    /*30*/ new OracleParameter( "indVeiculoComunicacao", OracleType.Int32),
                    /*31*/ new OracleParameter( "indClienteInformal", OracleType.Int32),
                    /*32*/ new OracleParameter( "indPot", OracleType.Int32),
                    /*33*/ new OracleParameter( "indSet", OracleType.Int32),
                    /*34*/ new OracleParameter( "indEntidadeCredenciada", OracleType.Int32),
                    new OracleParameter( "dddCelular", OracleType.VarChar),
                    new OracleParameter( "dataEnvio", OracleType.DateTime),
                    /*37*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[1].Value = this.codPais.CodPais;
            parms[2].Value = this.codEstado.CodEstado;
            parms[3].Value = this.codCidade.CodCidade;
            parms[4].Value = this.codeUsuario.CodUsuario;
            parms[5].Value = this.tpoCliente;
            parms[6].Value = this.nomeCliente.ToUpper();
            parms[7].Value = this.dscLogradouroEnd.ToUpper();
            parms[8].Value = this.dddTelefone.ToUpper();
            parms[9].Value = this.numTelefone;
            parms[10].Value = DBNull.Value;
            if (this.numTelefone2 != null)
            { parms[10].Value = this.numTelefone2; }
            parms[11].Value = DBNull.Value;
            if (this.numFax != null)
            { parms[11].Value = this.numFax; }
            parms[12].Value = "";
            if (this.dscBairroEnd != null)
            {
                parms[12].Value = this.dscBairroEnd.ToUpper();
            }
            parms[13].Value = DBNull.Value;
            if (this.cepEndereco != null)
            { parms[13].Value = this.cepEndereco; }
            parms[14].Value = "";
            if (this.emailCliente != null)
            {
                parms[14].Value = this.emailCliente.ToLower();
            }
            parms[15].Value = DBNull.Value;
            if (this.indDesativado != null)
            { parms[15].Value = this.indDesativado; }
            parms[16].Value = DBNull.Value;
            if (this.indClienteEspecial != null)
            { parms[16].Value = this.indClienteEspecial; }
            parms[17].Value = DBNull.Value;
            if (this.tempoMedio != null)
            { parms[17].Value = this.tempoMedio; }
            parms[18].Value = "";
            if (this.obsTelefone != null)
            { parms[18].Value = this.obsTelefone.ToUpper(); }
            parms[19].Value = DBNull.Value;
            if (this.CliFor != null)
            { parms[19].Value = this.CliFor; }
            parms[20].Value = DBNull.Value;
            if (this.numcaixaPostal != null)
            { parms[20].Value = this.numcaixaPostal; }
            parms[21].Value = DBNull.Value;
            if (this.indClienteMaquina != null)
            { parms[21].Value = this.indClienteMaquina; }
            parms[22].Value = DBNull.Value;
            if (this.indClientePegn != null)
            { parms[22].Value = this.indClientePegn; }
            parms[23].Value = DBNull.Value;
            if (this.indDiskResolva != null)
            { parms[23].Value = this.indDiskResolva; }
            parms[24].Value = DBNull.Value;
            if (this.codBairro != null)
            { parms[24].Value = this.codBairro.CodBairro; }
            parms[25].Value = "";
            if (this.cepInternacional != null)
            { parms[25].Value = this.cepInternacional.ToUpper(); }
            parms[26].Value = DBNull.Value;
            if (this.codRua != null)
            { parms[26].Value = this.codRua.CodRua; }
            parms[27].Value = "";
            if (this.dscComplementoEnd != null)
            {
                parms[27].Value = this.dscComplementoEnd.ToUpper();
            }
            parms[28].Value = "N/I";
            if (this.numEnd != null)
            {
                parms[28].Value = this.numEnd.ToUpper();
            }
            parms[29].Value = "";
            if (this.siteCliente != null)
            { parms[29].Value = this.siteCliente.ToLower(); }
            parms[30].Value = DBNull.Value;
            if (this.indVeiculoComunicacao != null)
            { parms[30].Value = this.indVeiculoComunicacao; }
            parms[31].Value = DBNull.Value;
            if (this.indClienteInformal != null)
            { parms[31].Value = this.indClienteInformal; }
            parms[32].Value = DBNull.Value;
            if (this.indPot != null)
            { parms[32].Value = this.indPot; }
            parms[33].Value = DBNull.Value;
            if (this.indSet != null)
            { parms[33].Value = this.indSet; }
            parms[34].Value = DBNull.Value;
            if (this.indEntidadeCredenciada != null)
            { parms[34].Value = this.indEntidadeCredenciada; }
            parms[35].Value = " ";
            if (this.dddCelular != null)
                parms[35].Value = this.dddCelular;
            parms[36].Value = DBNull.Value;
            if (this.dataEnvio != null)
                parms[36].Value = this.dataEnvio;
            parms[37].Value = this.logUsuario.ToUpper();
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
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
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
                                                                  new OracleParameter("curClienteIntegral", OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrRelatorio(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDRelatorio, param);
            return dr;
        }

        //popular dados de entidades
        public static OracleDataReader LoadDataDrEntidadeGrupo(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTGRUPO, param);
            return dr;
        }
        public static OracleDataReader LoadDataDrEntidadeSubGrupo(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTSubGRUPO, param);
            return dr;
        }
        public static OracleDataReader LoadDataDrEntidadeDetalheGrupo(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTDetalheGRUPO, param);
            return dr;
        }
        public static OracleDataReader LoadDataDrEntidadeDadosEntidade(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTDadosEntidade, param);
            return dr;
        }
        //popular dados de Sebrae
        public static OracleDataReader LoadDataDrSebraeEscritorio(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTSebraeEscritorio, param);
            return dr;
        }
        public static OracleDataReader LoadDataDrSebraeRegionalEscr(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTRegionalEscr, param);
            return dr;
        }
        public static OracleDataReader LoadDataDrSebraeFederacao(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTFederacao, param);
            return dr;
        }
        public static OracleDataReader LoadDataDrSebraeDadosSebrae(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTDadosSebrae, param);
            return dr;
        }
        public static OracleDataReader LoadDataDrSebraeDadosSebraeFed(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter("curClienteIntegral", OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTDadosSebraeFed, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>ClienteIntegral</returns>
        public static ClienteIntegral GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            ClienteIntegral clienteI = new ClienteIntegral();
            try
            {
                if (dr.Read())
                {
                    clienteI.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        clienteI.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        clienteI.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        clienteI.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        clienteI.codeUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["A018_tp_cli"] != DBNull.Value && dr["A018_tp_cli"] != DBNull.Value && dr["A018_tp_cli"].ToString() != "")
                        clienteI.tpoCliente = Convert.ToInt32(dr["A018_tp_cli"]);
                    clienteI.nomeCliente = Convert.ToString(dr["A018_nm_cli"]);
                    clienteI.dscLogradouroEnd = Convert.ToString(dr["A018_end_cli"]);
                    clienteI.dddTelefone = Convert.ToString(dr["A018_ddd_cli"]);
                    clienteI.dddCelular = Convert.ToString(dr["A018_2ddd_cli"]);
                    if (dr["A018_tel_cli"] != DBNull.Value && dr["A018_tel_cli"] != DBNull.Value && dr["A018_tel_cli"].ToString() != "")
                        clienteI.numTelefone = Convert.ToInt32(dr["A018_tel_cli"]);
                    if (dr["A018_tel2_cli"] != DBNull.Value && dr["A018_tel2_cli"] != DBNull.Value && dr["A018_tel2_cli"].ToString() != "")
                        clienteI.numTelefone2 = Convert.ToInt32(dr["A018_tel2_cli"]);
                    if (dr["A018_fax_cli"] != DBNull.Value && dr["A018_fax_cli"] != DBNull.Value && dr["A018_fax_cli"].ToString() != "")
                        clienteI.numFax = Convert.ToInt32(dr["A018_fax_cli"]);
                    clienteI.dscBairroEnd = Convert.ToString(dr["A018_bair_cli"]);
                    if (dr["A018_cep_cli"] != DBNull.Value && dr["A018_cep_cli"] != DBNull.Value && dr["A018_cep_cli"].ToString() != "")
                        clienteI.cepEndereco = Convert.ToInt32(dr["A018_cep_cli"]);
                    clienteI.EmailCliente = Convert.ToString(dr["A018_e_mail"]).ToLower();
                    if (dr["A018_ind_des_cli"] != DBNull.Value && dr["A018_ind_des_cli"] != DBNull.Value && dr["A018_ind_des_cli"].ToString() != "")
                        clienteI.indDesativado = Convert.ToInt32(dr["A018_ind_des_cli"]);
                    if (dr["A018_ind_cli_esp"] != DBNull.Value && dr["A018_ind_cli_esp"] != DBNull.Value && dr["A018_ind_cli_esp"].ToString() != "")
                        clienteI.indClienteEspecial = Convert.ToInt32(dr["A018_ind_cli_esp"]);
                    if (dr["A018_tem_med"] != DBNull.Value && dr["A018_tem_med"] != DBNull.Value && dr["A018_tem_med"].ToString() != "")
                        clienteI.tempoMedio = Convert.ToInt32(dr["A018_tem_med"]);
                    clienteI.obsTelefone = Convert.ToString(dr["A018_obs_tel"]);
                    if (dr["A018_cli_for"] != DBNull.Value && dr["A018_cli_for"] != DBNull.Value && dr["A018_cli_for"].ToString() != "")
                        clienteI.CliFor = Convert.ToInt32(dr["A018_cli_for"]);
                    if (dr["A018_cx_postal"] != DBNull.Value && dr["A018_cx_postal"] != DBNull.Value && dr["A018_cx_postal"].ToString() != "")
                        clienteI.numcaixaPostal = Convert.ToInt32(dr["A018_cx_postal"]);
                    if (dr["A018_ind_maq"] != DBNull.Value && dr["A018_ind_maq"] != DBNull.Value && dr["A018_ind_maq"].ToString() != "")
                        clienteI.indClienteMaquina = Convert.ToInt32(dr["A018_ind_maq"]);
                    if (dr["A018_ind_pegn"] != DBNull.Value && dr["A018_ind_pegn"] != DBNull.Value && dr["A018_ind_pegn"].ToString() != "")
                        clienteI.indClientePegn = Convert.ToInt32(dr["A018_ind_pegn"]);
                    if (dr["A018_ind_disk"] != DBNull.Value && dr["A018_ind_disk"] != DBNull.Value && dr["A018_ind_disk"].ToString() != "")
                        clienteI.indDiskResolva = Convert.ToInt32(dr["A018_ind_disk"]);
                    if (dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"].ToString() != "")
                        clienteI.codBairro = new Bairro(Convert.ToInt32(dr["A616_cd_bairro"]));
                    clienteI.cepInternacional = Convert.ToString(dr["A018_cep_cli_internac"]);
                    if (dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"].ToString() != "")
                        clienteI.codRua = new Rua(Convert.ToInt32(dr["A615_cd_rua"]));
                    clienteI.dscComplementoEnd = Convert.ToString(dr["A018_comp_end"]);
                    clienteI.numEnd = Convert.ToString(dr["A018_nmr_end"]);
                    clienteI.siteCliente = Convert.ToString(dr["A018_site"]).ToLower();
                    if (dr["A018_ind_veic_com"] != DBNull.Value && dr["A018_ind_veic_com"] != DBNull.Value && dr["A018_ind_veic_com"].ToString() != "")
                        clienteI.indVeiculoComunicacao = Convert.ToInt32(dr["A018_ind_veic_com"]);
                    if (dr["A018_ind_informal"] != DBNull.Value && dr["A018_ind_informal"] != DBNull.Value && dr["A018_ind_informal"].ToString() != "")
                        clienteI.indClienteInformal = Convert.ToInt32(dr["A018_ind_informal"]);
                    if (dr["A018_ind_cli_pot"] != DBNull.Value && dr["A018_ind_cli_pot"] != DBNull.Value && dr["A018_ind_cli_pot"].ToString() != "")
                        clienteI.IndPot = Convert.ToInt32(dr["A018_ind_cli_pot"]);
                    if (dr["A018_ind_cli_set"] != DBNull.Value && dr["A018_ind_cli_set"] != DBNull.Value && dr["A018_ind_cli_set"].ToString() != "")
                        clienteI.indSet = Convert.ToInt32(dr["A018_ind_cli_set"]);
                    if (dr["A018_entidade_credenciada"] != DBNull.Value && dr["A018_entidade_credenciada"] != DBNull.Value && dr["A018_entidade_credenciada"].ToString() != "")
                        clienteI.indEntidadeCredenciada = Convert.ToInt32(dr["A018_entidade_credenciada"]);
                        clienteI.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                    if (dr["A018_dt_incl"] != DBNull.Value && dr["A018_dt_incl"] != DBNull.Value && dr["A018_dt_incl"].ToString() != "")
                        clienteI.dataInclusao = Convert.ToDateTime(dr["A018_dt_incl"]);
                    if (dr["dta_inc_alt"] != DBNull.Value && dr["dta_inc_alt"] != DBNull.Value && dr["dta_inc_alt"].ToString() != "")
                        clienteI.dataUltAlteracao = Convert.ToDateTime(dr["dta_inc_alt"]);
                    if (dr["A018_DT_ENVIO"] != DBNull.Value && dr["A018_DT_ENVIO"] != DBNull.Value && dr["A018_DT_ENVIO"].ToString() != "")
                        clienteI.dataEnvio = Convert.ToDateTime(dr["A018_DT_ENVIO"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                clienteI = new ClienteIntegral();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return clienteI;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ClienteIntegral</returns>
        public static ClienteIntegral GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            ClienteIntegral clienteI = new ClienteIntegral();
            try
            {
                if (dr.Read())
                {
                    clienteI.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"] != DBNull.Value && dr["A035_cd_pais"].ToString() != "")
                        clienteI.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    if (dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"] != DBNull.Value && dr["A021_cd_est"].ToString() != "")
                        clienteI.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    if (dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"] != DBNull.Value && dr["A011_cd_cid"].ToString() != "")
                        clienteI.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        clienteI.codeUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["A018_tp_cli"] != DBNull.Value && dr["A018_tp_cli"] != DBNull.Value && dr["A018_tp_cli"].ToString() != "")
                        clienteI.tpoCliente = Convert.ToInt32(dr["A018_tp_cli"]);
                    clienteI.nomeCliente = Convert.ToString(dr["A018_nm_cli"]);
                    clienteI.dscLogradouroEnd = Convert.ToString(dr["A018_end_cli"]);
                    clienteI.dddTelefone = Convert.ToString(dr["A018_ddd_cli"]);
                    clienteI.dddCelular = Convert.ToString(dr["A018_2ddd_cli"]);
                    if (dr["A018_tel_cli"] != DBNull.Value && dr["A018_tel_cli"] != DBNull.Value && dr["A018_tel_cli"].ToString() != "")
                        clienteI.numTelefone = Convert.ToInt32(dr["A018_tel_cli"]);
                    if (dr["A018_tel2_cli"] != DBNull.Value && dr["A018_tel2_cli"] != DBNull.Value && dr["A018_tel2_cli"].ToString() != "")
                        clienteI.numTelefone2 = Convert.ToInt32(dr["A018_tel2_cli"]);
                    if (dr["A018_fax_cli"] != DBNull.Value && dr["A018_fax_cli"] != DBNull.Value && dr["A018_fax_cli"].ToString() != "")
                        clienteI.numFax = Convert.ToInt32(dr["A018_fax_cli"]);
                    clienteI.dscBairroEnd = Convert.ToString(dr["A018_bair_cli"]);
                    if (dr["A018_cep_cli"] != DBNull.Value && dr["A018_cep_cli"] != DBNull.Value && dr["A018_cep_cli"].ToString() != "")
                        clienteI.cepEndereco = Convert.ToInt32(dr["A018_cep_cli"]);
                    clienteI.EmailCliente = Convert.ToString(dr["A018_e_mail"]).ToLower();
                    if (dr["A018_ind_des_cli"] != DBNull.Value && dr["A018_ind_des_cli"] != DBNull.Value && dr["A018_ind_des_cli"].ToString() != "")
                        clienteI.indDesativado = Convert.ToInt32(dr["A018_ind_des_cli"]);
                    if (dr["A018_ind_cli_esp"] != DBNull.Value && dr["A018_ind_cli_esp"] != DBNull.Value && dr["A018_ind_cli_esp"].ToString() != "")
                        clienteI.indClienteEspecial = Convert.ToInt32(dr["A018_ind_cli_esp"]);
                    if (dr["A018_tem_med"] != DBNull.Value && dr["A018_tem_med"] != DBNull.Value && dr["A018_tem_med"].ToString() != "")
                        clienteI.tempoMedio = Convert.ToInt32(dr["A018_tem_med"]);
                    clienteI.obsTelefone = Convert.ToString(dr["A018_obs_tel"]);
                    if (dr["A018_cli_for"] != DBNull.Value && dr["A018_cli_for"] != DBNull.Value && dr["A018_cli_for"].ToString() != "")
                        clienteI.CliFor = Convert.ToInt32(dr["A018_cli_for"]);
                    if (dr["A018_cx_postal"] != DBNull.Value && dr["A018_cx_postal"] != DBNull.Value && dr["A018_cx_postal"].ToString() != "")
                        clienteI.numcaixaPostal = Convert.ToInt32(dr["A018_cx_postal"]);
                    if (dr["A018_ind_maq"] != DBNull.Value && dr["A018_ind_maq"] != DBNull.Value && dr["A018_ind_maq"].ToString() != "")
                        clienteI.indClienteMaquina = Convert.ToInt32(dr["A018_ind_maq"]);
                    if (dr["A018_ind_pegn"] != DBNull.Value && dr["A018_ind_pegn"] != DBNull.Value && dr["A018_ind_pegn"].ToString() != "")
                        clienteI.indClientePegn = Convert.ToInt32(dr["A018_ind_pegn"]);
                    if (dr["A018_ind_disk"] != DBNull.Value && dr["A018_ind_disk"] != DBNull.Value && dr["A018_ind_disk"].ToString() != "")
                        clienteI.indDiskResolva = Convert.ToInt32(dr["A018_ind_disk"]);
                    if (dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"] != DBNull.Value && dr["A616_cd_bairro"].ToString() != "")
                        clienteI.codBairro = new Bairro(Convert.ToInt32(dr["A616_cd_bairro"]));
                    clienteI.cepInternacional = Convert.ToString(dr["A018_cep_cli_internac"]);
                    if (dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"] != DBNull.Value && dr["A615_cd_rua"].ToString() != "")
                        clienteI.codRua = new Rua(Convert.ToInt32(dr["A615_cd_rua"]));
                    clienteI.dscComplementoEnd = Convert.ToString(dr["A018_comp_end"]);
                    clienteI.numEnd = Convert.ToString(dr["A018_nmr_end"]);
                    clienteI.siteCliente = Convert.ToString(dr["A018_site"]).ToLower();
                    if (dr["A018_ind_veic_com"] != DBNull.Value && dr["A018_ind_veic_com"] != DBNull.Value && dr["A018_ind_veic_com"].ToString() != "")
                        clienteI.indVeiculoComunicacao = Convert.ToInt32(dr["A018_ind_veic_com"]);
                    if (dr["A018_ind_informal"] != DBNull.Value && dr["A018_ind_informal"] != DBNull.Value && dr["A018_ind_informal"].ToString() != "")
                        clienteI.indClienteInformal = Convert.ToInt32(dr["A018_ind_informal"]);
                    if (dr["A018_ind_cli_pot"] != DBNull.Value && dr["A018_ind_cli_pot"] != DBNull.Value && dr["A018_ind_cli_pot"].ToString() != "")
                        clienteI.IndPot = Convert.ToInt32(dr["A018_ind_cli_pot"]);
                    if (dr["A018_ind_cli_set"] != DBNull.Value && dr["A018_ind_cli_set"] != DBNull.Value && dr["A018_ind_cli_set"].ToString() != "")
                        clienteI.indSet = Convert.ToInt32(dr["A018_ind_cli_set"]);
                    if (dr["A018_entidade_credenciada"] != DBNull.Value && dr["A018_entidade_credenciada"] != DBNull.Value && dr["A018_entidade_credenciada"].ToString() != "")
                        clienteI.indEntidadeCredenciada = Convert.ToInt32(dr["A018_entidade_credenciada"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        clienteI.logUsuario = Convert.ToString(dr["usu_inc_alt"]); 
                    if (dr["A018_dt_incl"] != DBNull.Value && dr["A018_dt_incl"] != DBNull.Value && dr["A018_dt_incl"].ToString() != "")
                        clienteI.dataInclusao = Convert.ToDateTime(dr["A018_dt_incl"]);
                    if (dr["dta_inc_alt"] != DBNull.Value && dr["dta_inc_alt"] != DBNull.Value && dr["dta_inc_alt"].ToString() != "")
                        clienteI.dataUltAlteracao = Convert.ToDateTime(dr["dta_inc_alt"]);
                    if (dr["A018_DT_ENVIO"] != DBNull.Value && dr["A018_DT_ENVIO"] != DBNull.Value && dr["A018_DT_ENVIO"].ToString() != "")
                        clienteI.dataEnvio = Convert.ToDateTime(dr["A018_DT_ENVIO"]);

                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                clienteI = new ClienteIntegral();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return clienteI;
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
                new OracleParameter("curClienteIntegral", OracleType.Cursor),
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

        public static Paginacao LoadDataPaginacaoRelatorio(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter("curClienteIntegral", OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
              };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGRelatorio, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        
        #endregion

        #endregion

    }
}
