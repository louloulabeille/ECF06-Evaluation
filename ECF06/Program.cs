using ECF06.Models;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Evaluation de Aurélien
/// 17/02/2021
/// </summary>

namespace ECF06
{
    class Program
    {
        static void Main()
        {
            using (ORM2020 dbContext = new ORM2020())
            {
                //Req_Test(dbContext);
                Req_01_RechercheStagiairesByCaracteres(dbContext, "boc");
                Req_02_RechercheEntrepriseBySiret(dbContext, "70204275500281");
                Req_03_ListeStagiaires(dbContext, 8495, "19011");
                Req_04_VolumesParAnneeEtablissement(dbContext, 2018, "19011");
                Req_05_StagiairesDemissionnairesParAnneeEtablissement(dbContext, 2018, "19011");
                Req_06_TauxDepartAnticipeParAnneeEtablissement(dbContext, 2018, "19011");
                Req_07_ListeTiersEntrepriseBySiret(dbContext, "70204275500281");
            }
            Console.Read();
        }

        private static void Req_Test(ORM2020 dbContexte)
        {

            Console.WriteLine("\r\nTests Impression\r\n");
            Console.WriteLine("\r\nImpression objet\r\n");
            var unique = dbContexte.ProduitQualifiant.First();
            ObjectDumper.Write(unique);
            Console.WriteLine("\r\nImpression Liste objets\r\n");
            var listeProduitQualifiant = dbContexte.
                ProduitQualifiant.Where(p => p.NiveauFormation < 4);
            ObjectDumper.Write(listeProduitQualifiant);
            Console.WriteLine("\r\nImpression Projection Liste\r\n");
            var listeInfos = dbContexte.
                ProduitQualifiant.Where(p => p.NiveauFormation < 4)
                .Select(p => new { Id = p.IdProduitFormation, Description = p.DesignationProduitFormation });
            ObjectDumper.Write(listeInfos);
        }

        public static void Req_01_RechercheStagiairesByCaracteres(ORM2020 dbContexte, string suiteCaracteres)
        {
            Console.WriteLine("\r\nRequête 1\r\n");
            Console.WriteLine("\r\nImpression objet\r\n");
            var query = dbContexte.Stagiaire.Where(e => e.NomPersonne.Contains(suiteCaracteres))
                .OrderBy(e => e.NomPersonne)
                .Select(e => new { e.MatriculeStagiaire, e.NomPersonne, e.PrenomPersonne, e.DateNaissanceStagiaire });
            ObjectDumper.Write(query);

        }

        /// <summary>
        /// Recherche entreprise
        /// à partir du SIRET
        /// </summary>
        /// <param name="siret"></param>
        public static void Req_02_RechercheEntrepriseBySiret(ORM2020 dbContexte, string siret)
        {
            Console.WriteLine("\r\nRequête 2\r\n");
            Console.WriteLine("\r\nImpression objet\r\n");
            Entreprise query = new Entreprise();
            query = dbContexte.Entreprise.First(e => e.NumeroSiret == siret);
            
            ObjectDumper.Write(query);
        }

        /// <summary>
        /// Liste des stagiaires par offre de formation
        /// </summary>
        /// <param name="idOffreFormation"></param>
        /// <param name="idEtablissement"></param>
        static public void Req_03_ListeStagiaires(ORM2020 dbContexte, int idOffreFormation, string idEtablissement)
        {
            Console.WriteLine("\r\nRequête 3\r\n");
            Console.WriteLine("\r\nImpression objet\r\n");
            var query = dbContexte.StagiaireOffreFormation
                .Where(e => e.OffreFormation.IdOffreFormation == idOffreFormation 
                && e.OffreFormation.IdEtablissement == idEtablissement)
                .Select(e => new
                {
                    e.Stagiaire.MatriculeStagiaire,
                    e.Stagiaire.CivilitePersonne,
                    e.Stagiaire.NomPersonne,
                    e.Stagiaire.PrenomPersonne,
                    e.OffreFormation.ProduitOffre.DesignationProduitFormation
                });
            ObjectDumper.Write(query);
        }

