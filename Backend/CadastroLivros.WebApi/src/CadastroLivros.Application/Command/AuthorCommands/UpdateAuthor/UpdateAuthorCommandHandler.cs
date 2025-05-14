using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.Author.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, bool>
    {
        private readonly IAuthorService _autorSerivice;
        public UpdateAuthorCommandHandler(IAuthorService autorSerivice)
        {
            _autorSerivice = autorSerivice;
        }

        public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var autor = new AuthorEntity { AuthorId = request.Id, Name = request.Name };
            var result = await _autorSerivice.UpdateAuthorAsync(autor);
            return result;
        }
    }
}
