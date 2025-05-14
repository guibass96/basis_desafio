using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Application.Dtos
{
    public class UpdateBookAuthorDto
    {
        public int OldBookId { get; set; }
        public int OldAuthorId { get; set; }

        public int NewBookId { get; set; }
        public int NewAuthorId { get; set; }
    }
}
