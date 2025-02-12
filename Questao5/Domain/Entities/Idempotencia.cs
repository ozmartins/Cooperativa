using Newtonsoft.Json;

namespace Questao5.Domain.Entities;

public class Idempotencia(string chaveIdempotencia, string requisicao, string resultado)
{
    [JsonProperty(nameof(ChaveIdempotencia))]
    public string ChaveIdempotencia { get; } = chaveIdempotencia;

    [JsonProperty(nameof(Requisicao))]
    public string Requisicao { get; } = requisicao;

    [JsonProperty(nameof(Resultado))]
    public string Resultado { get; } = resultado;
}