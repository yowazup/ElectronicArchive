using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicArchive.Workers
{
    public class Checker
    {
        public static void DirectoryCheck(DirectoryInfo folderPath)
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

        public static void ArchiveClean(string archivePath)
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
