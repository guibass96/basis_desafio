using CadastroLivros.Application.Command.Author.DeleteAuthor;
using CadastroLivros.Application.Command.Books.DeleteBook;
using CadastroLivros.Application.Command.Books.UpdateBook;
using CadastroLivros.Application.Command.Livros.InserirLivro;
using CadastroLivros.Application.Queries.Author;
using CadastroLivros.Application.Queries.Book;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroLivros.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;

        public BookController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            var result = await _mediator.Send(new GetAllBooksQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetBookById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateBook([FromBody] CreateBookCommand request)
        {
            var result = await _mediator.Send(request);
            return result > 0;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBook([FromBody] UpdateBookCommand request)
        {
            var result = await _mediator.Send(request);
            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido.");

            var result = await _mediator.Send(new DeleteBookCommand { Id = id });

            if (!result)
                return NotFound("Não encontrado.");

            return NoContent();
        }



    }
}
