using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Management;

class Program
{
    static void Main()
    {
        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GetDiskInfo();
                    break;
                case "2":
                    ShowCurrentDirectory();
                    break;
                case "3":
                    ChangeDirectory();
                    break;
                case "4":
                    Console.Write("Введите имя файла: ");
                    string fileName = Console.ReadLine();
                    CreateFile(fileName);
                    break;
                case "5":
                    Console.Write("Введите имя файла: ");
                    fileName = Console.ReadLine();
                    Console.Write("Введите текст для записи: ");
                    string content = Console.ReadLine();
                    WriteFile(fileName, content);
                    break;
                case "6":
                    Console.Write("Введите имя файла: ");
                    fileName = Console.ReadLine();
                    Console.WriteLine("Содержимое файла: " + ReadFile(fileName));
                    break;
                case "7":
                    Console.Write("Введите имя файла: ");
                    fileName = Console.ReadLine();
                    DeleteFile(fileName);
                    break;
                case "8":
                    Console.Write("Введите ключ для json: ");
                    string jsonKey = Console.ReadLine();
                    Console.Write("Введите значение для json: ");
                    string jsonValue = Console.ReadLine();
                    Console.Write("Введите имя json файла: ");
                    string jsonFileName = Console.ReadLine();
                    CreateJsonFile(jsonFileName, new Dictionary<string, string> { { jsonKey, jsonValue } });
                    break;
                case "9":
                    Console.Write("Введите имя JSON файла: ");
                    fileName = Console.ReadLine();
                    Console.WriteLine("Содержимое JSON файла: " + ReadJsonFile(fileName));
                    break;
                case "10":
                    Console.Write("Введите имя JSON файла: ");
                    fileName = Console.ReadLine();
                    DeleteFile(fileName);
                    break;
                case "11":
                    Console.Write("Введите значение для <name></name> в xml файле: ");
                    string xmlName = Console.ReadLine();
                    Console.Write("Введите значение для <surname></surname> в xml файле: ");
                    string xmlSurname = Console.ReadLine();
                    Console.Write("Введите имя xml файла: ");
                    string xmlFileName = Console.ReadLine();
                    CreateXmlFile(xmlFileName, new Dictionary<string, string> { { "name", xmlName }, { "surname", xmlSurname } });
                    break;
                case "12":
                    Console.Write("Введите имя XML файла: ");
                    fileName = Console.ReadLine();
                    Console.WriteLine("Содержимое XML файла: " + ReadXmlFile(fileName));
                    break;
                case "13":
                    Console.Write("Введите имя XML файла: ");
                    fileName = Console.ReadLine();
                    DeleteFile(fileName);
                    break;
                case "14":
                    Console.Write("Введите имя ZIP архива: ");
                    string zipFileName = Console.ReadLine();
                    CreateZipArchive(zipFileName);
                    break;
                case "15":
                    Console.Write("Введите имя ZIP архива: ");
                    zipFileName = Console.ReadLine();
                    Console.Write("Введите имя файла для добавления: ");
                    fileName = Console.ReadLine();
                    AddFileToZip(zipFileName, fileName);
                    break;
                case "16":
                    Console.Write("Введите имя ZIP архива: ");
                    zipFileName = Console.ReadLine();
                    Console.WriteLine("Размер архива: " + GetZipFileSize(zipFileName) + " bytes");
                    break;
                case "17":
                    Console.Write("Введите имя ZIP архива: ");
                    zipFileName = Console.ReadLine();
                    UnzipFile(zipFileName, "./");
                    break;
                case "18":
                    Console.WriteLine("Выход из программы.");
                    return;
                default:
                    Console.WriteLine("Неверный ввод, попробуйте снова.");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("---------------------------------------------------------------------------------------");
        Console.WriteLine("\nВыберите действие:");
        Console.WriteLine("1. Показать информацию о дисках");
        Console.WriteLine("2. Показать текущую рабочую директорию");
        Console.WriteLine("3. Изменить рабочую директорию");
        Console.WriteLine("4. Создать файл");
        Console.WriteLine("5. Записать в файл");
        Console.WriteLine("6. Прочитать файл");
        Console.WriteLine("7. Удалить файл");
        Console.WriteLine("8. Создать JSON файл");
        Console.WriteLine("9. Прочитать JSON файл");
        Console.WriteLine("10. Удалить JSON файл");
        Console.WriteLine("11. Создать XML файл");
        Console.WriteLine("12. Прочитать XML файл");
        Console.WriteLine("13. Удалить XML файл");
        Console.WriteLine("14. Создать ZIP архив");
        Console.WriteLine("15. Добавить файл в ZIP архив");
        Console.WriteLine("16. Получить размер ZIP архива");
        Console.WriteLine("17. Разархивировать ZIP архив");
        Console.WriteLine("18. Выход");
        Console.WriteLine("---------------------------------------------------------------------------------------");
        Console.WriteLine("");
        Console.WriteLine("");
    }

