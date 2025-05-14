using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.BookAuthor.CreateAuhtor;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.BookSubject.CreateBookSubject
{
    public class CreateBookSubjectCommandHandler : IRequestHandler<CreateBookSubjectCommand, int>
    {
        private readonly IBookSubjectService _bookSubjectService;
        public CreateBookSubjectCommandHandler(IBookSubjectService bookSubjectService)
        {
            _bookSubjectService = bookSubjectService;
        }

        public async Task<int> Handle(CreateBookSubjectCommand request, CancellationToken cancellationToken)
        {
            var bookAuthor = new BookSubjectEntity
            {
                BookId = request.BookId,
                SubjectId = request.SubjectId
            };
            var result = await _bookSubjectService.AddBookSubjectAsync(bookAuthor);
            return result;
        }
    }
}
