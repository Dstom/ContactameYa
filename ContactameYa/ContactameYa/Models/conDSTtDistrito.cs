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

    [Table("conDSTtDistrito")]
    public partial class conDSTtDistrito
    {
        [Key]
        public int DSTid_distrito { get; set; }

        public int PRVid_provincia { get; set; }

        [Required]
        [StringLength(100)]
        public string DSTnombre { get; set; }

        public decimal DSTlatitud { get; set; }

        public decimal DSTlongitud { get; set; }

        public virtual conPRVtProvincia conPRVtProvincia { get; set; }

        public List<conDSTtDistrito> mtdListar(int xGintIdProvincia) //retornar un collection
        {
            var LobjDistritos = new List<conDSTtDistrito>();

            try
            {
                //conexion con la fuente de datos
                using (var conModelo = new conModelo())
                {
                    LobjDistritos = conModelo.conDSTtDistrito.Where(x => x.PRVid_provincia == xGintIdProvincia).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return LobjDistritos;
        }

        public conDSTtDistrito mtdObtenerLatitudLongitud(int xGintIdDepartamento, int xGintIdProvincia, int xGintIdDistrito)
        {
            var LobjDistrito = new conDSTtDistrito();

            try
            {
                using (var db = new conModelo())
                {
                    LobjDistrito = db.conDSTtDistrito.Where(x => x.conPRVtProvincia.DTOid_departamento == xGintIdDepartamento &&
                    x.PRVid_provincia == xGintIdProvincia &&
                    x.DSTid_distrito == xGintIdDistrito).SingleOrDefault();

                    return LobjDistrito;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}