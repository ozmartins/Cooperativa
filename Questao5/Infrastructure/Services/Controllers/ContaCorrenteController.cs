using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class ContaCorrenteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContaCorrenteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Saldo")]
    public async Task<ActionResult<double>> Get(Guid idContaCorrente)
    {
        var query = new ObterSaldoQuery { IdContaCorrente = idContaCorrente};
        
        var saldo = await _mediator.Send(query);
        
        return new ActionResult<double>(saldo.Saldo);
    }
}