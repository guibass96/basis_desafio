using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Application.Queries.Dtos
{
    public class BookAuthorDto
    {
        public int AuthorId { get; set; }
        public string NameAuthor { get; set; }  
        public string BookName { get; set; } 
        public int BookId { get; set; } 


    }
}
