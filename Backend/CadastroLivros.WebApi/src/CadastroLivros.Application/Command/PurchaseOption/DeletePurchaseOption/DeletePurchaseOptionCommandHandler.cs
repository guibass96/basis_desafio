using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.PurchaseOption.UpdatePurchaseOption;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.PurchaseOption.DeletePurchaseOption
{
    public class DeletePurchaseOptionCommandHandler : IRequestHandler<DeletePurchaseOptionCommand, bool>
    {
        private readonly IPurchaseOptionService _purchaseService;
        public DeletePurchaseOptionCommandHandler(IPurchaseOptionService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public async Task<bool> Handle(DeletePurchaseOptionCommand request, CancellationToken cancellationToken)
        {
            return await _purchaseService.DeleteBookPurchaseOptionAsync(new BookPurchaseOptionEntity { Id = request.Id});
        }
    }
}
