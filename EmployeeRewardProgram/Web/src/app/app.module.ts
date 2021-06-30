import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { APP_BASE_HREF } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TiposOpcoesComponent } from './perfil/admin-tec/tipos-opcoes/tipos-opcoes.component';
import { AplicacaoComponent } from './perfil/admin-tec/aplicacao/aplicacao.component';
import { LogsComponent } from './perfil/admin-tec/logs/logs.component';
import { MenusComponent } from './perfil/admin-tec/menus/menus.component';
import { PerfilAcessoComponent } from './perfil/admin-tec/perfil-acesso/perfil-acesso.component';
import { SituacaoAvaliacaoComponent } from './perfil/admin-tec/situacao-avaliacao/situacao-avaliacao.component';
import { SituacaoRecomendacaoComponent } from './perfil/admin-tec/situacao-recomendacao/situacao-recomendacao.component';
import { SituacaoCompraComponent } from './perfil/admin-tec/situacao-compra/situacao-compra.component';
import { SituacaoTrocaComponent } from './perfil/admin-tec/situacao-troca/situacao-troca.component';
import { TipoRecomendacaoComponent } from './perfil/admin-tec/tipo-recomendacao/tipo-recomendacao.component';
import { MeioCompraComponent } from './perfil/admin-tec/meio-compra/meio-compra.component';
import { LojasComponent } from './perfil/admin/lojas/lojas.component';
import { CargosComponent } from './perfil/admin/cargos/cargos.component';
import { ColaboradoresComponent } from './perfil/admin/colaboradores/colaboradores.component';
import { ProdutosComponent } from './perfil/admin/produtos/produtos.component';
import { OpcoesComponent } from './perfil/admin/produtos/opcoes/opcoes.component';
import { OpcoesValoresComponent } from './perfil/admin/produtos/opcoes-valores/opcoes-valores.component';
import { TaxaConversaoComponent } from './perfil/admin/taxa-conversao/taxa-conversao.component';
import { VerbaComponent } from './perfil/admin/verba/verba.component';
import { OpcoesEntregaComponent } from './perfil/admin/opcoes-entrega/opcoes-entrega.component';
import { ReconhecerComponent } from './perfil/gestor/reconhecer/reconhecer.component';
import { EquipeComponent } from './perfil/gestor/reconhecer/equipe/equipe.component';
import { OutraEquipeComponent } from './perfil/gestor/reconhecer/outra-equipe/outra-equipe.component';
import { HistoricoRecomendacoesComponent } from './perfil/gestor/historico-recomendacoes/historico-recomendacoes.component';
import { RelatorioTrocaPontosComponent } from './perfil/gestor/relatorio-colaboradores/troca-pontos/troca-pontos.component';
import { RelatorioQtdePontosComponent } from './perfil/gestor/relatorio-colaboradores/qtde-pontos/qtde-pontos.component';
// tslint:disable-next-line: max-line-length
import { RelatorioHistoricoAtribuicoesComponent } from './perfil/gestor/relatorio-colaboradores/historico-atribuicoes/historico-atribuicoes.component';
import { AvaliarComponent } from './perfil/gestor/avaliar/avaliar.component';
import { SolicitacoesComponent } from './perfil/loja/solicitacoes/solicitacoes.component';
import { HistoricoTrocasComponent } from './perfil/loja/historico-trocas/historico-trocas.component';
import { MeusPontosComponent } from './perfil/colaborador/meus-pontos/meus-pontos.component';
import { DashboardComponent } from './perfil/colaborador/dashboard/dashboard.component';
import { TrocarPontosComponent } from './perfil/colaborador/trocar-pontos/trocar-pontos.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ConfigurarVerbasComponent } from './perfil/admin/configurar-verbas/configurar-verbas.component';
import { ConfigurarExpiracaoComponent } from './perfil/admin/configurar-expiracao/configurar-expiracao.component';
import { TrocarProdutosComponent } from './perfil/loja/trocar-produtos/trocar-produtos.component';
import { RealizarVendaComponent } from './perfil/loja/realizar-venda/realizar-venda.component';
import { AplicacaoNovoComponent } from './perfil/admin-tec/aplicacao/aplicacao-novo/aplicacao-novo.component';
import { MeioCompraNovoComponent } from './perfil/admin-tec/meio-compra/meio-compra-novo/meio-compra-novo.component';
import { MenuNovoComponent } from './perfil/admin-tec/menus/menu-novo/menu-novo.component';
import { PerfilAcessoNovoComponent } from './perfil/admin-tec/perfil-acesso/perfil-acesso-novo/perfil-acesso-novo.component';
// tslint:disable-next-line: max-line-length
import { SituacaoAvaliacaoNovoComponent } from './perfil/admin-tec/situacao-avaliacao/situacao-avaliacao-novo/situacao-avaliacao-novo.component';
import { SituacaoCompraNovoComponent } from './perfil/admin-tec/situacao-compra/situacao-compra-novo/situacao-compra-novo.component';
// tslint:disable-next-line: max-line-length
import { SituacaoRecomendacaoNovoComponent } from './perfil/admin-tec/situacao-recomendacao/situacao-recomendacao-novo/situacao-recomendacao-novo.component';
import { SituacaoTrocaNovoComponent } from './perfil/admin-tec/situacao-troca/situacao-troca-novo/situacao-troca-novo.component';
// tslint:disable-next-line: max-line-length
import { TipoRecomendacaoNovoComponent } from './perfil/admin-tec/tipo-recomendacao/tipo-recomendacao-novo/tipo-recomendacao-novo.component';
import { TiposOpcoesNovoComponent } from './perfil/admin-tec/tipos-opcoes/tipos-opcoes-novo/tipos-opcoes-novo.component';
// tslint:disable-next-line: max-line-length
import { LojasNovoComponent } from './perfil/admin/lojas/lojas-novo/lojas-novo.component';
import { OpcoesEntregaNovoComponent } from './perfil/admin/opcoes-entrega/opcoes-entrega-novo/opcoes-entrega-novo.component';
import { ProdutosNovoComponent } from './perfil/admin/produtos/produtos-novo/produtos-novo.component';
import { TaxaConversaoNovoComponent } from './perfil/admin/taxa-conversao/taxa-conversao-novo/taxa-conversao-novo.component';
import { VerbaNovoComponent } from './perfil/admin/verba/verba-novo/verba-novo.component';
import { OpcoesNovoComponent } from './perfil/admin/produtos/opcoes/opcoes-novo/opcoes-novo.component';
import { OpcoesValoresNovoComponent } from './perfil/admin/produtos/opcoes-valores/opcoes-valores-novo/opcoes-valores-novo.component';
import { NgxUiLoaderModule, NgxUiLoaderHttpModule,
  NgxUiLoaderRouterModule, NgxUiLoaderConfig,
  SPINNER, POSITION, PB_DIRECTION, NgxUiLoaderRouterConfig } from 'ngx-ui-loader';
