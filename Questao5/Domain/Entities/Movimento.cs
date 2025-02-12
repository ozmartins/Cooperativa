using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

public class Movimento
{
    public Guid IdMovimento { get; }
    public Guid IdContaCorrente { get; }
    public DateTime DataMovimento { get; }
    public TipoMovimento TipoMovimento { get; }
    public double Valor { get; }
    
    public Movimento(Guid idContaCorrente, DateTime dataMovimento, TipoMovimento tipoMovimento, double valor)
    {
        IdMovimento = Guid.NewGuid();
        IdContaCorrente = idContaCorrente;
        DataMovimento = dataMovimento;
        TipoMovimento = tipoMovimento;
        Valor = valor;
    }
}