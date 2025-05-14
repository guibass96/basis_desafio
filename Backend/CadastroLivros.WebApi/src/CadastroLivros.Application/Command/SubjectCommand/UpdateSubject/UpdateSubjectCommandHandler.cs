using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.Subject.UpdateSubject;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.SubjectCommands.UpdateSubject
{
    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, bool>
    {
        private readonly ISubjectService _subjectService;
        public UpdateSubjectCommandHandler(ISubjectService subjectService)
        {

            _subjectService = subjectService;
        }

        public async Task<bool> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = new SubjectEntity
            {
                SubjectId = request.Id,
                Description = request.Description
            };
            var result = await _subjectService.UpdateSubjectAsync(subject);
            return result;
        }
    }
}