import { DetalheRecomendacaoComponent } from './perfil/gestor/historico-recomendacoes/detalhe-recomendacao/detalhe-recomendacao.component';
import { ListarAvaliacoesComponent } from './perfil/gestor/avaliar/listar-avaliacoes/listar-avaliacoes.component';
import { MinhasTrocasComponent } from './perfil/colaborador/minhas-trocas/minhas-trocas.component';
import { VerTaxaConversaoComponent } from './perfil/gestor/ver-taxa-conversao/ver-taxa-conversao.component';
import { VerProdutoComponent } from './perfil/colaborador/trocar-pontos/ver-produto/ver-produto.component';
import { DetalheSolicitacaoComponent } from './perfil/loja/solicitacoes/detalhe-solicitacao/detalhe-solicitacao.component';
import { ListarProdutosComponent } from './perfil/loja/realizar-venda/listar-produtos/listar-produtos.component';
import { EfetivarVendaComponent } from './perfil/loja/realizar-venda/efetivar-venda/efetivar-venda.component';
import { RelatoriosComponent } from './perfil/loja/relatorios/relatorios.component';
import { TrocasEfetuadasComponent } from './perfil/loja/relatorios/trocas-efetuadas/trocas-efetuadas.component';
import { SolicitacoesFaturamentoComponent } from './perfil/admin/faturamento/solicitacoes-faturamento/solicitacoes-faturamento.component';
import { UsuarioLojaComponent } from './perfil/loja/usuario-loja/usuario-loja.component';
import { CadastroUsuarioLojaComponent } from './perfil/loja/usuario-loja/cadastro-usuario-loja/cadastro-usuario-loja.component';

