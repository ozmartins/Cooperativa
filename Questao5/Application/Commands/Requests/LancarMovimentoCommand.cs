using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests;

internal class LancarMovimentoCommand : IRequest<LancarMovimentoResponse>
{
    public Guid IdentificacaoRequisicao { get; init; }
    public Guid IdContaCorrente { get; init; }
    public DateTime DataMovimento { get; init; }
    public TipoMovimento TipoMovimento { get; init; }
    public double Valor { get; init; }
}