using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;

namespace CadastroLivros.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AuthorEntity> Author { get; }
        IRepository<BookEntity> Book { get; }
        IRepository<SubjectEntity> Subject { get; }
        IRepository<BookPurchaseOptionEntity> BookPurchaseOptions { get; }
        IRepository<BookAuthorsEntity> BookAuthors { get; }
        IRepository<BookSubjectEntity> BookSubjects { get; }
        Task<int> CompleteAsync();
    }
}
