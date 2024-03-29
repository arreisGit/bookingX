// using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace BookingX.Core.Application.MediatRBehaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);
        var validationFailures =  _validators
                                .Select(validator => validator.Validate(request))
                                .SelectMany(validationResult => validationResult.Errors)
                                .Where(validationFailure => validationFailure != null)
                                .ToList();
                                
        if (validationFailures.Any())
        {
            throw new ValidationException(validationFailures);
        }
        return await next();
    }
}
}