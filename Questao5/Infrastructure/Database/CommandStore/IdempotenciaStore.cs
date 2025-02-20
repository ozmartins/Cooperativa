using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Response;
using Questao5.Infrastructure.Database.QueryStore.Response;

namespace Questao5.Infrastructure.Database.CommandStore;

public class IdempotenciaStore(SqliteConnection connection) : IIdempotenciaStore
{
    public async Task InsertAsync(Idempotencia idempotencia)
    {
        const string sql = """
                           INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) 
                           VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)
                           """;
        
        await connection.ExecuteAsync(sql, idempotencia);
    }
    
    public async Task<IdempotenciaResponse?> SelectAsync(string chaveIdempotencia)
    {
        const string sql = """
                           SELECT *
                           FROM idempotencia
                           WHERE chave_idempotencia  = @chaveIdempotencia
                           """;
        
        var response = await connection.QueryFirstOrDefaultAsync<IdempotenciaResponse>(sql, new { chaveIdempotencia });

        return response;
    }
}
