
namespace ElectronicArchive
{
    public class Document
    {
        public Document(string account, string counterparty, int year, int month, int day, int amount, string currency) // метод-конструктор
        {
            Account = account;
            Counterparty = counterparty;
            Year = year;
            Month = month;
            Day = day;
            Amount = amount;
            Currency = currency;
        }

        public String Account { get; }
        public String Counterparty { get; }
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public int Amount { get; }
        public String Currency { get; } 

    }
}
