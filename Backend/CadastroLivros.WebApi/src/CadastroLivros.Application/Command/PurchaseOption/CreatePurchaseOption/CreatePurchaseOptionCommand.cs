using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.Books.AddPurchaseOption
{
    public class CreatePurchaseOptionCommand : IRequest<int>
    {
        public required int BookId { get; set; }
        public required string SaleCategory { get; set; }
        public required decimal Price { get; set; }
    }
}
