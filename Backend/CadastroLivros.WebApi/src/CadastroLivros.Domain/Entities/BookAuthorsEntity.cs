using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Domain.Entities
{

    [Table("Book_Author")]
    public class BookAuthorsEntity
    {
        public int BookId { get; set; }
        public virtual BookEntity Book { get; set; }

        public int AuthorId { get; set; }
        public virtual AuthorEntity Author { get; set; }

    }
}
