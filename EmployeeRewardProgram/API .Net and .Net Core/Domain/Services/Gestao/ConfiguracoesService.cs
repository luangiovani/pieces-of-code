using Database.Models.Gestao;
using Domain.DTO.Gestao;
using Domain.Repositories.Gestao;
using System;
using System.Linq;

namespace Domain.Services.Gestao
{
    public class ConfiguracaoVerbaService : BaseService<ConfiguracaoDistribuicaoVerbas>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly ConfiguracaoVerbaRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public ConfiguracaoVerbaService(ConfiguracaoVerbaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ConfiguracaoDistribuicaoVerbasDTO Gravar(ConfiguracaoDistribuicaoVerbasDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                ((dto.pontos_minimos.HasValue || dto.pontos_por_area.HasValue || dto.pontos_por_colaborador.HasValue) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                var objVerba = _repository.VerificarSeExiste();
                if (objVerba != null)
                {
                    objVerba.pontos_minimos = dto.pontos_minimos.Value;
                    objVerba.pontos_por_area = dto.pontos_por_area.Value;
                    objVerba.pontos_por_colaborador = dto.pontos_por_colaborador.Value;
                    objVerba.ativo = dto.ativo;
                    objVerba.data_hora_alteracao = DateTime.Now;
                    objVerba.cs_colaborador_alteracao = cs_colaborador_logado;
                }
                else
                {
                    objVerba = new ConfiguracaoDistribuicaoVerbas()
                    {
                        pontos_minimos = dto.pontos_minimos.Value,
                        pontos_por_area = dto.pontos_por_area.Value,
                        pontos_por_colaborador = dto.pontos_por_colaborador.Value,
                        ativo = dto.ativo,
                        data_hora_criacao = DateTime.Now,
                        cs_colaborador_criacao = cs_colaborador_logado,
                        data_hora_alteracao = DateTime.Now,
                        cs_colaborador_alteracao = cs_colaborador_logado
                };
                }

                if (!string.IsNullOrEmpty(dto.dt_bloquear))
                {
                    objVerba.bloquear_apartir = DateTime.ParseExact(dto.dt_bloquear, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None);
                }
                if (!string.IsNullOrEmpty(dto.dt_disponivel))
                {
                    objVerba.disponivel_apartir = DateTime.ParseExact(dto.dt_disponivel, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None);
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objVerba, logOperacaoId).id;
                else
                    _repository.Update(objVerba, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para Distribuição de Verba não foram preenchidos");
            }
        }

        public ConfiguracaoDistribuicaoVerbasDTO ObterConfiguracaoVerba()
        {
            var objVerba = _repository.FindAll().FirstOrDefault();
            if (objVerba != null)
            {
                return new ConfiguracaoDistribuicaoVerbasDTO()
                {
                    id = objVerba.id,
                    ativo = objVerba.ativo,
                    sequencial = objVerba.sequencial,
                    dt_bloquear = objVerba.bloquear_apartir.HasValue ? objVerba.bloquear_apartir.Value.ToString("dd/MM/yyyy") : "",
                    dt_disponivel = objVerba.disponivel_apartir.HasValue ? objVerba.disponivel_apartir.Value.ToString("dd/MM/yyyy") : "",
                    pontos_minimos = objVerba.pontos_minimos,
                    pontos_por_area = objVerba.pontos_por_area,
                    pontos_por_colaborador = objVerba.pontos_por_colaborador
                };
            }
            else
                return null;
        }
    }

    public class ConfiguracaoExpiracaoService : BaseService<ConfiguracaoExpiracaoPontos>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly ConfiguracaoExpiracaoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public ConfiguracaoExpiracaoService(ConfiguracaoExpiracaoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ConfiguracaoExpiracaoPontosDTO Gravar(ConfiguracaoExpiracaoPontosDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (((dto.qtde_expiracao.HasValue && !string.IsNullOrEmpty(dto.tipo_expiracao)) || 
                  (dto.qtde_expiracao_desligamento.HasValue && !string.IsNullOrEmpty(dto.tipo_expiracao_desligamento))) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                var objExpiracao = _repository.VerificarSeExiste();
                if (objExpiracao != null)
                {
                    objExpiracao.qtde_expiracao = dto.qtde_expiracao.Value;
                    objExpiracao.qtde_expiracao_desligamento = dto.qtde_expiracao_desligamento.Value;
                    objExpiracao.tipo_expiracao = dto.tipo_expiracao;
                    objExpiracao.tipo_expiracao_desligamento = dto.tipo_expiracao_desligamento;
                    objExpiracao.ativo = dto.ativo;
                    objExpiracao.data_hora_alteracao = DateTime.Now;
                    objExpiracao.cs_colaborador_alteracao = cs_colaborador_logado;
                }
                else
                {
                    objExpiracao = new ConfiguracaoExpiracaoPontos()
                    {
                        qtde_expiracao = dto.qtde_expiracao.Value,
                        qtde_expiracao_desligamento = dto.qtde_expiracao_desligamento.Value,
                        tipo_expiracao = dto.tipo_expiracao,
                        tipo_expiracao_desligamento = dto.tipo_expiracao_desligamento,
                        ativo = dto.ativo,
                        data_hora_criacao = DateTime.Now,
                        cs_colaborador_criacao = cs_colaborador_logado,
                        data_hora_alteracao = DateTime.Now,
                        cs_colaborador_alteracao = cs_colaborador_logado
                    };
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objExpiracao, logOperacaoId).id;
                else
                    _repository.Update(objExpiracao, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos para Expiração de Pontos não foram preenchidos");
            }
        }

        public ConfiguracaoExpiracaoPontosDTO ObterConfiguracaoExpiracao()
        {
            var objExpiracao = _repository.FindAll().FirstOrDefault();
            if (objExpiracao != null)
            {
                return new ConfiguracaoExpiracaoPontosDTO()
                {
                    id = objExpiracao.id,
                    ativo = objExpiracao.ativo,
                    sequencial = objExpiracao.sequencial,
                    qtde_expiracao = objExpiracao.qtde_expiracao,
                    qtde_expiracao_desligamento = objExpiracao.qtde_expiracao_desligamento,
                    tipo_expiracao = objExpiracao.tipo_expiracao,
                    tipo_expiracao_desligamento = objExpiracao.tipo_expiracao_desligamento
                };
            }
            else
                return null;
        }
    }
}
