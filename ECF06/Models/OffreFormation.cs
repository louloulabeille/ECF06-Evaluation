using System;
using System.Collections.Generic;

#nullable disable

namespace ECF06.Models
{
    public partial class OffreFormation
    {
        public OffreFormation()
        {
            StagiaireOffresFormation = new HashSet<StagiaireOffreFormation>();
        }

        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public DateTime DateDebutOffreFormation { get; set; }
        public DateTime DateFinOffreFormation { get; set; }
        public int? IdPersonne { get; set; }
        public string IdProduitFormation { get; set; }

        public virtual Etablissement EtablissementOffre { get; set; }
        public virtual CollaborateurAfpa CollaborateurAfpa { get; set; }
        public virtual ProduitDeFormation ProduitOffre { get; set; }
        public virtual ICollection<StagiaireOffreFormation> StagiaireOffresFormation { get; set; }
    }
}
