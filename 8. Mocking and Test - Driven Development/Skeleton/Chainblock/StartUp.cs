using System;
using System.Collections.Generic;
using Chainblock.Contracts;
using Chainblock.Enums;

namespace Chainblock;

using Chainblock.Models;

public class StartUp
{
    static void Main(string[] args)
    {
        try
        {
            IChainblock chainblock = new Chainblock();

            ITransaction transaction1 = new Transaction(1, TransactionStatus.Successful, "Krasimir", "Ivan", 150.00m);
            ITransaction transaction2 = new Transaction(2, TransactionStatus.Unauthorised, "Ivan", "Anna", 6.00m);
            ITransaction transaction3 = new Transaction(3, TransactionStatus.Aborted, "Anna", "Filip", 200.00m);
            ITransaction transaction4 = new Transaction(4, TransactionStatus.Successful, "Sofia", "Krasimir", 20.50m);
            ITransaction transaction5 = new Transaction(5, TransactionStatus.Failed, "Anna", "Krasimir", 650.00m);

            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);
            chainblock.Add(transaction4);
            chainblock.Add(transaction5);

            OperateWithChainblock(chainblock);
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine("ArgumentException: " + exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine("InvalidOperationException: " + exception.Message);
        }
    }

    private static void OperateWithChainblock(IChainblock chainblock)
    {
        Console.WriteLine($"Total Transactions: {chainblock.Count}");

        Console.WriteLine("Contains ID 3: " + chainblock.Contains(3));
        Console.WriteLine("Contains ID 6: " + chainblock.Contains(6));
        
        chainblock.ChangeTransactionStatus(3, TransactionStatus.Successful);
        Console.WriteLine("Changed status of ID 3 form Aborted to Successful.");
        
        ITransaction transactionById = chainblock.GetById(1);
        Console.WriteLine($"Transaction with ID 1: {transactionById}");

        IEnumerable<ITransaction> byStatus = chainblock.GetByTransactionStatus(TransactionStatus.Successful);
        Console.WriteLine("Transactions with status \"Successful\":");
        foreach (ITransaction transaction in byStatus)
            Console.WriteLine(transaction);

        IEnumerable<string> senders = chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Successful);
        Console.WriteLine("Senders with Successful transactions:");
        Console.WriteLine(string.Join(", ", senders));

        IEnumerable<string> receivers = chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Successful);
        Console.WriteLine("Receivers with Successful transactions:");
        Console.WriteLine(string.Join(", ", receivers));

        IEnumerable<ITransaction> ordered = chainblock.GetAllOrderedByAmountDescendingThenById();
        Console.WriteLine("All transactions ordered by amount descending then by ID:");
        foreach (ITransaction transaction in ordered)
            Console.WriteLine(transaction);

        IEnumerable<ITransaction> bySender = chainblock.GetBySenderOrderedByAmountDescending("Anna");
        Console.WriteLine("Transactions by sender Anna:");
        foreach (ITransaction transaction in bySender)
            Console.WriteLine(transaction);

        IEnumerable<ITransaction> byReceiver = chainblock.GetByReceiverOrderedByAmountThenById("Krasimir");
        Console.WriteLine("Transactions to receiver Krasimir:");
        foreach (ITransaction transaction in byReceiver)
            Console.WriteLine(transaction);

        IEnumerable<ITransaction> filtered = chainblock.GetByTransactionStatusAndMaximumAmount(TransactionStatus.Successful, 200.00m);
        Console.WriteLine("Successful transactions with amount <= 200.00:");
        foreach (ITransaction transaction in filtered)
            Console.WriteLine(transaction);

        IEnumerable<ITransaction> senderAmount = chainblock.GetBySenderAndMinimumAmountDescending("Anna", 220.00m);
        Console.WriteLine("Anna's transactions with amount > 220.00:");
        foreach (ITransaction transaction in senderAmount)
            Console.WriteLine(transaction);

        IEnumerable<ITransaction> receiverRange = chainblock.GetByReceiverAndAmountRange("Krasimir", 2.00m, 60.00m);
        Console.WriteLine("Transactions to Krasimir with amount in range [2.00, 60.00):");
        foreach (ITransaction transaction in receiverRange)
            Console.WriteLine(transaction);

        IEnumerable<ITransaction> inRange = chainblock.GetAllInAmountRange(20.00m, 200.00m);
        Console.WriteLine("All transactions with amount in range [20.00, 200.00]:");
        foreach (ITransaction transaction in inRange)
            Console.WriteLine(transaction);

        chainblock.RemoveTransactionById(1);
        Console.WriteLine("Removed transaction with ID 1.");
        Console.WriteLine($"Total Transactions after removal: {chainblock.Count}");
    }
}