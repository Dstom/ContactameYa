namespace ContactameYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("conCALpCalificacion")]
    public partial class conCALpCalificacion
    {
        [Key]
        public int CALid_calificacion { get; set; }

        public int USUid_usuario { get; set; }

        public int SERid_servicio { get; set; }

        [Column(TypeName = "text")]
        public string CALdescripcion_servicio { get; set; }

        [Required]
        [StringLength(1)]
        public string CALcalificacion { get; set; }

        public virtual conUSUpUsuario conUSUpUsuario { get; set; }

        public virtual conSERpServicio conSERpServicio { get; set; }
    }
}
