// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  confirm: {
      email: '',
      password: ''
  },
  baseUrl: 'http://localhost:26537/api/',
  aplicacao_id: 'XXXX',
  controllers: {
    admin: {
      lojas: 'lojas/',
      cargos: 'cargo/',
      colaboradores: 'colaborador/',
      produtos: 'produto/',
      opcoes: '',
      valoresopcoes: '',
      taxaconversao: 'taxaconversao/',
      verba: 'verba/',
      opcaoentrega: 'opcaoentrega/',
      configurar: 'configurar/'
    },
    admintec:
    { aplicacao: 'aplicacao/',
      menu: 'menu/',
      perfil: 'perfil/',
      meiodecompra: 'meiodecompra/',
      situacaoAvaliacao: 'situacaoavaliacao/',
      situacaoRecomendacao: 'situacaorecomendacao/',
      situacaoCompra: 'situacaocompra/',
      situacaoTroca: 'situacaotroca/',
      tiposOpcoes: 'tipoopcao/',
      tiposRecomendacoes: 'tiporecomendacao/',
      logs: 'logs/',
    },
    gestor:
    {
      avaliar: 'avaliar/',
      reconhecer: 'reconhecer/'
    },
    loja: {
      trocas: 'trocas/'
    },
    colaborador: {
      compra: 'compra/'
    }
  },
  routes: {
    common: {
      listar : 'listar',
      gravar : 'cadastrar',
      situacao : 'ativar-inativar',
      consultar : 'consultar',
      efetivar: 'efetivar'
    },
    admin: {},
    admintec: {},
    gestor: {},
    loja: {},
    colaborador: {},
  },
  enumns: {
    SituacaoRecomendacaoEnum: {
      Aprovada: '5468FA72-FCA6-4A92-8BAC-BFC9E971BEB6',
      Reprovada: 'A045A8FE-3674-475C-8454-380AE3CC9FB4',
      EmAnalise: '633B5B11-CF4B-4D44-A9F4-B39E38924BDE'
    },
    SituacaoAvaliacaoEnum: {
      Aprovada: '9FB43F1E-0F7C-4AB8-9FEF-E0AEB6C4265F',
      Reprovada: 'FE041110-BFD9-4F86-85CC-55CC31E9B68A',
      Pendente: 'C402396C-4938-4FAB-9CDF-6B94BE5C5802'
    },
    SituacaoCompraEnum: {
      SolicitacaoTroca: '6196698E-95D7-48CF-B07A-CEC3BDC15D18',
      EmAnaliseDaLoja: 'EBBC16C6-DC45-48DC-ACE7-1C31D7A8D909',
      Efetivada: '72C108D6-E972-437E-82A7-FE83CD54DD0A',
      Finalizada: '7D4E5C01-FF0D-433A-9A72-1BCD68612A5E',
      Cancelada: 'FDB7422B-521F-4970-9E24-FE5B4BC68C04',
      Recusada: 'AE254858-4843-493D-92B1-C198DAEFD088',
      ProdutosRecebidos: '69EA87EC-1D85-4F31-AA98-F3AE33F204FB'
    },
    MeioDeCompraEnum: {
      PortalWeb: '5CA79155-AF62-4E03-BB7D-5DFD7E4B5CA9',
      Aplicativo: 'AAE66B43-75B6-42F9-A228-63F7292A86F9',
      NaLoja: '2D2F7B07-9B3E-4E16-A097-BABBDD47551A'
    },
    OpcaoEntregaEnum: {
      RetirarNaLojaMaisProxima: '81CEBE67-D92E-4058-BD14-0214ECF69647',
      ReceberViaCorreio: 'DBDA8B89-6A0C-46EE-A3C1-19CEA9BD02C7',
      ReceberViaMalote: 'A38D36B8-C48C-4145-984D-BB3D707DB485',
      ReceberAtravesDoGestor: '2601ACF2-E36C-4A6A-8194-0BB9CDD4357A'
    }
  }
};
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
