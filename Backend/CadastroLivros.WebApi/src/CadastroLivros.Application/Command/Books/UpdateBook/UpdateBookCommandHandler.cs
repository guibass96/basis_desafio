using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.Books.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IBookService _bookService;
        public UpdateBookCommandHandler(IBookService autorSerivice)
        {

            _bookService = autorSerivice;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new BookEntity { 
                BookId = request.Id,
                Title = request.Title,
                Publisher = request.Publisher,
                Edition = request.Edition,
                PublicationYear = request.PublicationYear,
            };
            var result = await _bookService.UpdateAuthorAsync(book);
            return result;
        }
    }
}
