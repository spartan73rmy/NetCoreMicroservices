using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty()
                .WithMessage("El nombre de usuario no puede estar vacio");

            RuleFor(p => p.EmailAddress)
                .NotEmpty()
                .WithMessage("El email no puede quedar vacio")
                .EmailAddress(mode: FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        }
    }
}
