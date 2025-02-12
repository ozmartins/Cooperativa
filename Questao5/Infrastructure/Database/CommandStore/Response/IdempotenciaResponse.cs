namespace Questao5.Infrastructure.Database.CommandStore.Response;

public class IdempotenciaResponse
{
    public bool RegistroJaExiste { get; init; }
    public string Resultado { get; init; } = string.Empty;
}