using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests;

public class LancarMovimentoCommand : IRequest<LancarMovimentoResponse>
{
    public string IdentificacaoRequisicao { get; init; } = string.Empty;
    public string IdContaCorrente { get; init; } = string.Empty;
    public DateTime DataMovimento { get; init; }
    public string TipoMovimento { get; init; } = string.Empty;
    public double Valor { get; init; }
}