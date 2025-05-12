using System.Security.Principal;
using System.Windows.Markup;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task4
{
    public class Account
    {   public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }
        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }
        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $" Name is :{Name} , Balnce is : {Balance}";
        }
        /// Overloading method

        public static Account operator +(Account lhs, Account rhs)
        {
            Account account3 = new Account( lhs.Name + " " + rhs.Name, lhs.Balance + rhs.Balance);
            return account3;
        }

    }
     
    public class SavingsAccount : Account
    {
        public SavingsAccount(string Name ="unnamed  account" , double Balance =0.0, double rate =0):base(Name, Balance)
        {
            Rate = rate;
        }
        public double Rate { get; set; }
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + Rate);
        }
        public override string ToString()
        {
            return $"{base.ToString()} , Rate :{Rate}";
        }
    }
    public class CheckingAccount : Account
    {
        public CheckingAccount(string Name = "Unnamed Account", double Balance = 0.0, string name = "None ", double balance = 0.0, double fee = 1.5)
        {
           Name = name;
           Balance = balance;
            Fee = fee;
        }
        public string Name { get; set; }
        public double Balance { get; set; }
        public double Fee { get; set; }
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount - Fee);
        }
        public override string ToString()
        {
            return $"{base.ToString()} , fee is :{Fee}";
        }
    }
    public class TrustAccount : SavingsAccount
    {
        public TrustAccount(string Name, double Balance, double rate ,DateTime openAccount):base(Name ,Balance,rate)
        {
            OpenAccountDate = openAccount;
        } 

        public DateTime OpenAccountDate { get; set; }

        public override bool Deposit(double amount)
        {
            if (amount > 5000)
            {
                Balance += amount +50;
                return true;
            }
            else if(amount <5000 && amount >0)
            {
                Balance += amount; 
                /// bouness add succseefully
                return true;
            }
            return false;
        }
        public virtual bool Withdraw(double amount)
        {
            int maxWithdaw = 3;
            
            double percentage = 0.2;

            if(maxWithdaw <4)
            {
                if ( amount  < Balance * percentage && amount > 0)
                {
                    Balance -= amount;
                    maxWithdaw -= 1;

                    return true;

                }
            }
            
            return false;
        }

        public override string ToString()
        {
            return $"{base.ToString()} , Date  :{DateTime.Now}";
        }

    }
    public static class AccountUtil
    {
        // Utility helper functions for Account class

        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }
    internal class Program
    {
        static void Main()

        {
      
                // Accounts
                var accounts = new List<Account>();
                accounts.Add(new Account());
                accounts.Add(new Account("Larry"));
                accounts.Add(new Account("Moe", 2000));
                accounts.Add(new Account("Curly", 5000));

                AccountUtil.Display(accounts);
                AccountUtil.Deposit(accounts, 1000);
                AccountUtil.Withdraw(accounts, 2000);

                // Savings
                var savAccounts = new List<Account>();
                savAccounts.Add(new SavingsAccount());
                savAccounts.Add(new SavingsAccount("Superman"));
                savAccounts.Add(new SavingsAccount("Batman", 2000));
                savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

                AccountUtil.Display(savAccounts);
                AccountUtil.Deposit(savAccounts, 1000);
                AccountUtil.Withdraw(savAccounts, 2000);

                // Checking
                var checAccounts = new List<Account>();
                checAccounts.Add(new CheckingAccount());
                checAccounts.Add(new CheckingAccount("Larry2"));
                checAccounts.Add(new CheckingAccount("Moe2", 2000));
                checAccounts.Add(new CheckingAccount("Curly2", 5000));

                AccountUtil.Display(checAccounts);
                AccountUtil.Deposit(checAccounts, 1000);
                AccountUtil.Withdraw(checAccounts, 2000);
                AccountUtil.Withdraw(checAccounts, 2000);

         
                // Trust
                //var trustAccounts = new List<Account>();
                //trustAccounts.Add(new TrustAccount());
                //trustAccounts.Add(new TrustAccount("Superman2"));
                //trustAccounts.Add(new TrustAccount("Batman2", 2000));
                //trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

                //AccountUtil.Display(trustAccounts);
                //AccountUtil.Deposit(trustAccounts, 1000);
                //AccountUtil.Deposit(trustAccounts, 6000);
                //AccountUtil.Withdraw(trustAccounts, 2000);
                //AccountUtil.Withdraw(trustAccounts, 3000);
                //AccountUtil.Withdraw(trustAccounts, 500);

                Console.WriteLine();
            // ---- Overloading
            Account account1 = new Account() { Name = "Esraa",  Balance = 10000 };
            Account account2 = new Account() { Name = "Aly", Balance = 1000 };




        }
    }
}
