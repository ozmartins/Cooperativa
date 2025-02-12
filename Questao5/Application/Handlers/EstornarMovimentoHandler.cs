using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore;

namespace Questao5.Application.Handlers;

internal class EstornarMovimentoHandler : IRequestHandler<EstornarMovimentoCommand, EstornarMovimentoResponse>
{
    private readonly ICommandStore _commandStore;

    public EstornarMovimentoHandler(ICommandStore commandStore)
    {
        _commandStore = commandStore;
    }

    public Task<EstornarMovimentoResponse> Handle(EstornarMovimentoCommand request, CancellationToken cancellationToken)
    {
        var movimentoOriginal = new Movimento(Guid.Empty, DateTime.Now, TipoMovimento.Credito, 0);

        var tipoMovimento = movimentoOriginal.TipoMovimento.Equals(TipoMovimento.Credito)
            ? TipoMovimento.Debito
            : TipoMovimento.Credito;
        
        var movimento = new Movimento(
            movimentoOriginal.IdContaCorrente,
            movimentoOriginal.DataMovimento,
            tipoMovimento,
            movimentoOriginal.Valor);
        
        _commandStore.Save(movimento);
        
        var response = new EstornarMovimentoResponse{ IdMovimento = movimento.IdMovimento };
        
        return Task.FromResult(response);
    }
}