using CadastroLivros.Application.Command.BookSubject.CreateBookSubject;
using CadastroLivros.Application.Command.BookSubject.DeleteBookSubject;
using CadastroLivros.Application.Command.BookSubject.UpdateBookSubject;
using CadastroLivros.Application.Command.Livros.InserirLivro;
using CadastroLivros.Application.Command.SubjectCommand.DeleteSubject;
using CadastroLivros.Application.Queries.BookSubject;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Queries.PurchaseOption;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CadastroLivros.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookSubjectController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;

        public BookSubjectController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookSubjectDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllBookSubjectQuery { });
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CreateBookSubjectCommand request)
        {
            var result = await _mediator.Send(request);
            return result > 0;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromBody] UpdateBookSubjectCommand request)
        {
            var result = await _mediator.Send(request);
            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteBookSubjectCommand request)
        {

            var result = await _mediator.Send(request);

            if (!result)
                return NotFound("Não encontrado.");

            return NoContent();
        }
    }
}
