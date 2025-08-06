using System;
using Chainblock.Contracts;
using Chainblock.Enums;
using Chainblock.Exceptions;
using NUnit.Framework;

namespace Chainblock.Tests;

using Models;

[TestFixture]
public class TransactionTests
{
    private const ulong Id = 1;
    private const TransactionStatus Status = TransactionStatus.Successful;
    private const string Sender = "Krasimir";
    private const string Receiver = "Ivan";
    private const decimal Amount = 100.00m;

    [Test]
    public void Constructor_ShouldInitializeTransactionCorrectly()
    {
        ITransaction transaction = new Transaction(Id, Status, Sender, Receiver, Amount);

        Assert.IsNotNull(transaction);
        Assert.That(transaction.Id, Is.EqualTo(Id));
        Assert.That(transaction.Status, Is.EqualTo(Status));
        Assert.That(transaction.Sender, Is.EqualTo(Sender));
        Assert.That(transaction.Receiver, Is.EqualTo(Receiver));
        Assert.That(transaction.Amount, Is.EqualTo(Amount));
        Assert.IsInstanceOf<Transaction>(transaction);
    }

    [TestCase(0ul)]
    public void IdSetter_ShouldThrowExceptionWithZeroId(ulong id)
    {
        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            ITransaction transaction = new Transaction(id, Status, Sender, Receiver, Amount);
        });

        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.IdCannotBeZeroOrNegative));
    }

    [TestCase(-1)]
    [TestCase(4)]
    public void StatusSetter_ShouldThrowExceptionWithInvalidStatus(int status)
    {
        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            ITransaction transaction = new Transaction(Id, (TransactionStatus)status, Sender, Receiver, Amount);
        });

        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.InvalidTransactionStatus));
    }
    
    [TestCase(TransactionStatus.Failed)]
    [TestCase(TransactionStatus.Successful)]
    [TestCase(TransactionStatus.Aborted)]
    [TestCase(TransactionStatus.Unauthorised)]
    public void StatusSetter_ShouldWorkWithEveryStatusCorrectly(TransactionStatus status)
    {
        ITransaction transaction = new Transaction(Id, status, Sender, Receiver, Amount);
        
        Assert.IsNotNull(transaction);
        Assert.That(transaction.Status, Is.EqualTo(status));
    }

    [TestCase(null)]
    [TestCase(default)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("  ")]
    [TestCase("\t")]
    [TestCase("\n")]
    [TestCase("\r\n")]
    public void SenderSetter_ShouldThrowExceptionWithInvalidSenderName(string sender)
    {
        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            ITransaction transaction = new Transaction(Id, Status, sender, Receiver, Amount);
        });

        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.InvalidSenderName));
    }

    [TestCase(null)]
    [TestCase(default)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("  ")]
    [TestCase("\t")]
    [TestCase("\n")]
    [TestCase("\r\n")]
    public void ReceiverSetter_ShouldThrowExceptionWithInvalidReceiverName(string receiver)
    {
        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            ITransaction transaction = new Transaction(Id, Status, Sender, receiver, Amount);
        });

        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.InvalidReceiverName));
    }

    [TestCase(0.00)]
    [TestCase(-1.00)]
    [TestCase(-100.00)]
    [TestCase(-0.01)]
    [TestCase(-0.000000000000000000000000000000000000000000000000000000000000001)]
    [TestCase(-9_999_999_999_999_999_999_999_999_999.999999999999999999999999999999999999999999999999999999999999999)]
    public void AmountSetter_ShouldThrowExceptionWithInvalidAmountOfMoney(decimal amount)
    {
        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            ITransaction transaction = new Transaction(Id, Status, Sender, Receiver, amount);
        });

        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.AmountCannotBeZeroOrNegative));
    }

    [Test]
    public void ToString_ShouldReturnCorrectString()
    {
        ITransaction transaction = new Transaction(Id, Status, Sender, Receiver, Amount);
        string expected = $"ID: {Id}, Status: {Status}, Sender: {Sender}, Receiver: {Receiver}, Amount: {Amount:F2}";
        
        string actual = transaction.ToString();
        
        Assert.That(actual, Is.EqualTo(expected));
    }
}