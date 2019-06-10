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

    [Table("conPDSpPedidoServicio")]
    public partial class conPDSpPedidoServicio
    {
        [Key]
        public int PDSid_pedidoServicio { get; set; }

        public int SERid_servicio { get; set; }

        public int USUid_usuario { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime PDSfecha_inicio { get; set; }

        [Display(Name = "Fecha de Entrega")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime PDSfecha_entrega { get; set; }

        [Required]
        [StringLength(1)]
        public string PDSestado { get; set; }

        public virtual conSERpServicio conSERpServicio { get; set; }

        public virtual conUSUpUsuario conUSUpUsuario { get; set; }

        public List<conPDSpPedidoServicio> mtdListarPedidosProveedor() // int xLintIdUsuario
        {
            var lstPedidosServicios = new List<conPDSpPedidoServicio>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    lstPedidosServicios = db.conPDSpPedidoServicio
                        .Include("conSERpServicio")
                        .Include("conUSUpUsuario")
                        //.Where(x=>x.conSERpServicio.USUid_usuario == xLintIdUsuario)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstPedidosServicios;
        }

        public List<conPDSpPedidoServicio> mtdListarPedidosCliente() // int xLintIdUsuario
        {
            var lstPedidosServicios = new List<conPDSpPedidoServicio>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    lstPedidosServicios = db.conPDSpPedidoServicio
                        .Include("conSERpServicio")
                        .Include("conUSUpUsuario")
                        //.Where(USUid_usuario == xLintIdUsuario)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstPedidosServicios;
        }

        //metodo Obtener
        public conPDSpPedidoServicio mtdObtener(int xGintIdPedidoServicio) //retorna un objeto
        {
            var LobjPedidoServicio = new conPDSpPedidoServicio();
            try
            {
                using (var db = new conModelo())
                {
                    LobjPedidoServicio = db.conPDSpPedidoServicio
                        .Include("conSERpServicio")
                        .Include("conUSUpUsuario")
                                .Where(x => x.PDSid_pedidoServicio == xGintIdPedidoServicio)
                                .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return LobjPedidoServicio;
        }

        public void mtdGuardar()
        {
            try
            {
                using (var db = new conModelo())
                {
                    if (this.PDSid_pedidoServicio > 0)
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

    }
}