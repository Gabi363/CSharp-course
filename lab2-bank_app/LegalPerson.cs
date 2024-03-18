public class LegalPerson : AccountHolder {
    private string? name;
    private string? headquarters;

    public string? Name { get => name; }
    public string? Headquarters { get => headquarters; }

    public LegalPerson(string name, string headquarters) {
        this.name = name;
        this.headquarters = headquarters;
    }

    public override string ToString() {
        return "Legal person: " + name + " from " + headquarters;
    }

    public static LegalPerson createLegalPerson() {
        Console.WriteLine("Enter name: ");
        string name = Console.ReadLine() ?? "";
        Console.WriteLine("Enter headquarters: ");
        string headquarters = Console.ReadLine() ?? "";
        LegalPerson person = new LegalPerson(name, headquarters);
        return person;
    }
}