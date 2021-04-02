using System;
using System.Collections.Generic;

#nullable disable

namespace ECF06.Models
{
    public partial class Entreprise
    {
        public Entreprise()
        {
            Pees = new HashSet<Pee>();
        }

        public int IdEntreprise { get; set; }
        public string NumeroSiret { get; set; }
        public string RaisonSociale { get; set; }
        public string ComplementIdentificationEntreprise { get; set; }
        public string NumeroNomVoieEntreprise { get; set; }
        public string ComplementAdresseEntreprise { get; set; }
        public string CodePostalEntreprise { get; set; }
        public string VilleEntreprise { get; set; }

        public virtual ICollection<Pee> Pees { get; set; }
    }
}
