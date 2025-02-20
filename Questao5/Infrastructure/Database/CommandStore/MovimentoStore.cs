using Dapper;
using Questao5.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore;

public class MovimentoStore(IConfiguration configuration)
{
    private readonly string _connectionString = configuration.GetValue<string>("DatabaseName");
    
    public async Task InsertAsync(Movimento movimento)
    {
        const string sql = """
                           INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                           VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)
                           """;
        
        await using var connection = new SqliteConnection(_connectionString);
        
        await connection.ExecuteAsync(sql, movimento);
    }
}