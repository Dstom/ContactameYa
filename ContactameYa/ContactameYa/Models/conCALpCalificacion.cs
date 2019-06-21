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
        
        public int CALcalificacion { get; set; }

        public virtual conUSUpUsuario conUSUpUsuario { get; set; }

        public virtual conSERpServicio conSERpServicio { get; set; }

        public void mtdGuardar()
        {
            try
            {
                using (var db = new conModelo())
                {
                    if (this.CALid_calificacion > 0)
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
                throw ex;
            }
        }
    }
}
