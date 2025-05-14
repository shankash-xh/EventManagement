using FluentValidation;
using MediatR;
using FluentValidation.Results;
namespace EventManagement.Application.Behaviour;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
: IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        if (validators.Any())
        {
            ValidationContext<TRequest>? context = new ValidationContext<TRequest>(request);

            ValidationResult[]? validationResults = await Task.WhenAll(
                validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);

           List<ValidationFailure>? failures = validationResults
                .Where(r => r.Errors.Count > 0)
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count > 0)
                throw new FluentValidation.ValidationException(failures);
        }
        return await next().ConfigureAwait(false);
    }
}
