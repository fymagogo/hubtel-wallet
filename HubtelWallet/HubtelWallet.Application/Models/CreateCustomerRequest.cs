using FluentValidation;

namespace HubtelWallet.Application.Models
{
    public record CreateCustomerRequest(string PhoneNumber);

    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(cr => cr.PhoneNumber).NotNull().NotEmpty().NotEqual("string")
                .WithMessage("Phone Number is required please");
        }
    }
}
