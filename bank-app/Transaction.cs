public enum TransactionType {
        payment,
        paycheck
    }
public class Transaction {
    

    private BankAccount? sourceAccount;
    private BankAccount? targetAccount;
    private decimal sum;
    private string? description;
    private TransactionType transactionType;


    public BankAccount? SourceAccount {
        get => sourceAccount;
        set => sourceAccount = value;
    }
    public BankAccount? TargetAccount {
        get => targetAccount;
        set => targetAccount = value;
    }
    public decimal Sum {
        get => sum;
        set => sum = value;
    }
    public string? Description {
        get => description;
        set => description = value;
    }
    public TransactionType TransactionType {
        get => transactionType;
        set => transactionType = value;
    }

    public Transaction(BankAccount sourceAccount, BankAccount targetAccount, decimal sum, string description, TransactionType transactionType) {
        
        if(sourceAccount == null && targetAccount == null){
            throw new Exception("Accounts cannot be null");
        }
        this.sourceAccount = sourceAccount;
        this.targetAccount = targetAccount;
        this.sum = sum;
        this.description = description;
        this.transactionType = transactionType;
    }

    public override string ToString()
    {
        return new string('-', 30) + "\nSource account: " + (sourceAccount==null ? "-" : sourceAccount.Number)
                + "\nTarget account: " + (targetAccount==null ? "-" : targetAccount.Number) + "\nSum: " + sum 
                + "\nDescription: " + description + "\nType: " + transactionType + "\n" + new string('-', 30);
    }
}