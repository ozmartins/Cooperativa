using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Response;

namespace Questao5.Infrastructure.Database.QueryStore;

internal class ContaCorrenteStore(SqliteConnection connection) : IContaCorrenteStore
{
    public async Task<SaldoContaCorrenteResponse> SelectSaldoAsync(string idContaCorrente)
    {
        const string sql = """
                           SELECT
                               SUM(
                                   CASE 
                                       WHEN tipomovimento = "C" THEN valor
                                       WHEN tipomovimento = "D" THEN -valor
                                       ELSE 0
                                   END
                               ) as saldo
                           FROM movimento 
                           WHERE idcontacorrente = @idContaCorrente
                           """;
        
        var saldo = await connection.QueryAsync<double?>(sql, new { idContaCorrente });
        
        var contaCorrente = await SelectAsync(idContaCorrente);

        return new SaldoContaCorrenteResponse{ ContaCorrente = contaCorrente, Saldo = saldo.FirstOrDefault() ?? 0 };
    }

    public async Task<ContaCorrente?> SelectAsync(string idContaCorrente)
    {
        const string sql = """
                           SELECT *
                           FROM contacorrente 
                           WHERE idcontacorrente = @idContaCorrente
                           """;
        
        var contaCorrente = 
            await connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { idContaCorrente });

        return contaCorrente;
    }
}