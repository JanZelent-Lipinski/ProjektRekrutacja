using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektRekrutacja.Models
{
    [Table("JednostkiMiarAsortymentow", Schema = "ModelDanychContainer")]
    public class JednostkaMiaryAsortymentu
    {
        [Key, Column(Order = 0)]
        public int Asortyment_Id { get; set; }

        [Key, Column(Order = 1)]
        public int JednostkaMiary_Id { get; set; }
    }
}