public class BankAccount {
    private static int count = 0;
    private string? number;
    private decimal accountStatus = 0;
    private bool overdraftAllowed;
    private List<AccountHolder>accountHolders = new List<AccountHolder>();
    private List<Transaction> transactions = new List<Transaction>();

    public string? Number {
        get => number;
        set => number = value;
    }
    public decimal AccountStatus {
        get => accountStatus;
        set => accountStatus = value;
    }
    public bool OverdraftAllowed {
        get => overdraftAllowed;
        set => overdraftAllowed = value;
    }
    public List<AccountHolder> AccountHolders {
        get => accountHolders;
        set => accountHolders = value;
    }
    public List<Transaction> Transactions {
        get => transactions;
        set => transactions = value;
    }

    public BankAccount(bool overdraftAllowed, List<AccountHolder> accountHolders) {
        
        count++;
        if(accountHolders.Count == 0) throw new Exception("List of account holders cannot be empty");
        this.number = count.ToString();
        this.overdraftAllowed = overdraftAllowed;
        this.accountHolders = accountHolders;
    }


    public static void makeTransaction(BankAccount sourceAccount, Bank bank){
        Console.WriteLine("What kind of transaction do you want to make?");
        Console.WriteLine("1: cashless payment\n2: cashless paycheck\n3: transfer between accounts");
        string? a = Console.ReadLine();

        Console.WriteLine("Enter sum:");
        int sum = Int32.TryParse(Console.ReadLine(), out sum) ? sum : throw new Exception("Wrong input!");
        if(sum < 0) throw new Exception("Sum cannot be less than zero");

        Console.WriteLine("Enter description:");
        string? description = Console.ReadLine();
        if(a == "1") BankAccount.cashlessPayment(sourceAccount, sum, description);
        else if(a == "2"){
             if(!sourceAccount.overdraftAllowed && sum > sourceAccount.accountStatus) throw new Exception("Not enough moeny");
             BankAccount.cashlessPaycheck(sourceAccount, sum, description);
        }
        else if(a == "3"){
             if(!sourceAccount.overdraftAllowed && sum > sourceAccount.accountStatus) throw new Exception("Not enough moeny");
            Console.WriteLine("Enter number of target account:");
            string? number = Console.ReadLine();
            BankAccount.transfer(sourceAccount, bank.BankAccounts.Find(a => a.Number==number), sum, description);
        }
        else { Console.WriteLine("Wrong input!"); }
    }


    private static void cashlessPayment(BankAccount targetAccount, decimal sum, string description){
        targetAccount.AccountStatus += sum;
        targetAccount.Transactions.Add(new Transaction(null, targetAccount, sum, description, TransactionType.payment));
    }

    private static void cashlessPaycheck(BankAccount sourceAccount, decimal sum, string description){
        sourceAccount.AccountStatus -= sum;
        sourceAccount.Transactions.Add(new Transaction(sourceAccount, null, sum, description, TransactionType.paycheck));
    }

    private static void transfer(BankAccount sourceAccount, BankAccount targetAccount, decimal sum, string description){
        sourceAccount.AccountStatus -= sum;
        targetAccount.AccountStatus += sum;
        sourceAccount.Transactions.Add(new Transaction(sourceAccount, targetAccount, sum, description, TransactionType.paycheck));
        targetAccount.Transactions.Add(new Transaction(sourceAccount, targetAccount, sum, description, TransactionType.payment));
    }


    public override string ToString()
    {
        return new string('-', 50) + "\nBank account:\n" + new string('-', 50) + "\nAccount number: " + this.number
                + "\nAccount status: " + this.accountStatus + "\nAccount holders:\n\t" + String.Join("\n\t", this.accountHolders)
                + "\nTransactions:" + (this.Transactions.Count==0 ? " -" : "\n" + String.Join("\n", this.transactions)) 
                + "\n" + new string('-', 50);
    }


    public static BankAccount operator + (BankAccount account, AccountHolder holder){
        if(account.AccountHolders.Contains(holder)) throw new Exception("Account already has such holder");
        
        account.AccountHolders.Add(holder);
        return account;
    }


    public static BankAccount operator - (BankAccount account, AccountHolder holder){
        if(!account.AccountHolders.Contains(holder)) throw new Exception("Account does not have such holder");
        if(account.AccountHolders.Count <= 1) throw new Exception("Account cannot have less holders");
        
        account.AccountHolders.Remove(holder);
        return account;
    }
    
}