using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Response;
using Questao5.Infrastructure.Database.QueryStore.Response;

namespace Questao5.Infrastructure.Database.CommandStore;

public class IdempotenciaStore(IConfiguration configuration)
{
    private readonly string _connectionString = configuration.GetValue<string>("DatabaseName");
    
    public async Task Insert(Idempotencia idempotencia)
    {
        const string sql = """
                           INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) 
                           VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)
                           """;
        
        await using var connection = new SqliteConnection(_connectionString);
        
        await connection.ExecuteAsync(sql, idempotencia);
    }
    
    public async Task<IdempotenciaResponse> SelectAsync(string chaveIdempotencia)
    {
        const string sql = """
                           SELECT count(*) as qtd
                           FROM idempotencia
                           WHERE chave_idempotencia  = @chaveIdempotencia
                           """;
        
        var connection = new SqliteConnection(_connectionString);
        
        var response = await connection.QueryAsync<IdempotenciaResponse>(sql, new { chaveIdempotencia = chaveIdempotencia.ToString() });

        return response.FirstOrDefault() ?? new IdempotenciaResponse{ RegistroJaExiste = false };
    }
}
