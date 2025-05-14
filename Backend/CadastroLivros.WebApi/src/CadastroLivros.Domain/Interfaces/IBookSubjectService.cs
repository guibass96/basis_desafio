using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Domain.Interfaces
{
    public interface IBookSubjectService
    {
        Task<int> AddBookSubjectAsync(BookSubjectEntity subject);
        Task<bool> UpdateBookSubjectAsync(BookSubjectEntity book, int subjectNew);
        Task<IEnumerable<BookSubjectEntity>> GetAllBookSubjectsAsync();
        Task<bool> DeleteBookSubjectAsync(BookSubjectEntity subject);
    }
}
