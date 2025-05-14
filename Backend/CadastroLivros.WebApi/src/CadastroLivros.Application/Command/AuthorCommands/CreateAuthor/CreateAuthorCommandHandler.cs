using CadastroLivros.Application.Command.AuthorCommands.CreateAuthor;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Command.Author.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IAuthorService _autorSerivice;
        public CreateAuthorCommandHandler(IAuthorService autorSerivice)
        {

            _autorSerivice = autorSerivice;
        }
        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var autor = new AuthorEntity { Name = request.Name };
            var result = await _autorSerivice.AddAuthorAsync(autor);
            return result;
        }
    }
}
