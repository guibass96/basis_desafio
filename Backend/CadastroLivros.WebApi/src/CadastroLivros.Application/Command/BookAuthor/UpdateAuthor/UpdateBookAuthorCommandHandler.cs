using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.BookAuthor.CreateAuhtor;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.BookAuthor.UpdateAuthor
{
    public class UpdateBookAuthorCommandHandler : IRequestHandler<UpdateBookAuthorCommand, bool>
    {
        private readonly IBookAuthorService _bookAuthor;
        public UpdateBookAuthorCommandHandler(IBookAuthorService bookAuthor)
        {
            _bookAuthor = bookAuthor;
        }

        public async  Task<bool> Handle(UpdateBookAuthorCommand request, CancellationToken cancellationToken)
        {
            var bookAuthor = new BookAuthorsEntity
            {
                BookId = request.BookId,
                AuthorId =  request.AuhtorId
            };
            var result = await _bookAuthor.UpdateBookAuthorAsync(bookAuthor,request.BookNew);
            return result;

        }
    }
}
