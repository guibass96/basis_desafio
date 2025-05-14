using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Domain.Interfaces
{
    public interface IPurchaseOptionService
    {
        Task<int> AddBookPurchaseOptionAsync(BookPurchaseOptionEntity bookOp);
        Task<bool> UpdateBookPurchaseOptionAsync(BookPurchaseOptionEntity bookOp);
        Task<BookPurchaseOptionEntity> GetBookPurchaseOptionByIdAsync(int bookOpId);
        Task<IEnumerable<BookPurchaseOptionEntity>> GetAllBookPurchaseOptionAsync();
        Task<bool> DeleteBookPurchaseOptionAsync(BookPurchaseOptionEntity bookOp);
    }
}
