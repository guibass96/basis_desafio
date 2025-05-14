using Azure.Core;
using CadastroLivros.Application.Command.Books.AddPurchaseOption;
using CadastroLivros.Application.Command.PurchaseOption.DeletePurchaseOption;
using CadastroLivros.Application.Command.PurchaseOption.UpdatePurchaseOption;
using CadastroLivros.Application.Command.Subject.CreateSubject;
using CadastroLivros.Application.Queries.Dtos;
using CadastroLivros.Application.Queries.PurchaseOption;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;

namespace CadastroLivros.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookPurchaseOptionController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;

        public BookPurchaseOptionController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<PurchaseOptionDto>>> Get()
        {
            var result = await _mediator.Send(new GetAllPurchaseOptionQuery {});
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CreatePurchaseOptionCommand request)
        {
            var result = await _mediator.Send(request);
            return result > 0;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromBody] UpdatePurchaseOptionCommand request)
        {
            if (request.Id == 0)
                return BadRequest("Error id invalid");

            var result = await _mediator.Send(request);
            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (id == 0)
                return BadRequest("Error id invalid");

            var result = await _mediator.Send(new DeletePurchaseOptionCommand { Id = id});
            if (!result)
                return NotFound();

            return Ok(result);
        }
    }
}
