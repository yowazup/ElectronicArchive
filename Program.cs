using ElectronicArchive.Workers;

/// надо еще 2 метода - проверять все лив файлы, доставать интересующий сет документов

namespace ElectronicArchive
{
    public class Archive
    {
        DirectoryInfo inboxPathDocuments = new DirectoryInfo("D:/Finance model/International/Accounting/Sorting hub/Documents");
        DirectoryInfo inboxPathAgreements = new DirectoryInfo("D:/Finance model/International/Accounting/Sorting hub/Agreements");
        DirectoryInfo archivePath = new DirectoryInfo("D:/Finance model/International/Accounting/Accounting documents");
        FileInfo allAgreements = new FileInfo("D:/Finance model/International/Accounting/Sorting hub/1. All agreements list.txt");
        FileInfo allInvoices = new FileInfo("D:/Finance model/International/Accounting/Sorting hub/2. All invoices list.txt");
        FileInfo outstandingAgreements = new FileInfo("D:/Finance model/International/Accounting/Sorting hub/3. Agreements outstanding.txt");
        FileInfo outstandingInvoices = new FileInfo("D:/Finance model/International/Accounting/Sorting hub/4. Invoices outstanding.txt");
        FileInfo notcorrectFiles = new FileInfo("D:/Finance model/International/Accounting/Sorting hub/5. Not correct files.txt");

        static void Main(DirectoryInfo inboxPathDocuments, DirectoryInfo inboxPathAgreements, DirectoryInfo archivePath)
        {
            
            // ПРОВЕРЯЕМ ВСЕ ДИРЕКТОРИИ И ФАЙЛЫ
            Checker.DirectoryCheck(inboxPathDocuments);
            Checker.DirectoryCheck(inboxPathAgreements);
            Checker.DirectoryCheck(archivePath);


            // ЧИСТИМ АРХИВ ОТ ПУСТЫХ ПАПОК
            Checker.ArchiveClean(archivePath.FullName);

            while (true)
            {
                // ОБРАБАТЫВАЕМ ВХОДЯЩИЕ ИНВОЙСЫ
                if (inboxPathDocuments.GetFiles().Length > 0)
                {
                    foreach (var file in inboxPathDocuments.GetFiles())
                    {
                        Uploader.InvoiceMove(file, inboxPathDocuments, archivePath);
                        Console.WriteLine();
                    }
                }
                // ОБРАБАТЫВАЕМ ВХОДЯЩИЕ ДОГОВОРА
                else if (inboxPathAgreements.GetFiles().Length > 0)
                {
                    foreach (var file in inboxPathAgreements.GetFiles())
                    {
                        Uploader.AgreementMove(file, inboxPathAgreements, archivePath);
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}