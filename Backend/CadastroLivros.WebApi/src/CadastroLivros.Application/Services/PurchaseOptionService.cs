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
    public class PurchaseOptionService : IPurchaseOptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddBookPurchaseOptionAsync(BookPurchaseOptionEntity bookOp)
        {
            try
            {
                _unitOfWork.BookPurchaseOptions.Add(bookOp);
                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao adicionar a opção de compra.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao adicionar a opção de compra.", ex);
            }
        }


        public async Task<bool> DeleteBookPurchaseOptionAsync(BookPurchaseOptionEntity bookOp)
        {
            try
            {
                var existingOption = await _unitOfWork.BookPurchaseOptions.GetByIdAsync(x => x.Id == bookOp.Id);
                if (existingOption == null)
                {
                    throw new NotFoundException($"Opção de compra com ID {bookOp.Id} não encontrado.");
                }
                _unitOfWork.BookPurchaseOptions.Remove(existingOption);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao deletar o opção de compra", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado opção de compra.", ex);
            }
        }

        public async Task<IEnumerable<BookPurchaseOptionEntity>> GetAllBookPurchaseOptionAsync()
        {
            try
            {
                return await _unitOfWork.BookPurchaseOptions.GetAllAsync();
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao buscar todos os livros.", ex);
            }
        }

        public Task<BookEntity> GetBookPurchaseOptionByIdAsync(int bookOpId)
        {
            throw new NotImplementedException();
        }

         public async Task<bool> UpdateBookPurchaseOptionAsync(BookPurchaseOptionEntity bookOp)
        {
            try
            {
                var existingOption = await _unitOfWork.BookPurchaseOptions.GetByIdAsync(x => x.Id == bookOp.Id);
                if (existingOption == null)
                {
                    throw new NotFoundException($"Opção de compra com ID {bookOp.Id} não encontrado.");
                }

                existingOption.Price = bookOp.Price;
                existingOption.PurchaseType = bookOp.PurchaseType;

                await _unitOfWork.CompleteAsync();
                return true;

            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao adicionar a opção de compra.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao adicionar a opção de compra.", ex);
            }
        }

        Task<BookPurchaseOptionEntity> IPurchaseOptionService.GetBookPurchaseOptionByIdAsync(int bookOpId)
        {
            throw new NotImplementedException();
        }
    }
}
