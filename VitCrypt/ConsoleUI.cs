using System.Configuration;
using System.Diagnostics;

namespace VitCrypt
{
    internal class ConsoleUI(FileEncryptor encryptor)
    {
        private readonly FileEncryptor _encryptor = encryptor;
        private readonly ConfigEditor _configEditor = new ConfigEditor();

        public void PrintWelcomMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("########################################################################");
            Console.WriteLine("Welcome to VitCrypt!\nVitCrypt is a simple console UI for VEncrypt Lib\n" +
                "VitCrypt and VEncrypt is created by Riten");
            Console.WriteLine("Source code: https://github.com/mrRiten/VitCrypt");
            Console.WriteLine("My GitHub: https://github.com/mrRiten");
            Console.WriteLine("########################################################################\n\n");
            Console.ResetColor();
        }

        public void PrintMenu()
        {
            Console.WriteLine("Choose action (write number)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Actions:");
            Console.ResetColor();
            Console.WriteLine("\t1 Start Encrypt");
            Console.WriteLine("\t2 Start Decrypt");
            Console.WriteLine("\t3 Change config (in dev)");
            Console.WriteLine("\t4 Exit");
            Console.Write("Enter a number: ");
        }

        public async Task StartEncryptAsync()
        {
            Console.WriteLine("\n-->StartEncrypt\n");
            Console.WriteLine("Enter a SecretKey for Encrypt");
            Console.Write("Enter: ");

            var key = Console.ReadLine();

            await _encryptor.EncryptDirectoryAsync(key);

            PrintDoneMessage();

            var encryptedDirectory = ConfigurationManager.AppSettings["EncryptedDirectory"];
            OpenDirectory(encryptedDirectory);
        }

        public async Task StartDecryptAsync()
        {
            Console.WriteLine("\n-->StartDecrypt\n");
            Console.WriteLine("Enter a SecretKey for Decrypt");
            Console.Write("Enter: ");

            var key = Console.ReadLine();

            await _encryptor.DecryptDirectoryAsync(key);

            PrintDoneMessage();

            var decryptedDirectory = ConfigurationManager.AppSettings["DecryptedDirectory"];
            OpenDirectory(decryptedDirectory);
        }

        public void EditConfig()
        {
            Console.WriteLine("\n-->EditConfig\n");
            Console.WriteLine("Choose Action:\n\t1 Edit InputDirectory\n\t2 Edit EncryptDirectory\n\t" +
                "3 Edit DecryptDirectory\n");
            Console.Write("Enter: ");

            var activeNumber = int.Parse(Console.ReadLine());
            string? path;

            switch (activeNumber)
            {
                case 1:
                    Console.Write("Enter InputDirectory path: ");
                    path = Console.ReadLine();
                    _configEditor.SetInputDirectory(path);
                    break;
                case 2:
                    Console.Write("Enter EncryptDirectory path: ");
                    path = Console.ReadLine();
                    _configEditor.SetEncryptDirectory(path);
                    break;
                case 3:
                    Console.Write("Enter DecryptDirectory path: ");
                    path = Console.ReadLine();
                    _configEditor.SetDecryptDirectory(path);
                    break;
            }

            PrintDoneMessage();
        }

        private void PrintDoneMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done. Press any key.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void OpenDirectory(string directoryPath)
        {
            Process.Start("explorer.exe", directoryPath);
        }
    }
}
