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


    [Table("conCTRpCotizacionRespuesta")]
    public partial class conCTRpCotizacionRespuesta : IValidatableObject
    {
        [Key]
        public int CTRid_cotizacionRespuesta { get; set; }

        public int COTid_cotizacion { get; set; }

        public int USUid_usuario { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        [StringLength(150)]
        public string CTRdescripcion { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Column(TypeName = "date")]
        public DateTime CTRfecha_inicio { get; set; }

        [Display(Name = "Fecha Entrega")]
        [Column(TypeName = "date")]
        public DateTime CTRfecha_entrega { get; set; }

        [Display(Name = "Precio")]
        [Range(0.0, double.MaxValue, ErrorMessage = "El valor debe ser Mayor a 0")]
        public decimal CTRprecio { get; set; }

        public virtual conCOTpCotizacion conCOTpCotizacion { get; set; }

        public virtual conUSUpUsuario conUSUpUsuario { get; set; }

        //validaciones
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ( CTRfecha_entrega < CTRfecha_inicio)
            {
                yield return new ValidationResult("La fecha de entrega no puede ser menor a la fecha de inicio", new List<string> { "CTRfecha_entrega" });
            }
            
            //if(CTRfecha_entrega < DateTime.Now)
            //{
            //    yield return new ValidationResult("La fecha de entrega no puede ser menor a la fecha de publicacion", new List<string> { "CTRfecha_entrega" });
            //}

            //if (CTRfecha_inicio < DateTime.Now)
            //{
            //    yield return new ValidationResult("La fecha de inicio no puede ser menor a la fecha de publicacion", new List<string> { "CTRfecha_inicio" });
            //}
        }

        public List<conCTRpCotizacionRespuesta> mtdListar() //retornar un collection
        {
            var cotizacionRespuestas = new List<conCTRpCotizacionRespuesta>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    cotizacionRespuestas = db.conCTRpCotizacionRespuesta.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cotizacionRespuestas;
        }


        //metodo Obtener
        public conCOTpCotizacion mtdObtener(int ixGintIdCotizaciond) //retorna un objeto
        {
            var cotizacionRespuesta = new conCOTpCotizacion();
            try
            {
                using (var db = new conModelo())
                {
                    cotizacionRespuesta = db.conCOTpCotizacion
                        .Where(x => x.COTid_cotizacion == ixGintIdCotizaciond)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cotizacionRespuesta;
        }

        public void mtdGuardarCotizacionRespuesta()
        {
            try
            {
                using (var db = new conModelo())
                {
                    if (this.CTRid_cotizacionRespuesta > 0)
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
