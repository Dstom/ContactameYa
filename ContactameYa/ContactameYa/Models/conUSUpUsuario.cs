namespace ContactameYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    using System.Linq;
    using System.Data.Entity;

    using System.Data.Entity.Validation;
    using System.Web;
    using System.IO;

    [Table("conUSUpUsuario")]
    public partial class conUSUpUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conUSUpUsuario()
        {
            conCALpCalificacion = new HashSet<conCALpCalificacion>();
            conCOTpCotizacion = new HashSet<conCOTpCotizacion>();
            conCTRpCotizacionRespuesta = new HashSet<conCTRpCotizacionRespuesta>();
            conFOPpForoPreguntas = new HashSet<conFOPpForoPreguntas>();
            conSERpServicio = new HashSet<conSERpServicio>();
        }

        [Key]
        public int USUid_usuario { get; set; }

        public int TIUid_tipo_usuario { get; set; }

        [Required]
        [StringLength(100)]
        public string USUusuario { get; set; }

        [Required]
        [StringLength(100)]
        public string USUclave { get; set; }

        [Required]
        [StringLength(20)]
        public string USUnombre { get; set; }

        [Required]
        [StringLength(20)]
        public string USUapellido { get; set; }

        [StringLength(50)]
        public string USUemail { get; set; }

        public decimal? USUlatitud { get; set; }

        public decimal? USUlongitud { get; set; }

        [Required]
        [StringLength(1)]
        public string USUestado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conCALpCalificacion> conCALpCalificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conCOTpCotizacion> conCOTpCotizacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conCTRpCotizacionRespuesta> conCTRpCotizacionRespuesta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conFOPpForoPreguntas> conFOPpForoPreguntas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conPDSpPedidoServicio> conPDSpPedidoServicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conSERpServicio> conSERpServicio { get; set; }

        public virtual conTIUtTipoUsuario conTIUtTipoUsuario { get; set; }

        public List<conUSUpUsuario> mtdListarProveedores() //retornar un collection
        {
            var GobjListaUsuarios= new List<conUSUpUsuario>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    //INNER JOIN EN TIPO USUARIO, SERVICIOS QUE OFRECE Y QUE ESTE ACTIVO
                    GobjListaUsuarios = db.conUSUpUsuario   
                        .Include("conSERpServicio")
                        .Where(x=>x.conTIUtTipoUsuario.TIUnombre.Equals("Proveedor") && x.USUestado.Equals("A")).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return GobjListaUsuarios;
        }
    }
}
