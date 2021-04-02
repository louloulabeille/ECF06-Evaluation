using System;
using System.Collections.Generic;

#nullable disable

namespace ECF06.Models
{
    public partial class PeriodePee
    {
        public int IdPee { get; set; }
        public DateTime DateDebutPeriodePee { get; set; }
        public DateTime DateFinPeriodePee { get; set; }

        public virtual Pee IdPeeNavigation { get; set; }
    }
}
