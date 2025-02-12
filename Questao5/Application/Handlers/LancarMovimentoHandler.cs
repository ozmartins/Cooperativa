using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore;

namespace Questao5.Application.Handlers;

internal class LancarMovimentoHandler : IRequestHandler<LancarMovimentoCommand, LancarMovimentoResponse>
{
    private readonly ICommandStore _commandStore;

    public LancarMovimentoHandler(ICommandStore commandStore)
    {
        _commandStore = commandStore;
    }

    public Task<LancarMovimentoResponse> Handle(LancarMovimentoCommand request, CancellationToken cancellationToken)
    {
        var movimento = new Movimento(
            request.IdContaCorrente,
            request.DataMovimento,
            request.TipoMovimento,
            request.Valor);

        _commandStore.Save(movimento);
        
        var response = new LancarMovimentoResponse{ IdMovimento = movimento.IdMovimento };
        
        return Task.FromResult(response);
    }
}