using FluentValidation;
using OrderContext.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Application.Validations
{
    public class PayOrderCommandValidator: AbstractValidator<PayOrderCommand>
    {
        public PayOrderCommandValidator()
        {
            RuleFor(p=>p .OrderNumber).NotEmpty();
        }
    }
}