        ///// <summary>
        ///// Volumes de bénéficiaires pour un établissement et une année
        ///// </summary>
        ///// <param name="annee"></param>
        ///// <param name="idEtablissement"></param>
        public static void Req_04_VolumesParAnneeEtablissement(ORM2020 dbContexte, int annee, string idEtablissement)
        {
            Console.WriteLine("\r\nRequête 4\r\n");
            int query = dbContexte.StagiaireOffreFormation.Where(e => e.DateEntreeStagiaire.Year == annee
            && e.OffreFormation.IdEtablissement == idEtablissement).Count();

            Console.WriteLine("Nombre de Stagiaire " + query.ToString() + " année " + annee);

        }

        /// <summary>
        /// Liste des stagiaires démissionnaires
        /// </summary>
        /// <param name="annee"></param>
        /// <param name="idEtablissement"></param>
        public static void Req_05_StagiairesDemissionnairesParAnneeEtablissement(ORM2020 dbContexte, int annee, string idEtablissement)
        {
            Console.WriteLine("\r\nRequête 5\r\n");
            Console.WriteLine("\r\nImpression objet\r\n");
            var query = dbContexte.StagiaireOffreFormation.Where(e => e.DateEntreeStagiaire.Year == annee
                && e.OffreFormation.IdEtablissement == idEtablissement
                && e.DateSortieStagiaire < e.OffreFormation.DateFinOffreFormation)
                .Select(e => new { e.Stagiaire.MatriculeStagiaire, e.Stagiaire.NomPersonne, e.Stagiaire.PrenomPersonne });

            ObjectDumper.Write(query);

        }
        /// <summary>
        /// Taux de départ anticipé / Année & ETablissement
        /// </summary>
        /// <param name="annee"></param>
        /// <param name="idEtablissement"></param>
        public static void Req_06_TauxDepartAnticipeParAnneeEtablissement(ORM2020 dbContexte, int annee, string idEtablissement)
        {
            Console.WriteLine("\r\nRequête 6\r\n");
            //récupération de tous
            var query = dbContexte.StagiaireOffreFormation.Where(e => e.DateEntreeStagiaire.Year == annee
                && e.OffreFormation.IdEtablissement == idEtablissement
                && e.DateSortieStagiaire < e.OffreFormation.DateFinOffreFormation).Count();
            //récupération des démissionaires
            var query2 = dbContexte.StagiaireOffreFormation.Where(e => e.DateEntreeStagiaire.Year == annee
                && e.OffreFormation.IdEtablissement == idEtablissement).Count();

            query2 = query2 == 0 ? 1 : query2;  // -- eviter la division par 0
            decimal taux = query2==1 ? 0 :Math.Round(((decimal)query / (decimal)query2) * 100, 2);

            Console.WriteLine("Taux de sortie anticipée " + string.Format("{0}%", taux));

        }
        /// <summary>
        /// Liste des personnes responsables juridiques ou tuteurs pour une entreprise connue
        /// </summary>
        /// <param name="siret"></param>
        public static void Req_07_ListeTiersEntrepriseBySiret(ORM2020 dbContexte, string siret)
        {
            Console.WriteLine("\r\nRequête 7 \r\n");
            Console.WriteLine("\r\nImpression objet\r\n");
            var query = dbContexte.Pee.Where(e => e.EntreprisePee.NumeroSiret == siret)
                .Select(e => new { e.Tuteur.IdPersonne, e.Tuteur.NomPersonne, e.Tuteur.PrenomPersonne })
                .Union(dbContexte.Pee.
                Where(e => e.EntreprisePee.NumeroSiret == siret)
                .Select(e => new { e.ResponsableJuridique.IdPersonne, e.ResponsableJuridique.NomPersonne, e.ResponsableJuridique.PrenomPersonne }))
                .Distinct();
            Console.WriteLine("\r\nRequête 7 version 1\r\n");
            ObjectDumper.Write(query);


            var query2 = dbContexte.Tiers
                .Where(e => e.PeeIdResponsableJuridiqueNavigation
                .Any(e => e.EntreprisePee.NumeroSiret == siret) 
                || e.PeeIdTuteurNavigation
                .Any(e => e.EntreprisePee.NumeroSiret == siret))
                .Select(e => new { e.IdPersonne, e.NomPersonne,e.PrenomPersonne })
                .Distinct();

            Console.WriteLine("\r\nRequête 7 version 2\r\n");
            ObjectDumper.Write(query2);
        }

    }
}

