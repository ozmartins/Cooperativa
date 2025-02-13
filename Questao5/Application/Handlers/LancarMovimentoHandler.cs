using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers.Exceptions;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Application.Handlers;

public class LancarMovimentoHandler(IConfiguration configuration)
    : IRequestHandler<LancarMovimentoCommand, LancarMovimentoResponse>
{
    private readonly MovimentoStore _movimentoStore = new(configuration);
    private readonly IdempotenciaStore _idempotenciaStore = new(configuration);
    private readonly ContaCorrenteStore _contaCorrenteStore = new(configuration);

    public async Task<LancarMovimentoResponse> Handle(LancarMovimentoCommand request,
        CancellationToken cancellationToken)
    {
        //TODO: usar Fluent|Validator
        var contaCorrente = await _contaCorrenteStore.SelectAsync(request.IdContaCorrente);
        
        if (contaCorrente is null)
            throw new InvalidAccountException();
        
        if (!contaCorrente.Ativo)
            throw new InactiveAccountException();
        
        if (request.Valor <= 0)
            throw new InvalidValueException();
        
        if (request.TipoMovimento != TipoMovimento.Credito && request.TipoMovimento != TipoMovimento.Debito)
            throw new InvalidTypeException();
        
        var idempotencia = await _idempotenciaStore.SelectAsync(request.IdentificacaoRequisicao);

        if (idempotencia is not null)
        {
            var result = JsonConvert.DeserializeObject<Movimento>(idempotencia.Resultado);
            return new LancarMovimentoResponse{ IdMovimento = result?.IdMovimento ?? Guid.Empty.ToString() };
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
        
        //TODO: usar transação aqui.
        await _movimentoStore.Insert(movimento);
        await _idempotenciaStore.Insert(novaIdempotencia);

        return response;
    }
}