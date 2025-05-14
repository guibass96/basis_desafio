using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.BookAuthor.UpdateAuthor;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.BookAuthor.DeleteAuhtorBook
{
    public class DeleteBookAuthorCommandHandler : IRequestHandler<DeleteBookAuthorCommand, bool>
    {
        private readonly IBookAuthorService _bookAuthor;
        public DeleteBookAuthorCommandHandler(IBookAuthorService bookAuthor)
        {
            _bookAuthor = bookAuthor;
        }

        public async Task<bool> Handle(DeleteBookAuthorCommand request, CancellationToken cancellationToken)
        {
            return await _bookAuthor.DeleteBookAuthorAsync(new BookAuthorsEntity
            {
               AuthorId = request.AuthorId,
               BookId = request.BookId,
            });

        }
    }
}
