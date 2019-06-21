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

    [Table("conCOTpCotizacion")]
    public partial class conCOTpCotizacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conCOTpCotizacion()
        {
            conCTRpCotizacionRespuesta = new HashSet<conCTRpCotizacionRespuesta>();
        }

        [Key]
        public int COTid_cotizacion { get; set; }

        public int USUid_usuario { get; set; }

        [Display(Name = "Nombre de Cotizacion")]
        [Required]
        [StringLength(100)]
        public string COTnombre_cotizacion { get; set; }

        [Display(Name = "Descripcion")]
        [Required]
        [StringLength(150)]
        public string COTdescripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime COTfecha_publicacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Fecha de Entrega")]
        [Column(TypeName = "date")]
        public DateTime COTfecha_limiteEntrega { get; set; }

        public virtual conUSUpUsuario conUSUpUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conCTRpCotizacionRespuesta> conCTRpCotizacionRespuesta { get; set; }


        public List<conCOTpCotizacion> mtdListarCotizaciones() //retornar un collection
        {
            var cotizaciones = new List<conCOTpCotizacion>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    cotizaciones = db.conCOTpCotizacion.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cotizaciones;
        }

        public List<conCOTpCotizacion> mtdListarMisCotizaciones(int xGintIdUsuario) //retornar un collection
        {
            var cotizaciones = new List<conCOTpCotizacion>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    cotizaciones = db.conCOTpCotizacion
                        .Where(x=>x.USUid_usuario == xGintIdUsuario).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cotizaciones;
        }


        //metodo Obtener
        public conCOTpCotizacion mtdObtenerCotizacion(int ixGintIdCotizaciond) //retorna un objeto
        {
            var cotizacion = new conCOTpCotizacion();
            try
            {
                using (var db = new conModelo())
                {
                    cotizacion = db.conCOTpCotizacion
                        .Include("conUSUpUsuario")
                        .Include("conCTRpCotizacionRespuesta")
                        .Where(x => x.COTid_cotizacion == ixGintIdCotizaciond)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cotizacion;
        }

        public void mtdGuardar()
        {
            try
            {
                using (var db = new conModelo())
                {
                    if (this.COTid_cotizacion > 0)
                    {
                       
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
