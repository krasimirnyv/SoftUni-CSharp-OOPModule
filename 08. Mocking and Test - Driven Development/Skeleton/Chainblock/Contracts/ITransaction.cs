using Chainblock.Enums;

namespace Chainblock.Contracts;

public interface ITransaction
{
    ulong Id { get; }

    TransactionStatus Status { get; set; }

    string Sender { get; }

    string Receiver { get; }

    decimal Amount { get; }
}