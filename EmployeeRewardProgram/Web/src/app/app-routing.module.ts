import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { AuthGuard } from './_helpers';
import { AplicacaoComponent } from './perfil/admin-tec/aplicacao/aplicacao.component';
import { MenusComponent } from './perfil/admin-tec/menus/menus.component';
import { PerfilAcessoComponent } from './perfil/admin-tec/perfil-acesso/perfil-acesso.component';
import { MeioCompraComponent } from './perfil/admin-tec/meio-compra/meio-compra.component';
import { SituacaoAvaliacaoComponent } from './perfil/admin-tec/situacao-avaliacao/situacao-avaliacao.component';
import { SituacaoRecomendacaoComponent } from './perfil/admin-tec/situacao-recomendacao/situacao-recomendacao.component';
import { SituacaoCompraComponent } from './perfil/admin-tec/situacao-compra/situacao-compra.component';
import { SituacaoTrocaComponent } from './perfil/admin-tec/situacao-troca/situacao-troca.component';
import { TiposOpcoesComponent } from './perfil/admin-tec/tipos-opcoes/tipos-opcoes.component';
import { TipoRecomendacaoComponent } from './perfil/admin-tec/tipo-recomendacao/tipo-recomendacao.component';
import { LogsComponent } from './perfil/admin-tec/logs/logs.component';
import { LojasComponent } from './perfil/admin/lojas/lojas.component';
import { CargosComponent } from './perfil/admin/cargos/cargos.component';
import { ColaboradoresComponent } from './perfil/admin/colaboradores/colaboradores.component';
import { ProdutosComponent } from './perfil/admin/produtos/produtos.component';
import { OpcoesComponent } from './perfil/admin/produtos/opcoes/opcoes.component';
import { OpcoesValoresComponent } from './perfil/admin/produtos/opcoes-valores/opcoes-valores.component';
import { TaxaConversaoComponent } from './perfil/admin/taxa-conversao/taxa-conversao.component';
import { VerbaComponent } from './perfil/admin/verba/verba.component';
import { OpcoesEntregaComponent } from './perfil/admin/opcoes-entrega/opcoes-entrega.component';
import { ConfigurarVerbasComponent } from './perfil/admin/configurar-verbas/configurar-verbas.component';
import { ConfigurarExpiracaoComponent } from './perfil/admin/configurar-expiracao/configurar-expiracao.component';
import { EquipeComponent } from './perfil/gestor/reconhecer/equipe/equipe.component';
import { OutraEquipeComponent } from './perfil/gestor/reconhecer/outra-equipe/outra-equipe.component';
import { ReconhecerComponent } from './perfil/gestor/reconhecer/reconhecer.component';
import { HistoricoRecomendacoesComponent } from './perfil/gestor/historico-recomendacoes/historico-recomendacoes.component';
import { RelatorioTrocaPontosComponent } from './perfil/gestor/relatorio-colaboradores/troca-pontos/troca-pontos.component';
import { RelatorioQtdePontosComponent } from './perfil/gestor/relatorio-colaboradores/qtde-pontos/qtde-pontos.component';
// tslint:disable-next-line: max-line-length
import { RelatorioHistoricoAtribuicoesComponent } from './perfil/gestor/relatorio-colaboradores/historico-atribuicoes/historico-atribuicoes.component';
import { AvaliarComponent } from './perfil/gestor/avaliar/avaliar.component';
import { SolicitacoesComponent } from './perfil/loja/solicitacoes/solicitacoes.component';
import { HistoricoTrocasComponent } from './perfil/loja/historico-trocas/historico-trocas.component';
import { TrocarProdutosComponent } from './perfil/loja/trocar-produtos/trocar-produtos.component';
import { RealizarVendaComponent } from './perfil/loja/realizar-venda/realizar-venda.component';
import { MeusPontosComponent } from './perfil/colaborador/meus-pontos/meus-pontos.component';
import { DashboardComponent } from './perfil/colaborador/dashboard/dashboard.component';
import { TrocarPontosComponent } from './perfil/colaborador/trocar-pontos/trocar-pontos.component';
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
import { OpcoesNovoComponent } from './perfil/admin/produtos/opcoes/opcoes-novo/opcoes-novo.component';
import { OpcoesValoresNovoComponent } from './perfil/admin/produtos/opcoes-valores/opcoes-valores-novo/opcoes-valores-novo.component';
import { TaxaConversaoNovoComponent } from './perfil/admin/taxa-conversao/taxa-conversao-novo/taxa-conversao-novo.component';
import { VerbaNovoComponent } from './perfil/admin/verba/verba-novo/verba-novo.component';
import { DetalheRecomendacaoComponent } from './perfil/gestor/historico-recomendacoes/detalhe-recomendacao/detalhe-recomendacao.component';
import { ListarAvaliacoesComponent } from './perfil/gestor/avaliar/listar-avaliacoes/listar-avaliacoes.component';
import { VerTaxaConversaoComponent } from './perfil/gestor/ver-taxa-conversao/ver-taxa-conversao.component';
import { VerProdutoComponent } from './perfil/colaborador/trocar-pontos/ver-produto/ver-produto.component';
import { MinhasTrocasComponent } from './perfil/colaborador/minhas-trocas/minhas-trocas.component';
import { DetalheSolicitacaoComponent } from './perfil/loja/solicitacoes/detalhe-solicitacao/detalhe-solicitacao.component';
import { ListarProdutosComponent } from './perfil/loja/realizar-venda/listar-produtos/listar-produtos.component';
import { EfetivarVendaComponent } from './perfil/loja/realizar-venda/efetivar-venda/efetivar-venda.component';
import { TrocasEfetuadasComponent } from './perfil/loja/relatorios/trocas-efetuadas/trocas-efetuadas.component';
import { SolicitacoesFaturamentoComponent } from './perfil/admin/faturamento/solicitacoes-faturamento/solicitacoes-faturamento.component';
import { UsuarioLojaComponent } from './perfil/loja/usuario-loja/usuario-loja.component';
import { CadastroUsuarioLojaComponent } from './perfil/loja/usuario-loja/cadastro-usuario-loja/cadastro-usuario-loja.component';

