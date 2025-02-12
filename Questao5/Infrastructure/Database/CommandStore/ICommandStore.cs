using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.CommandStore;

//TODO: implementar
public interface ICommandStore
{
    public void Save(Movimento movimento);
}