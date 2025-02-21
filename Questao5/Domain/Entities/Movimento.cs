using Newtonsoft.Json;

namespace Questao5.Domain.Entities;

internal class Movimento(string idContaCorrente, DateTime dataMovimento, string tipoMovimento, double valor)
{
    [JsonProperty(nameof(IdMovimento))]
    public string IdMovimento { get; } = Guid.NewGuid().ToString();
    
    [JsonProperty(nameof(IdContaCorrente))]
    public string IdContaCorrente { get; } = idContaCorrente;
    
    [JsonProperty(nameof(DataMovimento))]
    public DateTime DataMovimento { get; } = dataMovimento;
    
    [JsonProperty(nameof(TipoMovimento))]
    public string TipoMovimento { get; } = tipoMovimento;
    
    [JsonProperty(nameof(Valor))]
    public double Valor { get; } = valor;
}