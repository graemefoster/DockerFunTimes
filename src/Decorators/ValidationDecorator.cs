using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace DDDPerth.Features.Assessment
{
    public class ValidationDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest: IRequest<TResponse>
    {
        private IEnumerable<IValidate<TRequest>> _validators;
        private IRequestHandler<TRequest, TResponse> _next;

        public ValidationDecorator(
            IEnumerable<IValidate<TRequest>> validators,
            IRequestHandler<TRequest, TResponse> next)
        {
            _validators = validators;
            _next = next;
        }
        public TResponse Handle(TRequest message)
        {
            var errors = _validators.SelectMany(x => x.Validate(message));
            if (errors.Any())
            {
                throw new ArgumentException(string.Join(Environment.NewLine, errors.Select(x => x.Message)));
            }
            return _next.Handle(message);
        }
    }

    public interface IValidate<TRequest> {
        IEnumerable<ValidationFault> Validate(TRequest request);
    }

    public class ValidationFault {
        public ValidationFault(string message) {
            this.Message = message;
        }
        public string Message {get;private set;}
    }
}
