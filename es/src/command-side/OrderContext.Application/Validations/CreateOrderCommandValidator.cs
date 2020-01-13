using FluentValidation; 
using static OrderContext.Domain.Messages.Orders.Commands;

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
