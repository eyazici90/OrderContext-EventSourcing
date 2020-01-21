using FluentValidation; 
using static OrderContext.Domain.Messages.Orders.Commands;

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
