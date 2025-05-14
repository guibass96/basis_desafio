using CadastroLivros.Application.Command.Author.CreateAuthor;
using CadastroLivros.Application.Command.Author.DeleteAuthor;
using CadastroLivros.Application.Command.Author.UpdateAuthor;
using CadastroLivros.Application.Command.AuthorCommands.CreateAuthor;
using CadastroLivros.Application.Queries.Author;
using CadastroLivros.Application.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CadastroLivros.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IMediator _mediator;

        public AuthorController(ILogger<AuthorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<string>> CreateAuthor([FromBody] CreateAuthorCommand request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetAuthorById), new { id = result }, result);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand request)
        {

            var result = await _mediator.Send(request);
            if (!result)
                return NotFound();

            return Ok(new { success = true });
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAllAuthors()
        {
            var result = await _mediator.Send(new GetAllAuthorsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetAuthorByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuhor(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido.");

            var result = await _mediator.Send(new DeleteAuthorCommand { Id = id });

            if (!result)
                return NotFound("Não encontrado.");

            return NoContent();
        }
    }
}
