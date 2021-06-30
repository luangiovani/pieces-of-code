using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class PlaylistConfiguration : EntityTypeConfiguration<Playlist>
    {
        public PlaylistConfiguration()
        {
            HasKey(o => o.id_playlist);

            Property(o => o.url)
                .IsRequired()
                .HasMaxLength(150);

            Property(o => o.dt_cadastro)
                .IsRequired();
        }
    }
}