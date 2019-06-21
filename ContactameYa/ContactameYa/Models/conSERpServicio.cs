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

    [Table("conSERpServicio")]
    public partial class conSERpServicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conSERpServicio()
        {
            conCALpCalificacion = new HashSet<conCALpCalificacion>();
            conFOPpForoPreguntas = new HashSet<conFOPpForoPreguntas>();
        }

        [Key]
        public int SERid_servicio { get; set; }

        [Required(ErrorMessage = "Categoria es obligatorio")]
        public int CATid_categoria { get; set; }

        public int USUid_usuario { get; set; }

        [Display(Name = "Nombre del Servicio")]
        [Required(ErrorMessage = "Nombre de servicio es obligatorio")]
        [StringLength(100)]
        public string SERnombre_servicio { get; set; }

        [Display(Name = "Descripcion")]
        [Column(TypeName = "text")]
        public string SERdescripcion_servicio { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "El valor debe ser Mayor a 0")]
        [Display(Name = "Precio")]
        public int SERprecio { get; set; }

        [Display(Name = "Imagen")]
        [StringLength(250)]
        public string SERimagenes { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [StringLength(20)]
        public string SERestado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conCALpCalificacion> conCALpCalificacion { get; set; }

        public virtual conCATtCategoria conCATtCategoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conFOPpForoPreguntas> conFOPpForoPreguntas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conPDSpPedidoServicio> conPDSpPedidoServicio { get; set; }
        public virtual conUSUpUsuario conUSUpUsuario { get; set; }

        public List<conSERpServicio> mtdListar() //retornar un collection
        {
            var servicios = new List<conSERpServicio>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    servicios = db.conSERpServicio.Include("conCATtCategoria").Where(x=>x.SERestado.Equals("A")).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return servicios;
        }

        public List<conSERpServicio> mtdListarMisServicios(int xGintIdUsuario) //retornar un collection
        {
            var servicios = new List<conSERpServicio>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    servicios = db.conSERpServicio.Include("conCATtCategoria").Where(x=>x.USUid_usuario == xGintIdUsuario).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return servicios;
        }


        //metodo Obtener
        public conSERpServicio mtdObtener(int id) //retorna un objeto
        {
            var servicio = new conSERpServicio();
            try
            {
                using (var db = new conModelo())
                {
                    servicio = db.conSERpServicio
                        .Include("conCATtCategoria")
                        .Include("conUSUpUsuario")
                        .Include("conCALpCalificacion")
                                .Where(x => x.SERid_servicio == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return servicio;
        }

        //metodo Buscar
        //public List<Servicios> Buscar(string criterio) //retornar un collection
        //{
        //    var usuarios = new List<Servicios>();

        //    string estadousu = "";
        //    if (criterio.Equals("Activo")) estadousu = "A";
        //    if (criterio == "Inactivo") estadousu = "I";

        //    try
        //    {
        //        //conexion con la fuente de datos
        //        using (var db = new cyaModelo())
        //        {
        //            usuarios = db.Usuario.Include("Persona")
        //                       .Where(x => x.Persona.nombre.Contains(criterio) ||
        //                                   x.Persona.apellido.Contains(criterio) ||
        //                                   x.estado == estadousu)
        //                       .ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return usuarios;
        //}

        //public conDSTtDistrito mtdObtenerLatitudLongitud(int xGintIdDepartamento, int xGintIdProvincia, int xGintIdDistrito)
        //{
        //    var LobjDistrito = new conDSTtDistrito();

        //    try
        //    {
        //        using (var db = new conModelo())
        //        {
        //            LobjDistrito = db.conDSTtDistrito.Where(x => x.conPRVtProvincia.DTOid_departamento == xGintIdDepartamento &&
        //            x.PRVid_provincia == xGintIdProvincia &&
        //            x.DSTid_distrito == xGintIdDistrito).SingleOrDefault();

        //            return LobjDistrito;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        // metodo Guardar
        public void mtdGuardar()
        {
            try
            {
                using (var db = new conModelo())
                {
                    if (this.SERid_servicio > 0)
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
                throw;
            }
        }

        //metodo eliminar
        public void mtdEliminar()
        {
            try
            {
                using (var db = new conModelo())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
