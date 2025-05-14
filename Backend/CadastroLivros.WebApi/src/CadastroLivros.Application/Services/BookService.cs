using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Execeptions;
using CadastroLivros.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CadastroLivros.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddBookAsync(BookEntity book)
        {
            try
            {
                _unitOfWork.Book.Add(book);
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

        public async Task<IEnumerable<BookEntity>> GetAllBooksAsync()
        {
            try
            {
                return await _unitOfWork.Book.GetAllAsync();
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao buscar todos os livros.", ex);
            }
        }

        public async Task<BookEntity> GetBookByIdAsync(int bookId)
        {
            try
            {
                return await _unitOfWork.Book.GetByIdAsync(x => x.BookId == bookId);
            }
            catch (DbException ex)
            {
                throw new ApplicationException($"Erro ao buscar o livro com ID {bookId}.", ex);
            }
        }

        public async Task<bool> UpdateAuthorAsync(BookEntity book)
        {
            try
            {
                var existingBook = await _unitOfWork.Book.GetByIdAsync(x => x.BookId == book.BookId);
                if (existingBook == null)
                {
                    throw new NotFoundException($"Autor Livro com ID {book.BookId} não encontrado.");
                }

                existingBook.Title = book.Title;
                existingBook.Publisher = book.Publisher;
                existingBook.Edition = book.Edition;
                existingBook.PublicationYear = book.PublicationYear;

                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ApplicationException("Conflito de concorrência ao atualizar o livro.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao atualizar o livro no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao atualizar o livro.", ex);
            }
        }

        public async Task<bool> DeleteAuthorAsync(BookEntity book)
        {
            try
            {
                var existingBook = await _unitOfWork.Book.GetByIdAsync(x => x.BookId == book.BookId);
                if (existingBook == null)
                {
                    throw new NotFoundException($"Autor Livro com ID {book.BookId} não encontrado.");
                }

                _unitOfWork.Book.Remove(existingBook);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao deletar o livro do banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao deletar o livro.", ex);
            }
        }
    }
}
