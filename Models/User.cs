using System;
using System.Collections.Generic;

namespace DocumentoOborotWpfApp.Models
{
    public partial class User
    {
        public User()
        {
            NotifsendFkRecUserNavigations = new HashSet<Notifsend>();
            NotifsendFkSendUserNavigations = new HashSet<Notifsend>();
            SendingFkRecUserNavigations = new HashSet<Sending>();
            SendingFkSendUserNavigations = new HashSet<Sending>();
        }

        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
        public DateOnly? Birthday { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int? FkRole { get; set; }
        public int? Status { get; set; }

        public virtual Role? FkRoleNavigation { get; set; }
        public virtual ICollection<Notifsend> NotifsendFkRecUserNavigations { get; set; }
        public virtual ICollection<Notifsend> NotifsendFkSendUserNavigations { get; set; }
        public virtual ICollection<Sending> SendingFkRecUserNavigations { get; set; }
        public virtual ICollection<Sending> SendingFkSendUserNavigations { get; set; }
    }
}
