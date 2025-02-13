using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class MovimentoController(IMediator mediator) : ControllerBase
{
    [HttpPost("Lancar")]
    public async Task<ActionResult<string>> Post(
        string identificacaoRequisicao, 
        string idContaCorrente, 
        string tipoMovimento,
        double valor
        )
    {
        try
        {
            var comando = new LancarMovimentoCommand
            {
                IdentificacaoRequisicao = identificacaoRequisicao,
                IdContaCorrente = idContaCorrente,
                DataMovimento = DateTime.Now,
                TipoMovimento = tipoMovimento,
                Valor = valor
            };
        
            var movimento = await mediator.Send(comando);

            return new ActionResult<string>(movimento.IdMovimento);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}