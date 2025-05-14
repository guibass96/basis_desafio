using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Execeptions;
using CadastroLivros.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CadastroLivros.Application.Services
{
    public class BookSubjectService : IBookSubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookSubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddBookSubjectAsync(BookSubjectEntity subject)
        {
            try
            {
                _unitOfWork.BookSubjects.Add(subject);
                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao adicionar o livro no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao adicionar o livro.", ex);
            }
        }

        public async Task<bool> DeleteBookSubjectAsync(BookSubjectEntity subject)
        {
            try
            {
                var existingBookSubject = await _unitOfWork.BookSubjects.GetByIdAsync(x => x.BookId == subject.BookId && x.SubjectId == subject.SubjectId);
                if (existingBookSubject == null)
                {
                    throw new NotFoundException($"Assunto com ID {subject.SubjectId} não encontrado.");
                }

                _unitOfWork.BookSubjects.Remove(existingBookSubject);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao deletar o banco de dados ao livro assunto.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao livro assunto.", ex);
            }
        }

        public async Task<IEnumerable<BookSubjectEntity>> GetAllBookSubjectsAsync()
        {
            try
            {
                return await _unitOfWork.BookSubjects.GetAllAsync();
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao buscar todos os livros.", ex);
            }
        }

        public async Task<bool> UpdateBookSubjectAsync(BookSubjectEntity book, int subjectNew)
        {
            try
            {
                var existingBookSubject = await _unitOfWork.BookSubjects.GetByIdAsync(x => x.BookId == book.BookId && x.SubjectId == book.SubjectId);
                if (existingBookSubject == null)
                {
                    throw new NotFoundException($"Livro  com ID {book.BookId} assunto ID {book.SubjectId} não encontrado.");
                }

                _unitOfWork.BookSubjects.Remove(existingBookSubject);
                await _unitOfWork.CompleteAsync();

                var newBookSubject = new BookSubjectEntity
                {
                    BookId = book.BookId,
                    SubjectId = subjectNew
                };

                _unitOfWork.BookSubjects.Add(newBookSubject);

                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ApplicationException("Conflito de concorrência ao atualizar o livro assunto.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao atualizar o livro assunto no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao atualizar o livro assunto.", ex);
            }
        }

    }
}
