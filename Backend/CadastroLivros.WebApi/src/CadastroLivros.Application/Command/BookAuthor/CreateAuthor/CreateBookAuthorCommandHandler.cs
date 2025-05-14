using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.Livros.InserirLivro;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.BookAuthor.CreateAuhtor
{
    public class CreateBookAuthorCommandHandler : IRequestHandler<CreateBookAuthorCommand, int>
    {

        private readonly IBookAuthorService _bookAuthor;
        public CreateBookAuthorCommandHandler(IBookAuthorService bookAuthor)
        {
            _bookAuthor = bookAuthor;
        }

        public async Task<int> Handle(CreateBookAuthorCommand request, CancellationToken cancellationToken)
        {

            var bookAuthor = new BookAuthorsEntity
            {
                BookId = request.BookId,
                AuthorId = request.AuthorId
            };
            var result = await _bookAuthor.AddBookAuthorAsync(bookAuthor);
            return result;
        }
    }
}
