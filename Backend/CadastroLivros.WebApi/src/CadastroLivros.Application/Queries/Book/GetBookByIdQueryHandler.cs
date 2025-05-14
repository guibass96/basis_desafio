using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Author;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Queries.Book
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IBookService _bookService;
        public GetBookByIdQueryHandler(IBookService autorSerivice)
        {

            _bookService = autorSerivice;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBookByIdAsync(request.Id);

             var bookDto = new BookDto
            {
                
                Title = book.Title,
                Publisher = book.Publisher,
                Edition = book.Edition,
                PublicationYear = book.PublicationYear,
            };

            return bookDto;
        }
    }
}
