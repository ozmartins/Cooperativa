using Questao5.Domain.Language;

namespace Questao5.Application.Handlers.Exceptions;

internal class InactiveAccountException() : Exception(Resources.INACIVE_ACCOUNT);