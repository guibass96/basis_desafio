using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Queries.Subject
{
    public class GetAllSubjectQueryHandler : IRequestHandler<GetAllSubjectQuery, List<SubjectDto>>
    {
        private readonly ISubjectService _subjectService;
        public GetAllSubjectQueryHandler(ISubjectService subjectService)
        {

            _subjectService = subjectService;
        }

        public async Task<List<SubjectDto>> Handle(GetAllSubjectQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();

            return subjects.Select(subject => new SubjectDto
            {
                id = subject.SubjectId,
               Description  = subject.Description
            }).ToList();
        }
    }
}
