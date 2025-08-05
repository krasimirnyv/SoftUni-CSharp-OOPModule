using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chainblock.Contracts;
using Chainblock.Enums;
using Chainblock.Exceptions;
using NUnit.Framework;

namespace Chainblock.Tests;

using Models;

[TestFixture]
public class ChainblockTests
{
    private const ulong Id = 1;
    private const TransactionStatus Status = TransactionStatus.Successful;
    private const string Sender = "Krasimir";
    private const string Receiver = "Ivan";
    private const decimal Amount = 100.00m;

    private IChainblock _chainblock;
    private ITransaction _transaction;

    [SetUp]
    public void SetUp()
    {
        _chainblock = new Chainblock();
        _transaction = new Transaction(Id, Status, Sender, Receiver, Amount);
    }

    [Test]
    public void Constructor_ShouldInitializeChainblockCorrectly()
    {
        IChainblock chainblock = new Chainblock();

        Assert.IsNotNull(chainblock);
        Assert.IsInstanceOf<Chainblock>(chainblock);
        Assert.That(chainblock.Count, Is.EqualTo(0));
    }

    [Test]
    public void Constructor_ShouldInitializeCollectionOfTransactionsCorrectly()
    {
        IChainblock chainblock = new Chainblock();

        Type chainblockType = chainblock.GetType();

        FieldInfo transactionField = chainblockType
            .GetField("_transactions", BindingFlags.Instance | BindingFlags.NonPublic);

        IDictionary<ulong, ITransaction> value = transactionField
            .GetValue(chainblock) as IDictionary<ulong, ITransaction>;

        Assert.IsNotNull(transactionField);
        Assert.IsNotNull(value);
        Assert.IsEmpty(value);
        Assert.IsInstanceOf<IDictionary<ulong, ITransaction>>(value);
        Assert.That(chainblock.Count, Is.EqualTo(value.Count));
    }

    [Test]
    public void Add_ShouldAppendTransactionCorrectly()
    {
        _chainblock.Add(_transaction);

        Assert.IsTrue(_chainblock.Contains(_transaction.Id));
        Assert.That(_chainblock.Count, Is.EqualTo(1));
    }

