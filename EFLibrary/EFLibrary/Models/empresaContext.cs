using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFLibrary.Models
{
    public partial class empresaContext : DbContext
    {
        public empresaContext()
        {
        }

        public empresaContext(DbContextOptions<empresaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Comanda> Comanda { get; set; }
        public virtual DbSet<Dept> Dept { get; set; }
        public virtual DbSet<Producte> Producte { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Datasource=E:\\Docencia\\Material_Moduls\\M7_DI\\repoGit\\ICB0_M07_DI_Samples_18_19\\EFLibrary\\EFLibrary\\empresa.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientCod);

                entity.ToTable("CLIENT");

                entity.HasIndex(e => e.Nom)
                    .HasName("CLIENT_NOM");

                entity.HasIndex(e => new { e.ReprCod, e.ClientCod })
                    .HasName("CLIENT_REPR+CLI");

                entity.Property(e => e.ClientCod)
                    .HasColumnName("CLIENT_COD")
                    .HasColumnType("NUMERIC(6)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Area)
                    .HasColumnName("AREA")
                    .HasColumnType("NUMERIC(3)");

                entity.Property(e => e.Ciutat)
                    .IsRequired()
                    .HasColumnName("CIUTAT")
                    .HasColumnType("VARCHAR (30)");

                entity.Property(e => e.CodiPostal)
                    .IsRequired()
                    .HasColumnName("CODI_POSTAL")
                    .HasColumnType("VARCHAR (9)");

                entity.Property(e => e.Direccio)
                    .IsRequired()
                    .HasColumnName("DIRECCIO")
                    .HasColumnType("VARCHAR (40)");

                entity.Property(e => e.Estat)
                    .HasColumnName("ESTAT")
                    .HasColumnType("VARCHAR (2)");

                entity.Property(e => e.LimitCredit)
                    .HasColumnName("LIMIT_CREDIT")
                    .HasColumnType("NUMERIC(9,2)");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("NOM")
                    .HasColumnType("VARCHAR (45)");

                entity.Property(e => e.Observacions)
                    .HasColumnName("OBSERVACIONS")
                    .HasColumnType("VARCHAR(200)");

                entity.Property(e => e.ReprCod)
                    .HasColumnName("REPR_COD")
                    .HasColumnType("NUMERIC(4)");

                entity.Property(e => e.Telefon)
                    .HasColumnName("TELEFON")
                    .HasColumnType("VARCHAR (9)");
            });

            modelBuilder.Entity<Comanda>(entity =>
            {
                entity.HasKey(e => e.ComNum);

                entity.ToTable("COMANDA");

                entity.HasIndex(e => new { e.ComData, e.ComNum })
                    .HasName("COMANDA_DATA+NUM");

                entity.HasIndex(e => new { e.DataTramesa, e.ComNum })
                    .HasName("COMANDA_TRAMESA+NUM");

                entity.Property(e => e.ComNum)
                    .HasColumnName("COM_NUM")
                    .HasColumnType("NUMERIC(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClientCod)
                    .IsRequired()
                    .HasColumnName("CLIENT_COD")
                    .HasColumnType("NUMERIC(6)");

                entity.Property(e => e.ComData)
                    .HasColumnName("COM_DATA")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.ComTipus)
                    .HasColumnName("COM_TIPUS")
                    .HasColumnType("VARCHAR (1)");

                entity.Property(e => e.DataTramesa)
                    .HasColumnName("DATA_TRAMESA")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.Total)
                    .HasColumnName("TOTAL")
                    .HasColumnType("NUMERIC(8,2)");

                entity.HasOne(d => d.ClientCodNavigation)
                    .WithMany(p => p.Comanda)
                    .HasForeignKey(d => d.ClientCod)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Dept>(entity =>
            {
                entity.HasKey(e => e.DeptNo);

                entity.ToTable("DEPT");

                entity.HasIndex(e => e.Dnom)
                    .IsUnique();

                entity.Property(e => e.DeptNo)
                    .HasColumnName("DEPT_NO")
                    .HasColumnType("NUMERIC(2)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Dnom)
                    .IsRequired()
                    .HasColumnName("DNOM")
                    .HasColumnType("VARCHAR(14)");

                entity.Property(e => e.Loc)
                    .HasColumnName("LOC")
                    .HasColumnType("VARCHAR(14)");
            });

            modelBuilder.Entity<Producte>(entity =>
            {
                entity.HasKey(e => e.ProdNum);

                entity.ToTable("PRODUCTE");

                entity.HasIndex(e => e.Descripcio)
                    .IsUnique();

                entity.Property(e => e.ProdNum)
                    .HasColumnName("PROD_NUM")
                    .HasColumnType("NUMERIC(6)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcio)
                    .IsRequired()
                    .HasColumnName("DESCRIPCIO")
                    .HasColumnType("VARCHAR (30)");
            });
        }
    }
}
