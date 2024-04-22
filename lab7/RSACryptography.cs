using System.Security.Cryptography;  
using System.Text;   
class RSACryptography  
{  
    static void Main(string[] args)  
    {  
        if(args.Length == 0){
            Console.WriteLine("Pass parameter!");
            return;
        }
        string argument = args[0];
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string filePublicKey = "publicKey.xml";
        string filePrivateKey = "privateKey.xml";
        string ?publicKey = null;
        string ?privateKey = null;

        if(argument == "0"){
            publicKey = rsa.ToXmlString(false);
            File.WriteAllText(filePublicKey, publicKey);
            privateKey = rsa.ToXmlString(true);
            File.WriteAllText(filePrivateKey, privateKey);  
            return;
        }
        else if(argument == "1" || argument == "2"){
            if(args.Length < 3){
                Console.WriteLine("Pass file names!");
                return;
            }
            string fileFrom = args[1];
            string fileTo = args[2];
            
            if (!File.Exists(filePublicKey) || !File.Exists(filePrivateKey)){
                Console.WriteLine("File doesn't exist!");
                return;
            }
            publicKey = File.ReadAllText(filePublicKey);
            privateKey = File.ReadAllText(filePrivateKey);
            if (!File.Exists(fileFrom)){
                    Console.WriteLine("File doesn't exist!");
                    return;
                }
            if(argument == "1"){
                EncryptText(publicKey, fileFrom, fileTo);  
            }
            if(argument == "2"){
                DecryptData(privateKey, fileFrom, fileTo);
            }

        }
        else{
            Console.WriteLine("Wrong parameter!");
        }
        return;
    }  

    static void EncryptText(string publicKey ,string fileFrom, string fileTo)  
    {  
       string text = File.ReadAllText(fileFrom); 
        UnicodeEncoding byteConverter = new UnicodeEncoding();  
        byte[] dataToEncrypt = byteConverter.GetBytes(text);  
        byte[] encryptedData;   
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())  
        {  
            rsa.FromXmlString(publicKey);  
            encryptedData = rsa.Encrypt(dataToEncrypt, false);   
        }  
        File.WriteAllBytes(fileTo, encryptedData);  
        Console.WriteLine("Data has been encrypted");   
    }  

    static void DecryptData(string privateKey, string fileFrom, string fileTo)  
    {  
        byte[] daneDoOdszyfrowania = File.ReadAllBytes(fileFrom);  
        byte[] odszyfrowaneDane;  
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())  
        {  
            rsa.FromXmlString(privateKey);  
            odszyfrowaneDane = rsa.Decrypt(daneDoOdszyfrowania, false);   
        }  
        UnicodeEncoding byteConverter = new UnicodeEncoding();  
        string decryptedData = byteConverter.GetString(odszyfrowaneDane);  
        File.WriteAllText(fileTo, decryptedData); 
        Console.WriteLine("Data has been decrypted");   
    }  
}  