const routes: Routes = [
    /// Login
    { path: 'login', component: LoginComponent },
    /// Admin
    { path: 'cargos', component: CargosComponent, canActivate: [AuthGuard] },
    { path: 'colaboradores', component: ColaboradoresComponent, canActivate: [AuthGuard] },
    { path: 'configurar-expiracao', component: ConfigurarExpiracaoComponent, canActivate: [AuthGuard] },
    { path: 'configurar-verbas', component: ConfigurarVerbasComponent, canActivate: [AuthGuard] },
    { path: 'solicitacoes-faturamento', component: SolicitacoesFaturamentoComponent, canActivate: [AuthGuard] },
    { path: 'lojas', component: LojasComponent, canActivate: [AuthGuard] },
    { path: 'lojas-novo/:lojaId', component: LojasNovoComponent, canActivate: [AuthGuard] },
    { path: 'lojas-novo', component: LojasNovoComponent, canActivate: [AuthGuard] },
    { path: 'opcoes-entrega', component: OpcoesEntregaComponent, canActivate: [AuthGuard] },
    { path: 'opcoes-entrega-novo/:opcaoId', component: OpcoesEntregaNovoComponent, canActivate: [AuthGuard] },
    { path: 'opcoes-entrega-novo', component: OpcoesEntregaNovoComponent, canActivate: [AuthGuard] },
    { path: 'produtos', component: ProdutosComponent, canActivate: [AuthGuard] },
    { path: 'produtos-novo/:produtoId', component: ProdutosNovoComponent, canActivate: [AuthGuard] },
    { path: 'produtos-novo', component: ProdutosNovoComponent, canActivate: [AuthGuard] },
    { path: 'opcoes', component: OpcoesComponent, canActivate: [AuthGuard] },
    { path: 'opcoes-novo', component: OpcoesNovoComponent, canActivate: [AuthGuard] },
    { path: 'opcoes-valores', component: OpcoesValoresComponent, canActivate: [AuthGuard] },
    { path: 'opcoes-valores-novo', component: OpcoesValoresNovoComponent, canActivate: [AuthGuard] },
    { path: 'taxa-conversao', component: TaxaConversaoComponent, canActivate: [AuthGuard] },
    { path: 'taxa-conversao-novo/:taxaId', component: TaxaConversaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'taxa-conversao-novo', component: TaxaConversaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'verba', component: VerbaComponent, canActivate: [AuthGuard] },
    { path: 'verba-novo/:verbaId', component: VerbaNovoComponent, canActivate: [AuthGuard] },
    { path: 'verba-novo', component: VerbaNovoComponent, canActivate: [AuthGuard] },
    /// Admin Tec
    { path: 'aplicacao', component: AplicacaoComponent, canActivate: [AuthGuard] },
    { path: 'aplicacao-novo/:aplicacaoId', component: AplicacaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'aplicacao-novo', component: AplicacaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'logs', component: LogsComponent, canActivate: [AuthGuard] },
    { path: 'meio-compra', component: MeioCompraComponent, canActivate: [AuthGuard] },
    { path: 'meio-compra-novo/:meioId', component: MeioCompraNovoComponent, canActivate: [AuthGuard] },
    { path: 'meio-compra-novo', component: MeioCompraNovoComponent, canActivate: [AuthGuard] },
    { path: 'menus', component: MenusComponent, canActivate: [AuthGuard] },
    { path: 'menu-novo/:menuId', component: MenuNovoComponent, canActivate: [AuthGuard] },
    { path: 'menu-novo', component: MenuNovoComponent, canActivate: [AuthGuard] },
    { path: 'perfil-acesso', component: PerfilAcessoComponent, canActivate: [AuthGuard] },
    { path: 'perfil-acesso-novo/:perfilId', component: PerfilAcessoNovoComponent, canActivate: [AuthGuard] },
    { path: 'perfil-acesso-novo', component: PerfilAcessoNovoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-avaliacao', component: SituacaoAvaliacaoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-avaliacao-novo/:situacaoAvaliacaoId', component: SituacaoAvaliacaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-avaliacao-novo', component: SituacaoAvaliacaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-compra', component: SituacaoCompraComponent, canActivate: [AuthGuard] },
    { path: 'situacao-compra-novo/:situacaoCompraId', component: SituacaoCompraNovoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-compra-novo', component: SituacaoCompraNovoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-recomendacao', component: SituacaoRecomendacaoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-recomendacao-novo/:situacaoRecomendacaoId', component: SituacaoRecomendacaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-recomendacao-novo', component: SituacaoRecomendacaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-troca', component: SituacaoTrocaComponent, canActivate: [AuthGuard] },
    { path: 'situacao-troca-novo/:situacaoTrocaId', component: SituacaoTrocaNovoComponent, canActivate: [AuthGuard] },
    { path: 'situacao-troca-novo', component: SituacaoTrocaNovoComponent, canActivate: [AuthGuard] },
    { path: 'tipo-recomendacao', component: TipoRecomendacaoComponent, canActivate: [AuthGuard] },
    { path: 'tipo-recomendacao-novo/:tipoRecomendacaoId', component: TipoRecomendacaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'tipo-recomendacao-novo', component: TipoRecomendacaoNovoComponent, canActivate: [AuthGuard] },
    { path: 'tipos-opcoes', component: TiposOpcoesComponent, canActivate: [AuthGuard] },
    { path: 'tipos-opcoes-novo/:tipoOpcaoId', component: TiposOpcoesNovoComponent, canActivate: [AuthGuard] },
    { path: 'tipos-opcoes-novo', component: TiposOpcoesNovoComponent, canActivate: [AuthGuard] },
    /// Colaborador
    { path: 'meus-pontos', component: MeusPontosComponent, canActivate: [AuthGuard] },
    { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
    { path: 'trocar-pontos', component: TrocarPontosComponent, canActivate: [AuthGuard] },
    { path: 'ver-produto/:id', component: VerProdutoComponent, canActivate: [AuthGuard] },
    { path: 'minhas-trocas', component: MinhasTrocasComponent, canActivate: [AuthGuard] },
    /// Gestor
    { path: 'avaliar', component: AvaliarComponent, canActivate: [AuthGuard] },
    { path: 'listar-avaliacoes-realizadas', component: ListarAvaliacoesComponent, canActivate: [AuthGuard] },
    { path: 'historico-recomendacoes', component: HistoricoRecomendacoesComponent, canActivate: [AuthGuard] },
    { path: 'detalhe-recomendacao/:id', component: DetalheRecomendacaoComponent, canActivate: [AuthGuard] },
    { path: 'reconhecer/:cs/:meutime', component: ReconhecerComponent, canActivate: [AuthGuard] },
    { path: 'reconhecer-equipe', component: EquipeComponent, canActivate: [AuthGuard] },
    { path: 'reconhecer-outra-equipe', component: OutraEquipeComponent, canActivate: [AuthGuard] },
    { path: 'relatorio-troca-pontos', component: RelatorioTrocaPontosComponent, canActivate: [AuthGuard] },
    { path: 'relatorio-qtde-pontos', component: RelatorioQtdePontosComponent, canActivate: [AuthGuard] },
    { path: 'relatorio-historico-atribuicoes', component: RelatorioHistoricoAtribuicoesComponent, canActivate: [AuthGuard] },
    { path: 'ver-taxa-conversao', component: VerTaxaConversaoComponent ,  canActivate: [AuthGuard] },
    /// Loja
    { path: 'solicitacoes', component: SolicitacoesComponent, canActivate: [AuthGuard] },
    { path: 'detalhe-solicitacao/:id', component: DetalheSolicitacaoComponent, canActivate: [AuthGuard] },
    { path: 'historico-trocas', component: HistoricoTrocasComponent, canActivate: [AuthGuard] },
    { path: 'trocar-produtos', component: TrocarProdutosComponent, canActivate: [AuthGuard] },
    { path: 'realizar-venda', component: RealizarVendaComponent, canActivate: [AuthGuard] },
    { path: 'listar-produtos/:cs', component: ListarProdutosComponent, canActivate: [AuthGuard] },
    { path: 'efetivar-venda/:cs/:id', component: EfetivarVendaComponent, canActivate: [AuthGuard] },
    { path: 'relatorio-trocas-efetuadas', component: TrocasEfetuadasComponent, canActivate: [AuthGuard] },
    { path: 'usuario-loja', component: UsuarioLojaComponent, canActivate: [AuthGuard] },
    { path: 'cadastro-usuario-loja', component: CadastroUsuarioLojaComponent, canActivate: [AuthGuard] },
    { path: 'cadastro-usuario-loja/:id/:loja_id', component: CadastroUsuarioLojaComponent, canActivate: [AuthGuard] },
    /// Padrão redireciona para a HOME, esperar o Template para montar a Home
    { path: '**', component: HomeComponent, canActivate: [AuthGuard] },
];

export const AppRoutingModule = RouterModule.forRoot(routes);
