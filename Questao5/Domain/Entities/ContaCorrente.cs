namespace Questao5.Domain.Entities;

internal class ContaCorrente
{
    public Guid IdContaCorrente { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
}