using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class ContaCorrenteController(IMediator mediator) : ControllerBase
{
    [HttpGet("Saldo")]
    public async Task<ActionResult<ObterSaldoResponse>> Get(string idContaCorrente)
    {
        try
        {
            var query = new ObterSaldoQuery { IdContaCorrente = idContaCorrente};
        
            var obterSaldoResponse = await mediator.Send(query);
        
            return new ActionResult<ObterSaldoResponse>(obterSaldoResponse);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
}