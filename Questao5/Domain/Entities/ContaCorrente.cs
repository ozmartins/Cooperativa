namespace Questao5.Domain.Entities;

internal class ContaCorrente
{
    public Guid IdContaCorrente { get; }
    public int Numero { get; }
    public bool Ativo { get; private set; }
    public ContaCorrente(int numero)
    {
        IdContaCorrente = Guid.NewGuid();
        Numero = numero;
        Ativo = true;
    }
    public void EncerrarContaCorrente()
    {
        Ativo = false;
    }
}