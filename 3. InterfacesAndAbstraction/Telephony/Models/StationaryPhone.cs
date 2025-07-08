namespace Telephony.Models;

using Interfaces;

public class StationaryPhone : ICallable
{
    public string Call(string phoneNumber)
    {
        if (phoneNumber.Any(ch => !char.IsDigit(ch)))
        {
            throw new ArgumentException("Invalid number!");
        }

        return $"Dialing... {phoneNumber}";
    }
}