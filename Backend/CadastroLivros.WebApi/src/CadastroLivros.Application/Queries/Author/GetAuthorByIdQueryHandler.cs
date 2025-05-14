using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Domain.Interfaces;
using MediatR;

namespace CadastroLivros.Application.Queries.Author
{

    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
    {
        private readonly IAuthorService _authorService; // Exemplo de serviço para pegar os autores

        public GetAuthorByIdQueryHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorService.GetAuthorByIdAsync(request.Id);

            return new AuthorDto() { Id = author.AuthorId, Name = author.Name };
        }
    }
}
