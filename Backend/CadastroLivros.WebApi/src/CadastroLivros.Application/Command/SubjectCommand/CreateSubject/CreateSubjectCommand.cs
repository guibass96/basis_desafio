using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.Subject.CreateSubject
{
    public class CreateSubjectCommand : IRequest<int>
    {
        public required string Description { get; set; }
    }
}
