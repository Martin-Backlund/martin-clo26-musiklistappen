using System;
using System.Collections.Generic;
using System.Text;

namespace MusikApp
{
    public class Album
    {
        public int Id { get; set; }
        public string? Titel { get; set; }
        public int UtgivningsAr { get; set; }
        public int ArtistId { get; set; }
    }
}
