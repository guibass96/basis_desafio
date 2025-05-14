using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.BookAuthor.UpdateAuthor
{
    public class UpdateBookAuthorCommand : IRequest<bool>
    {
        public required int AuhtorId { get; set; } 
        public required int BookId { get; set; } 
        public required int BookNew { get; set; } 
    }
}
