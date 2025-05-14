using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.BookAuthor.UpdateAuthor;
using CadastroLivros.Application.Command.Subject.UpdateSubject;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.BookSubject.UpdateBookSubject
{
    public class UpdateBookSubjectCommandHandler : IRequestHandler<UpdateBookSubjectCommand, bool>
    {
        private readonly IBookSubjectService _bookSubjectService;
        public UpdateBookSubjectCommandHandler(IBookSubjectService bookSubjectService)
        {
            _bookSubjectService = bookSubjectService;
        }

        public async Task<bool> Handle(UpdateBookSubjectCommand request, CancellationToken cancellationToken)
        {
            var bookAuthor = new BookSubjectEntity
            {
                BookId = request.BookId,
                SubjectId = request.SubjectId
            };
            var result = await _bookSubjectService.UpdateBookSubjectAsync(bookAuthor, request.SubjectNew);
            return result;
        }
    }
}
