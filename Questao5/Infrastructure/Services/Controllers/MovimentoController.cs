using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class MovimentoController : ControllerBase
{
    [HttpPost("Lancar")]
    public ActionResult<Guid> Post(
        Guid identificacaoRequisicao, 
        Guid idContaCorrente, 
        double valor,
        TipoMovimento tipoMovimento)
    {
        return new ActionResult<Guid>(new Movimento().IdMovimento);
    }
    
    [HttpDelete("Estornar")]
    public ActionResult<Guid> Delete(Guid idMovimento)
    {
        return new ActionResult<Guid>(new Movimento().IdMovimento);
    }
    
    [HttpGet("Obter")]
    public ActionResult<Movimento> Get(Guid idMovimento)
    {
        return new ActionResult<Movimento>(new Movimento());
    }
}