using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.BookSubject.UpdateBookSubject
{
    public class UpdateBookSubjectCommand : IRequest<bool>
    {
        public required int SubjectId { get; set; }
        public required int BookId { get; set; }
        public required int SubjectNew { get; set; }
    }
}
