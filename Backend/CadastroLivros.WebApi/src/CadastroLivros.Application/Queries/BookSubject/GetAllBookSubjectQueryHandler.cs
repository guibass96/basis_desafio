using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Queries.Subject;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Queries.BookSubject
{
    public class GetAllBookSubjectQueryHandler : IRequestHandler<GetAllBookSubjectQuery, List<BookSubjectDto>>
    {

        private readonly IBookSubjectService _bookSubjectService;
        public GetAllBookSubjectQueryHandler(IBookSubjectService bookSubjectService)
        {

            _bookSubjectService = bookSubjectService;
        }
        public async Task<List<BookSubjectDto>> Handle(GetAllBookSubjectQuery request, CancellationToken cancellationToken)
        {
            var bookSubjects = await _bookSubjectService.GetAllBookSubjectsAsync();

            return bookSubjects.Select(bookSubject => new BookSubjectDto
            {
                BookId = bookSubject.BookId,
                BookName = bookSubject.Book.Title,
                SubjectId = bookSubject.SubjectId,
                SubjectionDescription = bookSubject.Subject.Description

            }).ToList();
        }
    }
}
