using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.BookAuthor.DeleteAuhtorBook;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.BookSubject.DeleteBookSubject
{
    public class DeleteBookSubjectCommandHandler : IRequestHandler<DeleteBookSubjectCommand, bool>
    {
        private readonly IBookSubjectService _bookSubjectService;
        public DeleteBookSubjectCommandHandler(IBookSubjectService bookSubjectService)
        {
            _bookSubjectService = bookSubjectService;
        }

        public async Task<bool> Handle(DeleteBookSubjectCommand request, CancellationToken cancellationToken)
        {
            return await _bookSubjectService.DeleteBookSubjectAsync(new BookSubjectEntity
            {
                SubjectId = request.SubjectId,
                BookId = request.BookId,
            });
        }
    }
}
