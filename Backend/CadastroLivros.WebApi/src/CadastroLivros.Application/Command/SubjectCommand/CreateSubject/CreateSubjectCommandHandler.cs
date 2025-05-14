using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.Subject.CreateSubject;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.SubjectCommands.CreateSubject
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, int>
    {
        private readonly ISubjectService _subjectService;
        public CreateSubjectCommandHandler(ISubjectService subjectService)
        {

            _subjectService = subjectService;
        }

        public async Task<int> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = new SubjectEntity
            {
                Description = request.Description
            };
            var result = await _subjectService.AddSubjectAsync(subject);
            return result;
        }
    }
}
