using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.AuthorCommands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public required string Name { get; set; }
    }
}
