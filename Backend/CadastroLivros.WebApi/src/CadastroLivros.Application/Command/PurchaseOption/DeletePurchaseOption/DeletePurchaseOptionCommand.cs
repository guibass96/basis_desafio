using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CadastroLivros.Application.Command.PurchaseOption.DeletePurchaseOption
{
    public class DeletePurchaseOptionCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
