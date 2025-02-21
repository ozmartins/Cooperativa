using Dapper;
using Questao5.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Questao5.Application.Handlers;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore;

internal class MovimentoStore(SqliteConnection connection) : IMovimentoStore
{
    public async Task InsertAsync(Movimento movimento)
    {
        const string sql = """
                           INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                           VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)
                           """;
        
        await connection.ExecuteAsync(sql, movimento);
    }
}