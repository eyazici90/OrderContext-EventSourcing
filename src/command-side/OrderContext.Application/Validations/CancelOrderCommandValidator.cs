using FluentValidation;
using OrderContext.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Application.Validations
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(p=> p.OrderNumber).NotEmpty();
        }
    }
}
