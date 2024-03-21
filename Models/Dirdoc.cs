using System;
using System.Collections.Generic;

namespace DocumentoOborotWpfApp.Models
{
    public partial class Dirdoc
    {
        public int Id { get; set; }
        public int? FkDir { get; set; }
        public int? FkDoc { get; set; }

        public virtual Directory? FkDirNavigation { get; set; }
        public virtual Document? FkDocNavigation { get; set; }
    }
}
