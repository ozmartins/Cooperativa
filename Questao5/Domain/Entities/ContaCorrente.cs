namespace Questao5.Domain.Entities;

public class ContaCorrente
{
    public string IdContaCorrente { get; } = Guid.NewGuid().ToString();
    public int Numero { get; private set; } = 0; 
    public string Nome { get; private set; } = string.Empty;
    public bool Ativo { get; private set; } = true;
}