    static void ShowCurrentDirectory()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine("Текущая директория: " + currentDirectory);
    }

    static void ChangeDirectory()
    {
        Console.Write("Введите новый путь к директории: ");
        string newDirectory = Console.ReadLine();

        try
        {
            Directory.SetCurrentDirectory(newDirectory);
            Console.WriteLine("Рабочая директория успешно изменена.");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Указанная директория не найдена.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при изменении директории: " + ex.Message);
        }
    }
    static void GetDiskInfo()
    {
        var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
        foreach (ManagementObject disk in searcher.Get())
        {
            Console.WriteLine($"Точка монтирования: {disk["Name"]}");
            Console.WriteLine($"Общий размер: {Convert.ToInt64(disk["Size"]) / (1024 * 1024 * 1024)} GB");
            Console.WriteLine($"Свободное место: {Convert.ToInt64(disk["FreeSpace"]) / (1024 * 1024 * 1024)} GB");
            Console.WriteLine($"Тип файловой системы: {disk["FileSystem"]}\n");
        }
        string currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine("Текущая рабочая директория: " + currentDirectory);
    }

    static void CreateFile(string fileName)
    {
        File.Create(fileName).Close();
    }

    static void WriteFile(string fileName, string content)
    {
        File.AppendAllText(fileName, content);
    }

    static string ReadFile(string fileName)
    {
        return File.ReadAllText(fileName);
    }

    static void DeleteFile(string fileName)
    {
        File.Delete(fileName);
    }

    static void CreateJsonFile(string fileName, Dictionary<string, string> data)
    {
        string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, jsonString);
    }

    static string ReadJsonFile(string fileName)
    {
        return File.ReadAllText(fileName);
    }

    static void CreateXmlFile(string fileName, Dictionary<string, string> data)
    {
        XElement root = new XElement("person");
        foreach (var kvp in data)
        {
            root.Add(new XElement(kvp.Key, kvp.Value));
        }
        root.Save(fileName);
    }

    static string ReadXmlFile(string fileName)
    {
        XElement root = XElement.Load(fileName);
        return root.ToString();
    }

    static void CreateZipArchive(string zipFileName)
    {
        using (ZipArchive zip = ZipFile.Open(zipFileName, ZipArchiveMode.Create)) { }
    }

    static void AddFileToZip(string zipFileName, string fileName)
    {
        using (ZipArchive zip = ZipFile.Open(zipFileName, ZipArchiveMode.Update))
        {
            zip.CreateEntryFromFile(fileName, Path.GetFileName(fileName));
        }
    }

    static long GetZipFileSize(string zipFileName)
    {
        return new FileInfo(zipFileName).Length;
    }

    static void UnzipFile(string zipFileName, string outputDir)
    {
        ZipFile.ExtractToDirectory(zipFileName, outputDir);
    }
}