using FluentValidation;
using OrderContext.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Application.Validations
{
    public class ShipOrderCommandValidator : AbstractValidator<ShipOrderCommand>
    {
        public ShipOrderCommandValidator()
        {
            RuleFor(p=> p.OrderNumber).NotEmpty();
        }
    }
}
