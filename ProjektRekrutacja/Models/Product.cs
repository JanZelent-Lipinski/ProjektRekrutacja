using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjektRekrutacja.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }      
        public decimal Price { get; set; }    
        public decimal Quantity { get; set; } 
        public string Unit { get; set; }      
        public string Barcode { get; set; }   
    }
}
