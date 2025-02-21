using Questao5.Domain.Language;

namespace Questao5.Application.Handlers.Exceptions;

internal class InvalidAccountException() : Exception(Resources.INVALID_ACCOUNT);