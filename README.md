# VitCrypt

VitCrypt is a simple console UI for VEncrypt Lib, a library for encrypting and decrypting files using the AES algorithm. This program allows you to easily encrypt and decrypt files in a directory using a secret key.

## Features

* Encrypt files in a directory using a secret key
* Decrypt files in a directory using a secret key
* Change the input, encrypted, and decrypted directory paths in the configuration file
* Open the encrypted or decrypted directory in the file explorer after encryption or decryption

## Usage

1. Clone the repository or download the source code.
2. Build the solution in Visual Studio.
3. Run the `VitCrypt.exe` executable.
4. Choose an action from the menu by entering the corresponding number.
5. Follow the prompts to enter the secret key and confirm the action.

## Configuration

The program uses a configuration file (`app.config`) to store the paths to the input, encrypted, and decrypted directories. You can edit these paths using the `EditConfig` option in the program menu.

## Dependencies

* .NET Core 3.1 or later

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

* [Riten](https://github.com/mrRiten) for creating the VEncrypt Lib and VitCrypt program.
* [Microsoft](https://docs.microsoft.com/en-us/dotnet/core/) for providing the .NET Core framework.