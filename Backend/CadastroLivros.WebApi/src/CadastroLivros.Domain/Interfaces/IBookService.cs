using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Domain.Interfaces
{
    public interface IBookService
    {
        Task<int> AddBookAsync(BookEntity book);
        Task<bool> UpdateAuthorAsync(BookEntity book);
        Task<BookEntity> GetBookByIdAsync(int bookId);
        Task<IEnumerable<BookEntity>> GetAllBooksAsync();
        Task<bool> DeleteAuthorAsync(BookEntity book);
    }
}
