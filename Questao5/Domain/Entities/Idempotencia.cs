namespace Questao5.Domain.Entities;

internal class Idempotencia
{
    public Guid ChaveIdempotencia { get; set; }
    public string Requisicao { get; set; } = string.Empty;
    public string Resultado { get; set; } = string.Empty;
}