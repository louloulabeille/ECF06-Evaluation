using System;
using System.Collections.Generic;

#nullable disable

namespace ECF06.Models
{
    public partial class Etablissement
    {
        public Etablissement()
        {
            InverseIdEtablissementRattachementNavigation = new HashSet<Etablissement>();
            OffreFormations = new HashSet<OffreFormation>();
        }

        public string IdEtablissement { get; set; }
        public string DesignationEtablissement { get; set; }
        public string ComplementIdentificationEtablissement { get; set; }
        public string NumeroNomVoieEtablissement { get; set; }
        public string ComplementAdresseEtablissement { get; set; }
        public string CodePostalEtablissement { get; set; }
        public string VilleEtablissement { get; set; }
        public string IdEtablissementRattachement { get; set; }

        public virtual Etablissement IdEtablissementRattachementNavigation { get; set; }
        public virtual ICollection<Etablissement> InverseIdEtablissementRattachementNavigation { get; set; }
        public virtual ICollection<OffreFormation> OffreFormations { get; set; }
    }
}
