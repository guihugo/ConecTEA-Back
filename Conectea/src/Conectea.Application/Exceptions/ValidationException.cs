namespace Conectea.Application.Exceptions;

public class ValidationException : Exception
{
    public IReadOnlyCollection<string> Errors { get; }

    public ValidationException(IEnumerable<string> errors)
        : base("Um ou mais erros de validação ocorreram.")
    {
        Errors = errors.ToList();
    }
}