const ngxUiLoaderConfig: NgxUiLoaderConfig = {
    bgsColor: '#OOACC1',
    bgsOpacity: 0.5,
    bgsPosition: POSITION.bottomCenter,
    bgsSize: 60,
    bgsType: SPINNER.threeStrings,
    fgsColor: '#00ACC1',
    fgsPosition: POSITION.centerCenter,
    fgsSize: 60,
    fgsType: SPINNER.threeStrings,
    logoUrl: '',
    pbColor: '#FF0000',
    pbDirection: PB_DIRECTION.leftToRight,
    pbThickness: 5,
    text: 'Preparando tudo...',
    textColor: '#FFFFFF',
    textPosition: POSITION.centerCenter
  };

const ngxUiLoaderRouterConfig: NgxUiLoaderRouterConfig = {
  showForeground: true
};
@NgModule({
  declarations: [
    AppComponent,
    TiposOpcoesComponent,
    AplicacaoComponent,
    LogsComponent,
    MenusComponent,
    PerfilAcessoComponent,
    SituacaoAvaliacaoComponent,
    SituacaoRecomendacaoComponent,
    SituacaoCompraComponent,
    SituacaoTrocaComponent,
    TipoRecomendacaoComponent,
    MeioCompraComponent,
    LojasComponent,
    CargosComponent,
    ColaboradoresComponent,
    ProdutosComponent,
    OpcoesComponent,
    OpcoesValoresComponent,
    TaxaConversaoComponent,
    VerbaComponent,
    OpcoesEntregaComponent,
    ReconhecerComponent,
    EquipeComponent,
    OutraEquipeComponent,
    HistoricoRecomendacoesComponent,
    RelatorioTrocaPontosComponent,
    RelatorioQtdePontosComponent,
    RelatorioHistoricoAtribuicoesComponent,
    AvaliarComponent,
    SolicitacoesComponent,
    HistoricoTrocasComponent,
    MeusPontosComponent,
    DashboardComponent,
    TrocarPontosComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ConfigurarVerbasComponent,
    ConfigurarExpiracaoComponent,
    TrocarProdutosComponent,
    RealizarVendaComponent,
    AplicacaoNovoComponent,
    MeioCompraNovoComponent,
    MenuNovoComponent,
    PerfilAcessoNovoComponent,
    SituacaoAvaliacaoNovoComponent,
    SituacaoCompraNovoComponent,
    SituacaoRecomendacaoNovoComponent,
    SituacaoTrocaNovoComponent,
    TipoRecomendacaoNovoComponent,
    TiposOpcoesNovoComponent,
    LojasNovoComponent,
    OpcoesEntregaNovoComponent,
    ProdutosNovoComponent,
    TaxaConversaoNovoComponent,
    VerbaNovoComponent,
    OpcoesNovoComponent,
    OpcoesValoresNovoComponent,
    DetalheRecomendacaoComponent,
    ListarAvaliacoesComponent,
    MinhasTrocasComponent,
    VerTaxaConversaoComponent,
    VerProdutoComponent,
    DetalheSolicitacaoComponent,
    ListarProdutosComponent,
    EfetivarVendaComponent,
    RelatoriosComponent,
    TrocasEfetuadasComponent,
    SolicitacoesFaturamentoComponent,
    UsuarioLojaComponent,
    CadastroUsuarioLojaComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    NgxUiLoaderHttpModule.forRoot(ngxUiLoaderConfig),
    NgxUiLoaderHttpModule.forRoot({ showForeground: true }),
    NgxUiLoaderRouterModule.forRoot(ngxUiLoaderRouterConfig)
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: APP_BASE_HREF, useValue: '/web' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
