using System;

namespace TAJ.Database.Entity
{
    public class Playlist
    {
        public Guid id_playlist { get; set; }
        public string url { get; set; }
        public DateTime dt_cadastro { get; set; }
    }
}