using File = System.IO.File;

namespace ElectronicArchive
{
    public class Archive
    {
        static void Main(string[] args)
        {
            var inboxPathDocuments = new DirectoryInfo("D:/Finance model/International/Accounting/Sorting hub/Documents");
            DirectoryCheck(inboxPathDocuments);

            var inboxPathAgreements = new DirectoryInfo("D:/Finance model/International/Accounting/Sorting hub/Agreements");
            DirectoryCheck(inboxPathAgreements);

            var archivePath = new DirectoryInfo("D:/Finance model/International/Accounting/Accounting documents");
            DirectoryCheck(archivePath);
            ArchiveClean(archivePath.FullName);

            while (true)
            {
                // ОБРАБАТЫВАЕМ ВХОДЯЩИЕ ИНВОЙСЫ
                if (inboxPathDocuments.GetFiles().Length > 0)
                {
                    foreach (var file in inboxPathDocuments.GetFiles())
                    {
                        InvoiceMove(file, inboxPathDocuments, archivePath);
                        Console.WriteLine();
                    }
                }
                // ОБРАБАТЫВАЕМ ВХОДЯЩИЕ ДОГОВОРА
                else if (inboxPathAgreements.GetFiles().Length > 0)
                {
                    foreach (var file in inboxPathAgreements.GetFiles())
                    {
                        AgreementMove(file, inboxPathAgreements, archivePath);
                        Console.WriteLine();
                    }
                }
            }
        }

        static void DirectoryCheck(DirectoryInfo folderPath)
        {
            if (folderPath.Exists) // Проверим, что директория существует
            {
                Console.WriteLine("Папка {0} найдена. Перехожу к работе с файлами.", folderPath);
                Console.WriteLine();
            }
            else
            {
                folderPath.Create();
                Console.WriteLine("Папка не найдена. Создал новую: {0}. Перехожу к работе с файлами.", folderPath);
                Console.WriteLine();
            }
        }

        static void InvoiceMove(FileInfo file, DirectoryInfo inboxPathDocuments, DirectoryInfo archivePath)
        {
            try
            {
                // Определяем атрибуты файла
                string[] fileData = Path.GetFileNameWithoutExtension($"{inboxPathDocuments}/{file.Name}").Split('-');

                // записываем атрибуты в класс документ
                var document = new Document(fileData[0], fileData[1], int.Parse(fileData[2]), int.Parse(fileData[3]), int.Parse(fileData[4]), int.Parse(fileData[5]), fileData[6]);

                // объявляем директорию для нового файла
                var folderPathNew = new DirectoryInfo($"{archivePath}/{document.Account}/{document.Counterparty}/{document.Year}");
                DirectoryCheck(folderPathNew);

                // объявляем старый и новый путь к файлу
                var filePathNew = $"{folderPathNew}/{file.Name}";
                var filePathInbox = $"{inboxPathDocuments}/{file.Name}";

                if (File.Exists(filePathNew)) // Проверим, что файл существует в архиве
                {
                    File.Delete(file.FullName);
                    Console.WriteLine("Файл {0} уже есть в архиве. Удалил из инбокса и все.", file.Name);
                }
                else
                {
                    File.Copy(file.FullName, filePathNew, true);
                    File.Delete(filePathInbox);
                    Console.WriteLine("Файл новый. Перенес его в архив - можно найти тут: {0}.", filePathNew);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Файл уже есть в архиве или передан файл {0} не поддерживаемого формата: Счет-Контрагент-Год/Месяц/День-Сумма-Валюта. Попробуйте с правильным файлом.", file.Name);
                Console.WriteLine("Этот файл удаляю и жду успешной попытки.");
                file.Delete();
            }
        }

        static void AgreementMove(FileInfo file, DirectoryInfo inboxPathAgreements, DirectoryInfo archivePath)
        {
            try
            {
                // Определяем атрибуты файла
                string[] fileData = Path.GetFileNameWithoutExtension($"{inboxPathAgreements}/{file.Name}").Split('-');

                // записываем атрибуты в класс документ
                var document = new Agreement(fileData[0], fileData[1], fileData[2]);

                // объявляем директорию для нового файла
                var folderPathNew = new DirectoryInfo($"{archivePath}/{document.Account}/{document.Counterparty}");
                DirectoryCheck(folderPathNew);

                // объявляем старый и новый путь к файлу
                var filePathNew = $"{folderPathNew}/{file.Name}";
                var filePathInbox = $"{inboxPathAgreements}/{file.Name}";

                if (File.Exists(filePathNew)) // Проверим, что файл существует в архиве
                {
                    File.Delete(file.FullName);
                    Console.WriteLine("Файл {0} уже есть в архиве. Удалил из инбокса и все.", file.Name);
                }
                else
                {
                    File.Copy(file.FullName, filePathNew, true);
                    File.Delete(filePathInbox);
                    Console.WriteLine("Файл новый. Перенес его в архив - можно найти тут: {0}.", filePathNew);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Файл уже есть в архиве или передан файл {0} не поддерживаемого формата: Счет-Контрагент-Предмет договора. Попробуйте с правильным файлом.", file.Name);
                Console.WriteLine("Этот файл удаляю и жду успешной попытки.");
                file.Delete();
            }
        }

        static void ArchiveClean(string archivePath)
        {
            string[] folders = Directory.GetDirectories(archivePath);  // Получим все содержащиеся папки

            foreach (string folder in folders)  // Удаление пустых папок
            {
                ArchiveClean(folder);

                try
                {
                    DirectoryInfo Folder = new DirectoryInfo(folder);
                    Folder.Delete();
                    Console.WriteLine("Папка {0} пустая, поэтому ее удалили.", folder);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}