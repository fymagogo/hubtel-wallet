
using FluentValidation;
using HubtelWallet.Domain.Entities;

namespace HubtelWallet.Application.Models
{
    public record CreateWalletRequest(int CustomerId, string Name, WalletType WalletType, string AccountNumber, AccountScheme AccountScheme);

    public class CreateWalletRequestValidator : AbstractValidator<CreateWalletRequest>
    {
        public CreateWalletRequestValidator()
        {
            RuleFor(r => r.CustomerId).GreaterThan(0).WithMessage("Enter a valid customer Id");

            RuleFor(cr => cr.Name).NotNull().NotEmpty().NotEqual("string")
                .WithMessage("Name is required please");

            RuleFor(r => r.AccountNumber).MinimumLength(10).WithMessage("Should be a valid phone number or credit card number");
        }
    }
}
