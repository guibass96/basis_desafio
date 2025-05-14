using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Queries.Subject;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Queries.PurchaseOption
{
    public class GetAllPurchaseOptionQueryHandler : IRequestHandler<GetAllPurchaseOptionQuery, List<PurchaseOptionDto>>
    {
        private readonly IPurchaseOptionService _purchaseService;
        public GetAllPurchaseOptionQueryHandler(IPurchaseOptionService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public async Task<List<PurchaseOptionDto>> Handle(GetAllPurchaseOptionQuery request, CancellationToken cancellationToken)
        {
            var options = await _purchaseService.GetAllBookPurchaseOptionAsync();

            return options.Select(option => new PurchaseOptionDto
            {
                Id = option.Id,
                Price = option.Price,
                BookId = option.BookId,
                NameBook = option.Book.Title,
                SaleCategory = option.PurchaseType

            }).ToList();
        }
    }
}
