using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.Livros.InserirLivro
{
    public class CreateBookCommand : IRequest<int>
    {
        public required string Title { get; set; }
        public required string Publisher { get; set; }
        public required int Edition { get; set; }
        public required string PublicationYear { get; set; }
    }
}
