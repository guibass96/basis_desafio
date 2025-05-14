using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.BookAuthor.DeleteAuhtorBook
{
    public class DeleteBookAuthorCommand : IRequest<bool>
    {
        public required int AuthorId { get; set; }
        public required int BookId { get; set; }
    }
}
