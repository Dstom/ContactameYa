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


    [Table("conCATtCategoria")]
    public partial class conCATtCategoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conCATtCategoria()
        {
            conSERpServicio = new HashSet<conSERpServicio>();
        }

        [Key]
        public int CATid_categoria { get; set; }

        [Display(Name = "Categoria")]
        [StringLength(20)]
        public string CATnombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conSERpServicio> conSERpServicio { get; set; }



        public List<conCATtCategoria> Listar() //retornar un collection
        {
            var catServicios = new List<conCATtCategoria>();

            try
            {
                //conexion con la fuente de datos
                using (var db = new conModelo())
                {
                    catServicios = db.conCATtCategoria.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return catServicios;
        }
    }
}
