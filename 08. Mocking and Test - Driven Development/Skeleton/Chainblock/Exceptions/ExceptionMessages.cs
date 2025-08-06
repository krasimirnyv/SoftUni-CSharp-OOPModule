namespace Chainblock.Exceptions;

public static class ExceptionMessages
{
    // Transaction-related exceptions
    public const string IdCannotBeZeroOrNegative = "Id cannot be equal to zero or negative value!";
    
    public const string InvalidTransactionStatus = "Invalid transaction status!";
    
    public const string InvalidSenderName = "Sender's name cannot be null, empty or white space!";
    public const string InvalidReceiverName = "Receiver's name cannot be null, empty or white space!";
    
    public const string AmountCannotBeZeroOrNegative = "The amount of money must be a positive number!";
    
    
    // Chainblock-related exceptions
    public const string TransactionAlreadyExists = "Transaction with the given id already exists!";
    public const string TransactionNotFound = "Transaction with the given id does not exist!";
    
    public const string NoTransactionsWithGivenStatus = "There are no transactions with the given status!";
    
    public const string NoTransactionsWithSender = "No transactions found for sender: {0}";
    public const string NoTransactionsWithReceiver = "No transactions found for receiver: {0}";
}