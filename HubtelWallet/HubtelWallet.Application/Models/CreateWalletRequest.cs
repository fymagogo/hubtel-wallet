
namespace HubtelWallet.Application.Models
{
    public record CreateWalletRequest(int CustomerId, string Name, string WalletType, string AccountNumber, string AccountScheme);
}
