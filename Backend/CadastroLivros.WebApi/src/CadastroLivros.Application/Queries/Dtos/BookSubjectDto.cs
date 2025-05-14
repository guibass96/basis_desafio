using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Application.Queries.Dtos
{
    public class BookSubjectDto
    {
        public string BookName { get; set; }
        public int BookId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectionDescription { get; set; }
    }
}
