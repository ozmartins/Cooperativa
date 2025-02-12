using Dapper;
using Questao5.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Questao5.Infrastructure.Database.CommandStore;

public class MovimentoStore 
{
    private readonly string _connectionString;

    public MovimentoStore(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseName");
    }

    public async Task Insert(Movimento movimento)
    {
        var connection = new SqlConnection(_connectionString);
        
        const string sql = """
                           INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                           VALUES (@IdMovimento, @IdContacorrente, @DataMovimento, @TipoMovimento, @Valor)
                           """;
        
        await connection.ExecuteAsync(sql, movimento);
    }
    
    public async Task<Movimento?> Select(Guid idMovimento)
    {
        var connection = new SqlConnection(_connectionString);

        const string sql = """
                           SELECT idmovimento, idcontacorrente, datamovimento, tipomovimento, valor 
                           FROM movimento 
                           WHERE idmovimento = @IdMovimento
                           """; 
        
        IEnumerable<Movimento?> movimentos = await connection.QueryAsync<Movimento>(sql, idMovimento);

        return movimentos.FirstOrDefault();
    }
}