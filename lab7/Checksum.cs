using System.Security.Cryptography;  
using System.Text;   
class Checksum  
{  
    static void Main(string[] args)  
    {  
        if(args.Length < 3){
            Console.WriteLine("Pass parameters!");
            return;
        }
        string dataFile = args[0];
        string hashFile = args[1];
        string hashAlgoithm = args[2];

        if (!File.Exists(dataFile)){
            Console.WriteLine("File doesn't exist!");
            return;
        }

        string data = File.ReadAllText(dataFile);
        string hash = "";
        switch (hashAlgoithm){
                case "SHA256":
                    hash = hashSHA256(data);
                    break;
                case "SHA512":
                    hash = hashSHA512(data);
                    break;
                case "MD5":
                    hash = hashMD5(data);
                    break;    
        }
        if(hash == null) {
            Console.WriteLine("Wrong hash!");
            return;
        }
        if (!File.Exists(hashFile)){
            File.WriteAllText(hashFile, hash);
            return;
        }
        if(hash == File.ReadAllText(hashFile)){
            Console.WriteLine("Hash is correct");
        }
        else{
            Console.WriteLine("Hash is not correct");
        }
        

        
    } 

    static String hashSHA256(String napis)
    {
        Encoding enc = Encoding.UTF8;
        var hashBuilder = new StringBuilder();
        using var hash = SHA256.Create();
        byte[] result = hash.ComputeHash(enc.GetBytes(napis));
        foreach (var b in result)
            hashBuilder.Append(b.ToString("x2"));
        return hashBuilder.ToString();
    }

    static String hashSHA512(String napis)
    {
        Encoding enc = Encoding.UTF8;
        var hashBuilder = new StringBuilder();
        using var hash = SHA512.Create();
        byte[] result = hash.ComputeHash(enc.GetBytes(napis));
        foreach (var b in result)
            hashBuilder.Append(b.ToString("x2"));
        return hashBuilder.ToString();
    }

    static String hashMD5(String napis)
    {
        Encoding enc = Encoding.UTF8;
        var hashBuilder = new StringBuilder();
        using var hash = MD5.Create();
        byte[] result = hash.ComputeHash(enc.GetBytes(napis));
        foreach (var b in result)
            hashBuilder.Append(b.ToString("x2"));
        return hashBuilder.ToString();
    }
}  