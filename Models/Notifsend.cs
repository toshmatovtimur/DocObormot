using System;
using System.Collections.Generic;

namespace DocumentoOborotWpfApp.Models
{
    public partial class Notifsend
    {
        public int Id { get; set; }
        public int? FkRecUser { get; set; }
        public int? FkSendUser { get; set; }
        public int? FkSend { get; set; }
        public bool? Active { get; set; }
        public int? FkStatusNotif { get; set; }
        public string? Comments { get; set; }

        public virtual User? FkRecUserNavigation { get; set; }
        public virtual Sending? FkSendNavigation { get; set; }
        public virtual User? FkSendUserNavigation { get; set; }
        public virtual Notifstatus? FkStatusNotifNavigation { get; set; }
    }
}
