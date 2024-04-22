using System.Security.Cryptography;
using System.Text;


class Signature{
    static void Main(string[] args){

        if(args.Length < 2){
            Console.WriteLine("Pass arguments!");
        }
        string dataFile = args[0];
        string signatureFile = args[1];
        if (!File.Exists(dataFile)){
            Console.WriteLine("File doesn't exist!");
            return;
        }
        string source_data = File.ReadAllText(dataFile);
        byte[] data = Encoding.ASCII.GetBytes(source_data);

        // using SHA256 alg  = SHA256.Create();  
        // byte[] hash = alg.ComputeHash(data);
        // RSAParameters parametersRSA;

        string publicKey = File.ReadAllText("publicKey.xml");
        string privateKey = File.ReadAllText("privateKey.xml");
        byte[] signature;


        if (!File.Exists(signatureFile)){
        //     using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        //     {
        //         rsa.FromXmlString(publicKey);  
        //         parametersRSA = rsa.ExportParameters(false);
        //         RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
        //         rsaFormatter.SetHashAlgorithm(nameof(SHA256));
        //         signature = rsaFormatter.CreateSignature(hash);
        //         return;
        //     }

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())  
            {  
                rsa.FromXmlString(privateKey);  
                signature = rsa.SignData(data, new SHA1CryptoServiceProvider());
                File.WriteAllBytes(signatureFile, signature);
            }
        }

        // signature = File.ReadAllBytes(signatureFile);
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())  
        {  
        //     rsa.FromXmlString(privateKey);  
        //     // parametersRSA = rsa.ExportParameters(false);
        //     // rsa.ImportParameters(parametersRSA);
        //     RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
        //     rsaDeformatter.SetHashAlgorithm(nameof(SHA256));
        //     if (rsaDeformatter.VerifySignature(hash, signature))
        //     {
        //         Console.WriteLine("Podpis jest prawidłowy");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Podpis nie jest prawidłowy");
        //     }

            rsa.FromXmlString(publicKey);  
            signature = File.ReadAllBytes(signatureFile);
            if (rsa.VerifyData(data, new SHA1CryptoServiceProvider(), signature)){

                Console.WriteLine("Signature is correct");
            }
            else{
                Console.WriteLine("Signature is not correct");
            }
        }
    }
}