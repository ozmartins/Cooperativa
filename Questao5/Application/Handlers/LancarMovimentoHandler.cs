using System.Data;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers.Exceptions;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore;

namespace Questao5.Application.Handlers;

public class LancarMovimentoHandler(
    IMovimentoStore movimentoStore,
    IContaCorrenteStore contaCorrenteStore,
    IIdempotenciaStore idempotenciaStore,
    SqliteConnection connection)
    : IRequestHandler<LancarMovimentoCommand, LancarMovimentoResponse>
{
    public async Task<LancarMovimentoResponse> Handle(LancarMovimentoCommand request,
        CancellationToken cancellationToken)
    {
        var contaCorrente = await contaCorrenteStore.SelectAsync(request.IdContaCorrente);

        if (contaCorrente is null)
            throw new InvalidAccountException();

        if (!contaCorrente.Ativo)
            throw new InactiveAccountException();

        if (request.Valor <= 0)
            throw new InvalidValueException();

        if (request.TipoMovimento != TipoMovimento.Credito && request.TipoMovimento != TipoMovimento.Debito)
            throw new InvalidTypeException();

        var idempotencia = await idempotenciaStore.SelectAsync(request.IdentificacaoRequisicao);

        if (idempotencia is not null)
        {
            var result = JsonConvert.DeserializeObject<Movimento>(idempotencia.Resultado);
            return new LancarMovimentoResponse { IdMovimento = result?.IdMovimento ?? Guid.Empty.ToString() };
        }

        var movimento = new Movimento(
            request.IdContaCorrente,
            request.DataMovimento,
            request.TipoMovimento,
            request.Valor);

        var response = new LancarMovimentoResponse { IdMovimento = movimento.IdMovimento };

        var novaIdempotencia = new Idempotencia(
            request.IdentificacaoRequisicao,
            JsonConvert.SerializeObject(movimento),
            JsonConvert.SerializeObject(response));

        if (connection.State != ConnectionState.Open)
            connection.Open();
        
        await using var transaction = connection.BeginTransaction();
        
        try
        {
            await movimentoStore.InsertAsync(movimento);
            await idempotenciaStore.InsertAsync(novaIdempotencia);
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }

        return response;
    }
}