using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Command.Author.CreateAuthor;
using CadastroLivros.Application.Command.Author.DeleteAuthor;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.AuthorCommands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IAuthorService _autorSerivice;
        public DeleteAuthorCommandHandler(IAuthorService autorSerivice)
        {

            _autorSerivice = autorSerivice;
        }

 
        Task<bool> IRequestHandler<DeleteAuthorCommand, bool>.Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            return _autorSerivice.DeleteAuthorAsync(new AuthorEntity { AuthorId = request.Id,Name=""});
        }
    }
}
