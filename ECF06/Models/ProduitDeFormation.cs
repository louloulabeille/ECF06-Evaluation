using System;
using System.Collections.Generic;

#nullable disable

namespace ECF06.Models
{
    public  abstract partial class ProduitDeFormation
    {
        public string IdProduitFormation { get; set; }
        public string DesignationProduitFormation { get; set; }
        public int TypeFormation { get; set; }
        public virtual ICollection<OffreFormation> OffreFormation { get; set; }

    }
}
