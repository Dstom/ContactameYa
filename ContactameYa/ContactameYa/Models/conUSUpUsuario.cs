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

        [Display(Name = "Tipo de Usuario")]
        public int TIUid_tipo_usuario { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Usuario")]
        public string USUusuario { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Contraseña")]
        public string USUclave { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nombres")]
        public string USUnombre { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Apellidos")]
        public string USUapellido { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Correo Electronico")]
        public string USUemail { get; set; }

        [StringLength(250)]
        [Display(Name = "Dirección")]
        public string USUdireccion { get; set; }

        [StringLength(9)]
        [Display(Name = "Celular")]
        public string USUtelefono { get; set; }

        [Display(Name = "Latitud")]
        public decimal? USUlatitud { get; set; }
    
        [Display(Name = "Longitud")]
        public decimal? USUlongitud { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Estado")]
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

        public conUSUpUsuario mtdObtener(int xGintIdUsuario) 
        {
            var GobjUsuario = new conUSUpUsuario();
            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    //INNER JOIN EN TIPO USUARIO, SERVICIOS QUE OFRECE Y QUE ESTE ACTIVO
                    GobjUsuario = db.conUSUpUsuario                       
                        .Include("conTIUtTipoUsuario")
                        .Where(x=>x.USUid_usuario == xGintIdUsuario)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return GobjUsuario;
        }

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

        public void mtdGuardar()
        {
            try
            {
                using (var db = new conModelo())
                {
                    if (this.USUid_usuario > 0)
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



        public ResponseModel mtdSeguridad(string xGstrUsuario, string xGstrContraseña)
        {
            var rm = new ResponseModel();
            try
            {
                using (var db = new conModelo())
                {
                    var contraseñaSHA = HashHelper.SHA1(xGstrContraseña);

                    var usuario = db.conUSUpUsuario
                        .Where(x => x.USUusuario.Equals(xGstrUsuario) &&
                                x.USUclave.Equals(contraseñaSHA)).SingleOrDefault();

                    if (usuario != null)
                    {
                        if (usuario.USUestado.Equals("A"))
                        {
                            SessionHelper.AddUserToSession(usuario.USUid_usuario.ToString());
                            rm.SetResponse(true);
                        }
                        else
                        {
                            rm.SetResponse(false, "Usuario desactivado, comuniquese con el administrador.");
                        }
                    }
                    else
                    {
                        rm.SetResponse(false, "Usuario o Contraseña incorrectos.");
                    }
                }
            }
            catch (Exception)
            {

            }
            return rm;
        }


    }
}
