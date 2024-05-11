
namespace HubtelWallet.Application.Models
{
    public record CreateWallet(int CustomerId, string Name, string WalletType, string AccountNumber, string AccountScheme);
}
