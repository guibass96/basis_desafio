using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;

namespace CadastroLivros.Domain.Interfaces
{
    public interface ISubjectService
    {
        Task<int> AddSubjectAsync(SubjectEntity book);
        Task<bool> UpdateSubjectAsync(SubjectEntity book);
        Task<SubjectEntity> GetSubjectByIdAsync(int bookId);
        Task<IEnumerable<SubjectEntity>> GetAllSubjectsAsync();
        Task<bool> DeleteSubjectAsync(SubjectEntity book);
    }
}
