using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Response;

internal class SaldoContaCorrenteResponse
{
    public ContaCorrente? ContaCorrente { get; set; }
    public double Saldo { get; init; }
}