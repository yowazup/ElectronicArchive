
namespace ElectronicArchive.Models
{
    public class Agreement
    {
        public Agreement(string account, string counterparty, string subject) // метод-конструктор
        {
            Account = account;
            Counterparty = counterparty;
            Subject = subject;
        }

        public String Account { get; }
        public String Counterparty { get; }
        public String Subject { get; } 

    }
}
