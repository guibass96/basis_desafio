using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.Books.AddPurchaseOption;
using CadastroLivros.Application.Command.Livros.InserirLivro;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.PurchaseOption.CreatePurchaseOption
{
    public class CreatePurchaseOptionCommandHandler : IRequestHandler<CreatePurchaseOptionCommand, int>
    {
        private readonly IPurchaseOptionService _purchaseService;
        public CreatePurchaseOptionCommandHandler(IPurchaseOptionService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public async Task<int> Handle(CreatePurchaseOptionCommand request, CancellationToken cancellationToken)
        {
            var option = new BookPurchaseOptionEntity
            {
                BookId = request.BookId,
                Price = request.Price,
                PurchaseType = request.SaleCategory

            };
            var result = await _purchaseService.AddBookPurchaseOptionAsync(option);
            return result;
        }
    }
}
