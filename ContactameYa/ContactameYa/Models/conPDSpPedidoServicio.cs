namespace ContactameYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("conPDSpPedidoServicio")]
    public partial class conPDSpPedidoServicio
    {
        [Key]
        public int PDSid_pedidoServicio { get; set; }

        public int SERid_servicio { get; set; }

        public int USUid_usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime PDSfecha_entrega { get; set; }

        [Required]
        [StringLength(1)]
        public string PDSestado { get; set; }

        public virtual conSERpServicio conSERpServicio { get; set; }

        public virtual conUSUpUsuario conUSUpUsuario { get; set; }
    }
}