using System;

namespace Questao1;

public class ContaBancaria 
{
    private const double TaxaParaSaque = 3.50;
    public ContaBancaria(int numero, string nomeTitular, double depositoInicial = 0.0)
    {
        Numero = numero;
        Deposito(depositoInicial);
        AlterarNomeTitular(nomeTitular);
    }

    ///<summary>
    ///Estou usando int como tipo de dados para essa propriedade, pois ela permite um número de conta de até 9 dígitos
    ///ou um número de conta de até 10 dígitos desde que não ultrasse o número 2.147.483.647 (int.MaxValue).
    ///Também estou supondo que a instituiçao trabalhe com números de contas puramente numéricos.
    ///Para finalizar, como não foi mencionado nada sobre isso, não estou considerando a existência de DV poara o numero
    ///da conta bancária.
    ///</summary>
    private int Numero { get; }
    private string NomeTitular { get; set; }

    ///<summary>
    ///A manutenção de uma propriedade "SaldoAtual" nessa entidade pode ser questionada em cenários reais pois pode
    ///induzir a erros de integridade de dados. No entanto, tem a vantagem de permir se obter o saldo atual da conta 
    ///com muita agilidade. Levando-se em conta que não existe nenhum requisitoo específico sobre o armazenamento de tal
    ///saldo, optei por criar essa propriedade.
    ///</summary>
    private double SaldoAtual { get; set; }

    private void AlterarNomeTitular(string novoNomeTitular)
    {
        if (string.IsNullOrEmpty(novoNomeTitular))
            throw new ArgumentException("O novo nome do títular não pode ser vazio", nameof(novoNomeTitular));
        NomeTitular = novoNomeTitular;
    }

    public void Deposito(double valor)
    {
        if (valor < 0.0)
            throw new ArgumentException("O valor do deposito deve ser maior que zero", nameof(valor));
        SaldoAtual += valor;
    }
    
    public void Saque(double valor)
    {
        if (valor < 0.0)
            throw new ArgumentException("O valor do saque deve ser maior que zero", nameof(valor));
        
        //Uma definição sobre um limite para a conta não foi definida nos requisitos, portanto estou validando apenas se
        //o saque não deixará a conta com valor inferior ao menor  double aceito pelo C#. 
        if (SaldoAtual - valor - TaxaParaSaque <= double.MinValue)
            throw new Exception("A conta atingiu seu valor negativo máximo e por isso o saque não pode ser realizado");
        SaldoAtual -= valor + TaxaParaSaque;
    }

    public override string ToString()
    {
        return $"Conta {Numero.ToString()}, Titular: {NomeTitular}, Saldo: {SaldoAtual:C}"; 
    }
}