using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.Livros.InserirLivro;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.Books.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookService _bookService;
        public DeleteBookCommandHandler(IBookService autorSerivice)
        {

            _bookService = autorSerivice;
        }

    

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookService.DeleteAuthorAsync(new BookEntity { 
                BookId = request.Id 
            });
        }
    }
}
