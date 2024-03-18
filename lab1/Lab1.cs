class Lab1{
    Lab1(){
        Console.WriteLine("Wybór zadania:");
        int opcja = Convert.ToInt32(Console.ReadLine());
        switch(opcja){
            case 1:
                zad1();
                break;
            case 2:
                Console.WriteLine("Nazwa pliku:");
                string nazwa_pliku = Console.ReadLine();
                Console.WriteLine("Ciąg znaków:");
                string znaki = Console.ReadLine();
                zad2(nazwa_pliku, znaki);
                break;
            case 3:
                Console.WriteLine("Nazwa pliku:");
                string nazwa = Console.ReadLine();
                Console.WriteLine("Ilość liczb:");
                int n = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Przedział (dwie liczby):");
                int x1 = Int32.Parse(Console.ReadLine());
                int x2 = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Seed:");
                int seed = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Liczby całkowite czy rzeczywiste (c/r):");
                string if_real = Console.ReadLine();
                zad3(nazwa, n, x1, x2, seed, if_real);
                break;
            case 4:
                Console.WriteLine("Nazwa pliku:");
                string nazwa_p = Console.ReadLine();
                zad4(nazwa_p);
                break;
        }
    }
    static void Main(string[] args){
        new Lab1();
    }

    void zad1(){
        Console.WriteLine("Podaj zestaw napisów a potem liczbę, ile razy chcesz je wypisać");
        string ostatni = "";
        // int liczba;
        while(true){
            string x = Console.ReadLine();
            // if(int.TryParse(x, out liczba)){
            //     break;
            // }
            if(x == "koniec!") break;
            if(ostatni == "") ostatni = x;
            else if(string.Compare(x, ostatni) > 0) ostatni = x;
            StreamWriter sw = new StreamWriter("napisy.txt", append:true);
            sw.WriteLine(x);
            sw.Close();
            // napis += x;
        }
        // for(int i=0; i<liczba; i++){
            Console.WriteLine(ostatni);
        // }
    }


    void zad2(string nazwa_pliku, string znaki){

        StreamReader sr = new StreamReader(nazwa_pliku);
        int linijka = 1;
        while (!sr.EndOfStream)
        {
            String napis = sr.ReadLine();
            int pozycja;
            bool zawiera = napis.Contains(znaki);
            if(zawiera){
                pozycja = napis.IndexOf(znaki);
                if(pozycja >= 0){
                    Console.WriteLine("Plik zawiera ciąg znaków w linijce {0} na pozycji {1}", linijka, pozycja+1);
                }
            }

            linijka++;
        }
        sr.Close();
    }


    void zad3(string nazwa_pliku, int n, int x1, int x2, int seed, string if_real){

        StreamWriter sw = new StreamWriter(nazwa_pliku, append:true);
        Random random = new Random(seed);
        for(int i=0; i<n; i++){
            // int seed = 0
            if(if_real == "c"){
                int x = random.Next(x1, x2);
                sw.WriteLine(x);
            } else if(if_real == "r"){
                double x = (random.NextDouble() * (x2-x1)) + x1;
                sw.WriteLine(x);
            }
        }
        sw.Close();
    }


    void zad4(string nazwa_pliku){
        StreamReader sr = new StreamReader(nazwa_pliku);

        var lineCount = File.ReadLines(nazwa_pliku).Count();
        Console.WriteLine("Liczba linii {0}", lineCount);
        var numberOfCharacters = File.ReadAllLines(nazwa_pliku).Sum(s => s.Length);
        Console.WriteLine("Liczba znaków {0}", numberOfCharacters);


        double min = 1000;
        double max = 0;
        double sum = 0;
        while (!sr.EndOfStream)
        {
            double liczba = Convert.ToDouble(sr.ReadLine());
            if(liczba < min) min = liczba;
            if(liczba > max) max = liczba;
            sum += liczba;
        }
        Console.WriteLine("Najmniejsza {0}", min);
        Console.WriteLine("Największa {0}", max);
        Console.WriteLine("Średnia {0}", sum/lineCount);



        sr.Close();



        sr.Close();
    }






    void wstep(){
        //wypisywanie do konsoli - rÃ³Å¼ne sposoby konkatenacji napisÃ³w
        int liczba = 45;
        string napis = "jakiś napis";
        Console.WriteLine("Liczba to " + liczba + " a napis to " + napis + ".");
        Console.WriteLine("Liczba to {0} a napis to {1}.", liczba, napis);
        Console.WriteLine($"Liczba to {liczba} a napis to {napis}.");

        Console.WriteLine("Wpisz tekst i naciśnij enter:");
        String napis1 = Console.ReadLine();
        Console.WriteLine($"Wczytano napis {napis1}");

        //odczytuje następny znak ze standardowego strumienia wejściowego,
        //zwraca wartość ascii tego znaku
        //spróbuj wpisać np. asd
        int kod = Console.Read();
        Console.WriteLine("Read wczytać literę " + (char)kod);
        kod = Console.Read();
        Console.WriteLine("Read wczytać literę " + (char)kod);
        Console.WriteLine("Wciśnij dowolny klawisz aby zakończyć program.");
        //ReadKey oczekuje na wciśnięcie klawisza
        Console.ReadKey();
    }
}



