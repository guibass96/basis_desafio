using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.Author.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
