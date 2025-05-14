using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Domain.Interfaces
{
    public interface IAuthorService
    {
        Task<int> AddAuthorAsync(AuthorEntity author);
        Task<bool> UpdateAuthorAsync(AuthorEntity author);
        Task<AuthorEntity> GetAuthorByIdAsync(int authorId);
        Task<IEnumerable<AuthorEntity>> GetAllAuthorsAsync();
        Task<bool> DeleteAuthorAsync(AuthorEntity author);
    }
}
