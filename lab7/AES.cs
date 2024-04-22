using System.Text;
using System.Security.Cryptography;

public class AES
{
    public static byte[]? Decrypt(String password, byte[]salt, byte[]initVector, int iterations, byte[]data)
    {
        Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        Aes decAlg = Aes.Create();
        decAlg.Key = k1.GetBytes(16);
        decAlg.IV = initVector;
        MemoryStream decryptionStreamBacking = new MemoryStream();
        CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
        decrypt.Write(data, 0, data.Length);
        decrypt.Flush();
        decrypt.Close();
        k1.Reset();
        return decryptionStreamBacking.ToArray();
    }

    public static byte[]? Encrypt(String password, byte[]salt, byte[]initVector,
                    int iteration, byte[]dataToEncrypt)
    {
        Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iteration, HashAlgorithmName.SHA256);
        Aes encAlg = Aes.Create();
        encAlg.IV = initVector;
        encAlg.Key = k1.GetBytes(16);
        MemoryStream encryptionStream = new MemoryStream();
        CryptoStream encrypt = new CryptoStream(encryptionStream, encAlg.CreateEncryptor(), CryptoStreamMode.Write);
        encrypt.Write(dataToEncrypt, 0, dataToEncrypt.Length);
        encrypt.FlushFinalBlock();
        encrypt.Close();
        byte[] edata1 = encryptionStream.ToArray();
        k1.Reset();
        return edata1;
    }
    public static void Main(string[] args)
    {
        if(args.Length < 4){
            Console.WriteLine("Pass parameters!");
            return;
        }
        string fileFrom = args[0];
        string fileTo = args[1];
        string password = args[2];
        string operation = args[3];
        if(!File.Exists(fileFrom)){
            Console.WriteLine("File doesn't exist!");
            return;
        }

        byte[] salt = Encoding.UTF8.GetBytes("CwuuJx/7");
        byte[] initVector = Encoding.UTF8.GetBytes("uyZG5sl561Wo2ZTE");
        int iterationNumber = 2000;

        if(operation == "0"){
            string text = File.ReadAllText(fileFrom);
            byte[] utfD1 = new System.Text.UTF8Encoding(false).GetBytes(text);
            byte[] encrypted = Encrypt(password, salt, initVector, iterationNumber, utfD1);
            File.WriteAllBytes(fileTo, encrypted);
            Console.WriteLine("Saved!");
        }
        else if(operation == "1"){
            byte[] encrypted = File.ReadAllBytes(fileFrom);
            byte[] decrypted = Decrypt(password, salt, initVector, iterationNumber, encrypted);
            string decryptedString = new UTF8Encoding(false).GetString(decrypted);
            Console.WriteLine(decryptedString);
            File.WriteAllText(fileTo, decryptedString);
        }

    }
}