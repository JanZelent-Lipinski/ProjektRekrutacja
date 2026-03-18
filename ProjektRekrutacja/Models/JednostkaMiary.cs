using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektRekrutacja.Models
{
[Table("JednostkiMiar", Schema = "ModelDanychContainer")]
public class JednostkaMiary
    {
        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; }
    }
}
