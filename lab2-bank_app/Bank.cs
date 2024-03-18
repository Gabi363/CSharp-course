using System.Threading.Tasks.Dataflow;

public class Bank {
    private List<BankAccount> bankAccounts = new List<BankAccount>();

    public List<BankAccount> BankAccounts {
        get => bankAccounts;
    }
    private void addAccount(){

        Console.WriteLine(new string('-', 30) + "CREATING NEW ACCOUNT" + new string('-', 30));
        Console.WriteLine("Is overdraft allowed for this account? [yes/no] ");
        bool overdraft;
        string? input = Console.ReadLine();
        if(input != "yes" && input != "no") throw new Exception("Wrong input!");
        overdraft = (input == "yes");
        bankAccounts.Add(new BankAccount(overdraft, new List<AccountHolder>{ createHolder() }));
    }

    private AccountHolder createHolder(){

        Console.WriteLine("New account holder is natural or legal person? [n/l] ");
        string? input = Console.ReadLine();
        if(input != "n" && input != "l") throw new Exception("Wrong input!");
        if(input == "n") return NaturalPerson.createNaturalPerson(); 
        else return LegalPerson.createLegalPerson(); 
    }

    private AccountHolder findHolder(BankAccount account, string name) {
        foreach(NaturalPerson holder in account.AccountHolders.FindAll(h => h is NaturalPerson)){
            if(holder.Name == name) return holder;
        }
        foreach(LegalPerson holder in account.AccountHolders.FindAll(h => h is LegalPerson)){
            if(holder.Name == name) return holder;
        }
        throw new Exception("No such holder");
    }   

    public override string ToString() {
        return this.bankAccounts.Count==0 ? "no accounts" : String.Join("\n", bankAccounts.Select(b=>b.Number));
    }


    public static void Main(string[] args){
        Bank bank = new Bank();
        int x;
        
        do {
            Console.WriteLine(new string('-', 50) + "SIMPLE BANK" + new string('-', 50) + "\n");
            Console.WriteLine("List of bank accounts:\n" + bank);
            Console.WriteLine("\nChoose what you want to do:");
            Console.WriteLine("1: add new account\n2: choose account\n3: exit\n");
            x = Int32.TryParse(Console.ReadLine(), out x) ? x : throw new Exception("Wrong input!");

            if(x == 1){
                try{ bank.addAccount(); } 
                catch(Exception e){ Console.WriteLine(e.Message); }
            }

            else if(x == 2){
                try {
                    Console.WriteLine("Choose number of account:");
                    x = Int32.TryParse(Console.ReadLine(), out x) ? x : throw new Exception("Wrong input!");
                    BankAccount? account = bank.BankAccounts.Find(a => a.Number == x.ToString());

                    int y;
                    do {
                        Console.WriteLine(account);
                        Console.WriteLine("\nChoose what you want to do:");
                        Console.WriteLine("1: add new holder\n2: delete holder\n3: make transaction\n4: main menu\n");
                        y = Int32.TryParse(Console.ReadLine(), out y) ? y : throw new Exception("Wrong input!");

                        switch (y) {
                            case 1:
                                account += bank.createHolder();
                                break;

                            case 2:
                                Console.WriteLine("Enter name of a holder you want to delete:");
                                string? name = Console.ReadLine();
                                account -= bank.findHolder(account, name);
                                break;

                            case 3:
                                BankAccount.makeTransaction(account, bank);
                                break;
                            default:
                                Console.WriteLine("Wrong input");
                                break;

                        }
                    } while(y != 4);
                } catch(Exception e){
                    Console.WriteLine(e.Message);
                }
            }
            else if( x!= 3) Console.WriteLine("Wrong input");
        } while(x != 3);
        System.Environment.Exit(0);

    }
}