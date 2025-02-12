using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class ContaCorrenteController : ControllerBase
{
    [HttpGet("Saldo")]
    public ActionResult<double> Get(Guid idContaCorrente)
    {
        return new ActionResult<double>(0);
    }
}