using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.Books.AddPurchaseOption;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.PurchaseOption.UpdatePurchaseOption
{
    public class UpdatePurchaseOptionCommandHandler : IRequestHandler<UpdatePurchaseOptionCommand, bool>
    {
        private readonly IPurchaseOptionService _purchaseService;
        public UpdatePurchaseOptionCommandHandler(IPurchaseOptionService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public async Task<bool> Handle(UpdatePurchaseOptionCommand request, CancellationToken cancellationToken)
        {
            var option = new BookPurchaseOptionEntity
            {
                Id = request.Id,
                BookId = request.BookId,
                Price = request.Price,
                PurchaseType = request.Type

            };
            var result = await _purchaseService.UpdateBookPurchaseOptionAsync(option);
            return result;
        }
    }
}
