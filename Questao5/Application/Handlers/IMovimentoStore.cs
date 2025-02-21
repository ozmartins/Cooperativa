using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers;

internal interface IMovimentoStore
{
    Task InsertAsync(Movimento movimento);
}