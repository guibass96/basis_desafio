using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Domain.Interfaces
{
    public interface IBookAuthorService
    {
        Task<int> AddBookAuthorAsync(BookAuthorsEntity author);
        Task<bool> UpdateBookAuthorAsync(BookAuthorsEntity author, int bookNew);
        Task<IEnumerable<BookAuthorsEntity>> GetAllBookAuthorsAsync();
        Task<bool> DeleteBookAuthorAsync(BookAuthorsEntity author);
    }
}
