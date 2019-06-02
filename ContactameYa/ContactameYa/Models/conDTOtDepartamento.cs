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

    [Table("conDTOtDepartamento")]
    public partial class conDTOtDepartamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conDTOtDepartamento()
        {
            conPRVtProvincia = new HashSet<conPRVtProvincia>();
        }

        [Key]
        public int DTOid_departamento { get; set; }

        [Required]
        [StringLength(100)]
        public string DTOnombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conPRVtProvincia> conPRVtProvincia { get; set; }

        public List<conDTOtDepartamento> mtdListar() //retornar un collection
        {
            var LobjDepartamentos = new List<conDTOtDepartamento>();

            try
            {
                //conexion con la fuente de datos
                using (var conModelo = new conModelo())
                {
                    LobjDepartamentos = conModelo.conDTOtDepartamento.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return LobjDepartamentos;
        }

    }


}