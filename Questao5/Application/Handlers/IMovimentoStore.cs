using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers;

public interface IMovimentoStore
{
    Task InsertAsync(Movimento movimento);
}