using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektRekrutacja.Models
{
    [Table("KodyKreskowe", Schema = "ModelDanychContainer")]
    public class KodyKreskowe
    {
        [Key]
        public int Id { get; set; }

        public string Kod { get; set; }

        public int? JednostkaMiaryAsortymentu_Id { get; set; }
    }
}