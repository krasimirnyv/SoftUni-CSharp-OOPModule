using System;
using System.Collections.Generic;
using System.Linq;
using Chainblock.Contracts;
using Chainblock.Enums;
using Chainblock.Exceptions;

namespace Chainblock.Models;

public class Chainblock : IChainblock
{
    private readonly IDictionary<ulong, ITransaction> _transactions;
    
    public Chainblock()
    {
        _transactions = new Dictionary<ulong, ITransaction>();
    }

    public int Count => _transactions.Count;
    
    public void Add(ITransaction transaction)
    {
        if (Contains(transaction.Id))
        {
            throw new ArgumentException(ExceptionMessages.TransactionAlreadyExists);
        }
        
        _transactions.Add(transaction.Id, transaction);
    }
    
    public bool Contains(ulong id)
        => _transactions.ContainsKey(id);
    

    public void ChangeTransactionStatus(ulong id, TransactionStatus newStatus)
    {
        if (!Contains(id)) 
            throw new InvalidOperationException(ExceptionMessages.TransactionNotFound);

        _transactions[id].Status = newStatus;
    }

    public void RemoveTransactionById(ulong id)
    {
        if (!Contains(id)) 
            throw new InvalidOperationException(ExceptionMessages.TransactionNotFound);
        
        _transactions.Remove(id);
    }

    public ITransaction GetById(ulong id)
    {
        if (!Contains(id)) 
            throw new InvalidOperationException(ExceptionMessages.TransactionNotFound);
        
        return _transactions[id];
    }

    public IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status)
    {
        if (!Enum.IsDefined(typeof(TransactionStatus), status))
            throw new ArgumentException(ExceptionMessages.InvalidTransactionStatus);

        IList<ITransaction> transactionsByStatus = new List<ITransaction>();

        foreach (ITransaction transaction in _transactions.Values)
        {
            if (transaction.Status == status)
                transactionsByStatus.Add(transaction);
        }

        if (transactionsByStatus.Count == 0)
            throw new InvalidOperationException(ExceptionMessages.NoTransactionsWithGivenStatus);

        IEnumerable<ITransaction> sortedTransactions = transactionsByStatus.OrderBy(t => t.Amount);
        return sortedTransactions;
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        if (!Enum.IsDefined(typeof(TransactionStatus), status))
            throw new ArgumentException(ExceptionMessages.InvalidTransactionStatus);

        IList<string> sendersWithStatus = _transactions
            .Where(t => t.Value.Status == status)
            .OrderBy(t => t.Value.Amount)
            .Select(t => t.Value.Sender)
            .ToList();
        
        if (sendersWithStatus.Count == 0)
            throw new InvalidOperationException(ExceptionMessages.NoTransactionsWithGivenStatus);

        return sendersWithStatus;
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        if (!Enum.IsDefined(typeof(TransactionStatus), status))
            throw new ArgumentException(ExceptionMessages.InvalidTransactionStatus);

        IList<string> receiversWithStatus = _transactions
            .Where(t => t.Value.Status == status)
            .OrderBy(t => t.Value.Amount)
            .Select(t => t.Value.Receiver)
            .ToList();
        
        if (receiversWithStatus.Count == 0)
            throw new InvalidOperationException(ExceptionMessages.NoTransactionsWithGivenStatus);

        return receiversWithStatus;
    }

    public IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById()
    {
        IEnumerable<ITransaction> sortedTransactions = _transactions.Values
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id);

        return sortedTransactions;
    }

    public IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        IEnumerable<ITransaction> sortedTransactions = _transactions.Values
            .Where(t => t.Sender == sender)
            .OrderByDescending(t => t.Amount);
        
        if(!sortedTransactions.Any())
            throw new InvalidOperationException(string.Format(ExceptionMessages.NoTransactionsWithSender, sender));
        
        return sortedTransactions;
    }

    public IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        IEnumerable<ITransaction> sortedTransactions = _transactions.Values
            .Where(t => t.Receiver == receiver)
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id);
        
        if (!sortedTransactions.Any())
            throw new InvalidOperationException(string.Format(ExceptionMessages.NoTransactionsWithReceiver, receiver));
        
        return sortedTransactions;
    }

    public IEnumerable<ITransaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, decimal maxAllowedAmount)
    {
        IEnumerable<ITransaction> filteredTransactions = _transactions.Values
            .Where(t => t.Status == status)
            .Where(t => t.Amount <= maxAllowedAmount)
            .OrderByDescending(t => t.Amount);

        return filteredTransactions;
    }

    public IEnumerable<ITransaction> GetBySenderAndMinimumAmountDescending(string sender, decimal minAmount)
    {
        IEnumerable<ITransaction> filteredTransactions = _transactions.Values
            .Where(t => t.Sender == sender)
            .Where(t => t.Amount > minAmount)
            .OrderByDescending(t => t.Amount);
        
        if (!filteredTransactions.Any())
            throw new InvalidOperationException(string.Format(ExceptionMessages.NoTransactionsWithSender, sender));
        
        return filteredTransactions;
    }

    public IEnumerable<ITransaction> GetByReceiverAndAmountRange(string receiver, decimal low, decimal high)
    {
        IEnumerable<ITransaction> filteredTransactions = _transactions.Values
            .Where(t => t.Receiver == receiver)
            .Where(t => t.Amount >= low && t.Amount < high)
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id);
        
        if (!filteredTransactions.Any())
            throw new InvalidOperationException(string.Format(ExceptionMessages.NoTransactionsWithReceiver, receiver));
        
        return filteredTransactions;
    }

    public IEnumerable<ITransaction> GetAllInAmountRange(decimal low, decimal high)
    {
        IEnumerable<ITransaction> filteredTransactions = _transactions.Values
            .Where(t => t.Amount >= low && t.Amount <= high);
        
        return filteredTransactions;
    }
}