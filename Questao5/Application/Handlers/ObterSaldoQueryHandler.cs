using MediatR;
using Questao5.Application.Handlers.Exceptions;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Handlers;

internal class ObterSaldoQueryHandler(IContaCorrenteStore contaCorrenteStore) : IRequestHandler<ObterSaldoQuery, ObterSaldoResponse>
{
    public async Task<ObterSaldoResponse> Handle(ObterSaldoQuery request, CancellationToken cancellationToken)
    {
        var contaCorrente = await contaCorrenteStore.SelectAsync(request.IdContaCorrente);
        
        if (contaCorrente is null)
            throw new InvalidAccountException();
        
        if (!contaCorrente.Ativo)
            throw new InactiveAccountException();
        
        var saldoContaCorrente = await contaCorrenteStore.SelectSaldoAsync(request.IdContaCorrente);

        return new ObterSaldoResponse
        {
            NumeroContaCorrente = saldoContaCorrente.ContaCorrente?.Numero.ToString() ?? string.Empty,
            NomeTitularContaCorrente = saldoContaCorrente.ContaCorrente?.Nome ?? string.Empty,
            DataHoraResposta = DateTime.Now,
            ValorSaldoAtual = saldoContaCorrente.Saldo
        };
    }
}