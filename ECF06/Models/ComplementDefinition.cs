using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECF06.Models
{

    public partial class ProduitQualifiant : ProduitDeFormation
    {
        public ProduitQualifiant()
        {
            OffreFormation = new HashSet<OffreFormation>();
        }
        public int? NiveauFormation { get; set; }
    }
    public partial class ProduitFC : ProduitDeFormation
    {
        public ProduitFC()
        {
            OffreFormation = new HashSet<OffreFormation>();
        }
        public bool? InterEntreprise { get; set; }
        public int? DureeJours { get; set; }
    }
    public partial class Tiers :Personne
    {

        public Tiers()
        {

            PeeIdResponsableJuridiqueNavigation = new HashSet<Pee>();

            PeeIdTuteurNavigation = new HashSet<Pee>();

        }
        public virtual ICollection<Pee> PeeIdResponsableJuridiqueNavigation { get; set; }

        public virtual ICollection<Pee> PeeIdTuteurNavigation { get; set; }

    }

    public partial class CollaborateurAfpa : Personne
    {
        public CollaborateurAfpa()
        {
            OffreFormation = new HashSet<OffreFormation>();

        }
        public string MatriculeCollaborateurAfpa { get; set; }
        public virtual ICollection<OffreFormation> OffreFormation { get; set; }
    }


    public partial class Stagiaire :Personne
    {
        public Stagiaire()
        {
            PeeIdStagiaireNavigation = new HashSet<Pee>();
            StagiaireOffreFormation = new HashSet<StagiaireOffreFormation>();
        }
        [Display(Name = "Matricule stagiaire")]
        [StringLength(08, ErrorMessage = "Longueur invalide")]
        [Required(ErrorMessage = "Le matricule est requis")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Ne doit comporter que des chiffres")]
        public string MatriculeStagiaire { get; set; }
        public DateTime? DateNaissanceStagiaire { get; set; }
        public virtual ICollection<Pee> PeeIdStagiaireNavigation { get; set; }
        public virtual ICollection<StagiaireOffreFormation> StagiaireOffreFormation { get; set; }
        [NotMapped]
        public int Age
        {
            get => (!DateNaissanceStagiaire.HasValue) ? 0
            : (DateTime.Now.DayOfYear >= this.DateNaissanceStagiaire.Value.DayOfYear)
            ? DateTime.Now.Year - this.DateNaissanceStagiaire.Value.Year + 1
            : DateTime.Now.Year - this.DateNaissanceStagiaire.Value.Year;
        }
        public override bool Equals(object obj)
        {
            Stagiaire stagiaire = obj as Stagiaire;
            return (stagiaire == null ? false : stagiaire.MatriculeStagiaire == this.MatriculeStagiaire);
        }
        public override int GetHashCode()
        {
            return (this.MatriculeStagiaire == null ? 0 : this.MatriculeStagiaire.GetHashCode());
        }
    }
    public abstract partial class Personne  
    {

        public int IdPersonne { get; set; }
        public string NomPersonne { get; set; }
        public string PrenomPersonne { get; set; }
        public string CivilitePersonne { get; set; }
        public int SexePersonne { get; set; }
        public string AdresseMail { get; set; }
        public string CatPersonne { get; set; }

    }
   
    
}

