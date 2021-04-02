using System;
using System.Collections.Generic;

#nullable disable

namespace ECF06.Models
{
    public partial class Pee
    {
        public Pee()
        {
            PeriodePees = new HashSet<PeriodePee>();
        }

        public int IdPee { get; set; }
        public int IdStagiaire { get; set; }
        public int IdTuteur { get; set; }
        public int IdResponsableJuridique { get; set; }
        public int IdEntreprise { get; set; }
        public int IdOffreFormation { get; set; }

        public virtual Entreprise EntreprisePee { get; set; }
        public virtual Tiers ResponsableJuridique { get; set; }
        public virtual Stagiaire StagiairePee { get; set; }
        public virtual Tiers Tuteur { get; set; }
        public virtual ICollection<PeriodePee> PeriodePees { get; set; }
    }
}
