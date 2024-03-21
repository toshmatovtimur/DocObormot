using System;
using System.Collections.Generic;

namespace DocumentoOborotWpfApp.Models
{
    public partial class Notifstatus
    {
        public Notifstatus()
        {
            Notifsends = new HashSet<Notifsend>();
        }

        public int Id { get; set; }
        public string? NotifstatusName { get; set; }

        public virtual ICollection<Notifsend> Notifsends { get; set; }
    }
}
