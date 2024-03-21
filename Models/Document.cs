using System;
using System.Collections.Generic;

namespace DocumentoOborotWpfApp.Models
{
    public partial class Document
    {
        public Document()
        {
            Dirdocs = new HashSet<Dirdoc>();
            Sendings = new HashSet<Sending>();
        }

        public int Id { get; set; }
        public string? DocName { get; set; }
        public byte[]? DocByte { get; set; }

        public virtual ICollection<Dirdoc> Dirdocs { get; set; }
        public virtual ICollection<Sending> Sendings { get; set; }
    }
}
