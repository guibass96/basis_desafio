using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Application.Queries.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int Edition { get; set; }
        public string PublicationYear { get; set; }


    }
}
