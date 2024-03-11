public class NaturalPerson : AccountHolder {
    private string? name;
    private string? surname;
    private string? secondName;
    private string? pesel;
    private string? passportNumber;

    public NaturalPerson(string name, string surname, string secondName, string pesel, string passportNumber) {
        
        if(pesel == null && passportNumber == null){
            throw new Exception("PESEL or passport number cannot be null");
        }
        if(pesel == null || pesel.Length != 11) throw new Exception("Wrong PESEL format");

        this.name = name;
        this.surname = surname;
        this.secondName = secondName;
        this.pesel = pesel;
        this.passportNumber = passportNumber;

    }

    public string? Name {
        get => name;
        set => name = value;
    }
    public string? Surname {
        get { return surname; }
        set { surname = value; }
    }
    public string? SecondName {
        get => secondName;
        set => secondName = value;
    }
    public string? Pesel {
        get => pesel;
        set {
                if(pesel == null || pesel.Length != 11) throw new Exception("Wrong PESEL format");
                else pesel = value;
            }
    }
    public string? PassportNumber {
        get => passportNumber;
        set => passportNumber = value;
    }

    public override string ToString() {
        return "Natural person: " + name + " " + surname;
    }

    public static NaturalPerson createNaturalPerson() {
        Console.WriteLine("Enter name: ");
        string name = Console.ReadLine() ?? "";
        Console.WriteLine("Enter surname: ");
        string surname = Console.ReadLine() ?? "";
        Console.WriteLine("Enter secondname: ");
        string secondname = Console.ReadLine() ?? "";
        Console.WriteLine("Enter PESEL: ");
        string pesel = Console.ReadLine() ?? "";
        Console.WriteLine("Enter passport number: ");
        string passportNumber = Console.ReadLine() ?? "";
        NaturalPerson person = new NaturalPerson(name, surname, secondname, pesel, passportNumber);
        return person;
    }
}