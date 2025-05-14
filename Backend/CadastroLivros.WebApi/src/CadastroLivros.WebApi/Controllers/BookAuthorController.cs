using CadastroLivros.Application.Command.Author.UpdateAuthor;
using CadastroLivros.Application.Command.AuthorCommands.CreateAuthor;
using CadastroLivros.Application.Command.BookAuthor.CreateAuhtor;
using CadastroLivros.Application.Command.BookAuthor.DeleteAuhtorBook;
using CadastroLivros.Application.Command.BookAuthor.UpdateAuthor;
using CadastroLivros.Application.Command.Books.DeleteBook;
using CadastroLivros.Application.Command.Livros.InserirLivro;
using CadastroLivros.Application.Queries.Author;
using CadastroLivros.Application.Queries.BookAuthor;
using CadastroLivros.Application.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CadastroLivros.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookAuthorController : ControllerBase
    {
        private readonly ILogger<BookAuthorController> _logger;
        private readonly IMediator _mediator;

        public BookAuthorController(ILogger<BookAuthorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<BookAuthorDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllBookAuthorQuery());
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CreateBookAuthorCommand request)
        {
            var result = await _mediator.Send(request);
            return result > 0;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromBody] UpdateBookAuthorCommand request)
        {

            var result = await _mediator.Send(request);
            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(DeleteBookAuthorCommand request)
        {

            var result = await _mediator.Send(request);

            if (!result)
                return NotFound("Não encontrado.");

            return NoContent();
        }
    }
}
