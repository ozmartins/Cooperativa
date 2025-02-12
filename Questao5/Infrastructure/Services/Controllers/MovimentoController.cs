using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class MovimentoController : ControllerBase
{
    private readonly IMediator _mediator;

    public MovimentoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Lancar")]
    public async Task<ActionResult<Guid>> Post(
        Guid identificacaoRequisicao, 
        Guid idContaCorrente, 
        TipoMovimento tipoMovimento,
        double valor
        )
    {
        var comando = new LancarMovimentoCommand
        {
            IdentificacaoRequisicao = identificacaoRequisicao,
            IdContaCorrente = idContaCorrente,
            DataMovimento = DateTime.Now,
            TipoMovimento = tipoMovimento,
            Valor = valor
        };
        
        var movimento = await _mediator.Send(comando);
        
        return new ActionResult<Guid>(movimento.IdMovimento);
    }
    
    [HttpDelete("Estornar")]
    public async Task<ActionResult<Guid>> Delete(Guid idMovimento)
    {
        var comando = new EstornarMovimentoCommand { IdMovimento = idMovimento };
        
        var movimento = await _mediator.Send(comando);
        
        return new ActionResult<Guid>(movimento.IdMovimento);
    }
}