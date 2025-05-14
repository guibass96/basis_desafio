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
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<AuthorDto>>
    {
        private readonly IAuthorService _authorService; 

        public GetAllAuthorsQueryHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<List<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
 
            var authors = await _authorService.GetAllAuthorsAsync();

            return authors.Select(author => new AuthorDto
            {
                Id = author.AuthorId,
                Name = author.Name
            }).ToList();
        }
    }
}
