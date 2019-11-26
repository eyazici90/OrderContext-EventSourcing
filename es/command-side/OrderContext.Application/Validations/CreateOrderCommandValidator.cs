using FluentValidation;
using OrderContext.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Application.Validations
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(p=> p.BuyerId).NotEmpty();
            RuleFor(p=> p.City).NotEmpty();
            RuleFor(p=> p.Street).NotEmpty();
        }
    }
}
