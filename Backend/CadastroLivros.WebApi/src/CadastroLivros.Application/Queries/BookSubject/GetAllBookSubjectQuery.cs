using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Application.Queries.Dtos;
using MediatR;

namespace CadastroLivros.Application.Queries.BookSubject
{
    public class GetAllBookSubjectQuery : IRequest<List<BookSubjectDto>> { } 
}
