namespace Telephony.Models;

using Interfaces;

public class Smartphone : ICallable, IBrowsable
{
    public string Call(string phoneNumber)
    {
        if (phoneNumber.Any(ch => !char.IsDigit(ch)))
        {
            throw new ArgumentException("Invalid number!");
        }

        return $"Calling... {phoneNumber}";
    }

    public string Browse(string url)
    {
        if (url.Any(char.IsDigit))
        {
            throw new ArgumentException("Invalid URL!");
        }

        return $"Browsing: {url}!";
    }
}