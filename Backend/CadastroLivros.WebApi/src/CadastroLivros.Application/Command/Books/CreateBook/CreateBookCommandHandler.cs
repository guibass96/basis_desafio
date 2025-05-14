using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.Livros.InserirLivro;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.Books.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookService _bookService;
        public CreateBookCommandHandler(IBookService autorSerivice)
        {

            _bookService = autorSerivice;
        }
        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {

            var autor = new BookEntity { 
                Title = request.Title,
                Edition = request.Edition,
                Publisher = request.Publisher,
                PublicationYear = request.PublicationYear
            };
            var result = await _bookService.AddBookAsync(autor);
            return result;
        }

 
    }
}
