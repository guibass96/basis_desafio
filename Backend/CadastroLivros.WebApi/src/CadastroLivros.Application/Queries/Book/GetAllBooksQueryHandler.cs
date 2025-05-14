using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Author;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Queries.Book
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookDto>>
    {
        private readonly IBookService _bookService;
        public GetAllBooksQueryHandler(IBookService autorSerivice)
        {

            _bookService = autorSerivice;
        }

        public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookService.GetAllBooksAsync();

            return books.Select(book => new BookDto
            {
                Id = book.BookId,
                Title = book.Title,
                Publisher = book.Publisher,
                Edition = book.Edition,
                PublicationYear = book.PublicationYear,

            }).ToList();
        }
    }
}
