using ObsBackup;

Console.WriteLine($"Hello, {Environment.UserName}");

const string targetFolder = @"C:\Program Files\obs-studio";
string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
string yamiFolderPath = Path.Combine(localAppDataPath, "Yami");

if (!Directory.Exists(targetFolder))
{
    Console.WriteLine("Folder was not found: " + targetFolder);
    return;
}

try
{
    var timer = new TimerFile();
    DateTime date;

    if (Directory.Exists(yamiFolderPath))
    {
        date = timer.ReadFile(yamiFolderPath);

        if (DateTime.Today > date.AddMonths(1))
        {
            timer.CreateObsBackup(targetFolder);
        }

    }
    else
    {
        Directory.CreateDirectory(yamiFolderPath);
        timer.CreateFile(yamiFolderPath);
        timer.CreateObsBackup(targetFolder);
    }
    Console.Clear();
    Console.WriteLine("Obs Backup was created.");
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}