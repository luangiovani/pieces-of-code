using Database.Models.Gestao;
using Domain.DTO.Gestao;
using Domain.Repositories.Gestao;
using System;

namespace Domain.Services.Gestao
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
    public class CargoService : BaseService<Cargo>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly CargoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public CargoService(CargoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastrar um novo Menu ou nova opção de Menu no banco de dados
        /// </summary>
        /// <param name="cargoDTO">Objeto de cargoFTO para manipulação de registros de Cargo</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado no sistema</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto Cargo cadastrado</returns>
        public CargoDTO Gravar(CargoDTO cargoDTO, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (!string .IsNullOrEmpty(cargoDTO.nomenclatura) && !string .IsNullOrEmpty(cs_colaborador_logado) && string.IsNullOrEmpty(cargoDTO.id.ToString()))
            {
                Cargo objCargo = new Cargo()
                {
                    //perfil_id = cargoDTO.perfil_id,
                    //codigo = cargoDTO.codigo,
                    nomenclatura = cargoDTO.nomenclatura,
                    descricao = cargoDTO.descricao,
                    elegivel = cargoDTO.elegivel,
                    ativo = true,
                    data_hora_criacao = DateTime.Now,                   
                    cs_colaborador_criacao = cs_colaborador_logado
                };

                objCargo = _repository.Add(objCargo, logOperacaoId);
                cargoDTO.id = objCargo.id;
                return cargoDTO;
            }
            else if (!string .IsNullOrEmpty(cargoDTO.nomenclatura) && !string .IsNullOrEmpty(cs_colaborador_logado) && !string.IsNullOrEmpty(cargoDTO.id.ToString()))
            {

                var objCargo = _repository.FindByID(cargoDTO.id.ToString());

                if (objCargo != null)
                {
                    //objCargo.perfil_id = cargoDTO.perfil_id;
                    objCargo.ativo = cargoDTO.ativo;
                    objCargo.elegivel = cargoDTO.elegivel;
                    objCargo.data_hora_alteracao = DateTime.Now;
                    objCargo.descricao = cargoDTO.descricao;
                    objCargo.nomenclatura = cargoDTO.nomenclatura;
                    objCargo.cs_colaborador_alteracao = cs_colaborador_logado;

                    _repository.Update(objCargo, logOperacaoId);

                    return cargoDTO;
                }
                else
                    throw new InvalidOperationException("Cargo não encontrado para efetuar a atualização.");
            }
            else
                return null;

        }

        /// <summary>
        /// Marcar o Cargo como Elegível / Inelegível
        /// </summary>
        /// <param name="id">Id do Cargo para Marcar</param>
        /// <param name="elegivel">true se Elegível e false se Inelegível</param>
        /// <param name="cs_colaborador_logado">Código do Colaborador que está realizando a transação</param>
        /// <param name="logOperacaoId">Id do Log para gravação de Log</param>
        public void ElegivelInelegivel(string id, bool elegivel, string cs_colaborador_logado, Guid logOperacaoId)
        {
            _repository.ElegivelInelegivel(id, elegivel, cs_colaborador_logado, logOperacaoId);
        }
    }
}