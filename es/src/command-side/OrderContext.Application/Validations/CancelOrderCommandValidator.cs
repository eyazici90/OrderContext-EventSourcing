using FluentValidation; 
using static OrderContext.Domain.Messages.Orders.Commands;

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