    [Test]
    public void Add_ShouldThrowException_WhenTransactionAlreadyExist()
    {
        _chainblock.Add(_transaction);

        Assert.That(_chainblock.Count, Is.EqualTo(1));
        ArgumentException ex = Assert.Throws<ArgumentException>(() => _chainblock.Add(_transaction));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.TransactionAlreadyExists));
        Assert.That(_chainblock.Count, Is.EqualTo(1));
    }

    [Test]
    public void ContainsById_ShouldReturnTrue_WhenTransactionExists()
    {
        _chainblock.Add(_transaction);

        Assert.IsTrue(_chainblock.Contains(_transaction.Id));

        Assert.That(_transaction.Id, Is.EqualTo(Id));
        Assert.That(_transaction.Status, Is.EqualTo(Status));
        Assert.That(_transaction.Sender, Is.EqualTo(Sender));
        Assert.That(_transaction.Receiver, Is.EqualTo(Receiver));
        Assert.That(_transaction.Amount, Is.EqualTo(Amount));
    }

    [Test]
    public void ContainsById_ShouldReturnFalse_WhenTransactionDoesNotExists()
    {
        Assert.IsFalse(_chainblock.Contains(_transaction.Id));
    }

    [TestCase(TransactionStatus.Failed, TransactionStatus.Failed)]
    [TestCase(TransactionStatus.Failed, TransactionStatus.Successful)]
    [TestCase(TransactionStatus.Failed, TransactionStatus.Aborted)]
    [TestCase(TransactionStatus.Failed, TransactionStatus.Unauthorised)]
    [TestCase(TransactionStatus.Successful, TransactionStatus.Failed)]
    [TestCase(TransactionStatus.Successful, TransactionStatus.Successful)]
    [TestCase(TransactionStatus.Successful, TransactionStatus.Aborted)]
    [TestCase(TransactionStatus.Successful, TransactionStatus.Unauthorised)]
    [TestCase(TransactionStatus.Aborted, TransactionStatus.Failed)]
    [TestCase(TransactionStatus.Aborted, TransactionStatus.Successful)]
    [TestCase(TransactionStatus.Aborted, TransactionStatus.Aborted)]
    [TestCase(TransactionStatus.Aborted, TransactionStatus.Unauthorised)]
    [TestCase(TransactionStatus.Unauthorised, TransactionStatus.Failed)]
    [TestCase(TransactionStatus.Unauthorised, TransactionStatus.Successful)]
    [TestCase(TransactionStatus.Unauthorised, TransactionStatus.Aborted)]
    [TestCase(TransactionStatus.Unauthorised, TransactionStatus.Unauthorised)]
    public void ChangeTransactionStatus_ShouldChangeStatus_WhenStatusIsCorrect(TransactionStatus fromStatus, TransactionStatus toStatus)
    {
        _transaction.Status = fromStatus;
        _chainblock.Add(_transaction);

        _chainblock.ChangeTransactionStatus(_transaction.Id, toStatus);

        Assert.That(_transaction.Status, Is.EqualTo(toStatus));
    }

    [Test]
    public void ChangeTransactionStatus_ShouldThrowException_WhenTransactionDoesNotExist()
    {
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.ChangeTransactionStatus(_transaction.Id, Status));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.TransactionNotFound));
    }
    
    [Test]
    public void ChangeTransactionStatus_ShouldThrowException_WhenStatusIsIncorrect()
    {
        _chainblock.Add(_transaction);
        
        ArgumentException ex = Assert.Throws<ArgumentException>(() => _chainblock.ChangeTransactionStatus(_transaction.Id, (TransactionStatus)4));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.InvalidTransactionStatus));
    }
    
    [Test]
    public void RemoveTransactionById_ShouldRemoveTransactionFromDataCollection()
    {
        _chainblock.Add(_transaction);
        
        Assert.IsTrue(_chainblock.Contains(_transaction.Id));
        Assert.That(_chainblock.Count, Is.EqualTo(1));

        _chainblock.RemoveTransactionById(_transaction.Id);

        Assert.IsFalse(_chainblock.Contains(_transaction.Id));
        Assert.That(_chainblock.Count, Is.EqualTo(0));
    }

    [Test]
    public void RemoveTransactionById_ShouldThrowException_WhenTransactionDoesNotExist()
    {
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.RemoveTransactionById(_transaction.Id));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.TransactionNotFound));
    }

    [Test]
    public void GetById_ShouldReturnTransaction_WhenTransactionExists()
    {
        _chainblock.Add(_transaction);
        ITransaction transaction = _chainblock.GetById(_transaction.Id);
        
        Assert.IsNotNull(transaction);
        Assert.IsInstanceOf<ITransaction>(transaction);
        Assert.That(transaction.Id, Is.EqualTo(Id));
        Assert.That(transaction.Status, Is.EqualTo(Status));
        Assert.That(transaction.Sender, Is.EqualTo(Sender));
        Assert.That(transaction.Receiver, Is.EqualTo(Receiver));
        Assert.That(transaction.Amount, Is.EqualTo(Amount));
        Assert.That(transaction, Is.EqualTo(_transaction));
    }
    
    [Test]
    public void GetById_ShouldThrowException_WhenTransactionDoesNotExist()
    {
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.GetById(_transaction.Id));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.TransactionNotFound));
    }

    [Test]
    public void GetByTransactionStatus_ShouldReturnTransactions_WhenTransactionsWithGivenStatusExist_OrderedByAmount()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender, Receiver, Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender, Receiver, Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender, Receiver, Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender, Receiver, Amount - 40m);
        ITransaction transactionWithDiffStatus = new Transaction(Id + 5, TransactionStatus.Failed, Sender, Receiver, Amount - 20m);
        
        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);
        
        IEnumerable<ITransaction> transactions = _chainblock.GetByTransactionStatus(_transaction.Status);
        
        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        
        Assert.That(transactions.Count(), Is.EqualTo(5));
        Assert.That(transactions, Is.Ordered.By("Amount"));
        
        Assert.That(transactions, Does.Contain(_transaction));
        Assert.That(transactions, Does.Contain(transaction2));
        Assert.That(transactions, Does.Contain(transaction3));
        Assert.That(transactions, Does.Contain(transaction4));
        Assert.That(transactions, Does.Contain(transaction5));
        Assert.That(transactions, Does.Not.Contain(transactionWithDiffStatus));
        
        Assert.That(transactions.First().Amount, Is.EqualTo(Amount - 40m));
        Assert.That(transactions.Last().Amount, Is.EqualTo(Amount + 2000m));
    }

    [Test]
    public void GetByTransactionStatus_ShouldThrowException_WhenNoTransactionsWithGivenStatusExist()
    {
        _chainblock.Add(_transaction);
        
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.GetByTransactionStatus(TransactionStatus.Aborted));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.NoTransactionsWithGivenStatus));
    }
    
    [Test]
    public void GetByTransactionStatus_ShouldThrowException_WhenStatusIsIncorrect()
    {
        _chainblock.Add(_transaction);
        
        ArgumentException ex = Assert.Throws<ArgumentException>(() => _chainblock.GetByTransactionStatus((TransactionStatus)4));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.InvalidTransactionStatus));
    }
    
    [Test]
    public void GetAllSendersWithTransactionStatusByStatus_ShouldReturnTransactions_WhenTransactionsWithGivenStatusExist()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender + "Naydenov", Receiver, Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender + "000", Receiver, Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender + "Naydenov", Receiver, Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender + "123", Receiver, Amount - 40m);
        ITransaction transactionWithDiffStatus = new Transaction(Id + 5, TransactionStatus.Failed, Sender + "Yok", Receiver, Amount - 20m);
        
        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);
        
        IEnumerable<string> senders = _chainblock.GetAllSendersWithTransactionStatus(_transaction.Status);
        
        Assert.IsNotNull(senders);
        Assert.IsInstanceOf<IEnumerable<string>>(senders);
        Assert.That(senders.Count(), Is.EqualTo(5));
        
        Assert.That(senders, Does.Contain(_transaction.Sender));
        Assert.That(senders, Does.Contain(transaction2.Sender));
        Assert.That(senders, Does.Contain(transaction3.Sender));
        Assert.That(senders, Does.Contain(transaction4.Sender));
        Assert.That(senders, Does.Contain(transaction5.Sender));
        Assert.That(senders, Does.Not.Contain(transactionWithDiffStatus.Sender));
        
        Assert.That(senders.First(), Is.EqualTo(Sender + "123"));
        Assert.That(senders.Last(), Is.EqualTo(Sender + "000"));
    }

    [Test]
    public void GetAllSendersWithTransactionStatusByStatus_ShouldThrowException_WhenNoTransactionsWithGivenStatusExist()
    {
        _chainblock.Add(_transaction);
        
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Aborted));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.NoTransactionsWithGivenStatus));
    }
    
    [Test]
    public void GetAllSendersWithTransactionStatusByStatus_ShouldThrowException_WhenStatusIsIncorrect()
    {
        _chainblock.Add(_transaction);
        
        ArgumentException ex = Assert.Throws<ArgumentException>(() => _chainblock.GetAllSendersWithTransactionStatus((TransactionStatus)4));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.InvalidTransactionStatus));
    }
    
    [Test]
    public void GetAllReceiversWithTransactionStatusByStatus_ShouldReturnTransactions_WhenTransactionsWithGivenStatusExist()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender, Receiver + "Nikolov", Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender, Receiver + "000", Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender, Receiver + "Nikolov", Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender, Receiver + "123", Amount - 40m);
        ITransaction transactionWithDiffStatus = new Transaction(Id + 5, TransactionStatus.Failed, Sender, Receiver + "Yok", Amount - 20m);

        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);
        
        IEnumerable<string> receivers = _chainblock.GetAllReceiversWithTransactionStatus(_transaction.Status);
        
        Assert.IsNotNull(receivers);
        Assert.IsInstanceOf<IEnumerable<string>>(receivers);
        Assert.That(receivers.Count(), Is.EqualTo(5));
        
        Assert.That(receivers, Does.Contain(_transaction.Receiver));
        Assert.That(receivers, Does.Contain(transaction2.Receiver));
        Assert.That(receivers, Does.Contain(transaction3.Receiver));
        Assert.That(receivers, Does.Contain(transaction4.Receiver));
        Assert.That(receivers, Does.Contain(transaction5.Receiver));
        Assert.That(receivers, Does.Not.Contain(transactionWithDiffStatus.Receiver));
        
        Assert.That(receivers.First(), Is.EqualTo(Receiver + "123"));
        Assert.That(receivers.Last(), Is.EqualTo(Receiver + "000"));
    }

    [Test]
    public void GetAllReceiversWithTransactionStatusByStatus_ShouldThrowException_WhenNoTransactionsWithGivenStatusExist()
    {
        _chainblock.Add(_transaction);
        
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Aborted));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.NoTransactionsWithGivenStatus));
    }
    
    [Test]
    public void GetAllReceiversWithTransactionStatusByStatus_ShouldThrowException_WhenStatusIsIncorrect()
    {
        _chainblock.Add(_transaction);
        
        ArgumentException ex = Assert.Throws<ArgumentException>(() => _chainblock.GetAllReceiversWithTransactionStatus((TransactionStatus)4));
        Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.InvalidTransactionStatus));
    }

    [Test]
    public void GetAllOrderedByAmountDescendingThenById_ShouldReturnTransactions_OrderedByAmountDescendingThenById()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender, Receiver, Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender, Receiver, Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender, Receiver, Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender, Receiver, Amount - 40m);
        ITransaction transactionWithDiffStatus = new Transaction(Id + 5, TransactionStatus.Failed, Sender, Receiver, Amount - 20m);
        
        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);
        
        IEnumerable<ITransaction> transactions = _chainblock.GetAllOrderedByAmountDescendingThenById();
        
        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        Assert.That(transactions.Count(), Is.EqualTo(6));
        Assert.That(transactions, Is.Ordered.By("Amount").Descending.Then.By("Id"));
        
        Assert.That(transactions, Does.Contain(_transaction));
        Assert.That(transactions, Does.Contain(transaction2));
        Assert.That(transactions, Does.Contain(transaction3));
        Assert.That(transactions, Does.Contain(transaction4));
        Assert.That(transactions, Does.Contain(transaction5));
        Assert.That(transactions, Does.Contain(transactionWithDiffStatus));
        
        Assert.That(transactions.First().Id, Is.EqualTo(Id + 2));
        Assert.That(transactions.First().Amount, Is.EqualTo(Amount + 2000m));
        Assert.That(transactions.Last().Id, Is.EqualTo(Id + 4));
        Assert.That(transactions.Last().Amount, Is.EqualTo(Amount - 40m));
    }
    
    [Test]
    public void GetBySenderOrderedByAmountDescending_ShouldReturnTransactions_WhenTransactionsWithGivenSenderExist()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender + "Naydenov", Receiver, Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender + "000", Receiver, Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender + "Naydenov", Receiver, Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender + "123", Receiver, Amount - 40m);
        ITransaction transactionWithDiffStatus = new Transaction(Id + 5, TransactionStatus.Failed, Sender + "Yok", Receiver, Amount - 20m);
        
        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);
        
        IEnumerable<ITransaction> transactions = _chainblock.GetBySenderOrderedByAmountDescending(transaction2.Sender);
        
        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        Assert.That(transactions.Count(), Is.EqualTo(2));
        Assert.That(transactions, Is.Ordered.By("Amount").Descending);
        
        Assert.That(transactions, Does.Contain(transaction2));
        Assert.That(transactions, Does.Contain(transaction4));

        Assert.That(transactions, Does.Not.Contain(_transaction));
        Assert.That(transactions, Does.Not.Contain(transaction3));
        Assert.That(transactions, Does.Not.Contain(transaction5));
        Assert.That(transactions, Does.Not.Contain(transactionWithDiffStatus));
    }

    [Test]
    public void GetBySenderOrderedByAmountDescending_ShouldThrowException_WhenNoTransactionsWithGivenSenderExist()
    {
        _chainblock.Add(_transaction);

        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.GetBySenderOrderedByAmountDescending(Sender + "NonExistent"));
        Assert.That(ex.Message, Is.EqualTo(string.Format(ExceptionMessages.NoTransactionsWithSender, Sender + "NonExistent")));
    }
    
    [Test]
    public void GetByReceiverOrderedByAmountThenById_ShouldReturnTransactions_WhenTransactionsWithGivenReceiverExist()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender, Receiver + "Nikolov", Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender, Receiver + "000", Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender, Receiver + "Nikolov", Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender, Receiver + "123", Amount - 40m);
        ITransaction transactionWithDiffStatus = new Transaction(Id + 5, TransactionStatus.Failed, Sender, Receiver + "Yok", Amount - 20m);

        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);

        IEnumerable<ITransaction> transactions = _chainblock.GetByReceiverOrderedByAmountThenById(transaction2.Receiver);
        
        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        Assert.That(transactions.Count(), Is.EqualTo(2));
        Assert.That(transactions, Is.Ordered.By("Amount").Descending.Then.By("Id"));
        
        Assert.That(transactions, Does.Contain(transaction2));
        Assert.That(transactions, Does.Contain(transaction4));

        Assert.That(transactions, Does.Not.Contain(_transaction));
        Assert.That(transactions, Does.Not.Contain(transaction3));
        Assert.That(transactions, Does.Not.Contain(transaction5));
        Assert.That(transactions, Does.Not.Contain(transactionWithDiffStatus));
    }

    [Test]
    public void GetByReceiverOrderedByAmountThenById_ShouldThrowException_WhenNoTransactionsWithGivenReceiverExist()
    {
        _chainblock.Add(_transaction);

        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.GetByReceiverOrderedByAmountThenById(Receiver + "NonExistent"));
        Assert.That(ex.Message, Is.EqualTo(string.Format(ExceptionMessages.NoTransactionsWithReceiver, Receiver + "NonExistent")));
    }
    
    [Test]
    public void GetByTransactionStatusAndMaximumAmount_ShouldReturnTransactions_WhenTransactionsWithGivenStatusExist_AndAmountLessThanOrEqualToMax()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender, Receiver, Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender, Receiver, Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender, Receiver, Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender, Receiver, Amount - 40m);
        ITransaction transactionWithDiffStatus = new Transaction(Id + 5, TransactionStatus.Failed, Sender, Receiver, Amount - 20m);
        
        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);
        
        IEnumerable<ITransaction> transactions = _chainblock.GetByTransactionStatusAndMaximumAmount(Status, Amount + 500m);
        
        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        Assert.That(transactions.Count(), Is.EqualTo(4));
        Assert.That(transactions, Is.Ordered.By("Amount").Descending);
        
        Assert.That(transactions, Does.Contain(_transaction));
        Assert.That(transactions, Does.Contain(transaction2));
        Assert.That(transactions, Does.Contain(transaction4));
        Assert.That(transactions, Does.Contain(transaction5));
        Assert.That(transactions, Does.Not.Contain(transaction3));
        Assert.That(transactions, Does.Not.Contain(transactionWithDiffStatus));
        
        Assert.That(transactions.First().Amount, Is.EqualTo(Amount + 500m));
        Assert.That(transactions.Last().Amount, Is.EqualTo(Amount - 40m));
    }
    
    [Test]
    public void GetByTransactionStatusAndMaximumAmount_ShouldReturnTransactions_WhenTransactionsWithGivenDoesNotStatusExist_ReturnsEmptyCollection()
    {
        _chainblock.Add(_transaction);
        
        IEnumerable<ITransaction> transactions = _chainblock.GetByTransactionStatusAndMaximumAmount(TransactionStatus.Aborted, Amount + 500m);
        
        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        Assert.That(transactions.Count(), Is.EqualTo(0));
        Assert.That(transactions, Is.Empty);
    }

    [Test]
    public void
        GetBySenderAndMinimumAmountDescending_ShouldReturnTransactions_WhenTransactionsWithGivenSenderExist_AndAmountGreaterThanMin()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender + "Naydenov", Receiver, Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender + "000", Receiver, Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender + "Naydenov", Receiver, Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender + "123", Receiver, Amount - 40m);
        ITransaction transactionWithDiffStatus =
            new Transaction(Id + 5, TransactionStatus.Failed, Sender + "Yok", Receiver, Amount - 20m);

        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);

        IEnumerable<ITransaction> transactions =
            _chainblock.GetBySenderAndMinimumAmountDescending(transaction2.Sender, Amount + 100m);

        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        Assert.That(transactions.Count(), Is.EqualTo(1));
        Assert.That(transactions, Is.Ordered.By("Amount").Descending);
        
        Assert.That(transactions, Does.Contain(transaction2));
        Assert.That(transactions, Does.Not.Contain(_transaction));
        Assert.That(transactions, Does.Not.Contain(transaction3));
        Assert.That(transactions, Does.Not.Contain(transaction4));
        Assert.That(transactions, Does.Not.Contain(transaction5));
        Assert.That(transactions, Does.Not.Contain(transactionWithDiffStatus));
        
        Assert.That(transactions.First().Amount, Is.EqualTo(Amount + 500m));
    }

    [Test]
    public void GetBySenderAndMinimumAmountDescending_ShouldThrowException_WhenNoTransactionsWithGivenSenderExist()
    {
        _chainblock.Add(_transaction);
        
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.GetBySenderAndMinimumAmountDescending(Sender + "NonExistent", Amount + 100m));
        Assert.That(ex.Message, Is.EqualTo(string.Format(ExceptionMessages.NoTransactionsWithSender, Sender + "NonExistent")));
    }

    [Test]
    public void
        GetByReceiverAndAmountRange_ShouldReturnTransactions_WhenTransactionsWithGivenReceiverExist_AndAmountInRange()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender, Receiver + "Nikolov", Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender, Receiver + "000", Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender, Receiver + "Nikolov", Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender, Receiver + "123", Amount - 40m);
        ITransaction transactionWithDiffStatus = new Transaction(Id + 5, TransactionStatus.Failed, Sender, Receiver + "Yok", Amount - 20m);

        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);

        IEnumerable<ITransaction> transactions = _chainblock.GetByReceiverAndAmountRange(Receiver + "Nikolov", Amount + 20m, Amount + 600m);

        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        Assert.That(transactions.Count(), Is.EqualTo(2));
        Assert.That(transactions, Is.Ordered.By("Amount").Descending.Then.By("Id"));

        Assert.That(transactions, Does.Contain(transaction2));
        Assert.That(transactions, Does.Contain(transaction4));

        Assert.That(transactions, Does.Not.Contain(_transaction));
        Assert.That(transactions, Does.Not.Contain(transaction3));
        Assert.That(transactions, Does.Not.Contain(transaction5));
        Assert.That(transactions, Does.Not.Contain(transactionWithDiffStatus));

        Assert.That(transactions.First().Amount, Is.EqualTo(Amount + 500m));
        Assert.That(transactions.Last().Amount, Is.EqualTo(Amount + 20m));
    }
    
    [Test]
    public void GetByReceiverAndAmountRange_ShouldThrowException_WhenNoTransactionsWithGivenReceiverExist()
    {
        _chainblock.Add(_transaction);
        
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _chainblock.GetByReceiverAndAmountRange(Receiver + "NonExistent", Amount + 20m, Amount + 600m));
        Assert.That(ex.Message, Is.EqualTo(string.Format(ExceptionMessages.NoTransactionsWithReceiver, Receiver + "NonExistent")));
    }

    [Test]
    public void GetAllInAmountRange_ShouldReturnTransactions_WhenTransactionsInRangeExist()
    {
        ITransaction transaction2 = new Transaction(Id + 1, Status, Sender, Receiver, Amount + 500m);
        ITransaction transaction3 = new Transaction(Id + 2, Status, Sender, Receiver, Amount + 2000m);
        ITransaction transaction4 = new Transaction(Id + 3, Status, Sender, Receiver, Amount + 20m);
        ITransaction transaction5 = new Transaction(Id + 4, Status, Sender, Receiver, Amount - 40m);
        ITransaction transactionWithDiffStatus = new Transaction(Id + 5, TransactionStatus.Failed, Sender, Receiver, Amount - 20m);

        _chainblock.Add(_transaction);
        _chainblock.Add(transaction2);
        _chainblock.Add(transaction3);
        _chainblock.Add(transaction4);
        _chainblock.Add(transaction5);
        _chainblock.Add(transactionWithDiffStatus);

        IEnumerable<ITransaction> transactions = _chainblock.GetAllInAmountRange(Amount, Amount + 1000m);

        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        Assert.That(transactions.Count(), Is.EqualTo(3));
        
        Assert.That(transactions, Does.Contain(_transaction));
        Assert.That(transactions, Does.Contain(transaction2));
        Assert.That(transactions, Does.Contain(transaction4));
        Assert.That(transactions, Does.Not.Contain(transaction5));
        Assert.That(transactions, Does.Not.Contain(transaction3));
        Assert.That(transactions, Does.Not.Contain(transactionWithDiffStatus));

        Assert.That(transactions.First().Amount, Is.EqualTo(Amount));
        Assert.That(transactions.Last().Amount, Is.EqualTo(Amount + 20m));
    }

    [Test]
    public void GetAllInAmountRange_ShouldReturnEmptyCollection_WhenNoTransactionsInRangeExist()
    {
        _chainblock.Add(_transaction);
        
        IEnumerable<ITransaction> transactions = _chainblock.GetAllInAmountRange(_transaction.Amount + 1000m, _transaction.Amount + 2000m);
        
        Assert.IsNotNull(transactions);
        Assert.IsInstanceOf<IEnumerable<ITransaction>>(transactions);
        Assert.That(transactions.Count(), Is.EqualTo(0));
        Assert.That(transactions, Is.Empty);
    }
}