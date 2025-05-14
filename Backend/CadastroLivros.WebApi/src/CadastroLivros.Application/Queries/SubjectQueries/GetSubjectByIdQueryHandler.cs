using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Book;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Queries.Subject
{
    public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, SubjectDto>
    {
        private readonly ISubjectService _subjectService;
        public GetSubjectByIdQueryHandler(ISubjectService subjectService)
        {

            _subjectService = subjectService;
        }

  
        public async Task<SubjectDto> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(request.Id);

            var subjectDto = new SubjectDto
            {
                Description = subject.Description
            };

            return subjectDto;
        }
    }
}
