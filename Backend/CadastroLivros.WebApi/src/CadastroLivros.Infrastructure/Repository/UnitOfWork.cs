using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using CadastroLivros.Infrastructure.Context;

namespace CadastroLivros.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookManagementDbContext _context;

        public IRepository<AuthorEntity> Author { get; }
        public IRepository<BookEntity> Book { get; }
        public IRepository<SubjectEntity> Subject { get; }
        public IRepository<BookPurchaseOptionEntity> BookPurchaseOptions { get; }
        public IRepository<BookAuthorsEntity> BookAuthors { get; }
        public IRepository<BookSubjectEntity> BookSubjects { get; }

        public UnitOfWork(BookManagementDbContext context)
        {
            _context = context;
            Author = new Repository<AuthorEntity>(_context);
            Book = new Repository<BookEntity>(_context);
            Subject = new Repository<SubjectEntity>(_context);
            BookPurchaseOptions = new Repository<BookPurchaseOptionEntity>(_context);
            BookAuthors = new Repository<BookAuthorsEntity>(_context);
            BookSubjects = new Repository<BookSubjectEntity>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
