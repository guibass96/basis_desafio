using CadastroLivros.Application.Command.Books.DeleteBook;
using CadastroLivros.Application.Command.Subject.CreateSubject;
using CadastroLivros.Application.Command.Subject.UpdateSubject;
using CadastroLivros.Application.Command.SubjectCommand.DeleteSubject;
using CadastroLivros.Application.Queries.Book;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Queries.Subject;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CadastroLivros.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly IMediator _mediator;

        public SubjectController(ILogger<SubjectController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<bool>> CreateSubject([FromBody] CreateSubjectCommand request)
        {
            var result = await _mediator.Send(request);
            return result > 0;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSubject([FromBody] UpdateSubjectCommand request)
        {
            if (request.Id == 0)
                return BadRequest("Error id invalid");

            var result = await _mediator.Send(request);
            if (!result)
                return NotFound();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubjectById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetSubjectByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllSubjects()
        {
            var result = await _mediator.Send(new GetAllSubjectQuery());
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido.");

            var result = await _mediator.Send(new DeleteSubjectCommand { Id = id });

            if (!result)
                return NotFound("Não encontrado.");

            return NoContent();
        }
    }
}
