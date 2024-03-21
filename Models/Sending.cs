using System;
using System.Collections.Generic;

namespace DocumentoOborotWpfApp.Models
{
    public partial class Sending
    {
        public Sending()
        {
            Notifsends = new HashSet<Notifsend>();
        }

        public int Id { get; set; }
        public int? FkSendUser { get; set; }
        public int? FkRecUser { get; set; }
        public int? FkDoc { get; set; }
        public int? FkAddfile { get; set; }
        public string? CommentSend { get; set; }
        public int? FkStatus { get; set; }

        public virtual Addfile? FkAddfileNavigation { get; set; }
        public virtual Document? FkDocNavigation { get; set; }
        public virtual User? FkRecUserNavigation { get; set; }
        public virtual User? FkSendUserNavigation { get; set; }
        public virtual Sendstatus? FkStatusNavigation { get; set; }
        public virtual ICollection<Notifsend> Notifsends { get; set; }
    }
}
