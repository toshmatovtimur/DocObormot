using System;
using System.Collections.Generic;

namespace DocumentoOborotWpfApp.Models
{
    public partial class Directory
    {
        public Directory()
        {
            Dirdocs = new HashSet<Dirdoc>();
        }

        public int Id { get; set; }
        public string? DirName { get; set; }

        public virtual ICollection<Dirdoc> Dirdocs { get; set; }
    }
}
