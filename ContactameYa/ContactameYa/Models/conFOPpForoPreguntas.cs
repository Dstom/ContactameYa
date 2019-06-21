namespace ContactameYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class conFOPpForoPreguntas
    {
        [Key]
        public int FOPid_foroPregunta { get; set; }

        public int SERid_servicio { get; set; }

        public int USUid_usuario { get; set; }

        [Required]
        [StringLength(150)]
        public string FOPpregunta { get; set; }

        [StringLength(150)]
        public string FOPrespuesta { get; set; }

        [Column(TypeName = "date")]
        public DateTime FOPfecha_respuesta { get; set; }

        public virtual conSERpServicio conSERpServicio { get; set; }

        public virtual conUSUpUsuario conUSUpUsuario { get; set; }

        public List<conFOPpForoPreguntas> mtdListarPreguntasServicio(int xGintIdServicio)
        {
            var lstPedidosServicios = new List<conFOPpForoPreguntas>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    lstPedidosServicios = db.conFOPpForoPreguntas
                        .Include("conSERpServicio")
                        .Include("conUSUpUsuario")
                        .Where(x => x.conSERpServicio.SERid_servicio == xGintIdServicio)
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstPedidosServicios;
        }
        public void mtdGuardar()
        {
            try
            {
                using (var db = new conModelo())
                {
                    if (this.FOPid_foroPregunta > 0)
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
            catch (Exception)
            {
                throw;
            }
        }
        public conFOPpForoPreguntas mtdObtener(int xGintIdForoPregunta)
        {
            var objPedidosServicio = new conFOPpForoPreguntas();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    objPedidosServicio = db.conFOPpForoPreguntas
                        .Include("conSERpServicio")
                        .Include("conUSUpUsuario")
                        .Where(x => x.FOPid_foroPregunta == xGintIdForoPregunta)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objPedidosServicio;
        }
    }
}
