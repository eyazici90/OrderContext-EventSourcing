using FluentValidation; 
using static OrderContext.Domain.Messages.Orders.Commands;

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
