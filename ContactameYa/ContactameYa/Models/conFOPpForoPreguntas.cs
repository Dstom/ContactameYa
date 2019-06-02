namespace ContactameYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class conFOPpForoPreguntas
    {
        [Key]
        public int FOPid_foroPregunta { get; set; }

        public int SERid_servicio { get; set; }

        public int USUid_usuario { get; set; }

        [Required]
        [StringLength(150)]
        public string FOPpregunta { get; set; }

        [Required]
        [StringLength(150)]
        public string FOPrespuesta { get; set; }

        [Column(TypeName = "date")]
        public DateTime FOPfecha_respuesta { get; set; }

        [Required]
        [StringLength(30)]
        public string FOPhora_repsuesta { get; set; }

        public virtual conSERpServicio conSERpServicio { get; set; }

        public virtual conUSUpUsuario conUSUpUsuario { get; set; }
    }
}
