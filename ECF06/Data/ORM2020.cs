using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

#nullable disable

namespace ECF06.Models
{
    public partial class ORM2020 : DbContext
    {

        public static readonly ILoggerFactory Consignation
    = LoggerFactory.Create(builder =>
    {
        builder
            .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
               && level == LogLevel.Information)
            .AddDebug();
    });
        public ORM2020()
        {
        }

        public ORM2020(DbContextOptions<ORM2020> options)
            : base(options)
        {
        }

        public virtual DbSet<Entreprise> Entreprise { get; set; }
        public virtual DbSet<Etablissement> Etablissement { get; set; }
        public virtual DbSet<OffreFormation> OffreFormation { get; set; }
        public virtual DbSet<Pee> Pee { get; set; }
        public virtual DbSet<PeriodePee> PeriodePee { get; set; }
        public virtual DbSet<Personne> Personne { get; set; }
        public virtual DbSet<Tiers> Tiers { get; set; }
        public virtual DbSet<Stagiaire> Stagiaire { get; set; }
        public virtual DbSet<CollaborateurAfpa> CollaborateurAfpa { get; set; }
        public virtual DbSet<ProduitDeFormation> ProduitDeFormation { get; set; }
        public virtual DbSet<ProduitQualifiant> ProduitQualifiant { get; set; }
        public virtual DbSet<ProduitFC> ProduitFC { get; set; }
        public virtual DbSet<StagiaireOffreFormation> StagiaireOffreFormation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost;Initial Catalog=ORM2020;Trusted_Connection=True;");
                optionsBuilder.UseLoggerFactory(Consignation);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entreprise>(entity =>
            {
                entity.HasKey(e => e.IdEntreprise);

                entity.Property(e => e.CodePostalEntreprise)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ComplementAdresseEntreprise)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementIdentificationEntreprise)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroNomVoieEntreprise)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroSiret)
                    .IsRequired()
                    .HasColumnName("NumeroSIRET")
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RaisonSociale)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VilleEntreprise)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Etablissement>(entity =>
            {
                entity.HasKey(e => e.IdEtablissement)
                    .HasName("PK_Etablissement_1");

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CodePostalEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ComplementAdresseEtablissement)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementIdentificationEtablissement)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationEtablissement)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IdEtablissementRattachement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NumeroNomVoieEtablissement)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VilleEtablissement)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEtablissementRattachementNavigation)
                    .WithMany(p => p.InverseIdEtablissementRattachementNavigation)
                    .HasForeignKey(d => d.IdEtablissementRattachement)
                    .HasConstraintName("FK_Etablissement_Etablissement");
            });

            modelBuilder.Entity<OffreFormation>(entity =>
            {
                entity.HasKey(e => new { e.IdOffreFormation, e.IdEtablissement });

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateDebutOffreFormation).HasColumnType("date");

                entity.Property(e => e.DateFinOffreFormation).HasColumnType("date");

                entity.Property(e => e.IdProduitFormation)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.EtablissementOffre)
                    .WithMany(p => p.OffreFormations)
                    .HasForeignKey(d => d.IdEtablissement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OffreFormation_Etablissement");

                entity.HasOne(d => d.CollaborateurAfpa)
                                   .WithMany(p => p.OffreFormation)
                                   .HasForeignKey(d => d.IdPersonne)
                                   .HasConstraintName("FK_OffreFormation_Formateur");


                entity.HasOne(d => d.ProduitOffre)
                    .WithMany(p => p.OffreFormation)
                    .HasForeignKey(d => d.IdProduitFormation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OffreFormation_ProduitDeFormation");
            });

            modelBuilder.Entity<Pee>(entity =>
            {
                entity.HasKey(e => e.IdPee);

                entity.HasOne(d => d.EntreprisePee)
                    .WithMany(p => p.Pees)
                    .HasForeignKey(d => d.IdEntreprise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEE_Entreprise");

                entity.HasOne(d => d.ResponsableJuridique)
                    .WithMany(p => p.PeeIdResponsableJuridiqueNavigation)
                    .HasForeignKey(d => d.IdResponsableJuridique)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEE_ResponsableJuridique");
                entity.HasOne(d => d.StagiairePee)
                   .WithMany(p => p.PeeIdStagiaireNavigation)
                   .HasForeignKey(d => d.IdStagiaire)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Pee_Stagiaire");


                entity.HasOne(d => d.Tuteur)
                    .WithMany(p => p.PeeIdTuteurNavigation)
                    .HasForeignKey(d => d.IdTuteur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEE_Tuteur");
            });

            modelBuilder.Entity<PeriodePee>(entity =>
            {
                entity.HasKey(e => new { e.IdPee, e.DateDebutPeriodePee, e.DateFinPeriodePee });

                entity.ToTable("Periode_Pee");

                entity.Property(e => e.DateDebutPeriodePee).HasColumnType("date");

                entity.Property(e => e.DateFinPeriodePee).HasColumnType("date");

                entity.HasOne(d => d.IdPeeNavigation)
                    .WithMany(p => p.PeriodePees)
                    .HasForeignKey(d => d.IdPee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Periode_Pee_Pee");
            });
            modelBuilder.Entity<Stagiaire>(entity =>
            {

                entity.Property(e => e.DateNaissanceStagiaire).HasColumnType("date");
                entity.Property(e => e.MatriculeStagiaire)
                   .HasMaxLength(8)
                   .IsUnicode(false)
                   .IsFixedLength();

            });

            modelBuilder.Entity<Tiers>();

            modelBuilder.Entity<CollaborateurAfpa>(entity =>
            {

                entity.Property(e => e.MatriculeCollaborateurAfpa)
                  .HasMaxLength(8)
                  .IsUnicode(false)
                  .IsFixedLength();

            });
            modelBuilder.Entity<Personne>(entity =>
            {
                entity.ToTable("Personne")
                .HasDiscriminator<string>(d => d.CatPersonne)
                .HasValue<Tiers>("P")
                .HasValue<CollaborateurAfpa>("F")
                .HasValue<Stagiaire>("S");
                entity.HasKey(e => e.IdPersonne);

                entity.Property(e => e.AdresseMail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CatPersonne)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('P')");

                entity.Property(e => e.CivilitePersonne)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);


                entity.Property(e => e.NomPersonne)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PrenomPersonne)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProduitDeFormation>(entity =>
            {
                entity.HasKey(e => e.IdProduitFormation);
                entity.HasDiscriminator<int>(p => p.TypeFormation)
                .HasValue<ProduitQualifiant>(1)
                .HasValue<ProduitFC>(2);

                entity.Property(e => e.IdProduitFormation)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DesignationProduitFormation)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StagiaireOffreFormation>(entity =>
            {
                entity.HasKey(e => new { e.IdPersonne, e.IdOffreFormation, e.IdEtablissement });

                entity.ToTable("Stagiaire_OffreFormation");

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateEntreeStagiaire).HasColumnType("date");

                entity.Property(e => e.DateSortieStagiaire).HasColumnType("date");

                entity.HasOne(d => d.Stagiaire)
                    .WithMany(p => p.StagiaireOffreFormation)
                    .HasForeignKey(d => d.IdPersonne)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stagiaire_OffreFormation_Personne");

                entity.HasOne(d => d.OffreFormation)
                    .WithMany(p => p.StagiaireOffresFormation)
                    .HasForeignKey(d => new { d.IdOffreFormation, d.IdEtablissement })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stagiaire_OffreFormation_OffreFormation");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
