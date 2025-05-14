using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Domain.Entities
{
    public class AuthorEntity
    {
        public int AuthorId { get; set; }
        public required string Name { get; set; }

        public ICollection<BookAuthorsEntity> BookAuthors { get; set; } = new List<BookAuthorsEntity>();
    }
}
