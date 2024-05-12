
using FluentValidation;
using HubtelWallet.Domain.Entities;

namespace HubtelWallet.Application.Models
{
    public record CreateWalletRequest(int CustomerId, string Name, WalletType WalletType, string AccountNumber, AccountScheme AccountScheme, DateTime IssueDate, DateTime ExpiryDate, string CVC);

    public class CreateWalletRequestValidator : AbstractValidator<CreateWalletRequest>
    {
        public CreateWalletRequestValidator()
        {
            RuleFor(r => r.CustomerId).GreaterThan(0).WithMessage("Enter a valid customer Id");

            RuleFor(r => r.WalletType).NotEqual(WalletType.unknown)
                .WithMessage("Wallet Type should not be unknown");

            RuleFor(cr => cr.Name).NotNull().NotEmpty().NotEqual("string")
                .WithMessage("Name is required please");

            RuleFor(r => r.AccountNumber).MinimumLength(10)
                .When(r => r.WalletType is WalletType.momo)
                .WithMessage("'{PropertyName}' Should be a valid phone number and have minimum length of 10");

            RuleFor(r => r.AccountNumber).MaximumLength(12)
                .When(r => r.WalletType is WalletType.momo)
                .WithMessage("'{PropertyName}' Should be a valid phone number and have max length of 12");

            RuleFor(r => r.AccountNumber).MinimumLength(16)
                .When(r => r.WalletType is WalletType.card)
                .WithMessage(" '{PropertyName}'Should be a valid card number and have minimum length of 16");

            RuleFor(r => r.IssueDate).Must(BeAValidDate)
                .When(r => r.WalletType is WalletType.card)
                .WithMessage("'{PropertyName}' Should be a valid date");

            RuleFor(r => r.ExpiryDate).Must(BeAValidDate)
                .When(r => r.WalletType is WalletType.card)
                .WithMessage("'{PropertyName}' Should be a valid date");

            RuleFor(r => r.ExpiryDate).GreaterThan(r => r.IssueDate)
                .When(r => r.WalletType is WalletType.card)
                .WithMessage("'{PropertyName}' Should be greater than the issue date");

            RuleFor(r => r.CVC).Length(3)
                .When(r => r.WalletType is WalletType.card)
                .WithMessage("'{PropertyName}' Should be 3 characters.");
        }

        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}
