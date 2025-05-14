using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.BookAuthor.CreateAuhtor
{
    public class CreateBookAuthorCommand : IRequest<int>
    {
        public required int BookId { get; set; }
        public required int AuthorId { get; set; }


    }
}
