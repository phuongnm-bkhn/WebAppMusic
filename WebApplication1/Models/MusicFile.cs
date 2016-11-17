using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppMusic.Models
{
    public class MusicFile
    {
        public int Id { get; set; }

        [StringLength(2000, MinimumLength = 3)]
        [DataType(DataType.Url)]
        public string Link { get; set; }

        [StringLength(2000, MinimumLength = 3)]
        public string LinkOnServer { get; set; }

        public virtual ICollection<MusicPlaylist> MusicPlaylists { get; set; }
    }
}