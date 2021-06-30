using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            HasKey(o => o.id_cliente);

            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.email)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.cpf)
                .HasMaxLength(14);

            Property(o => o.rg)
                .HasMaxLength(30);

            Property(o => o.cep)
                .HasMaxLength(9);

            Property(o => o.endereco)
                .HasMaxLength(200);

            Property(o => o.numero)
                .HasMaxLength(8);

            Property(o => o.telefone)
                .HasMaxLength(15);

            Property(o => o.celular)
                .HasMaxLength(15);

            Property(o => o.ativo)
                .IsRequired();

            Property(o => o.taj_pass)
                .IsRequired();

            Property(o => o.taj_pass_validado)
                .IsRequired();

            Property(o => o.taj_pass_validade);

            Property(o => o.motivo_bloqueio)
                .HasMaxLength(500);

            Property(o => o.dt_nascimento);

            Property(o => o.dt_cadastro)
                .IsRequired();

            Property(o => o.auth_token)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.imagem)
                .HasMaxLength(8000);

            Property(o => o.vip)
                .IsRequired();

            Property(o => o.tags);

            HasOptional(o => o.bairro)
             .WithMany(o => o.cliente)
             .HasForeignKey(o => o.id_bairro);

            Property(o => o.deviceId);

            Property(o => o.deviceSystem);


            Property(o => o.nomeBairro)
                .HasMaxLength(300);

            Property(o => o.nomeCidade)
                .HasMaxLength(300);

            Property(o => o.nomeEstado)
                .HasMaxLength(300);
        }
    }
}