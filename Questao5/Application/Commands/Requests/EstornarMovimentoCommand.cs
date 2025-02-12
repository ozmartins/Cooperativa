using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests;

internal class EstornarMovimentoCommand : IRequest<EstornarMovimentoResponse>
{
    public Guid IdMovimento { get; init; }
}