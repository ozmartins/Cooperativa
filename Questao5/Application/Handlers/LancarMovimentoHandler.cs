using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore;

namespace Questao5.Application.Handlers;

internal class LancarMovimentoHandler : IRequestHandler<LancarMovimentoCommand, LancarMovimentoResponse>
{
    private readonly MovimentoStore _movimentoStore;

    public LancarMovimentoHandler(MovimentoStore movimentoStore)
    {
        _movimentoStore = movimentoStore;
    }

    public async Task<LancarMovimentoResponse> Handle(LancarMovimentoCommand request, CancellationToken cancellationToken)
    {
        var movimento = new Movimento(
            request.IdContaCorrente,
            request.DataMovimento,
            request.TipoMovimento,
            request.Valor);

        await _movimentoStore.Insert(movimento);
        
        var response = new LancarMovimentoResponse{ IdMovimento = movimento.IdMovimento };
        
        return response;
    }
}