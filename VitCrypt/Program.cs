using System.Configuration;
using VitCrypt;

class Program
{
    static async Task Main(string[] args)
    {
        bool isActive = true;
        int activeNumber = 0;

        while (isActive)
        {
            var inputDirectory = ConfigurationManager.AppSettings["InputDirectory"];
            var encryptedDirectory = ConfigurationManager.AppSettings["EncryptedDirectory"];
            var decryptedDirectory = ConfigurationManager.AppSettings["DecryptedDirectory"];

            if (string.IsNullOrEmpty(inputDirectory) || string.IsNullOrEmpty(encryptedDirectory) || string.IsNullOrEmpty(decryptedDirectory))
            {
                Console.WriteLine("One or more required configuration values are missing. Please check your app.config file.");
                return;
            }

            EnsureDirectoriesExist(inputDirectory, encryptedDirectory, decryptedDirectory);

            var consoleUi = new ConsoleUI(new FileEncryptor(inputDirectory, encryptedDirectory, decryptedDirectory));

            consoleUi.PrintWelcomMessage();
            consoleUi.PrintMenu();

            activeNumber = int.Parse(Console.ReadLine());

            switch (activeNumber)
            {
                case 1: await consoleUi.StartEncryptAsync(); break;
                case 2: await consoleUi.StartDecryptAsync(); break;
                case 3: consoleUi.EditConfig(); break;
                case 4: isActive = false; break;
            }

            Console.Clear();
        }
    }

    public static void EnsureDirectoriesExist(string inputDirectory, string encryptedDirectory, string decryptedDirectory)
    {
        Directory.CreateDirectory(inputDirectory);
        Directory.CreateDirectory(encryptedDirectory);
        Directory.CreateDirectory(decryptedDirectory);
    }
}
