using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.Subject.CreateSubject;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.SubjectCommand.DeleteSubject
{
    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, bool>
    {
        private readonly ISubjectService _subjectService;
        public DeleteSubjectCommandHandler(ISubjectService subjectService)
        {

            _subjectService = subjectService;
        }

        public async Task<bool> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            return await _subjectService.DeleteSubjectAsync(new SubjectEntity { SubjectId = request.Id, Description = "" });
        }
    }
}
