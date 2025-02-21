using Questao5.Domain.Language;

namespace Questao5.Application.Handlers.Exceptions;

internal class InvalidTypeException() : Exception(Resources.INVALID_TYPE);