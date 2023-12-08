using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsBackup
{
    public class TimerFile
    {
        public TimerFile()
        {
        }
        public void CreateFile(string yamiFolderPath)
        {
            string timerFilePath = Path.Combine(yamiFolderPath, "timer.txt");

            if (!File.Exists(timerFilePath))
            {
                File.Create(timerFilePath).Close();
            }

            string dataToWrite = $"{DateTime.Today}";
            File.WriteAllText(timerFilePath, dataToWrite);
            Console.WriteLine($"timer.txt edited.");
        }

        public DateTime ReadFile(string yamiFolderPath)
        {
            string timerFilePath = Path.Combine(yamiFolderPath, "timer.txt");
            return Convert.ToDateTime(File.ReadAllText(timerFilePath));
        }

        public void CreateObsBackup(string sourceFolderPath)
        {
            string destinationFolderPath = @"C:\ObsBackup";
            Directory.CreateDirectory(destinationFolderPath);

            string zipFileName = "obs-studio.zip";
            string zipFilePath = Path.Combine(destinationFolderPath, zipFileName);

            try
            {
                ZipFile.CreateFromDirectory(sourceFolderPath, zipFilePath);
                Console.WriteLine("Backup was created: " + zipFilePath);

                DeleteOldBackups(destinationFolderPath, 12);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void DeleteOldBackups(string backupFolderPath, int maxBackups)
        {
            
            string[] backupFiles = Directory.GetFiles(backupFolderPath, "*.zip");

            
            if (backupFiles.Length > maxBackups)
            {
                Array.Sort(backupFiles, (a, b) => File.GetLastAccessTime(a).CompareTo(File.GetLastAccessTime(b)));

                File.Delete(backupFiles[0]);
                Console.WriteLine("Oldest backup deleted: " + backupFiles[0]);
            }
        }

    }
}
