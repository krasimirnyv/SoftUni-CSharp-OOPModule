using System;
using Chainblock.Contracts;
using Chainblock.Enums;
using Chainblock.Exceptions;

namespace Chainblock.Models;

public class Transaction : ITransaction
{
    private ulong _id;
    private TransactionStatus _status;
    private string _sender;
    private string _receiver;
    private decimal _amount;
    
    public Transaction(ulong id, TransactionStatus status, string sender, string receiver, decimal amount)
    {
        Id = id;
        Status = status;
        Sender = sender;
        Receiver = receiver;
        Amount = amount;
    }

    public ulong Id
    {
        get => _id;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException(ExceptionMessages.IdCannotBeZeroOrNegative);
            }

            _id = value;
        }
    }

    public TransactionStatus Status
    {
        get => _status;
        set
        {
            if (value is < (TransactionStatus)0 or > (TransactionStatus)3)
            {
                throw new ArgumentException(ExceptionMessages.InvalidTransactionStatus);
            }
            
            _status = value;
        }
    }
    
    public string Sender
    {
        get => _sender;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.InvalidSenderName);
            }
            
            _sender = value;
        }
    }
    
    public string Receiver 
    { 
        get => _receiver;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.InvalidReceiverName);
            }
            
            _receiver = value;
        } 
    }
    
    public decimal Amount
    {
        get => _amount;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException(ExceptionMessages.AmountCannotBeZeroOrNegative);
            }

            _amount = value;
        }
    }
    
    public override string ToString() 
        => $"ID: {Id}, Status: {Status}, Sender: {Sender}, Receiver: {Receiver}, Amount: {Amount:F2}";
}