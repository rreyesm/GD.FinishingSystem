using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GD.FinishingSystem.DAL.EFdbPlanta
{
    public partial class dbPlantaContext : DbContext
    {
        public dbPlantaContext()
        {
        }

        public dbPlantaContext(DbContextOptions<dbPlantaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FichaTecnicaTela> FichaTecnicaTelas { get; set; }
        public virtual DbSet<LotesDeProduccion> LotesDeProduccions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=192.168.7.242;Initial Catalog=dbPlanta;User Id=controlderollo;password=0545696sS*");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<FichaTecnicaTela>(entity =>
            {
                entity.HasKey(e => e.CódigoTela)
                    .HasName("PK__Ficha te__29A76780C5A90124");

                entity.ToTable("Ficha tecnica tela");

                entity.Property(e => e.CódigoTela).HasMaxLength(13);

                entity.Property(e => e.Acabado).HasMaxLength(95);

                entity.Property(e => e.AnchoAcabado).HasColumnName("Ancho acabado");

                entity.Property(e => e.AnchoPeine).HasColumnName("Ancho peine");

                entity.Property(e => e.Composición).HasMaxLength(80);

                entity.Property(e => e.Descripción).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Goma).HasMaxLength(55);

                entity.Property(e => e.HiladoTrama)
                    .HasMaxLength(130)
                    .HasColumnName("Hilado trama");

                entity.Property(e => e.HiladoTrama2)
                    .HasMaxLength(130)
                    .HasColumnName("Hilado trama 2");

                entity.Property(e => e.HiladoUrdiembre)
                    .HasMaxLength(130)
                    .HasColumnName("Hilado urdiembre");

                entity.Property(e => e.HiladoUrdiembre2)
                    .HasMaxLength(130)
                    .HasColumnName("Hilado urdiembre 2");

                entity.Property(e => e.HilosPorDiente).HasColumnName("# hilos por diente");

                entity.Property(e => e.HilosUrdiembre).HasColumnName("# hilos urdiembre");

                entity.Property(e => e.HilosUrdiembre2).HasColumnName("# hilos urdiembre 2");

                entity.Property(e => e.LastModify).HasColumnType("datetime");

                entity.Property(e => e.Lycra2).HasColumnName("Lycra 2");

                entity.Property(e => e.MaterialTrama)
                    .HasMaxLength(18)
                    .HasColumnName("Material trama");

                entity.Property(e => e.MaterialTrama2)
                    .HasMaxLength(18)
                    .HasColumnName("Material trama 2");

                entity.Property(e => e.MaterialUrdiembre)
                    .HasMaxLength(25)
                    .HasColumnName("Material urdiembre");

                entity.Property(e => e.MaterialUrdiembre2)
                    .HasMaxLength(25)
                    .HasColumnName("Material urdiembre 2");

                entity.Property(e => e.ModifyUser).HasMaxLength(50);

                entity.Property(e => e.NeTrama).HasColumnName("Ne trama");

                entity.Property(e => e.NeTrama2).HasColumnName("Ne trama 2");

                entity.Property(e => e.NeUrdiembre).HasColumnName("Ne urdiembre");

                entity.Property(e => e.NeUrdiembre2).HasColumnName("Ne urdiembre 2");

                entity.Property(e => e.NewEstilo).HasMaxLength(100);

                entity.Property(e => e.Nombre).HasMaxLength(33);

                entity.Property(e => e.PasadasAcabado).HasColumnName("Pasadas acabado");

                entity.Property(e => e.PasadasTelar).HasColumnName("Pasadas telar");

                entity.Property(e => e.PickUp).HasColumnName("Pick up");

                entity.Property(e => e.ProporcionTrama).HasColumnName("Proporcion trama");

                entity.Property(e => e.ProporcionTrama2).HasColumnName("Proporcion trama 2");

                entity.Property(e => e.Remontado).HasMaxLength(30);

                entity.Property(e => e.Sarga).HasMaxLength(55);

                entity.Property(e => e.Teñido).HasMaxLength(30);

                entity.Property(e => e.TeñidoEspecial).HasMaxLength(30);
            });

            modelBuilder.Entity<LotesDeProduccion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Lotes de Produccion");

                entity.Property(e => e.CódigoTela)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FechaProg).HasColumnType("date");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.NewEstilo).HasMaxLength(50);

                entity.Property(e => e.UrdidoMts).HasColumnName("Urdido(mts)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
