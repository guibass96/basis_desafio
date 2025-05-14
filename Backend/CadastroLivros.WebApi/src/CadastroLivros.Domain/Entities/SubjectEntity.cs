using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Domain.Entities
{
    public class SubjectEntity
    {
        public  int SubjectId { get; set; }
        public required string Description { get; set; }
        public ICollection<BookSubjectEntity> BookSubjects { get; set; } = new List<BookSubjectEntity>();
    }
}
