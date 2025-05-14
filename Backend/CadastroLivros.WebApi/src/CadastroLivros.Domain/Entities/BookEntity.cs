using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Domain.Entities
{
    public class BookEntity
    {
        public int BookId { get; set; }
        [MaxLength(40)]

        public string Title { get; set; }
        [MaxLength(40)]

        public string Publisher { get; set; }
        public int Edition { get; set; }

        [MaxLength(4)]
        public string PublicationYear { get; set; }
        public ICollection<BookAuthorsEntity> BookAuthors { get; set; } = new List<BookAuthorsEntity>();

        public ICollection<BookSubjectEntity> BookSubjects { get; set; } = new List<BookSubjectEntity>();
        public List<BookPurchaseOptionEntity> PurchaseOptions { get; set; } = new();

    }
}
