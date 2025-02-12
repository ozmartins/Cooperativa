using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Application.Handlers;

internal class ObterSaldoQueryHandler : IRequestHandler<ObterSaldoQuery, ObterSaldoResponse>
{
    private readonly IQueryStore _queryStore;

    public ObterSaldoQueryHandler(IQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public Task<ObterSaldoResponse> Handle(ObterSaldoQuery request, CancellationToken cancellationToken)
    {
        var saldo = _queryStore.Get();
        
        return Task.FromResult(new ObterSaldoResponse{ Saldo = saldo });
    }
}