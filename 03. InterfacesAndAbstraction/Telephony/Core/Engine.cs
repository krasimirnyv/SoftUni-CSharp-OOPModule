using Telephony.Core.Interfaces;
using Telephony.IO.Interfaces;
using Telephony.Models.Interfaces;
using Telephony.Models;

namespace Telephony.Core;

public class Engine : IEngine
{
    private IReader reader;
    private IWriter writer;
    
    public Engine(IReader reader, IWriter writer)
    {
        this.reader = reader;
        this.writer = writer;
    }
    
    public void Run()
    {
        string[] phoneNumbers = reader.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        string[] urls = reader.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        ICallable telephone;
        foreach (string number in phoneNumbers)
        {
            if (number.Length == 10)
                telephone = new Smartphone();
            else // number.Length == 7
                telephone = new StationaryPhone();
            try
            {
                writer.WriteLine(telephone.Call(number));
            }
            catch (Exception exception)
            {
                writer.WriteLine(exception.Message);
            }
        }
        
        IBrowsable browser;
        foreach (string url in urls)
        {
            browser = new Smartphone();
            try
            {
                writer.WriteLine(browser.Browse(url));
            }
            catch (Exception exception)
            {
                writer.WriteLine(exception.Message);
            }
        }
    }
}