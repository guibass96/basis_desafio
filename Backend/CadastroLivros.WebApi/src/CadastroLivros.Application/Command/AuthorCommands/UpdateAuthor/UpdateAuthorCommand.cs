using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.Author.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<bool>
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
