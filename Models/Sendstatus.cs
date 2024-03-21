using System;
using System.Collections.Generic;

namespace DocumentoOborotWpfApp.Models
{
    public partial class Sendstatus
    {
        public Sendstatus()
        {
            Sendings = new HashSet<Sending>();
        }

        public int Id { get; set; }
        public string? SendstatusName { get; set; }

        public virtual ICollection<Sending> Sendings { get; set; }
    }
}
