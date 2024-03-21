using System;
using System.Collections.Generic;

namespace DocumentoOborotWpfApp.Models
{
    public partial class Addfile
    {
        public Addfile()
        {
            Sendings = new HashSet<Sending>();
        }

        public int Id { get; set; }
        public string? AddfileName { get; set; }
        public byte[]? AddfileByte { get; set; }

        public virtual ICollection<Sending> Sendings { get; set; }
    }
}
