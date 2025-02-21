using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Response;

namespace Questao5.Application.Handlers;

internal interface IContaCorrenteStore
{
    Task<SaldoContaCorrenteResponse> SelectSaldoAsync(string idContaCorrente);
    Task<ContaCorrente?> SelectAsync(string idContaCorrente);
}