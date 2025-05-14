
using System.Data.Common;
using CadastroLivros.Application.Dtos;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Execeptions;
using CadastroLivros.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CadastroLivros.Application.Services
{
    public class BookAuthorService : IBookAuthorService
    {

        private readonly IUnitOfWork _unitOfWork;

        public BookAuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddBookAuthorAsync(BookAuthorsEntity bookAuthor)
        {
            try
            {
                _unitOfWork.BookAuthors.Add(bookAuthor);
                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao acessar o banco de dados ao adicionar autor.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao adicionar autor.", ex);
            }
        }

        public async Task<bool> DeleteBookAuthorAsync(BookAuthorsEntity author)
        {
            try
            {
                var existingBook = await _unitOfWork.BookAuthors.GetByIdAsync(x => x.BookId == author.BookId && x.AuthorId == author.AuthorId);
                if (existingBook == null)
                {
                    throw new NotFoundException($"Autor Livro com ID {author.AuthorId} não encontrado.");
                }

                _unitOfWork.BookAuthors.Remove(existingBook);
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

        public async Task<IEnumerable<BookAuthorsEntity>> GetAllBookAuthorsAsync()
        {
            try
            {
                return await _unitOfWork.BookAuthors.GetAllAsync();
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao buscar todos os livros.", ex);
            }
        }

        public async Task<bool> UpdateBookAuthorAsync(BookAuthorsEntity author, int bookNew)
        {
            try
            {
                var existingBook = await _unitOfWork.BookAuthors.GetByIdAsync(x => x.BookId == author.BookId && x.AuthorId == author.AuthorId);
                if (existingBook == null)
                {
                    throw new NotFoundException($"Autor Livro com ID {author.AuthorId} não encontrado.");
                }

                _unitOfWork.BookAuthors.Remove(existingBook);
                await _unitOfWork.CompleteAsync();

                var newBookAuthor = new BookAuthorsEntity
                {
                    BookId = bookNew,
                    AuthorId = author.AuthorId
                };

                _unitOfWork.BookAuthors.Add(newBookAuthor);

                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao atualizar o livro do banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao atualizar o livro.", ex);
            }

        }
    }
}
