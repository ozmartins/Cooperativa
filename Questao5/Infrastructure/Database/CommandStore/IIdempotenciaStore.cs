using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Response;

namespace Questao5.Infrastructure.Database.CommandStore;

public interface IIdempotenciaStore
{
    Task InsertAsync(Idempotencia idempotencia);
    Task<IdempotenciaResponse?> SelectAsync(string chaveIdempotencia);
}