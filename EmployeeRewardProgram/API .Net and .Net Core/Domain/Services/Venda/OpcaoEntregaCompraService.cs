﻿using Database.Models.Venda;
using Domain.Repositories.Venda;

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
    public class OpcaoEntregaCompraService : BaseService<OpcaoEntregaCompra>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly OpcaoEntregaCompraRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public OpcaoEntregaCompraService(OpcaoEntregaCompraRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}