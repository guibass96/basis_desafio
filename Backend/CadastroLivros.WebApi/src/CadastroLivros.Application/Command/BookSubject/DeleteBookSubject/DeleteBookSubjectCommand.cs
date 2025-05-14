using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.BookSubject.DeleteBookSubject
{
    public class DeleteBookSubjectCommand : IRequest<bool>
    {
        public required int SubjectId { get; set; }
        public required int BookId { get; set; }
    }
}
