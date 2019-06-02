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

    [Table("conPRVtProvincia")]
    public partial class conPRVtProvincia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conPRVtProvincia()
        {
            conDSTtDistrito = new HashSet<conDSTtDistrito>();
        }

        [Key]
        public int PRVid_provincia { get; set; }

        public int DTOid_departamento { get; set; }

        [Required]
        [StringLength(100)]
        public string PRVnombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conDSTtDistrito> conDSTtDistrito { get; set; }

        public virtual conDTOtDepartamento conDTOtDepartamento { get; set; }

        public List<conPRVtProvincia> mtdListar(int xGintIdDepartamento) //retornar un collection
        {
            var LobjProvincias = new List<conPRVtProvincia>();

            try
            {
                //conexion con la fuente de datos
                using (var conModelo = new conModelo())
                {
                    LobjProvincias = conModelo.conPRVtProvincia.Where(x => x.DTOid_departamento == xGintIdDepartamento).ToList();                        
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return LobjProvincias;
        }
    }
}