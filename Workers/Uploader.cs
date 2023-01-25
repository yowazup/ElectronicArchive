using ElectronicArchive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicArchive.Workers
{
    public class Uploader
    {

        public static void InvoiceMove(FileInfo file, DirectoryInfo inboxPathDocuments, DirectoryInfo archivePath)
        {
            try
            {
                // Определяем атрибуты файла
                string[] fileData = Path.GetFileNameWithoutExtension($"{inboxPathDocuments}/{file.Name}").Split('-');

                // записываем атрибуты в класс документ
                var document = new Document(fileData[0], fileData[1], int.Parse(fileData[2]), int.Parse(fileData[3]), int.Parse(fileData[4]), int.Parse(fileData[5]), fileData[6], int.Parse(fileData[7]));

                // объявляем директорию для нового файла
                var folderPathNew = new DirectoryInfo($"{archivePath}/{document.Account}/{document.Counterparty}/{document.Year}");
                Checker.DirectoryCheck(folderPathNew);

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

        public static void AgreementMove(FileInfo file, DirectoryInfo inboxPathAgreements, DirectoryInfo archivePath)
        {
            try
            {
                // Определяем атрибуты файла
                string[] fileData = Path.GetFileNameWithoutExtension($"{inboxPathAgreements}/{file.Name}").Split('-');

                // записываем атрибуты в класс документ
                var document = new Agreement(fileData[0], fileData[1], fileData[2]);

                // объявляем директорию для нового файла
                var folderPathNew = new DirectoryInfo($"{archivePath}/{document.Account}/{document.Counterparty}");
                Checker.DirectoryCheck(folderPathNew);

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

    }
}
