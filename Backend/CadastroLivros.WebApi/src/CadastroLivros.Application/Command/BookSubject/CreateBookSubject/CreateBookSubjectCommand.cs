using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.BookSubject.CreateBookSubject
{
    public class CreateBookSubjectCommand : IRequest<int>
    {
        public required int BookId { get; set; }
        public required int SubjectId { get; set; }
    }
}
