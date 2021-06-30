using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T022_eventos
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
    public class Eventos
    {
        #region Atributos
        private int codEvento;
        private int? codEscritorio;
        private Pais codPais;
        private Estado codEstado;
        private Cidade codCidade;
        private TituloEvento codTituloEvento;
        private string codProdSeb;
        private int? anoRef;
        private int? mesRef;
        private string nomeEvento;
        private DateTime? dataInicioEv;
        private DateTime? dataFimEv;
        private string nomHorario;
        private int? numVaga;
        private decimal? prMicro;
        private decimal? prOutros;
        private int? indEvAberto;
        private int? indParceria;
        private int? indProc;
        private int? indMov;
        private int? numReq;
        private string emailMatApoio;
        private string emailMatDidatico;
        private int? indProcSig;
        private int? cdUsuario;
        private int? indEtiqEmit;
        private int? indEvEstadual;
        private int? numVagaCons;
        private int? indDivulgaInternet;
        private int? indInscricaoOnLine;
        private int? indModular;
        private int? numMaxCliente;
        private decimal? cargaHoraria;
        private int? codSetor;
        private string motivoCancel;
        private int? codCliPrm;
        private int? cliRestrito;
        private int? codPaisLocal;
        private int? codEstadoLocal;
        private int? codCidadeLocal;
        private int? codSetorial;
        private int? indPago;
        private int? indConfirmado;
        private int? eventoConfirmado;
        private string nomObservacao;
        private int? codGrupo;
        private int? codClassifica;
        private int? indCertificado;
        private int? codEvPrincipal;
        private decimal? valorMaterialEv;
        private int? codEspecifica;
        private string nomSiad;
        private int? numVagInt;
        private int? numTotVag;
        private decimal? valorEvento;
        private int? indValorFechado;
        private int? diaAntec;
        private DateTime? datafechaLc;
        private string codSol;
        private string codObj;
        private string codAb;
        private string codAe;
        private string codSebraeNA;
        private string cttCusto;
        private string cttUnida5;
        private int? codAbordagem;
        private int? codCategoria;
        private int? codInstrumento;
        private string nomTipo;
        private int? previsaoReceita;
        private int? codSiac;
        private DateTime? dataSiac;
        private string horaInicial;
        private string minutoInicial;
        private string nomApelido;
        private int indGdEv;

        private string local127;
        private string apoio127;
        private decimal? percParceiro127;
        private string localInfo127;
        private decimal? horascons127;
        private string obs127;

        private string logUsuario;
        private string sStringao;
        #endregion
        
        #region Propriedades
        public int? CliRestrito
        {
            get { return cliRestrito; }
            set { cliRestrito = value; }
        }
        public Pais CodPais
        {
            get { return codPais; }
            set { codPais = value; }
        }
        public int CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
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
        public int? CodEscritorio
        {
            get { return codEscritorio; }
            set { codEscritorio = value; }
        }
        public TituloEvento CodTituloEvento
        {
            get { return codTituloEvento; }
            set { codTituloEvento = value; }
        }
        public string CodProdSeb
        {
            get { return codProdSeb; }
            set { codProdSeb = value; }
        }
        public int? AnoRef
        {
            get { return anoRef; }
            set { anoRef = value; }
        }
        public int? MesRef
        {
            get { return mesRef; }
            set { mesRef = value; }
        }
        public string NomeEvento
        {
            get { return nomeEvento; }
            set { nomeEvento = value; }
        }
        public DateTime? DataInicioEv
        {
            get { return dataInicioEv; }
            set { dataInicioEv = value; }
        }
        public DateTime? DataFimEv
        {
            get { return dataFimEv; }
            set { dataFimEv = value; }
        }
        public string NomHorario
        {
            get { return nomHorario; }
            set { nomHorario = value; }
        }
        public int? NumVaga
        {
            get { return numVaga; }
            set { numVaga = value; }
        }
        public decimal? PrMicro
        {
            get { return prMicro; }
            set { prMicro = value; }
        }
        public decimal? PrOutros
        {
            get { return prOutros; }
            set { prOutros = value; }
        }
        public int? IndEvAberto
        {
            get { return indEvAberto; }
            set { indEvAberto = value; }
        }
        public int? IndParceria
        {
            get { return indParceria; }
            set { indParceria = value; }
        }
        public int? IndProc
        {
            get { return indProc; }
            set { indProc = value; }
        }
        public int? IndMov
        {
            get { return indMov; }
            set { indMov = value; }
        }
        public int? NumReq
        {
            get { return numReq; }
            set { numReq = value; }
        }
        public string EmailMatApoio
        {
            get { return emailMatApoio; }
            set { emailMatApoio = value; }
        }
        public string EmailMatDidatico
        {
            get { return emailMatDidatico; }
            set { emailMatDidatico = value; }
        }
        public int? IndProcSig
        {
            get { return indProcSig; }
            set { indProcSig = value; }
        }
        public int? CdUsuario
        {
            get { return cdUsuario; }
            set { cdUsuario = value; }
        }
        public int? IndEtiqEmit
        {
            get { return indEtiqEmit; }
            set { indEtiqEmit = value; }
        }
        public int? IndEvEstadual
        {
            get { return indEvEstadual; }
            set { indEvEstadual = value; }
        }
        public int? NumVagaCons
        {
            get { return numVagaCons; }
            set { numVagaCons = value; }
        }
        public int? IndDivulgaInternet
        {
            get { return indDivulgaInternet; }
            set { indDivulgaInternet = value; }
        }
        public int? IndInscricaoOnLine
        {
            get { return indInscricaoOnLine; }
            set { indInscricaoOnLine = value; }
        }
        public int? IndModular
        {
            get { return indModular; }
            set { indModular = value; }
        }
        public int? NumMaxCliente
        {
            get { return numMaxCliente; }
            set { numMaxCliente = value; }
        }
        public decimal? CargaHoraria
        {
            get { return cargaHoraria; }
            set { cargaHoraria = value; }
        }
        public int? CodSetor
        {
            get { return codSetor; }
            set { codSetor = value; }
        }
        public string MotivoCancel
        {
            get { return motivoCancel; }
            set { motivoCancel = value; }
        }
        public int? CodCliPrm
        {
            get { return codCliPrm; }
            set { codCliPrm = value; }
        }
        public int? CodPaisLocal
        {
            get { return codPaisLocal; }
            set { codPaisLocal = value; }
        }
        public int? CodEstadoLocal
        {
            get { return codEstadoLocal; }
            set { codEstadoLocal = value; }
        }
        public int? CodCidadeLocal
        {
            get { return codCidadeLocal; }
            set { codCidadeLocal = value; }
        }
        public int? CodSetorial
        {
            get { return codSetorial; }
            set { codSetorial = value; }
        }
        public int? IndPago
        {
            get { return indPago; }
            set { indPago = value; }
        }
        public int? IndConfirmado
        {
            get { return indConfirmado; }
            set { indConfirmado = value; }
        }
        public int? EventoConfirmado
        {
            get { return eventoConfirmado; }
            set { eventoConfirmado = value; }
        }
        public string NomObservacao
        {
            get { return nomObservacao; }
            set { nomObservacao = value; }
        }
        public int? CodGrupo
        {
            get { return codGrupo; }
            set { codGrupo = value; }
        }
        public int? CodClassifica
        {
            get { return codClassifica; }
            set { codClassifica = value; }
        }
        public int? IndCertificado
        {
            get { return indCertificado; }
            set { indCertificado = value; }
        }
        public int? CodEvPrincipal
        {
            get { return codEvPrincipal; }
            set { codEvPrincipal = value; }
        }
        public decimal? ValorMaterialEv
        {
            get { return valorMaterialEv; }
            set { valorMaterialEv = value; }
        }
        public int? CodEspecifica
        {
            get { return codEspecifica; }
            set { codEspecifica = value; }
        }
        public string NomSiad
        {
            get { return nomSiad; }
            set { nomSiad = value; }
        }
        public int? NumVagInt
        {
            get { return numVagInt; }
            set { numVagInt = value; }
        }
        public int? NumTotVag
        {
            get { return numTotVag; }
            set { numTotVag = value; }
        }
        public decimal? ValorEvento
        {
            get { return valorEvento; }
            set { valorEvento = value; }
        }
        public int? IndValorFechado
        {
            get { return indValorFechado; }
            set { indValorFechado = value; }
        }
        public int? DiaAntec
        {
            get { return diaAntec; }
            set { diaAntec = value; }
        }
        public DateTime? DatafechaLc
        {
            get { return datafechaLc; }
            set { datafechaLc = value; }
        }
        public string CodSol
        {
            get { return codSol; }
            set { codSol = value; }
        }
        public string CodObj
        {
            get { return codObj; }
            set { codObj = value; }
        }
        public string CodAb
        {
            get { return codAb; }
            set { codAb = value; }
        }
        public string CodAe
        {
            get { return codAe; }
            set { codAe = value; }
        }
        public string CodSebraeNA
        {
            get { return codSebraeNA; }
            set { codSebraeNA = value; }
        }
        public string CttCusto
        {
            get { return cttCusto; }
            set { cttCusto = value; }
        }
        public string CttUnida5
        {
            get { return cttUnida5; }
            set { cttUnida5 = value; }
        }
        public int? CodAbordagem
        {
            get { return codAbordagem; }
            set { codAbordagem = value; }
        }
        public int? CodCategoria
        {
            get { return codCategoria; }
            set { codCategoria = value; }
        }
        public int? CodInstrumento
        {
            get { return codInstrumento; }
            set { codInstrumento = value; }
        }
        public string NomTipo
        {
            get { return nomTipo; }
            set { nomTipo = value; }
        }
        public int? PrevisaoReceita
        {
            get { return previsaoReceita; }
            set { previsaoReceita = value; }
        }
        public int? CodSiac
        {
            get { return codSiac; }
            set { codSiac = value; }
        }
        public DateTime? DataSiac
        {
            get { return dataSiac; }
            set { dataSiac = value; }
        }
        public string HoraInicial
        {
            get { return horaInicial; }
            set { horaInicial = value; }
        }
        public string MinutoInicial
        {
            get { return minutoInicial; }
            set { minutoInicial = value; }
        }
        public string NomApelido
        {
            get { return nomApelido; }
            set { nomApelido = value; }
        }

        public string Local127
        {
            get { return local127; }
            set { local127 = value; }
        }
        public string Apoio127
        {
            get { return apoio127; }
            set { apoio127 = value; }
        }
        public decimal? PercParceiro127
        {
            get { return percParceiro127; }
            set { percParceiro127 = value; }
        }
        public string LocalInfo127
        {
            get { return localInfo127; }
            set { localInfo127 = value; }
        }
        public decimal? Horascons127
        {
            get { return horascons127; }
            set { horascons127 = value; }
        }
        public string Obs127
        {
            get { return obs127; }
            set { obs127 = value; }
        }

        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        public int IndGdEv
        {
            get { return indGdEv; }
            set { indGdEv = value; }
        }
        #endregion

        #region Construtores
        public Eventos()
            : this(-1)
        { }
        public Eventos(int codEvento)
        {
            this.codEvento = codEvento;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Eventos.EventosInc";
        private const string SPUPDATE = "Eventos.EventosAlt";
        private const string SPUPDATEConfirmacao = "Eventos.EventosConfirmacaoAlt";
        private const string SPUPDATEFechamento = "Eventos.EventosFechamentoAlt";
        private const string SPDELETE = "Eventos.EventosDel";
        private const string SPDELETEINSEV = "Eventos.EventosDelInsEv";
        private const string SPSELECTID = "Eventos.EventosSelId";
        private const string SPSELECTIDRelatorio = "Eventos.EventosSelIdRelatorio";
        
        private const string SPSELECTPagamentoID = "Eventos.EventosPagamentoSelId";
        //questionarios
        private const string SPSELECTQuestionarioID = "Eventos.EventosQuestionarioSelId";
        private const string SPSELECTQuestionarioCliSelId = "Eventos.EventosQuestionarioCliSelId";
        private const string SPSELECTQuestionarioResSelId = "Eventos.EventosQuestionarioResSelId";
        private const string SPDELETEQUEST = "Eventos.QuestionarioDel";
        private const string SPINSERTQUEST = "Eventos.QuestionarioInc";
        private const string SPINSERTQUESTNull = "Eventos.QuestionarioNullInc";
        private const string SPINSERTQUESTRESP = "Eventos.QuestionarioResInc";

        private const string SPSELECTINSTRUTOR = "Eventos.EventoInstrutorSelId";
        private const string SPSELEVINSHORACOMP = "Eventos.EvInstrHoraCompSelId";
        private const string SPSELECTHORASINSTRUTOR = "Eventos.EventoInstrutorDistribHorasId";
        private const string SPSELECTMESREF = "Eventos.EventoMesRef";
        private const string SPSELECTPAG = "Eventos.EventosSelPag";
        //justificados
        private const string SPSELECTPAGJus = "Eventos.EventoSelPagJus";
        //Atingidos
        private const string SPSELECTAtingido = "Eventos.EvAtingidoSelPag";
        private const string SPInsertAtingido = "Eventos.EvAtingidoInsert";
        //departamento
        private const string SPSELECTDepartamento = "Eventos.DepartamentoSelPag";
        private const string SPInsertDepartamento = "Eventos.DepartamentoInsert";
        
        //fechamento
        private const string SPFecharEvento = "Eventos.FecharEvento";

        private const string SPSELECTESCR = "Eventos.EventosSelEscr";
        private const string SPSELECTPRODESB = "Eventos.EventosSelProdSeb";
        private const string SPSELECESTPAIS= "Eventos.EventosSelEstPais";
        private const string SPSELECTINSTABORDCATEG = "Eventos.EventosSelInstAbordCateg";
        private const string SPSELECTVAGACARGA = "Eventos.EvSelVagaCarga";
        private const string SPSELECTEVFECHADOATUAL = "Eventos.EvFechadosAtuais";
        private const string SPSELECTTIPO = "Eventos.EvTipo";
        private const string SPSELECTESCRITORIO = "Eventos.EvEscritorio";
        private const string SPSELECTTITULO = "Eventos.EvTitulo";
        private const string SPIncluiReplicacaoId = "Eventos.IncluiReplicacaoId";
        private const string SPPesquisaCarta = "Eventos.PesquisaCarta";
        //Troca de DLL Pesquisa por ConsultarRecibo
        private const string SPConsultarRecibo = "Eventos.ConsultarRecibo";
        private const string SPGerar_Relatorio = "Eventos.Gerar_Relatorio";
        //para ConsultarDetalhesOS
        private const string SPConsultarDetalhesOS = "Eventos.TConsultarDetalhesOS";

        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codEvento";
        private const string PARMQUEST = "codQuest";
        private const string PARMCODIGOD = "codData";
        private const string PARMCODIGOI = "codInstrutor";
        private const string PARMCURSOR = "curEventos";
        #endregion

        #region Metodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            //parms = Context.DataBase.GetCachedParameters(SPINSERT);
            //if (parms == null)
            //{
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "codEscritorio", OracleType.Int32),
                    /*2*/ new OracleParameter( "codPais", OracleType.Int32),
                    /*3*/ new OracleParameter( "codEstado", OracleType.Int32),
                    /*4*/ new OracleParameter( "codCidade", OracleType.Int32),
                    /*5*/ new OracleParameter( "codTituloEvento", OracleType.Int32),
                    /*6*/ new OracleParameter( "codProdSeb", OracleType.VarChar),
                    /*7*/ new OracleParameter( "nomeEvento", OracleType.VarChar),
                    /*8*/ new OracleParameter( "dataInicioEv", OracleType.DateTime),
                    /*9*/ new OracleParameter( "dataFimEv", OracleType.DateTime),
                    /*10*/ new OracleParameter( "nomHorario", OracleType.VarChar),
                    /*11*/ new OracleParameter( "numVaga", OracleType.Int32),
                    /*12*/ new OracleParameter( "prMicro", OracleType.Float),
                    /*13*/ new OracleParameter( "prOutros", OracleType.Float),  
                    /*14*/ new OracleParameter( "numMaxCliente", OracleType.Int32),
                    /*15*/ new OracleParameter( "cargaHoraria", OracleType.Float),
                    /*16*/ new OracleParameter( "nomTipo", OracleType.VarChar),
                    /*17 18*/ new OracleParameter( "logUsuario", OracleType.VarChar),
                    /*18 17*/ new OracleParameter( "previsaoReceita", OracleType.Int32),
                    /*19 27*/ new OracleParameter( "numVagaCons", OracleType.Int32),
                    /*20 26*/ new OracleParameter( "indEvEstadual", OracleType.Int32),
                    /*21 28*/ new OracleParameter( "indDivulgaInternet", OracleType.Int32),
                    /*22 29*/ new OracleParameter( "indCertificado", OracleType.Int32),
                    /*23 39*/ new OracleParameter( "indProc", OracleType.Int32),
                    //Dados Adicionais 
                    /*24 22*/ new OracleParameter( "codCliPrm", OracleType.Int32),
                    /*25 23*/ new OracleParameter( "cliRestrito", OracleType.Int32),
                    /*26 24*/ new OracleParameter( "apoio127", OracleType.VarChar),
                    /*27 25*/ new OracleParameter( "localInfo127", OracleType.VarChar),
                    /*28 19*/ new OracleParameter( "local127", OracleType.VarChar),
                    /*29 20*/ new OracleParameter( "percParceiro127", OracleType.Float),
                    /*30 21*/ new OracleParameter( "horascons127", OracleType.Float),
                    //Exportação NA                    
                    
                    /*32 31*/ new OracleParameter( "codAbordagem", OracleType.Int32),
                    /*33 32*/ new OracleParameter( "codCategoria", OracleType.Int32),
                    /*31 30*/ new OracleParameter( "codInstrumento", OracleType.Int32),
                    /*34 33*/ new OracleParameter( "nomApelido", OracleType.VarChar),
                    //Exportação NA                    
                    /*35 34*/ new OracleParameter( "CodSol", OracleType.VarChar),
                    /*36 35*/ new OracleParameter( "CodObj", OracleType.VarChar),
                    //comercialização - --
                    /*37 36*/ new OracleParameter( "CodClassifica", OracleType.Int32), 
                    /*38 37*/ new OracleParameter( "codGrupo", OracleType.Int32) ,
                    //Centro de custos
                    /*39 38*/ new OracleParameter( "cttCusto", OracleType.VarChar),
                    /*novo*/ new OracleParameter( "indInscricaoOnLine", OracleType.Int32)
                };

                // Criando cache dos parameters 
                Context.DataBase.CacheParameters(SPINSERT, parms);
            //}
            return (parms);
        }

        public static OracleParameter[] GetParametersConfirmacao()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            // parms = Context.DataBase.GetCachedParameters(SPINSERT);
            //if (parms == null)
            //{
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "indConfirmado", OracleType.Int32)                };

                // Criando cache dos parameters 
                Context.DataBase.CacheParameters(SPINSERT, parms);
            //}
            return (parms);
        }

        public static OracleParameter[] GetParametersFechamento()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            //parms = Context.DataBase.GetCachedParameters(SPINSERT);
            //if (parms == null)
            //{
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /**/ new OracleParameter( "IndProc", OracleType.Int32),
                    new OracleParameter( "MesRef", OracleType.Int32),
                    new OracleParameter( "AnoRef", OracleType.Int32)
                
                };

                // Criando cache dos parameters 
                Context.DataBase.CacheParameters(SPINSERT, parms);
            //}
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codEvento;
            parms[1].Value = this.codEscritorio;
            parms[2].Value = this.codPais.CodPais;
            parms[3].Value = this.codEstado.CodEstado;
            parms[4].Value = this.codCidade.CodCidade;
            parms[5].Value = this.codTituloEvento.CodTituloEv;
            parms[6].Value = "";
            if (this.codProdSeb != null)
            { parms[6].Value = this.codProdSeb.ToUpper(); }
            parms[7].Value = "";
            if (this.nomeEvento != null)
            { parms[7].Value = this.nomeEvento.ToUpper(); }
            parms[8].Value = this.dataInicioEv;
            parms[9].Value = this.dataFimEv;
            parms[10].Value = "";
            if (this.nomHorario != null)
            { parms[10].Value = this.nomHorario; }
            parms[11].Value = "";
            if (this.numVaga != null)
            { parms[11].Value = this.numVaga; }
            parms[12].Value = DBNull.Value;
            if (this.prMicro != null)
            { parms[12].Value = this.prMicro; }
            parms[13].Value = DBNull.Value;
            if (this.prOutros != null)
            { parms[13].Value = this.prOutros; }
            parms[14].Value = DBNull.Value;
            if (this.numMaxCliente != null)
            { parms[14].Value = this.numMaxCliente; }
            parms[15].Value = DBNull.Value;
            if (this.cargaHoraria != null)
            { parms[15].Value = this.cargaHoraria; }
            parms[16].Value = "";
            if (this.nomTipo != null)
            { parms[16].Value = this.nomTipo.ToUpper(); }

            parms[17].Value = "";
            if (this.logUsuario != null)
            { parms[17].Value = this.logUsuario.ToUpper(); }
            parms[18].Value = DBNull.Value;
            if (this.previsaoReceita != null)
            { parms[18].Value = this.previsaoReceita; }
            parms[19].Value = DBNull.Value;
            if (this.numVagaCons != null)
                parms[19].Value = this.numVagaCons;
            parms[20].Value = DBNull.Value;
            if (this.IndEvEstadual != null)
            { parms[20].Value = this.IndEvEstadual; }
            parms[21].Value = this.indDivulgaInternet;
            parms[22].Value = 0;
            if (this.indCertificado != null)
                parms[22].Value = this.indCertificado;
            parms[23].Value = 0;
            if (this.IndProc != null)
                parms[23].Value = this.IndProc;
            //Dados Adicionais
            parms[24].Value = DBNull.Value;
            if (this.codCliPrm != null)
            { parms[24].Value = this.codCliPrm; }
            parms[25].Value = DBNull.Value;
            if (this.cliRestrito != null)
            { parms[25].Value = this.cliRestrito; }
            parms[26].Value = "";
            if (this.apoio127 != null)
            { parms[26].Value = this.apoio127.ToUpper(); }
            parms[27].Value = "";
            if (this.localInfo127 != null)
            { parms[27].Value = this.localInfo127.ToUpper(); }

            parms[28].Value = "";
            if (this.local127 != null)
            { parms[28].Value = this.local127.ToUpper(); }
            parms[29].Value = DBNull.Value;
            if (this.percParceiro127 != null)
            { parms[29].Value = this.percParceiro127; }
            parms[30].Value = DBNull.Value;
            if (this.horascons127 != null)
            { parms[30].Value = this.horascons127; }

            //Exportação NA
            parms[31].Value = this.codAbordagem;
            parms[32].Value = this.codCategoria;
            parms[33].Value = this.codInstrumento;

            //Evento Modular
            parms[34].Value = "";
            if (this.nomApelido != null)
            { parms[34].Value = this.nomApelido.ToUpper(); }

            //Exportação NA --
            parms[35].Value = "";
            if (this.CodSol != null)
            { parms[35].Value = this.CodSol; }
            parms[36].Value = "";
            if (this.CodObj != null)
            { parms[36].Value = this.CodObj; }

            //comercialização - --
            parms[37].Value = DBNull.Value;
            if (this.CodClassifica != null)
            { parms[37].Value = this.CodClassifica; }

            parms[38].Value = DBNull.Value;
            if (this.codGrupo != null)
            { parms[38].Value = this.codGrupo; }

            parms[39].Value = "";
            if (this.cttCusto != null)
            { parms[39].Value = this.cttCusto.ToUpper(); }
            
            parms[40].Value = this.indInscricaoOnLine;

            //help
            sStringao = parms[1].Value.ToString() + "###";
            sStringao += parms[2].Value.ToString() + "###";
            sStringao += parms[3].Value.ToString() + "###";
            sStringao += parms[4].Value.ToString() + "###";
            sStringao += parms[5].Value.ToString() + "###";
            sStringao += parms[6].Value.ToString() + "###";
            sStringao += parms[7].Value.ToString() + "###";
            sStringao += parms[8].Value.ToString() + "###";
            sStringao += parms[9].Value.ToString() + "###";
            sStringao += parms[10].Value.ToString() + "###";
            sStringao += parms[11].Value.ToString() + "###";
            sStringao += parms[12].Value.ToString() + "###";
            sStringao += parms[13].Value.ToString() + "###";
            sStringao += parms[14].Value.ToString() + "###";
            sStringao += parms[15].Value.ToString() + "###";
            sStringao += parms[16].Value.ToString() + "###";
            sStringao += parms[17].Value.ToString() + "###";
            sStringao += parms[18].Value.ToString() + "###";
            sStringao += parms[19].Value.ToString() + "###";
            sStringao += parms[20].Value.ToString() + "###";
            sStringao += parms[21].Value.ToString() + "###";
            sStringao += parms[22].Value.ToString() + "###";
            sStringao += parms[23].Value.ToString() + "###";
            sStringao += parms[24].Value.ToString() + "###";
            sStringao += parms[25].Value.ToString() + "###";
            sStringao += parms[26].Value.ToString() + "###";
            sStringao += parms[27].Value.ToString() + "###";
            sStringao += parms[28].Value.ToString() + "###";
            sStringao += parms[29].Value.ToString() + "###";
            sStringao += parms[30].Value.ToString() + "###";
            sStringao += parms[31].Value.ToString() + "###";
            sStringao += parms[32].Value.ToString() + "###";
            sStringao += parms[33].Value.ToString() + "###";
            sStringao += parms[34].Value.ToString() + "###";
            sStringao += parms[35].Value.ToString() + "###";
            sStringao += parms[36].Value.ToString() + "###";
            sStringao += parms[37].Value.ToString() + "###";
            sStringao += parms[38].Value.ToString() + "###";
            sStringao += parms[39].Value.ToString() + "###";

            if (this.codEvento < 0)
            {
                parms[0].Direction = ParameterDirection.Output;
            }
            else
            {
                parms[0].Direction = ParameterDirection.Input;
            }
        }

        public void SetParametersConfirmacao(OracleParameter[] parms)
        {
            parms[0].Value = this.codEvento;
            parms[1].Value = this.indConfirmado;

            if (this.codEvento < 0)
            {
                parms[0].Direction = ParameterDirection.Output;
            }
            else
            {
                parms[0].Direction = ParameterDirection.Input;
            }
        }

        public void SetParametersFechamento(OracleParameter[] parms)
        {
            parms[0].Value = this.codEvento;
            parms[1].Value = this.indProc;
            parms[2].Value = this.mesRef;
            parms[3].Value = this.anoRef;

            if (this.codEvento < 0)
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
                        codEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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

        public void UpdateConfirmacao(OracleTransaction trans)
        {
            // -------------------------------------------------------- 
            // Obtendo e setando os parâmetros 
            OracleParameter[] parms = GetParametersConfirmacao();
            SetParametersConfirmacao(parms);
            // -------------------------------------------------------- 
            try
            {
                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATEConfirmacao, parms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } // END UPDATE 

        public void UpdateFechamento(OracleTransaction trans)
        {
            // -------------------------------------------------------- 
            // Obtendo e setando os parâmetros 
            OracleParameter[] parms = GetParametersFechamento();
            SetParametersFechamento(parms);
            // -------------------------------------------------------- 
            try
            {
                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATEFechamento, parms);
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

        public static void DeleteQuestionario(int codevento, int codaval, int codquest)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("codEvento", OracleType.Int32, 4) ,
                new OracleParameter("codAval", OracleType.Int32, 4) ,
            new OracleParameter("codQuest", OracleType.Int32, 4) 
            };
            parms[0].Value = codevento;
            parms[1].Value = codaval;
            parms[2].Value = codquest;

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETEQUEST, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end 

        public static void InsertQuestionario(int codAval, int codQuest, int codCli, int codCont, int codInst, int codEvento)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("codAval", OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                new OracleParameter("codQuest", OracleType.Int32, 4) ,
                new OracleParameter("codCli", OracleType.Int32, 4) ,
                new OracleParameter("codCont", OracleType.Int32, 4) ,
                new OracleParameter("codInst", OracleType.Int32, 4) ,
                new OracleParameter("codEvento", OracleType.Int32, 4)
            };
            parms[0].Value = codAval;
            parms[1].Value = codQuest;
            parms[2].Value = codCli;
            parms[3].Value = codCont;
            parms[4].Value = codInst;
            parms[5].Value = codEvento;

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPINSERTQUEST, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end 

        public static void InsertQuestionario(int codAval, int codQuest, int codInst, int codEvento)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("codAval", OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                new OracleParameter("codQuest", OracleType.Int32, 4) ,
                new OracleParameter("codInst", OracleType.Int32, 4) ,
                new OracleParameter("codEvento", OracleType.Int32, 4)
            };
            parms[0].Value = codAval;
            parms[1].Value = codQuest;
            parms[2].Value = codInst;
            parms[3].Value = codEvento;

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPINSERTQUESTNull, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end 

        public static void InsertRespostaQuestionario(int codEvento, int codAval, int codQuest, int codPerg, int codResp, string dscResp)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("codEvento", OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                new OracleParameter("codAval", OracleType.Int32, 4) ,
                new OracleParameter("codQuest", OracleType.Int32, 4) ,
                new OracleParameter("codPerg", OracleType.Int32, 4) ,
                new OracleParameter("codResp", OracleType.Int32, 4) ,
                new OracleParameter("dscResp", OracleType.VarChar)
            };
            parms[0].Value = codEvento;
            parms[1].Value = codAval;
            parms[2].Value = codQuest;
            parms[3].Value = codPerg;
            parms[4].Value = codResp;
            parms[5].Value = dscResp;

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPINSERTQUESTRESP, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end 

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

        public static void DeleteInstrutorEvento(int codigo)
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
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETEINSEV, parms);
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

        public static OracleDataReader LoadDataDrRelatorio(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDRelatorio, param);
            return dr;
        }

        public static OracleDataReader LoadDataEventoInstrutorDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTINSTRUTOR, param);
            return dr;
        }

        //Busca Horas gastas com instrutor
        public static OracleDataReader LoadDataInsHoraCompDr(int data, int instrutor)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGOD, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODIGOI, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = data;
            param[1].Value = instrutor;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELEVINSHORACOMP, param);
            return dr;
        }

        public static OracleDataReader LoadDataEventoHorasInstrutorDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTHORASINSTRUTOR, param);
            return dr;
        }
         
        //replicacao inclui e select ao mesmo tempo
        public static OracleDataReader LoadDataIncluiReplicacao(int titulo, int evento, int cliente)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("nTituloEvento", OracleType.Int32, 4), 
                    new OracleParameter("nCodEvento", OracleType.Int32, 4), 
                    new OracleParameter("colNumCliente", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = titulo;
            param[1].Value = evento;
            param[2].Value = cliente;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPIncluiReplicacaoId, param);
            return dr;
        }

        //busca dados para carta
        public static OracleDataReader LoadDataPesquisaCarta(string sWhere)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar,5000),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = sWhere;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPPesquisaCarta, param);
            return dr;
        }

        //busca dados para recibo que era DLL
        public static OracleDataReader LoadDataConsultarRecibo(int lngCdAgencia, string strDtInicio, string strDtFim, int lngIdFatura, int lngCdRecibo, int lngCdTitulo, int lngCdEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("nlngCdAgencia", OracleType.Int32, 8),
                    new OracleParameter("sstrDtInicio", OracleType.VarChar,5000),
                    new OracleParameter("sstrDtFim", OracleType.VarChar,100),
                    new OracleParameter("nlngIdFatura", OracleType.Int32, 8),
                    new OracleParameter("nlngCdRecibo", OracleType.Int32, 8),
                    new OracleParameter("lngCdTitulo", OracleType.Int32, 8),
                    new OracleParameter("lngCdEvento", OracleType.Int32, 8),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = lngCdAgencia;
            param[1].Value = strDtInicio;
            param[2].Value = strDtFim;
            param[3].Value = lngIdFatura;
            param[4].Value = lngCdRecibo;
            param[5].Value = (lngCdTitulo == null || lngCdTitulo == 0 ? -1 : lngCdTitulo);
            param[6].Value = (lngCdEvento == null || lngCdEvento == 0 ? -1 : lngCdEvento);
            param[7].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPConsultarRecibo, param);
            return dr;
        }

        //busca dados para recibo que era DLL
        public static OracleDataReader LoadDataGerar_Relatorio(string Entidade, string dt_inicio, string dt_fim, string tp_relatorio, string agencia, string entidadeX, string grupo, string produto, string Tipo, int Federacao )
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("sEntidade", OracleType.VarChar,100),
                new OracleParameter("sdt_inicio", OracleType.VarChar,100),
                new OracleParameter("sdt_fim", OracleType.VarChar,100),
                new OracleParameter("stp_relatorio", OracleType.VarChar,100),
                new OracleParameter("sagencia", OracleType.VarChar,100),
                new OracleParameter("sentidadeX", OracleType.VarChar,100),
                new OracleParameter("sgrupo", OracleType.VarChar,100),
                new OracleParameter("sproduto", OracleType.VarChar,100),
                new OracleParameter("sTipo", OracleType.VarChar,100),
                    new OracleParameter("nFederacao", OracleType.Int32, 8),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = Entidade;
            param[1].Value =dt_inicio;
                param[2].Value =dt_fim;
                param[3].Value =tp_relatorio;
                param[4].Value =agencia;
                param[5].Value =entidadeX;
                param[6].Value =grupo;
                param[7].Value =produto;
                param[8].Value =Tipo;
                param[9].Value = Federacao;
            param[10].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPGerar_Relatorio, param);
            return dr;
        }

        public static OracleDataReader LoadDataConsultarDetalhesOS(string Evento, string Ordem, string Parcela)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("sEvento", OracleType.VarChar,100),
                    new OracleParameter("sOrdem", OracleType.VarChar,100),
                    new OracleParameter("sParcela", OracleType.VarChar,100),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = Evento;
            param[1].Value = Ordem;
            param[2].Value = Parcela;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPConsultarDetalhesOS, param);
            return dr;
        }

        public static OracleDataReader LoadDataEventoMesRef()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTMESREF, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrPagamento(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPagamentoID, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrQuestionario(int codigoQuestionario)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMQUEST, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigoQuestionario;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTQuestionarioID, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrCliQuest( int codigoQuestionario, int codigoCliente, int codigoContato, int codigoInstrutor, int codigoEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codQuest", OracleType.Int32, 4), 
                    new OracleParameter("codCli", OracleType.Int32, 4), 
                    new OracleParameter("codCont", OracleType.Int32, 4), 
                    new OracleParameter("codInst", OracleType.Int32, 4), 
                    new OracleParameter("codEvento", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigoQuestionario;
            param[1].Value = codigoCliente;
            param[2].Value = codigoContato;
            param[3].Value = codigoInstrutor;
            param[4].Value = codigoEvento;
            param[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTQuestionarioCliSelId, param);
            return dr;
        }

        public static OracleDataReader LoadDataDrResQuest(int codigoQuestionario, int codigoCliente, int codigoContato, int codigoInstrutor, int codigoEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codQuest", OracleType.Int32, 4), 
                    new OracleParameter("codCli", OracleType.Int32, 4), 
                    new OracleParameter("codCont", OracleType.Int32, 4), 
                    new OracleParameter("codInst", OracleType.Int32, 4), 
                    new OracleParameter("codEvento", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigoQuestionario;
            param[1].Value = codigoCliente;
            param[2].Value = codigoContato;
            param[3].Value = codigoInstrutor;
            param[4].Value = codigoEvento;
            param[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTQuestionarioResSelId, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Eventos</returns>
        public static Eventos GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Eventos ev = new Eventos();
            try
            {
                if (dr.Read())
                {
                    ev.codEvento = Convert.ToInt32(dr["A022_cd_ev"]);
                    ev.codEscritorio = Convert.ToInt32(dr["a004_cd_escr"]);
                    ev.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    ev.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    ev.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    ev.codTituloEvento = new TituloEvento(Convert.ToInt32(dr["a008_cd_tit_ev"]));
                    ev.codProdSeb = Convert.ToString(dr["a042_cd_prod_seb"]);
                    ev.anoRef = null;
                    if (dr["a022_ano_ref"] != DBNull.Value)
                    { ev.anoRef = Convert.ToInt32(dr["a022_ano_ref"]); }
                    if (dr["a022_mes_ref"] != DBNull.Value)
                    { ev.mesRef = Convert.ToInt32(dr["a022_mes_ref"]); }
                    ev.nomeEvento = Convert.ToString(dr["a022_nm_ev"]);
                    if (dr["a022_dt_inicio_ev"] != DBNull.Value)
                    { ev.dataInicioEv = Convert.ToDateTime(dr["a022_dt_inicio_ev"]); }
                    if (dr["a022_dt_fim_ev"] != DBNull.Value)
                    { ev.dataFimEv = Convert.ToDateTime(dr["a022_dt_fim_ev"]); }
                    ev.nomHorario = Convert.ToString(dr["a022_horario"]);
                    if (dr["a022_num_vag"] != DBNull.Value)
                    { ev.numVaga = Convert.ToInt32(dr["a022_num_vag"]); }
                    if (dr["a022_pr_micro"] != DBNull.Value)
                    { ev.prMicro = Convert.ToInt32(dr["a022_pr_micro"]); }
                    if (dr["a022_pr_outros"] != DBNull.Value)
                    { ev.prOutros = Convert.ToInt32(dr["a022_pr_outros"]); }
                    if (dr["a022_ind_ev_aberto"] != DBNull.Value)
                    { ev.indEvAberto = Convert.ToInt32(dr["a022_ind_ev_aberto"]); }
                    if (dr["a022_ind_parceria"] != DBNull.Value)
                    { ev.indParceria = Convert.ToInt32(dr["a022_ind_parceria"]); }
                    if (dr["a022_ind_proc"] != DBNull.Value)
                    { ev.indProc = Convert.ToInt32(dr["a022_ind_proc"]); }
                    if (dr["a022_ind_mov"] != DBNull.Value)
                    { ev.indMov = Convert.ToInt32(dr["a022_ind_mov"]); }
                    if (dr["a022_num_req"] != DBNull.Value)
                    { ev.numReq = Convert.ToInt32(dr["a022_num_req"]); }
                    ev.emailMatApoio = Convert.ToString(dr["a022_email_mat_apoio"]);
                    ev.emailMatDidatico = Convert.ToString(dr["a022_email_mat_didatico"]);
                    if (dr["a022_ind_proc_sig"] != DBNull.Value)
                    { ev.indProcSig = Convert.ToInt32(dr["a022_ind_proc_sig"]); }
                    if (dr["a052_cd_usuario"] != DBNull.Value)
                    { ev.cdUsuario = Convert.ToInt32(dr["a052_cd_usuario"]); }
                    if (dr["a022_ind_etiq_emit"] != DBNull.Value)
                    { ev.indEtiqEmit = Convert.ToInt32(dr["a022_ind_etiq_emit"]); }
                    if (dr["a022_ind_ev_estadual"] != DBNull.Value)
                    { ev.indEvEstadual = Convert.ToInt32(dr["a022_ind_ev_estadual"]); }
                    if (dr["a022_num_vag_cons"] != DBNull.Value)
                    { ev.numVagaCons = Convert.ToInt32(dr["a022_num_vag_cons"]); }
                    if (dr["a022_ind_divulga_internet"] != DBNull.Value)
                    { ev.indDivulgaInternet = Convert.ToInt32(dr["a022_ind_divulga_internet"]); }

                    if (dr["A022_ind_insc_online"] != DBNull.Value)
                    { ev.indInscricaoOnLine = Convert.ToInt32(dr["A022_ind_insc_online"]); }

                    if (dr["a022_ind_modular"] != DBNull.Value)
                    { ev.indModular = Convert.ToInt32(dr["a022_ind_modular"]); }
                    if (dr["a022_num_max_cli"] != DBNull.Value)
                    { ev.numMaxCliente = Convert.ToInt32(dr["a022_num_max_cli"]); }
                    if (dr["a022_carga_hor"] != DBNull.Value)
                    { ev.cargaHoraria = Convert.ToDecimal(dr["a022_carga_hor"]); }
                    if (dr["a171_cd_setor"] != DBNull.Value)
                    { ev.codSetor = Convert.ToInt32(dr["a171_cd_setor"]); }
                    ev.motivoCancel = Convert.ToString(dr["a022_motiv_canc"]);
                    if (dr["a012_cd_cli_prm"] != DBNull.Value)
                    { ev.codCliPrm = Convert.ToInt32(dr["a012_cd_cli_prm"]); }
                    if (dr["a022_cli_restrito"] != DBNull.Value)
                    { ev.cliRestrito = Convert.ToInt32(dr["a022_cli_restrito"]); }
                    if (dr["a035_cd_pais_local"] != DBNull.Value)
                    { ev.codPaisLocal = Convert.ToInt32(dr["a035_cd_pais_local"]); }
                    if (dr["a021_cd_est_local"] != DBNull.Value)
                    { ev.codEstadoLocal = Convert.ToInt32(dr["a021_cd_est_local"]); }
                    if (dr["a011_cd_cid_local"] != DBNull.Value)
                    { ev.codCidadeLocal = Convert.ToInt32(dr["a011_cd_cid_local"]); }
                    if (dr["a439_cd_setorial"] != DBNull.Value)
                    { ev.codSetorial = Convert.ToInt32(dr["a439_cd_setorial"]); }
                    if (dr["a022_ind_pago"] != DBNull.Value)
                    { ev.indPago = Convert.ToInt32(dr["a022_ind_pago"]); }
                    if (dr["a022_ind_confirmado"] != DBNull.Value)
                    { ev.indConfirmado = Convert.ToInt32(dr["a022_ind_confirmado"]); }
                    if (dr["a022_evento_confirmado"]!= DBNull.Value)
                    {ev.eventoConfirmado = Convert.ToInt32(dr["a022_evento_confirmado"]);}
                    ev.nomObservacao = Convert.ToString(dr["a022_observacao"]);
                    if (dr["a2213_cd_grupo"]!= DBNull.Value)
                    {ev.codGrupo = Convert.ToInt32(dr["a2213_cd_grupo"]);}
                    if (dr["a1150_cd_classifica"]!= DBNull.Value)
                    {ev.codClassifica = Convert.ToInt32(dr["a1150_cd_classifica"]);}
                    if (dr["a022_ind_certificado"]!= DBNull.Value)
                    {ev.indCertificado = Convert.ToInt32(dr["a022_ind_certificado"]);}
                    if (dr["a022_cd_ev_prin"] != DBNull.Value)
                    { ev.codEvPrincipal = Convert.ToInt32(dr["a022_cd_ev_prin"]); }
                    //if (dr["a022_vlr_mat_evento"]!= DBNull.Value)
                    //{ev.valorMaterialEv = Convert.ToInt32(dr["a022_vlr_mat_evento"]);}
                    //if (dr["a774_cd_especifica"]!= DBNull.Value)
                    //{ev.codEspecifica = Convert.ToInt32(dr["a774_cd_especifica"]);}
                    //ev.nomSiad = Convert.ToString(dr["a775_siad"]);
                    //if (dr["a022_num_vag_int"]!= DBNull.Value)
                    //{ev.numVagInt = Convert.ToInt32(dr["a022_num_vag_int"]);}
                    //if (dr["a022_num_tot_vag"]!= DBNull.Value)
                    //{ev.numTotVag = Convert.ToInt32(dr["a022_num_tot_vag"]);}
                    //if (dr["a022_vlr_evento"]!= DBNull.Value)
                    //{ev.valorEvento = Convert.ToInt32(dr["a022_vlr_evento"]);}
                    //if (dr["a022_ind_vlr_fechado"]!= DBNull.Value)
                    //{ev.indValorFechado = Convert.ToInt32(dr["a022_ind_vlr_fechado"]);}
                    //if (dr["a022_dia_antec"]!= DBNull.Value)
                    //{ev.diaAntec = Convert.ToInt32(dr["a022_dia_antec"]);}
                    if (dr["a022_dt_fech_lc"] != DBNull.Value)
                    { ev.datafechaLc = Convert.ToDateTime(dr["a022_dt_fech_lc"]); }
                    ev.codSol = Convert.ToString(dr["codsol"]);
                    ev.codObj = Convert.ToString(dr["codobj"]);
                    ev.codAb = Convert.ToString(dr["codab"]);
                    ev.codAe = Convert.ToString(dr["codae"]);
                    ev.codSebraeNA = Convert.ToString(dr["a022_cd_sebraena"]);
                    ev.cttCusto = Convert.ToString(dr["ctt_custo"]);
                    ev.cttUnida5 = Convert.ToString(dr["ctt_unida5"]);
                    if (dr["a783_cd_abordagem"]!= DBNull.Value)
                    {ev.codAbordagem = Convert.ToInt32(dr["a783_cd_abordagem"]);}
                    if (dr["a784_cd_categoria"]!= DBNull.Value)
                    {ev.codCategoria = Convert.ToInt32(dr["a784_cd_categoria"]);}
                    if (dr["a786_cd_instrumento"]!= DBNull.Value)
                    {ev.codInstrumento = Convert.ToInt32(dr["a786_cd_instrumento"]);}
                    ev.nomTipo = Convert.ToString(dr["a785_tipo"]);
                    if (dr["a022_previsao_receita"]!= DBNull.Value)
                    {ev.previsaoReceita = Convert.ToInt32(dr["a022_previsao_receita"]);}
                    if (dr["a022_cd_siac"] != DBNull.Value)
                    { ev.codSiac = Convert.ToInt32(dr["a022_cd_siac"]); }
                    if (dr["a022_dt_siac"] != DBNull.Value)
                    { ev.dataSiac = Convert.ToDateTime(dr["a022_dt_siac"]); }
                    ev.horaInicial = Convert.ToString(dr["a022_hora_inicial"]);
                    ev.minutoInicial = Convert.ToString(dr["a022_minuto_inicial"]);
                    ev.nomApelido = Convert.ToString(dr["a022_apelido"]);
                    ev.logUsuario = Convert.ToString(dr["usu_inc_alt"]);

                    ev.local127 = Convert.ToString(dr["a127_loc_realiz"]);
                    ev.apoio127 = Convert.ToString(dr["a127_apoio_de"]);
                    if (dr["a127_percent_parceiro"] != DBNull.Value)
                    { ev.percParceiro127 = Convert.ToDecimal(dr["a127_percent_parceiro"]);}
                    ev.localInfo127 = Convert.ToString(dr["a127_local_inform_inscr"]);
                    if (dr["a127_horas_consult"] != DBNull.Value)
                    { ev.horascons127 = Convert.ToDecimal(dr["a127_horas_consult"]); }
                    //ev.obs127 = Convert.ToString(dr["a127_obs"]);
                    if (dr["A008_ind_gd_evento"] != DBNull.Value)
                    { ev.indGdEv = Convert.ToInt32(dr["A008_ind_gd_evento"]); }
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ev = new Eventos();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ev;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Eventos</returns>
        public static Eventos GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Eventos ev = new Eventos();
            try
            {
                if (dr.Read())
                {
                    ev.codEvento = Convert.ToInt32(dr["A022_cd_ev"]);
                    ev.codEscritorio = Convert.ToInt32(dr["a004_cd_escr"]);
                    ev.codPais = new Pais(Convert.ToInt32(dr["A035_cd_pais"]));
                    ev.codEstado = new Estado(Convert.ToInt32(dr["A021_cd_est"]));
                    ev.codCidade = new Cidade(Convert.ToInt32(dr["A011_cd_cid"]));
                    ev.codTituloEvento = new TituloEvento(Convert.ToInt32(dr["a008_cd_tit_ev"]));
                    ev.codProdSeb = Convert.ToString(dr["a042_cd_prod_seb"]);
                    if (dr["a022_ano_ref"] != DBNull.Value)
                    { ev.anoRef = Convert.ToInt32(dr["a022_ano_ref"]); }
                    if (dr["a022_mes_ref"] != DBNull.Value)
                    { ev.mesRef = Convert.ToInt32(dr["a022_mes_ref"]); }
                    ev.nomeEvento = Convert.ToString(dr["a022_nm_ev"]);
                    if (dr["a022_dt_inicio_ev"] != DBNull.Value)
                    { ev.dataInicioEv = Convert.ToDateTime(dr["a022_dt_inicio_ev"]); }
                    if (dr["a022_dt_fim_ev"] != DBNull.Value)
                    { ev.dataFimEv = Convert.ToDateTime(dr["a022_dt_fim_ev"]); }
                    ev.nomHorario = Convert.ToString(dr["a022_horario"]);
                    if (dr["a022_num_vag"] != DBNull.Value)
                    { ev.numVaga = Convert.ToInt32(dr["a022_num_vag"]); }
                    if (dr["a022_pr_micro"] != DBNull.Value)
                    { ev.prMicro = Convert.ToInt32(dr["a022_pr_micro"]); }
                    if (dr["a022_pr_outros"] != DBNull.Value)
                    { ev.prOutros = Convert.ToInt32(dr["a022_pr_outros"]); }
                    if (dr["a022_ind_ev_aberto"] != DBNull.Value)
                    { ev.indEvAberto = Convert.ToInt32(dr["a022_ind_ev_aberto"]); }
                    if (dr["a022_ind_parceria"] != DBNull.Value)
                    { ev.indParceria = Convert.ToInt32(dr["a022_ind_parceria"]); }
                    if (dr["a022_ind_proc"] != DBNull.Value)
                    { ev.indProc = Convert.ToInt32(dr["a022_ind_proc"]); }
                    if (dr["a022_ind_mov"] != DBNull.Value)
                    { ev.indMov = Convert.ToInt32(dr["a022_ind_mov"]); }
                    if (dr["a022_num_req"] != DBNull.Value)
                    { ev.numReq = Convert.ToInt32(dr["a022_num_req"]); }
                    ev.emailMatApoio = Convert.ToString(dr["a022_email_mat_apoio"]);
                    ev.emailMatDidatico = Convert.ToString(dr["a022_email_mat_didatico"]);
                    if (dr["a022_ind_proc_sig"] != DBNull.Value)
                    { ev.indProcSig = Convert.ToInt32(dr["a022_ind_proc_sig"]); }
                    if (dr["a052_cd_usuario"] != DBNull.Value)
                    { ev.cdUsuario = Convert.ToInt32(dr["a052_cd_usuario"]); }
                    if (dr["a022_ind_etiq_emit"] != DBNull.Value)
                    { ev.indEtiqEmit = Convert.ToInt32(dr["a022_ind_etiq_emit"]); }
                    if (dr["a022_ind_ev_estadual"] != DBNull.Value)
                    { ev.indEvEstadual = Convert.ToInt32(dr["a022_ind_ev_estadual"]); }
                    if (dr["a022_num_vag_cons"] != DBNull.Value)
                    { ev.numVagaCons = Convert.ToInt32(dr["a022_num_vag_cons"]); }
                    if (dr["a022_ind_divulga_internet"] != DBNull.Value)
                    { ev.indDivulgaInternet = Convert.ToInt32(dr["a022_ind_divulga_internet"]); }

                    if (dr["A022_ind_insc_online"] != DBNull.Value)
                    { ev.indInscricaoOnLine = Convert.ToInt32(dr["A022_ind_insc_online"]); }

                    if (dr["a022_ind_modular"] != DBNull.Value)
                    { ev.indModular = Convert.ToInt32(dr["a022_ind_modular"]); }
                    if (dr["a022_num_max_cli"] != DBNull.Value)
                    { ev.numMaxCliente = Convert.ToInt32(dr["a022_num_max_cli"]); }
                    if (dr["a022_carga_hor"] != DBNull.Value)
                    { ev.cargaHoraria = Convert.ToDecimal(dr["a022_carga_hor"]); }
                    if (dr["a171_cd_setor"] != DBNull.Value)
                    { ev.codSetor = Convert.ToInt32(dr["a171_cd_setor"]); }
                    ev.motivoCancel = Convert.ToString(dr["a022_motiv_canc"]);
                    if (dr["a012_cd_cli_prm"] != DBNull.Value)
                    { ev.codCliPrm = Convert.ToInt32(dr["a012_cd_cli_prm"]); }
                    if (dr["a022_cli_restrito"] != DBNull.Value)
                    { ev.cliRestrito = Convert.ToInt32(dr["a022_cli_restrito"]); }
                    if (dr["a035_cd_pais_local"] != DBNull.Value)
                    { ev.codPaisLocal = Convert.ToInt32(dr["a035_cd_pais_local"]); }
                    if (dr["a021_cd_est_local"] != DBNull.Value)
                    { ev.codEstadoLocal = Convert.ToInt32(dr["a021_cd_est_local"]); }
                    if (dr["a011_cd_cid_local"] != DBNull.Value)
                    { ev.codCidadeLocal = Convert.ToInt32(dr["a011_cd_cid_local"]); }
                    if (dr["a439_cd_setorial"] != DBNull.Value)
                    { ev.codSetorial = Convert.ToInt32(dr["a439_cd_setorial"]); }
                    if (dr["a022_ind_pago"] != DBNull.Value)
                    { ev.indPago = Convert.ToInt32(dr["a022_ind_pago"]); }
                    if (dr["a022_ind_confirmado"] != DBNull.Value)
                    { ev.indConfirmado = Convert.ToInt32(dr["a022_ind_confirmado"]); }
                    if (dr["a022_evento_confirmado"] != DBNull.Value)
                    { ev.eventoConfirmado = Convert.ToInt32(dr["a022_evento_confirmado"]); }
                    ev.nomObservacao = Convert.ToString(dr["a022_observacao"]);
                    if (dr["a2213_cd_grupo"] != DBNull.Value)
                    { ev.codGrupo = Convert.ToInt32(dr["a2213_cd_grupo"]); }
                    if (dr["a1150_cd_classifica"] != DBNull.Value)
                    { ev.codClassifica = Convert.ToInt32(dr["a1150_cd_classifica"]); }
                    if (dr["a022_ind_certificado"] != DBNull.Value)
                    { ev.indCertificado = Convert.ToInt32(dr["a022_ind_certificado"]); }
                    if (dr["a022_cd_ev_prin"] != DBNull.Value)
                    { ev.codEvPrincipal = Convert.ToInt32(dr["a022_cd_ev_prin"]); }
                    //if (dr["a022_vlr_mat_evento"] != DBNull.Value)
                    //{ ev.valorMaterialEv = Convert.ToInt32(dr["a022_vlr_mat_evento"]); }
                    //if (dr["a774_cd_especifica"] != DBNull.Value)
                    //{ ev.codEspecifica = Convert.ToInt32(dr["a774_cd_especifica"]); }
                    //ev.nomSiad = Convert.ToString(dr["a775_siad"]);
                    //if (dr["a022_num_vag_int"] != DBNull.Value)
                    //{ ev.numVagInt = Convert.ToInt32(dr["a022_num_vag_int"]); }
                    //if (dr["a022_num_tot_vag"] != DBNull.Value)
                    //{ ev.numTotVag = Convert.ToInt32(dr["a022_num_tot_vag"]); }
                    //if (dr["a022_vlr_evento"] != DBNull.Value)
                    //{ ev.valorEvento = Convert.ToInt32(dr["a022_vlr_evento"]); }
                    //if (dr["a022_ind_vlr_fechado"] != DBNull.Value)
                    //{ ev.indValorFechado = Convert.ToInt32(dr["a022_ind_vlr_fechado"]); }
                    //if (dr["a022_dia_antec"] != DBNull.Value)
                    //{ ev.diaAntec = Convert.ToInt32(dr["a022_dia_antec"]); }
                    if (dr["a022_dt_fech_lc"] != DBNull.Value)
                    { ev.datafechaLc = Convert.ToDateTime(dr["a022_dt_fech_lc"]); }
                    ev.codSol = Convert.ToString(dr["codsol"]);
                    ev.codObj = Convert.ToString(dr["codobj"]);
                    ev.codAb = Convert.ToString(dr["codab"]);
                    ev.codAe = Convert.ToString(dr["codae"]);
                    ev.codSebraeNA = Convert.ToString(dr["a022_cd_sebraena"]);
                    ev.cttCusto = Convert.ToString(dr["ctt_custo"]);
                    ev.cttUnida5 = Convert.ToString(dr["ctt_unida5"]);
                    if (dr["a783_cd_abordagem"] != DBNull.Value)
                    { ev.codAbordagem = Convert.ToInt32(dr["a783_cd_abordagem"]); }
                    if (dr["a784_cd_categoria"] != DBNull.Value)
                    { ev.codCategoria = Convert.ToInt32(dr["a784_cd_categoria"]); }
                    if (dr["a786_cd_instrumento"] != DBNull.Value)
                    { ev.codInstrumento = Convert.ToInt32(dr["a786_cd_instrumento"]); }
                    ev.nomTipo = Convert.ToString(dr["a785_tipo"]);
                    if (dr["a022_previsao_receita"] != DBNull.Value)
                    { ev.previsaoReceita = Convert.ToInt32(dr["a022_previsao_receita"]); }
                    if (dr["a022_cd_siac"] != DBNull.Value)
                    { ev.codSiac = Convert.ToInt32(dr["a022_cd_siac"]); }
                    if (dr["a022_dt_siac"] != DBNull.Value)
                    { ev.dataSiac = Convert.ToDateTime(dr["a022_dt_siac"]); }
                    ev.horaInicial = Convert.ToString(dr["a022_hora_inicial"]);
                    ev.minutoInicial = Convert.ToString(dr["a022_minuto_inicial"]);
                    ev.nomApelido = Convert.ToString(dr["a022_apelido"]);
                    ev.logUsuario = Convert.ToString(dr["usu_inc_alt"]);

                    ev.local127 = Convert.ToString(dr["a127_loc_realiz"]);
                    ev.apoio127 = Convert.ToString(dr["a127_apoio_de"]);
                    ev.percParceiro127 = Convert.ToDecimal(dr["a127_percent_parceiro"]);
                    ev.localInfo127 = Convert.ToString(dr["a127_local_inform_inscr"]);
                    ev.horascons127 = Convert.ToDecimal(dr["a127_horas_consult"]);
                    //ev.obs127 = Convert.ToString(dr["a127_obs"]);
                    if (dr["A008_ind_gd_evento"] != DBNull.Value)
                    { ev.indGdEv = Convert.ToInt32(dr["A008_ind_gd_evento"]); }
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ev = new Eventos();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ev;
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

        public static Context.Paginacao LoadDataPaginacaoJustificado(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAGJus, parms);

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static Context.Paginacao LoadDataAtingido(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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
            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTAtingido, parms);
            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static Context.Paginacao LoadDataDepartamento(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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
            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTDepartamento, parms);
            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }

        public static OracleDataReader InsertDepartamento(string sInsertUpdate, string sa236_cd_depto, string sCODSOL, string sCODOBJ)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("sInsertUpdate", OracleType.VarChar,100),
                    new OracleParameter("sa236_cd_depto", OracleType.VarChar,100),
                    new OracleParameter("sCODSOL", OracleType.VarChar,100),
                    new OracleParameter("sCODOBJ", OracleType.VarChar,100),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };
            param[0].Value = sInsertUpdate;
            param[1].Value = sa236_cd_depto;
            param[2].Value = sCODSOL;
            param[3].Value = sCODOBJ;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPInsertDepartamento, param);
            return dr;
        }

        public static OracleDataReader InsertAtingido(string sInsertUpdate, string sA522_CD_EV, string sA522_NM_EV, string sA522_DT_INICIO_EV, string sA522_DT_FIM_EV, string sA522_ATINGIDAS
            , string sCODSOL , string sCODOBJ, string sA522_CD_CATEGORIA, string sA522_TIPO, string sA004_cd_escr, string sA522_CdCanalInfor)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("sInsertUpdate", OracleType.VarChar,100),
                    new OracleParameter("sA522_CD_EV", OracleType.VarChar,100),
                    new OracleParameter("sA522_NM_EV", OracleType.VarChar,100),
                    new OracleParameter("sA522_DT_INICIO_EV", OracleType.VarChar,100),
                    new OracleParameter("sA522_DT_FIM_EV", OracleType.VarChar,100),
                    new OracleParameter("sA522_ATINGIDAS", OracleType.VarChar,100),
                    new OracleParameter("sCODSOL", OracleType.VarChar,100),
                    new OracleParameter("sCODOBJ", OracleType.VarChar,100),
                    new OracleParameter("sA522_CD_CATEGORIA", OracleType.VarChar,100),
                    new OracleParameter("sA522_TIPO", OracleType.VarChar,100),
                    new OracleParameter("sA004_cd_escr", OracleType.VarChar,100),
                    new OracleParameter("sA522_CdCanalInfor", OracleType.VarChar,100),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };
            param[0].Value = sInsertUpdate;
            param[1].Value = sA522_CD_EV;
            param[2].Value = sA522_NM_EV;
            param[3].Value = sA522_DT_INICIO_EV;
            param[4].Value = sA522_DT_FIM_EV;
            param[5].Value = sA522_ATINGIDAS;
            param[6].Value = sCODSOL;
            param[7].Value = sCODOBJ;
            param[8].Value = sA522_CD_CATEGORIA;
            param[9].Value = sA522_TIPO;
            param[10].Value = sA004_cd_escr;
            param[11].Value = sA522_CdCanalInfor;
            param[12].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPInsertAtingido, param);
            return dr;
        }

        public static OracleDataReader FecharEvento(string strusuario, string senha, string lngcdevento, string strparcelas)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter("strusuario", OracleType.VarChar,100),
                    new OracleParameter("senha", OracleType.VarChar,100),
                    new OracleParameter("lngcdevento", OracleType.VarChar,100),
                    new OracleParameter("strparcelas", OracleType.VarChar,100),
                    
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };
            param[0].Value = strusuario;
            param[1].Value = senha;
            param[2].Value = lngcdevento;
            param[3].Value = strparcelas;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPFecharEvento, param);
            return dr;
        }

        #endregion

        #endregion

        #region Metodos Especificos

        #region LoadDataDdlEscr
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlEscr()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTESCR, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlEscr(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
};

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTESCR, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlProdSebrae
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlProdSebrae()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPRODESB, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlProdSebrae(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
};

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTPRODESB, param);
            return dr;
        }

        #endregion

        #region LoadDataEstpais
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataEstpais(string nomCidade,string nomEstado,string nomPais)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("nomCidade", OracleType.VarChar),
                    new OracleParameter("nomEstado", OracleType.VarChar),
                    new OracleParameter("nomPais", OracleType.VarChar),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = nomCidade;
            param[1].Value = nomEstado;
            param[2].Value = nomPais;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECESTPAIS, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataEstpais(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
};

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECESTPAIS, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlInstAbordCateg
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlInstAbordCateg(string where)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = where;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTINSTABORDCATEG, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlInstAbordCateg(string where, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("sWhere", OracleType.VarChar),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = where;
            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTINSTABORDCATEG, param);
            return dr;
        }

        #endregion

        #region LoadDataVagaCargaEM
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataVagaCargaEM(int codEventoPrin, int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codEventoPrin", OracleType.Int32, 8),
                    new OracleParameter("codEvento", OracleType.Int32, 8),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEventoPrin;
            param[1].Value = codEvento;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTVAGACARGA, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataVagaCargaEM(int codEventoPri, int codEvento, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codEventoPrin", OracleType.Int32, 8),
                    new OracleParameter("codEvento", OracleType.Int32, 8),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEventoPri;
            param[1].Value = codEvento;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTVAGACARGA, param);
            return dr;
        }

        #endregion

        #region LoadDataEventosInscritos
        /// <summary>
        /// Popula o DropDownList Eventos com a where A022_IND_PROC = 0 e EXISTS ( SELECT FROM V001_ev_todas_inscricoes
        /// </summary>
        /// <returns></returns>
        public static Context.Paginacao LoadDataEventosInscritos()
        {
            string where = " and NVL( A022_IND_PROC, 0 ) = 0 AND EXISTS ( SELECT A022_CD_EV FROM V001_ev_todas_inscricoes V001 WHERE T022.A022_cd_ev = V001.A022_cd_ev )";
            //" AND a022_Cd_ev_prin=" + CodigoEvento + " AND nvl(A022_ind_modular,0)=2 AND nvl(A022_ind_proc,0)=0";
            Context.Paginacao drEv = Eventos.LoadDataPaginacao(where, 1, 1000, "A022_nm_ev");

            return drEv;
        }

        #endregion

        #region LoadDataEventosAbertosFechado
        /// <summary>
        /// Popula o DropDownList Eventos abertos com dr
        /// indAberto = 0 para aberto ou fechados
        /// indAberto = 1 para fechado
        /// </summary>
        /// <param name="indAberto"> 0 para aberto e 1 para fechado</param>
        /// <param name="whereFiltro">where para filtrar. filtrar por titulo por exemplo</param>
        /// <returns> paginação de eventos aberto ou fechados</returns>
        public static Context.Paginacao LoadDataEventosAbertosFechado(int indAberto, string whereFiltro)
        {
            string where = " and NVL( A022_IND_PROC, 0 ) = " + indAberto + whereFiltro;
            //" AND a022_Cd_ev_prin=" + CodigoEvento + " AND nvl(A022_ind_modular,0)=2 AND nvl(A022_ind_proc,0)=0";
            Context.Paginacao drEv = Eventos.LoadDataPaginacao(where, 1, 1000, "A022_nm_ev");

            return drEv;
        }
        #endregion

        #region LoadDataEventosFechadosAtual
        /// <summary>
        /// Popula o DropDownList Eventos fechados com dr
        /// com eventos atuais de 1 ano
        /// somente ev ativos
        ///  select sem paginação
        /// retorno somente coluna titEv do evento ativo
        /// </summary>
        /// <param name="indAberto"></param>
        /// <returns> dr com tituloEv do evento</returns>
        public static OracleDataReader LoadDataEventosFechadosAtual()
        {
            OracleParameter[] param = new OracleParameter[] {  
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTEVFECHADOATUAL, param);

            return dr;
        }

        #endregion

        #region LoadDataDdlTipo
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlTipo()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTTIPO, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlTipo(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTTIPO, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlEscritorio
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlEscritorio()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTESCRITORIO, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlEscritorio(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTESCRITORIO, param);
            return dr;
        }

        #endregion

        #region LoadDataDdlTitulo
        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlTitulo()
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTTITULO, param);
            return dr;
        }


        /// <summary>
        /// Popula o DropDownList Eventos
        /// </summary>
        /// <returns></returns>
        public static OracleDataReader LoadDataDdlTitulo(OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] {
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTTITULO, param);
            return dr;
        }

        #endregion

        #endregion

    }
}
