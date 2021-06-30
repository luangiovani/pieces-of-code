using Database.Models.Gestao;
using Database.Models.Loja;
using Database.Models.Venda;
using Domain.DTO.Loja;
using Domain.Helpers;
using Domain.Repositories.Gestao;
using Domain.Repositories.Loja;
using Domain.Repositories.Venda;
using System;
using System.Collections.Generic;

namespace Domain.Services.Venda
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Service do Middleware para operações entre Front e Backend
    /// </atividades>
    /// <summary>
    /// Implementação da Interface de Service para chamadas das operações de banco de dados
    /// </summary>
    public class CompraService : BaseService<Compra>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly CompraRepository _repository;

        /// <summary>
        /// Objeto do repositório de manipulação de items de Compras
        /// </summary>
        private readonly ItemCompraRepository _itemRepository;

        /// <summary>
        /// Objeto do repositório de manipulação de produtos dos items de Compras
        /// </summary>
        private readonly ProdutoRepository _produtoRepository;

        /// <summary>
        /// Objeto do repositório de manipulação de taxa de conversão
        /// </summary>
        private readonly TaxaConversaoRepository _taxaRepository;

        /// <summary>
        /// Objeto do repositório de manipulação de Colaboradores
        /// </summary>
        private readonly ColaboradorRepository _colaboradorRepository;

        /// <summary>
        /// Objeto do repositório de manipulação de Opções de Entrega
        /// </summary>
        private readonly OpcaoEntregaRepository _opcaoEntregaRepository;

        /// <summary>
        /// Objeto do repositório de manipulação de Opções de Entrega da Compra
        /// </summary>
        private readonly OpcaoEntregaCompraRepository _opcaoEntregaCompraRepository;

        /// <summary>
        /// Injeção do Serviço para Envio de Emails relacionados a comunicações de Recomendações
        /// </summary>
        private readonly SendEmailService _emailService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public CompraService(CompraRepository repository, ItemCompraRepository itemRepository,
            ProdutoRepository produtoRepository, TaxaConversaoRepository taxaRepository,
            ColaboradorRepository colaboradorRepository, OpcaoEntregaRepository opcaoEntregaRepository,
            OpcaoEntregaCompraRepository opcaoEntregaCompraRepository, SendEmailService emailService) : base(repository)
        {
            _repository = repository;
            _itemRepository = itemRepository;
            _produtoRepository = produtoRepository;
            _taxaRepository = taxaRepository;
            _colaboradorRepository = colaboradorRepository;
            _opcaoEntregaRepository = opcaoEntregaRepository;
            _opcaoEntregaCompraRepository = opcaoEntregaCompraRepository;
            _emailService = emailService;
        }

        /// <summary>
        /// Recupera uma listagem com os produtos mais trocados
        /// </summary>
        /// <returns>Lista de Objetos com os produtos mais trocados</returns>
        public IEnumerable<ProdutosMaisTrocadosDTO> ProdutosMaisTrocados()
        {
            return _repository.ProdutosMaisTrocados();
        }

        public TrocasPendentesDTO TrocasPendentes()
        {
            var objRetorno = new TrocasPendentesDTO() {
                TrocasPendentes = _repository.TrocasPendentes(),
                TrocasPendentesDeEnvio = _repository.TrocasPendentesEnvio()
            };

            return objRetorno;
        }

        public DetalheTrocaDTO DetalheTroca(string id)
        {
            return _repository.DetalheTroca(id);
        }

        public DetalheTrocaDTO ObterCompraMudarSituacao(MudaSituacaoDTO dto, DetalheTrocaDTO objCompra, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (objCompra == null)
                objCompra = _repository.DetalheTroca(dto.compra_id);

            if ((dto.situacao_compra_id == SituacaoCompraEnum.Recusada || 
                 dto.situacao_compra_id == SituacaoCompraEnum.Cancelada) && 
                 string.IsNullOrEmpty(dto.justificativa))
            {
                throw new Exception("Para Cancelar ou Recusar uma Compra é necessário informar uma justificativa.");
            }

            var colaborador = _colaboradorRepository.FindByCS(objCompra.cs_colaborador);
            var colaboradorLoja = _colaboradorRepository.ObterColaboradorLoja((objCompra.cs_colaborador == cs_colaborador_logado ? "" : cs_colaborador_logado), objCompra.loja_id);
            if (objCompra != null)
            {
                objCompra.situacao = _repository.MudarSituacaoCompra(dto, cs_colaborador_logado, logOperacaoId);

                if ((dto.situacao_compra_id == SituacaoCompraEnum.Recusada || dto.situacao_compra_id == SituacaoCompraEnum.Cancelada))
                {
                    if (colaborador != null)
                    {
                        colaborador.quantidade_pontos += objCompra.total_pontos;
                        colaborador.cs_colaborador_alteracao = cs_colaborador_logado;
                        colaborador.data_hora_alteracao = DateTime.Now;
                        _colaboradorRepository.AtualizarPontosColaborador(colaborador, cs_colaborador_logado, logOperacaoId);
                    }
                }

                #region Envia Email Compra

                try
                {
                    string corpoEmail = Templates.HTML_GENERICO;

                    /// Enviar Email para a Loja
                    if (dto.situacao_compra_id == SituacaoCompraEnum.Cancelada && colaboradorLoja != null && !string.IsNullOrEmpty(colaboradorLoja.email))
                    {
                        corpoEmail = corpoEmail.Replace("#DESTINATARIO#", colaboradorLoja.nome);
                        corpoEmail = corpoEmail.Replace("#MENSAGEM#", "A compra <strong>#" + objCompra.sequencial.ToString() + "</strong> foi <strong>CANCELADA</strong> pelo Colaborador!");
                        _emailService.SendAsync(corpoEmail, "Troca Cancelada pelo Colaborador", colaboradorLoja.email);
                    }
                    else if (dto.situacao_compra_id == SituacaoCompraEnum.ProdutosRecebidos && colaboradorLoja != null && !string.IsNullOrEmpty(colaboradorLoja.email))
                    {
                        corpoEmail = corpoEmail.Replace("#DESTINATARIO#", colaboradorLoja.nome);
                        corpoEmail = corpoEmail.Replace("#MENSAGEM#", "O colaborador confirmou o recebimento dos produtos, a Troca <strong>#" + objCompra.sequencial.ToString() + "</strong> já pode ser <strong>FINALIZADA</strong>!");
                        _emailService.SendAsync(corpoEmail, "Produtos Recebidos pelo Colaborador", colaboradorLoja.email);
                    }
                    else if (dto.situacao_compra_id == SituacaoCompraEnum.SolicitacaoTroca && colaboradorLoja != null && !string.IsNullOrEmpty(colaboradorLoja.email))
                    {
                        corpoEmail = corpoEmail.Replace("#DESTINATARIO#", colaboradorLoja.nome);
                        corpoEmail = corpoEmail.Replace("#MENSAGEM#", "Há uma nova Solicitação de Troca <strong>#" + objCompra.sequencial.ToString() + "</strong> que nescessita sua atenção!");
                        _emailService.SendAsync(corpoEmail, "Nova Solicitação de Troca de Pontos", colaboradorLoja.email);
                    }
                    else if (dto.situacao_compra_id == SituacaoCompraEnum.Efetivada && !string.IsNullOrEmpty(colaborador.email))
                    {
                        corpoEmail = corpoEmail.Replace("#DESTINATARIO#", colaborador.nome);
                        string msgEmail = "A Troca <strong>#" + objCompra.sequencial.ToString() + "</strong> foi <strong>EFETIVADA</strong> pela Loja, aguarde o recebimento do(s) produto(s)!";
                        msgEmail += !string.IsNullOrEmpty(objCompra.informacoes_complementares) ? "<p>A Loja deixou a seguinte informação adicional.: " + objCompra.informacoes_complementares + "</p>" : string.Empty;
                        corpoEmail = corpoEmail.Replace("#MENSAGEM#", msgEmail);
                        _emailService.SendAsync(corpoEmail, "Troca Efetivada pela Loja", colaborador.email);
                    }
                    else if (dto.situacao_compra_id == SituacaoCompraEnum.Recusada && !string.IsNullOrEmpty(colaborador.email))
                    {
                        corpoEmail = corpoEmail.Replace("#DESTINATARIO#", colaborador.nome);
                        corpoEmail = corpoEmail.Replace("#MENSAGEM#", "A Troca <strong>#" + objCompra.sequencial.ToString() + "</strong> foi <strong>RECUSADA</strong> pela Loja! Motivo: " + dto.justificativa);
                        _emailService.SendAsync(corpoEmail, "Troca Recusada pela Loja", colaborador.email);
                    }
                }
                catch
                {
                }

                #endregion


                return objCompra;
            }
            else
            {
                throw new Exception("Compra não encontrada no banco de dados");
            }
        }

        public IEnumerable<DetalheTrocaDTO> HistoricoTrocasRealizadas()
        {
            return _repository.HistoricoTrocasRealizadas();
        }

        public IEnumerable<DetalheTrocaDTO> HistoricoTrocasColaborador(string cs_colaborador)
        {
            return _repository.HistoricoTrocasColaborador(cs_colaborador);
        }

        public CombosTrocaDTO ObterOpcoesCombos()
        {
            return _repository.ObterOpcoesCombos();
        }

        public SolicitacaoCompraDTO GravarSolicitacaoDeTrocaDePontos(SolicitacaoCompraDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (!string.IsNullOrEmpty(dto.produto_id) &&
                !string.IsNullOrEmpty(cs_colaborador_logado))
            {
                #region Leitura de Objetos

                TaxaConversao taxaAtual;
                Produto produto;
                Colaborador colaborador, colaboradorLoja;
                OpcaoEntrega opcaoEntrega;

                try
                {
                    taxaAtual = _taxaRepository.ObterAtivo();
                    produto = _produtoRepository.FindByID(dto.produto_id);
                    if (string.IsNullOrEmpty(dto.cs_colaborador))
                    {
                        colaborador = _colaboradorRepository.FindByCS(cs_colaborador_logado);
                        colaboradorLoja = _colaboradorRepository.ObterColaboradorLoja("", dto.loja_id);
                    }
                    else
                    {
                        colaborador = _colaboradorRepository.FindByCS(dto.cs_colaborador);
                        colaboradorLoja = _colaboradorRepository.ObterColaboradorLoja(cs_colaborador_logado, dto.loja_id);
                    }
                    if (string.IsNullOrEmpty(dto.opcao_entrega_id))
                    {
                        dto.opcao_entrega_id = OpcaoEntregaEnum.RetirarNaLojaMaisProxima;
                    }
                    opcaoEntrega = _opcaoEntregaRepository.FindByID(dto.opcao_entrega_id);
                }
                catch (Exception exLeitura)
                {
                    throw exLeitura;
                }

                #endregion

                if (produto != null && taxaAtual != null && colaborador != null && opcaoEntrega != null)
                {

                    #region Verifica pontos Colaborador e produto

                    decimal totalPontos, totalValor;

                    try
                    {
                        totalPontos = (dto.qtde > 0 ? dto.qtde : 1) * produto.valor_pontos;
                        totalValor = (dto.qtde > 0 ? dto.qtde : 1) * produto.valor_monetario;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion

                    if (colaborador.quantidade_pontos >= totalPontos)
                    {
                        Compra objCompra;

                        #region Grava a Compra

                        objCompra = new Compra()
                        {
                            total_valor = totalValor,
                            total_pontos = totalPontos,
                            taxa_conversao = taxaAtual.fator,
                            situacao_compra_id = dto.meio_de_compra_id == MeioDeCompraEnum.NaLoja ? SituacaoCompraEnum.Finalizada : SituacaoCompraEnum.SolicitacaoTroca,
                            opcao_entrega_id = dto.opcao_entrega_id,
                            loja_id = string.IsNullOrEmpty(dto.loja_id) ? colaboradorLoja.loja_id : dto.loja_id,
                            meio_de_compra_id = MeioDeCompraEnum.PortalWeb,
                            cs_colaborador = colaborador.cs,
                            ativo = true,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado
                        };

                        objCompra = _repository.Add(objCompra, logOperacaoId);

                        #endregion

                        #region Grava a Opção de Entrega
                        OpcaoEntregaCompra objOpcaoEntregaCompra;

                        try
                        {
                            objOpcaoEntregaCompra = new OpcaoEntregaCompra()
                            {
                                compra_id = objCompra.id.ToString(),
                                label_loja = opcaoEntrega.label_loja,
                                label_colaborador = opcaoEntrega.label_colaborador,
                                ativo = true,
                                data_hora_criacao = DateTime.Now,
                                cs_colaborador_criacao = cs_colaborador_logado
                            };

                            objOpcaoEntregaCompra = _opcaoEntregaCompraRepository.Add(objOpcaoEntregaCompra, logOperacaoId);
                        }
                        catch (Exception exOpcao)
                        {
                            _repository.Remove(objCompra.id.ToString(), cs_colaborador_logado, logOperacaoId);
                            throw exOpcao;
                        }

                        #endregion

                        #region Grava itens da Compra 

                        try
                        {
                            for (int i = 0; i < dto.qtde; i++)
                            {
                                _itemRepository.Add(new ItemCompra()
                                {
                                    compra_id = objCompra.id.ToString(),
                                    descricao = produto.descricao,
                                    nome = produto.nome,
                                    valor_pontos = produto.valor_pontos,
                                    valor_monetario = produto.valor_monetario,
                                    observacao = dto.observacoes,
                                    produto_id = dto.produto_id,
                                    ativo = true,
                                    cs_colaborador_criacao = cs_colaborador_logado,
                                    data_hora_criacao = DateTime.Now
                                }, logOperacaoId);
                            }
                        }
                        catch (Exception exItens)
                        {
                            _repository.Remove(objCompra.id.ToString(), cs_colaborador_logado, logOperacaoId);
                            if (objOpcaoEntregaCompra != null)
                                _opcaoEntregaCompraRepository.Remove(objOpcaoEntregaCompra.id.ToString(), cs_colaborador_logado, logOperacaoId);

                            throw exItens;
                        }

                        #endregion

                        #region Atualiza Colaborador

                        try
                        {
                            colaborador.quantidade_pontos -= totalPontos;
                            _colaboradorRepository.AtualizarPontosColaborador(colaborador, cs_colaborador_logado, logOperacaoId);
                            dto.id = objCompra.id;
                        }
                        catch (Exception exAtualizaColaborador)
                        {
                            _repository.Remove(objCompra.id.ToString(), cs_colaborador_logado, logOperacaoId);
                            if (objOpcaoEntregaCompra != null)
                                _opcaoEntregaCompraRepository.Remove(objOpcaoEntregaCompra.id.ToString(), cs_colaborador_logado, logOperacaoId);

                            var items = _itemRepository.ListarItemsCompra(objCompra.id.ToString());
                            foreach (var item in items)
                            {
                                _itemRepository.Remove(item.id.ToString(), cs_colaborador_logado, logOperacaoId);
                            }

                            throw exAtualizaColaborador;
                        }

                        #endregion

                        #region Envia Email Compra

                        try
                        {
                            string corpoEmail = Templates.HTML_GENERICO;
                            string cpEmailLoja = corpoEmail;
                            string cpEmailColaborador = corpoEmail;

                            if (string.IsNullOrEmpty(dto.meio_de_compra_id))
                            {
                                if (!string.IsNullOrEmpty(colaboradorLoja.email))
                                {
                                    cpEmailLoja = cpEmailLoja.Replace("#DESTINATARIO#", colaboradorLoja.nome);
                                    cpEmailLoja = cpEmailLoja.Replace("#MENSAGEM#", "Há uma nova Solicitação de Troca <strong>#" + objCompra.sequencial.ToString() + "</strong> que nescessita sua atenção!");
                                    _emailService.SendAsync(cpEmailLoja, "Nova Solicitação de Troca de Pontos", colaboradorLoja.email);
                                }

                                cpEmailColaborador = cpEmailColaborador.Replace("#DESTINATARIO#", colaborador.nome);
                                cpEmailColaborador = cpEmailColaborador.Replace("#MENSAGEM#", "Sua Nova Solicitação de Troca <strong>#" + objCompra.sequencial.ToString() + "</strong> foi encaminhada para a Loja!");
                                _emailService.SendAsync(cpEmailColaborador, "Nova Solicitação de Troca de Pontos", colaborador.email);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(colaboradorLoja.email))
                                {
                                    cpEmailLoja = cpEmailLoja.Replace("#DESTINATARIO#", colaboradorLoja.nome);
                                    cpEmailLoja = cpEmailLoja.Replace("#MENSAGEM#", "Parabéns pela sua troca de pontos, desejamos que faça bom proveito de seu novo item!");
                                    _emailService.SendAsync(cpEmailLoja, "Nova Troca de Pontos", colaboradorLoja.email);
                                }
                            }
                            
                        }
                        catch
                        {
                        }

                        #endregion
                    }
                    else
                        throw new Exception("Você possui.: " + colaborador.quantidade_pontos.ToString() + ", mas a sua troca precisa de .: " + totalPontos.ToString());
                }
                else
                {
                    if (produto == null)
                        throw new Exception("Verifique se o produto informado para a troca é um produto existente!");
                    else if (taxaAtual == null)
                        throw new Exception("Ocorreu um erro ao tentar calcular o valor em pontos desta troca.");
                    else if (colaborador == null)
                        throw new Exception("Ocorreu um erro ao tentar obter as informações do Colaborador.");
                }
                return dto;
            }
            else
            {
                throw new Exception("Verifique as informações passadas para solicitar uma troca de pontos por produtos");
            }
        }

        public void CancelarCompra(string id, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var objCompra = _repository.FindByID(id);
                string situacao_compra_id;

                if (objCompra != null)
                {
                    situacao_compra_id = objCompra.situacao_compra_id;

                    if (objCompra.situacao_compra_id == SituacaoCompraEnum.Efetivada ||
                        objCompra.situacao_compra_id == SituacaoCompraEnum.Cancelada ||
                        objCompra.situacao_compra_id == SituacaoCompraEnum.Finalizada ||
                        objCompra.situacao_compra_id == SituacaoCompraEnum.ProdutosRecebidos ||
                        objCompra.situacao_compra_id == SituacaoCompraEnum.Recusada)
                    {
                        throw new Exception("A situação desta Solicitação de Compra, não permite que a mesma seja cancelada pelo Colaborador");
                    }

                    #region Devolve os pontos para o Colaborador

                    Colaborador colaborador;

                    try
                    {
                        colaborador = _colaboradorRepository.FindByCS(cs_colaborador_logado);
                        if (colaborador != null)
                        {
                            colaborador.quantidade_pontos += objCompra.total_pontos;
                            _colaboradorRepository.AtualizarPontosColaborador(colaborador, cs_colaborador_logado, logOperacaoId);
                        }
                    }
                    catch (Exception exCancelar)
                    {
                        throw exCancelar;
                    }

                    #endregion

                    #region Atualiza a Compra

                    try
                    {
                        objCompra.situacao_compra_id = SituacaoCompraEnum.Cancelada;
                        objCompra.ativo = false;
                        objCompra.cs_colaborador_alteracao = cs_colaborador_logado;
                        objCompra.data_hora_alteracao = DateTime.Now;

                        _repository.Update(objCompra, logOperacaoId);
                    }
                    catch (Exception exCompra)
                    {
                        colaborador.quantidade_pontos -= objCompra.total_pontos;
                        _colaboradorRepository.Update(colaborador, logOperacaoId);
                        throw exCompra;
                    }

                    try
                    {
                        var itens = _itemRepository.ListarItemsCompra(id);

                        foreach (var item in itens)
                        {
                            item.ativo = false;
                            item.ativo = false;
                            item.cs_colaborador_alteracao = cs_colaborador_logado;
                            item.data_hora_alteracao = DateTime.Now;

                            _itemRepository.Update(item, logOperacaoId);
                        }
                    }
                    catch (Exception exCompra)
                    {
                        colaborador.quantidade_pontos -= objCompra.total_pontos;
                        _colaboradorRepository.Update(colaborador, logOperacaoId);

                        objCompra.situacao_compra_id = situacao_compra_id;
                        objCompra.ativo = true;
                        _repository.Update(objCompra, logOperacaoId);
                        throw exCompra;
                    }
                    #endregion

                    #region Envia Email Compra

                    try
                    {
                        var colaboradorLoja = _colaboradorRepository.ObterColaboradorLoja("", objCompra.loja_id);
                        string corpoEmail = Templates.HTML_GENERICO;
                        if (colaboradorLoja != null && !string.IsNullOrEmpty(colaboradorLoja.email))
                        {
                            corpoEmail = corpoEmail.Replace("#DESTINATARIO#", colaboradorLoja.nome);
                            corpoEmail = corpoEmail.Replace("#MENSAGEM#", "A compra <strong>#" + objCompra.sequencial.ToString() + "</strong> foi <strong>CANCELADA</strong> pelo Colaborador!");
                            _emailService.SendAsync(corpoEmail, "Troca Cancelada pelo Colaborador", colaboradorLoja.email);
                        }
                    }
                    catch
                    {
                    }

                    #endregion
                }
                else
                    throw new Exception("Compra não Localizada");
            }
            else
                throw new Exception("Id da compra não foi informado");
        }

        public List<RelatorioComprasDTO> ObterRelatorioTrocas(string dataDe, string dataAte, string situacao, string pago, string loja_id)
        {
            var dataD = string.IsNullOrEmpty(dataDe) ? 
                DateTime.ParseExact("01/01/2019", "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None) 
                : 
                DateTime.ParseExact(dataDe, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None);

            var dataAt = string.IsNullOrEmpty(dataAte) ?
                DateTime.Now
                :
                DateTime.ParseExact(dataDe, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None);

            return _repository.ObterRelatorioTrocas(dataD, dataAt, situacao, Convert.ToInt32(string.IsNullOrEmpty(pago) ? "-2" : pago), loja_id);
        }

        public void FaturarCompras(List<string> comprasId, string cs_colaborador_logado, Guid logOperacaoId)
        {
            _repository.FaturarCompras(comprasId, cs_colaborador_logado, logOperacaoId);
        }

        public void PagarCompras(List<string> faturamentosId, string cs_colaborador_logado, Guid logOperacaoId)
        {
            _repository.PagarCompras(faturamentosId, cs_colaborador_logado, logOperacaoId);
        }
    }
}