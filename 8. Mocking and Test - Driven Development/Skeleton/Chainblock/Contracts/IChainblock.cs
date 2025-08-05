using System.Collections.Generic;
using Chainblock.Enums;

namespace Chainblock.Contracts;

public interface IChainblock
{ 
    int Count { get; }

    void Add(ITransaction transaction);
    
    bool Contains(ulong id);
    
    void ChangeTransactionStatus(ulong id, TransactionStatus newStatus);

    void RemoveTransactionById(ulong id);

    ITransaction GetById(ulong id);

    IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status);

    IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status);

    IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status);

    IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById();

    IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender);

    IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver);

    IEnumerable<ITransaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, decimal maxAllowedAmount);

    IEnumerable<ITransaction> GetBySenderAndMinimumAmountDescending(string sender, decimal minAmount);

    IEnumerable<ITransaction> GetByReceiverAndAmountRange(string receiver, decimal low, decimal high);

    IEnumerable<ITransaction> GetAllInAmountRange(decimal low, decimal high);
}