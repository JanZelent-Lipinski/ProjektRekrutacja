using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektRekrutacja.Models
{
    [Table("Asortymenty", Schema = "ModelDanychContainer")]
    public class Asortyment
    {
        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public decimal? CenaEwidencyjna { get; set; }
    }
}
