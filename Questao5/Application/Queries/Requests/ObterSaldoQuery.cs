using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests;

internal class ObterSaldoQuery : IRequest<ObterSaldoResponse>
{
    public Guid IdContaCorrente { get; set; }
}