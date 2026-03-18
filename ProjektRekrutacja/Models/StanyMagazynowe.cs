using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektRekrutacja.Models
{
    [Table("StanyMagazynowe", Schema = "ModelDanychContainer")]
    public class  StanMagazynowy
    {
        [Key]
        public int Id { get; set; }
        public decimal? IloscDostepna { get; set; }
        public int Asortyment_Id { get; set; }
    }
}
