using Questao5.Domain.Language;

namespace Questao5.Application.Handlers.Exceptions;

internal class InvalidValueException() : Exception(Resources.INVALID_VALUE);