using System;
using System.Text.RegularExpressions;

namespace FIRDev;

public class CurrentFiles
{
    public Action<string>? Notify;

    public CurrentFiles(string[] files)
    {
        PathFiles = files;
    }

    public string[] PathFiles;

    public void FileProcessing()
    {
        foreach (var file in PathFiles)
        {
            var cleanedName = Regex.Replace(Path.GetFileName(file), @"\d{8}_\d{6}_", "");

            switch (cleanedName)
            {
                case "treport_1.csv":
                    var csv1 = GetListClassOfCsvFiles<CsvFile1>(file);
                    Notify?.Invoke("TestSend");
                    Console.WriteLine(Path.GetFileName(file));
                    break;
                case "treport_2.csv":
                    var csv2 = GetListClassOfCsvFiles<CsvFile2>(file);
                    Console.WriteLine(Path.GetFileName(file));
                    break;
                case "treport_3.csv":
                    //var csv3 = GetListClassOfCsvFiles<CsvFile3>(file);
                    break;
                default:
                    Console.WriteLine("Файла нет в списке!");
                    break;
            }
        }
    }

    private static List<T> GetListClassOfCsvFiles<T>(string file)
    {
        var list = new List<T>();
        var lines = File.ReadAllLines(file);

        for (int row = 1; row < lines.Length; row++)
        {
            var result = Activator.CreateInstance(typeof(T), lines[row]);
            list.Add((T)result);
        }

        return list;
    }
}
