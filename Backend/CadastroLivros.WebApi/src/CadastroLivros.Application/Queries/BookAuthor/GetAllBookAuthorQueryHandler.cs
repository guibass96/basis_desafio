using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Queries.PurchaseOption;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Queries.BookAuthor
{
    public class GetAllBookAuthorQueryHandler : IRequestHandler<GetAllBookAuthorQuery, List<BookAuthorDto>>
    {
        private readonly IBookAuthorService _bookAuthor;
        public GetAllBookAuthorQueryHandler(IBookAuthorService bookAuthor)
        {
            _bookAuthor = bookAuthor;
        }

        public async Task<List<BookAuthorDto>> Handle(GetAllBookAuthorQuery request, CancellationToken cancellationToken)
        {

            var bookAuthor = await _bookAuthor.GetAllBookAuthorsAsync();

            return bookAuthor.Select(book => new BookAuthorDto
            {
               BookId = book.BookId,
               BookName = book.Book.Title,
               AuthorId = book.AuthorId,
               NameAuthor = book.Author.Name

            }).ToList();

        }
    }
}
