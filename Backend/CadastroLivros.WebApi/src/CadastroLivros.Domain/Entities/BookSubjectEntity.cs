using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Domain.Entities
{
    [Table("Book_Subject")]
    public class BookSubjectEntity
    {
        public int BookId { get; set; }
        public BookEntity Book { get; set; }

        public int SubjectId { get; set; }
        public SubjectEntity Subject { get; set; }
    }
}
