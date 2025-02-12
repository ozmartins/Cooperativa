using MediatR;
using Questao5.Application.Handlers.Exceptions;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.QueryStore;
using SQLitePCL;

namespace Questao5.Application.Handlers;

public class ObterSaldoQueryHandler(IConfiguration configuration) : IRequestHandler<ObterSaldoQuery, ObterSaldoResponse>
{
    private readonly ContaCorrenteStore _contaCorrenteStore = new(configuration);

    public async Task<ObterSaldoResponse> Handle(ObterSaldoQuery request, CancellationToken cancellationToken)
    {
        var contaCorrente = await _contaCorrenteStore.SelectAsync(request.IdContaCorrente);
        
        if (contaCorrente is null)
            throw new InvalidAccountException();
        
        if (!contaCorrente.Ativo)
            throw new InactiveAccountException();
        
        var saldoContaCorrente = await _contaCorrenteStore.SelectSaldoAsync(request.IdContaCorrente);

        return new ObterSaldoResponse
        {
            NumeroContaCorrente = saldoContaCorrente.ContaCorrente?.Numero.ToString() ?? string.Empty,
            NomeTitularContaCorrente = saldoContaCorrente.ContaCorrente?.Nome ?? string.Empty,
            DataHoraResposta = DateTime.Now,
            ValorSaldoAtual = saldoContaCorrente.Saldo
        };
    }
}