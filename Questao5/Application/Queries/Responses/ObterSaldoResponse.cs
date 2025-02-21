// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Questao5.Application.Queries.Responses;

public class ObterSaldoResponse
{
    public string NumeroContaCorrente { get; set; } = string.Empty;
    public string NomeTitularContaCorrente { get; set; } = string.Empty;
    public DateTime DataHoraResposta { get; set; }
    public double ValorSaldoAtual { get; set; }
}