using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CadastroLivros.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _bookService = new BookService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddBookAsync_ShouldReturnResult_WhenSuccessful()
        {
           
            var book = new BookEntity();
            _unitOfWorkMock.Setup(x => x.Book.Add(book));
            _unitOfWorkMock.Setup(x => x.CompleteAsync()).ReturnsAsync(1);

            var result = await _bookService.AddBookAsync(book);

            
            Assert.Equal(1, result);
            _unitOfWorkMock.Verify(x => x.Book.Add(book), Times.Once);
            _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnBooks_WhenSuccessful()
        {
            var books = new List<BookEntity> { new BookEntity { BookId = 1 } };
            _unitOfWorkMock.Setup(x => x.Book.GetAllAsync()).ReturnsAsync(books);

            var result = await _bookService.GetAllBooksAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            _unitOfWorkMock.Verify(x => x.Book.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnBook_WhenExists()
        {
            var book = new BookEntity { BookId = 1 };
            _unitOfWorkMock.Setup(x => x.Book.GetByIdAsync(It.IsAny<Expression<Func<BookEntity, bool>>>()))
                           .ReturnsAsync(book);

            var result = await _bookService.GetBookByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.BookId);
            _unitOfWorkMock.Verify(x => x.Book.GetByIdAsync(It.IsAny<Expression<Func<BookEntity, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAuthorAsync_ShouldReturnTrue_WhenBookExists()
        {
            var existingBook = new BookEntity { BookId = 1 };
            var updateBook = new BookEntity { BookId = 1, Title = "Updated" };

            _unitOfWorkMock.Setup(x => x.Book.GetByIdAsync(It.IsAny<Expression<Func<BookEntity, bool>>>()))
                           .ReturnsAsync(existingBook);
            _unitOfWorkMock.Setup(x => x.CompleteAsync()).ReturnsAsync(1);

            var result = await _bookService.UpdateAuthorAsync(updateBook);

            Assert.True(result);
            _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAuthorAsync_ShouldReturnFalse_WhenBookNotFound()
        {
            _unitOfWorkMock.Setup(x => x.Book.GetByIdAsync(It.IsAny<Expression<Func<BookEntity, bool>>>()))
                           .ReturnsAsync((BookEntity)null);

            var result = await _bookService.UpdateAuthorAsync(new BookEntity { BookId = 1 });

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAuthorAsync_ShouldReturnTrue_WhenBookExists()
        {
            var existingBook = new BookEntity { BookId = 1 };
            _unitOfWorkMock.Setup(x => x.Book.GetByIdAsync(It.IsAny<Expression<Func<BookEntity, bool>>>()))
                           .ReturnsAsync(existingBook);

            _unitOfWorkMock.Setup(x => x.CompleteAsync()).ReturnsAsync(1);

            var result = await _bookService.DeleteAuthorAsync(existingBook);

            Assert.True(result);
            _unitOfWorkMock.Verify(x => x.Book.Remove(existingBook), Times.Once);
            _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAuthorAsync_ShouldReturnFalse_WhenBookNotFound()
        {
            _unitOfWorkMock.Setup(x => x.Book.GetByIdAsync(It.IsAny<Expression<Func<BookEntity, bool>>>()))
                           .ReturnsAsync((BookEntity)null);

            var result = await _bookService.DeleteAuthorAsync(new BookEntity { BookId = 1 });

            Assert.False(result);
        }
    }
}
