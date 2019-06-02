namespace ContactameYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("conTIUtTipoUsuario")]
    public partial class conTIUtTipoUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conTIUtTipoUsuario()
        {
            conUSUpUsuario = new HashSet<conUSUpUsuario>();
        }

        [Key]
        public int TIUid_tipo_usuario { get; set; }

        [Required]
        [StringLength(30)]
        public string TIUnombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conUSUpUsuario> conUSUpUsuario { get; set; }
    }
}
