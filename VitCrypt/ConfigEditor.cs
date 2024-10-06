using System.Configuration;

namespace VitCrypt
{
    internal class ConfigEditor
    {
        private readonly Configuration _configuration = ConfigurationManager
            .OpenExeConfiguration(ConfigurationUserLevel.None);

        public void SetInputDirectory(string inputDirectory)
        {
            _configuration.AppSettings.Settings["InputDirectory"].Value = inputDirectory;
            _configuration.Save(ConfigurationSaveMode.Modified);
        }

        public void SetEncryptDirectory(string encryptDirectory)
        {
            _configuration.AppSettings.Settings["EncryptedDirectory"].Value = encryptDirectory;
            _configuration.Save(ConfigurationSaveMode.Modified);
        }

        public void SetDecryptDirectory(string decryptDirectory)
        {
            _configuration.AppSettings.Settings["DecryptedDirectory"].Value = decryptDirectory;
            _configuration.Save(ConfigurationSaveMode.Modified);
        }
    }
}
