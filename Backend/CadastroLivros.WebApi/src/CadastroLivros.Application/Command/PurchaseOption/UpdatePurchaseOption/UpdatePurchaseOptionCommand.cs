using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.PurchaseOption.UpdatePurchaseOption
{
    public class UpdatePurchaseOptionCommand : IRequest<bool> 
    {
        public required int Id { get; set; }
        public required int BookId { get; set; }
        public required string Type { get; set; }
        public required decimal Price { get; set; }
    }
}
