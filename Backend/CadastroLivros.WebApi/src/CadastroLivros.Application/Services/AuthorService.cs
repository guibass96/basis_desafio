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
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAuthorAsync(AuthorEntity author)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(author.Name))
                {
                    throw new ArgumentException("O nome do autor não pode estar vazio.");
                }

                _unitOfWork.Author.Add(author);
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

        public async Task<bool> UpdateAuthorAsync(AuthorEntity author)
        {
            try
            {
                var existingAuthor = await _unitOfWork.Author.GetByIdAsync(x=> x.AuthorId == author.AuthorId);
                if (existingAuthor == null)
                {
                    throw new NotFoundException($"Autor com ID {author.AuthorId} não encontrado.");
                }

                existingAuthor.Name = author.Name;
                _unitOfWork.Author.Update(existingAuthor);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ApplicationException("Conflito de concorrência ao atualizar o autor.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao atualizar o autor no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao atualizar o autor.", ex);
            }
        }

        public async Task<IEnumerable<AuthorEntity>> GetAllAuthorsAsync()
        {
            try
            {
                return await _unitOfWork.Author.GetAllAsync();
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao buscar todos os autores.", ex);
            }
        }

        public async Task<AuthorEntity> GetAuthorByIdAsync(int authorId)
        {
            try
            {
                return await _unitOfWork.Author.GetByIdAsync(x => x.AuthorId == authorId);
            }
            catch (DbException ex)
            {
                throw new ApplicationException($"Erro ao buscar o autor com ID {authorId}.", ex);
            }
        }

        public async Task<bool> DeleteAuthorAsync(AuthorEntity author)
        {
            try
            {
                var existingAuthor = await _unitOfWork.Author.GetByIdAsync(x => x.AuthorId == author.AuthorId);

                if (existingAuthor == null)
                {
                    throw new NotFoundException($"Autor com ID {author.AuthorId} não encontrado.");
                }

                _unitOfWork.Author.Remove(existingAuthor);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao deletar o autor do banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao deletar o autor.", ex);
            }
        }
    }
}
