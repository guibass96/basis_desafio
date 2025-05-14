using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Dtos;
using MediatR;

namespace CadastroLivros.Application.Queries.Book
{
    public class GetAllBooksQuery : IRequest<List<BookDto>> { } 

}
