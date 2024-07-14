using System;

namespace FIRDev
{
    public static class Program
    {
        static readonly string nameCurrentFolder = DateTime.Now.ToString("yyyyMMdd_HHmm");
        static readonly string sourcePath = Path.Combine("DirectoryFiles");
        static readonly string historyPath = Path.Combine("HistoryFiles");

        public static void Main(string[] args)
        {
            CheckHistoryFolderOrCreateHistoryFolder();
            CopySourcePathInHistoryPath();

            string[] currentFiles = Directory.GetFiles(
                Path.Combine(historyPath, nameCurrentFolder)
            );
            NotifyEmail email = new NotifyEmail();
            CurrentFiles current = new CurrentFiles(currentFiles);
            current.Notify += email.Send;
            current.FileProcessing();

            if (currentFiles.Length > 0)
                DeleteOldInHistoryFolder();
        }

        private static void CheckHistoryFolderOrCreateHistoryFolder()
        {
            if (!Directory.Exists(historyPath))
                Directory.CreateDirectory(historyPath);
        }

        private static void CopySourcePathInHistoryPath()
        {
            string[] files = Directory.GetFiles(sourcePath);

            foreach (var file in files)
            {
                Directory.CreateDirectory(Path.Combine(historyPath, nameCurrentFolder));
                var historyFullPathFile = Path.Combine(
                    historyPath,
                    nameCurrentFolder,
                    Path.GetFileName(file)
                );
                File.Copy(file, historyFullPathFile);
                //File.Delete(file);
            }
        }

        private static void DeleteOldInHistoryFolder()
        {
            string[] oldFolder = Directory.GetDirectories(historyPath);

            foreach (var folder in oldFolder)
            {
                var info = new DirectoryInfo(folder);
                if ((DateTime.Now - info.CreationTime).Hours > 2)
                {
                    Directory.Delete(folder, true);
                }
            }
        }
    }
}
