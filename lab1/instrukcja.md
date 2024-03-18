# Laboratorium 01: Pierwsze aplikacje konsolowe C# .NET Framework Core 7.0.
## Programowanie zaawansowane 2

- Maksymalna liczba punktĂłw: 10

- Skala ocen za punkty:
    - 9-10 ~ bardzo dobry (5.0)
    - 8 ~ plus dobry (4.5)
    - 7 ~ dobry (4.0)
    - 6 ~ plus dostateczny (3.5)
    - 5 ~ dostateczny (3.0)
    - 0-4 ~ niedostateczny (2.0)

Celem laboratorium jest zapoznanie z operacjami wejĹcia/wyjĹcia jÄzyka C# i praktyki implementacji prostych algorytmĂłw. 

NiektĂłre programy wymagajÄ podania z linii poleceĹ pewnych parametrĂłw. Dla uproszczenia przyjmijmy, Ĺźe programy nie muszÄ obsĹugiwaÄ wyjÄtkĂłw spowodowanych ewentualnymi bĹÄdami konwersji oraz, Ĺźe uĹźytkownicy podajÄ odpowiedniÄ liczbÄ parametrĂłw.

1. W programie Visual Studio Code stwĂłrz nowÄ aplikacjÄ konsolowÄ technologii .NET Framework 7.0 i uruchom go (1 punkt).

```cs

> dotnet new console --framework net7.0
> dotnet run
```

2. Napisz program, ktĂłry bÄdzie pobieraĹ z klawiatury napisy aĹź do momentu, kiedy uĹźytkownik wpisze napis: "koniec!". Program ma zapamiÄtywaÄ napis (trzymaÄ go w zmiennej), ktĂłry po posortowaniu zgodnie z kolejnoĹciÄ leksykograficznÄ bÄdzie ostatni. Wszystkie wprowadzane do tej pory napisy majÄ byÄ zapisywane do pliku tekstowego w nowych linijkach. Zapis do pliku proszÄ zrobiÄ w trybie append (2 punkty).

```cs

//Zapis linijki tekstu do pliku w trybie append
StreamWriter sw = new StreamWriter("NazwaPliku.txt", append:true);
sw.WriteLine("JakiĹ napis");
sw.Close();

```

3. Napisz program, ktĂłry w pliku tekstowym znajdzie wszystkie wystÄpienia wybranego ciÄgu znakĂłw. Program jako parametr (linii komend) ma pobieraÄ nazwÄ pliku oraz ciÄg znakĂłw, ktĂłry ma zostaÄ znaleziony. Jako wynik do konsoli proszÄ wypisaÄ numery linijek oraz pozycje od poczÄtku linijek, w ktĂłrych znaleziono szukany ciÄg znakĂłw, np. "linijka: 10, pozycja: 5" (2 punkty).

```cs

//czytanie z pliku tekstowego linijka po linijce aĹź do koĹca pliku
StreamReader sr = new StreamReader("NazwaPlikuTekstowego.txt");
while (!sr.EndOfStream)
{
    String napis = sr.ReadLine();
}
sr.Close();

```

4. Napisz program, ktĂłry do pliku tekstowego wygeneruje n losowych liczb. KaĹźda liczba powinna byÄ zapisywana w osobnej linijce. Parametry programu (linii komend) (2 punkty): 
    - nazwa pliku, 
    - liczba n, 
    - przedziaĹ wartoĹci, z ktĂłrego losowane sÄ liczby,
    - seed,
    - czy losowe liczby majÄ byÄ rzeczywiste czy caĹkowite

```cs

//generowanie losowej liczby
int seed = 0;
Random random = new Random(seed);
int l = random.Next(0,10);//liczba z przedziaĹu [0,10)

```

5. Napisz program, ktĂłry w pliku utworzonym przy pomocy programu z polecenia (4) bÄdzie obliczaĹ i wypisywaĹ na ekran (3 punkty):
    - liczbÄ linii pliku,
    - liczbÄ znakĂłw w pliku,
    - najwiÄkszÄ liczbÄ,
    - najmniejszÄ liczbÄ,
    - ĹredniÄ liczb.

6. Napisz (a nie ĹciÄgnij z Internetu ;-) ) implementacjÄ sortowania przez ĹÄczenie [link](https://pl.wikipedia.org/wiki/Sortowanie_przez_%C5%82%C4%85czenie_naturalne), ktĂłra bÄdzie pracowaÄ na plikach tekstowych (caĹoĹÄ danych ma NIE BYÄ wczytana na raz do jakiejĹ struktury pomocniczej: tabeli, listy itp. - program ma pracowaÄ na plikach o dostÄpie sekwencyjnym). Program ma mieÄ moĹźliwoĹÄ posortowania pliku tekstowego utworzonego przy pomocy programu z punktu (4). (Za rozwiÄzanie dodatkowa ocena 5.0).

Powodzenia! :-)