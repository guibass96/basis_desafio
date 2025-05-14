using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Domain.Entities
{
    public class BookPurchaseOptionEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string PurchaseType { get; set; }
        public decimal Price { get; set; }
        public virtual BookEntity Book { get; set; } 

    }
}
