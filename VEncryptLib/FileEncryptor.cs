using System.Security.Cryptography;

public class FileEncryptor(string inputPath, string encryptPath, string decryptPath)
{
    private readonly string _inputPath = inputPath;
    private readonly string _encryptPath = encryptPath;
    private readonly string _decryptPath = decryptPath;

    public async Task EncryptDirectoryAsync(string password)
    {
        Directory.CreateDirectory(_encryptPath);

        var tasks = Directory.EnumerateFiles(_inputPath, "*", SearchOption.AllDirectories)
            .Select(inputFile => EncryptFileAsync(inputFile, Path.Combine(_encryptPath, Path.GetRelativePath(_inputPath, inputFile)), password));

        await Task.WhenAll(tasks);
    }

    public async Task DecryptDirectoryAsync(string password)
    {
        Directory.CreateDirectory(_decryptPath);

        var tasks = Directory.EnumerateFiles(_encryptPath, "*", SearchOption.AllDirectories)
            .Select(inputFile => DecryptFileAsync(inputFile, Path.Combine(_decryptPath, Path.GetRelativePath(_encryptPath, inputFile)), password));

        await Task.WhenAll(tasks);
    }

    private async Task EncryptFileAsync(string inputFile, string outputFile, string password)
    {
        byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
        byte[] salt = new byte[32];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        using (var aes = Aes.Create())
        {
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 10000, HashAlgorithmName.SHA256);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);

            using (var inputStream = File.OpenRead(inputFile))
            using (var outputStream = File.Create(outputFile))
            using (var cryptoStream = new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                await outputStream.WriteAsync(salt, 0, salt.Length);
                await inputStream.CopyToAsync(cryptoStream);
            }
        }
    }

    private async Task DecryptFileAsync(string inputFile, string outputFile, string password)
    {
        byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
        byte[] salt = new byte[32];

        using (var inputStream = File.OpenRead(inputFile))
        {
            await inputStream.ReadAsync(salt, 0, salt.Length);

            using (var aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(passwordBytes, salt, 10000, HashAlgorithmName.SHA256);
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);

                using (var cryptoStream = new CryptoStream(inputStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var outputStream = File.Create(outputFile))
                {
                    await cryptoStream.CopyToAsync(outputStream);
                }
            }
        }

        File.Delete(inputFile);
    }
}
