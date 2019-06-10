namespace ContactameYa.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class conModelo : DbContext
    {
        public conModelo()
            : base("name=conModelo")
        {
        }

        public virtual DbSet<conCALpCalificacion> conCALpCalificacion { get; set; }
        public virtual DbSet<conCATtCategoria> conCATtCategoria { get; set; }
        public virtual DbSet<conCOTpCotizacion> conCOTpCotizacion { get; set; }
        public virtual DbSet<conCTRpCotizacionRespuesta> conCTRpCotizacionRespuesta { get; set; }
        public virtual DbSet<conDSTtDistrito> conDSTtDistrito { get; set; }
        public virtual DbSet<conDTOtDepartamento> conDTOtDepartamento { get; set; }
        public virtual DbSet<conFOPpForoPreguntas> conFOPpForoPreguntas { get; set; }
        public virtual DbSet<conPDSpPedidoServicio> conPDSpPedidoServicio { get; set; }
        public virtual DbSet<conPRVtProvincia> conPRVtProvincia { get; set; }
        public virtual DbSet<conSERpServicio> conSERpServicio { get; set; }
        public virtual DbSet<conTIUtTipoUsuario> conTIUtTipoUsuario { get; set; }
        public virtual DbSet<conUSUpUsuario> conUSUpUsuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<conCALpCalificacion>()
                .Property(e => e.CALdescripcion_servicio)
                .IsUnicode(false);

            modelBuilder.Entity<conCALpCalificacion>()
                .Property(e => e.CALcalificacion)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<conCATtCategoria>()
                .Property(e => e.CATnombre)
                .IsUnicode(false);

            modelBuilder.Entity<conCATtCategoria>()
                .HasMany(e => e.conSERpServicio)
                .WithRequired(e => e.conCATtCategoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conCOTpCotizacion>()
                .Property(e => e.COTnombre_cotizacion)
                .IsUnicode(false);

            modelBuilder.Entity<conCOTpCotizacion>()
                .Property(e => e.COTdescripcion)
                .IsUnicode(false);

            modelBuilder.Entity<conCOTpCotizacion>()
                .HasMany(e => e.conCTRpCotizacionRespuesta)
                .WithRequired(e => e.conCOTpCotizacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conCTRpCotizacionRespuesta>()
                .Property(e => e.CTRdescripcion)
                .IsUnicode(false);

            modelBuilder.Entity<conCTRpCotizacionRespuesta>()
                .Property(e => e.CTRprecio)
                .HasPrecision(18, 0);

            modelBuilder.Entity<conDSTtDistrito>()
                .Property(e => e.DSTnombre)
                .IsUnicode(false);

            modelBuilder.Entity<conDSTtDistrito>()
                .Property(e => e.DSTlatitud)
                .HasPrecision(12, 9);

            modelBuilder.Entity<conDSTtDistrito>()
                .Property(e => e.DSTlongitud)
                .HasPrecision(12, 9);

            modelBuilder.Entity<conDTOtDepartamento>()
                .Property(e => e.DTOnombre)
                .IsUnicode(false);

            modelBuilder.Entity<conDTOtDepartamento>()
                .HasMany(e => e.conPRVtProvincia)
                .WithRequired(e => e.conDTOtDepartamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conFOPpForoPreguntas>()
                .Property(e => e.FOPpregunta)
                .IsUnicode(false);

            modelBuilder.Entity<conFOPpForoPreguntas>()
                .Property(e => e.FOPrespuesta)
                .IsUnicode(false);

            modelBuilder.Entity<conFOPpForoPreguntas>()
                .Property(e => e.FOPhora_repsuesta)
                .IsUnicode(false);

            modelBuilder.Entity<conPDSpPedidoServicio>()
                .Property(e => e.PDSestado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<conPRVtProvincia>()
                .Property(e => e.PRVnombre)
                .IsUnicode(false);

            modelBuilder.Entity<conPRVtProvincia>()
                .HasMany(e => e.conDSTtDistrito)
                .WithRequired(e => e.conPRVtProvincia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conSERpServicio>()
                .Property(e => e.SERnombre_servicio)
                .IsUnicode(false);

            modelBuilder.Entity<conSERpServicio>()
                .Property(e => e.SERdescripcion_servicio)
                .IsUnicode(false);

            modelBuilder.Entity<conSERpServicio>()
                .Property(e => e.SERimagenes)
                .IsUnicode(false);

            modelBuilder.Entity<conSERpServicio>()
                .Property(e => e.SERestado)
                .IsUnicode(false);

            modelBuilder.Entity<conSERpServicio>()
                .HasMany(e => e.conCALpCalificacion)
                .WithRequired(e => e.conSERpServicio)
                .HasForeignKey(e => e.USUid_usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conSERpServicio>()
                .HasMany(e => e.conFOPpForoPreguntas)
                .WithRequired(e => e.conSERpServicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conSERpServicio>()
                .HasMany(e => e.conPDSpPedidoServicio)
                .WithRequired(e => e.conSERpServicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conTIUtTipoUsuario>()
                .Property(e => e.TIUnombre)
                .IsUnicode(false);

            modelBuilder.Entity<conTIUtTipoUsuario>()
                .HasMany(e => e.conUSUpUsuario)
                .WithRequired(e => e.conTIUtTipoUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .Property(e => e.USUusuario)
                .IsUnicode(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .Property(e => e.USUclave)
                .IsUnicode(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .Property(e => e.USUnombre)
                .IsUnicode(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .Property(e => e.USUapellido)
                .IsUnicode(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .Property(e => e.USUemail)
                .IsUnicode(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .Property(e => e.USUlatitud)
                .HasPrecision(12, 9);

            modelBuilder.Entity<conUSUpUsuario>()
                .Property(e => e.USUlongitud)
                .HasPrecision(12, 9);

            modelBuilder.Entity<conUSUpUsuario>()
                .Property(e => e.USUestado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .HasMany(e => e.conCALpCalificacion)
                .WithRequired(e => e.conUSUpUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .HasMany(e => e.conCOTpCotizacion)
                .WithRequired(e => e.conUSUpUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .HasMany(e => e.conCTRpCotizacionRespuesta)
                .WithRequired(e => e.conUSUpUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .HasMany(e => e.conFOPpForoPreguntas)
                .WithRequired(e => e.conUSUpUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .HasMany(e => e.conPDSpPedidoServicio)
                .WithRequired(e => e.conUSUpUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conUSUpUsuario>()
                .HasMany(e => e.conSERpServicio)
                .WithRequired(e => e.conUSUpUsuario)
                .WillCascadeOnDelete(false);
        }
    }    
}
