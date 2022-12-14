namespace ConsoleBankApp
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        // Store the account information in a list of Account objects
        static List<Account> accounts = new List<Account>();

        // Define a dictionary to map currency codes to currency names and symbols
        private static readonly Dictionary<string, (string Name, string Symbol, decimal ExchangeRate)> Currencies = new Dictionary<string, (string Name, string Symbol, decimal ExchangeRate)>
        {
            { "USD", ("US Dollar", "$", 1.0m) },
            { "EUR", ("Euro", "€", 0.9m) },
            { "GBP", ("British Pound", "£", 0.8m) },
            { "JPY", ("Japanese Yen", "¥", 105.0m) },
            { "AUD", ("Australian Dollar", "AUD", 1.4m) },
            { "CAD", ("Canadian Dollar", "CAD", 1.3m) },
            { "CHF", ("Swiss Franc", "CHF", 1.1m) },
            { "HKD", ("Hong Kong Dollar", "HKD", 7.8m) },
            { "MXN", ("Mexican Peso", "MXN", 21.0m) },
            { "SEK", ("Swedish Krona", "SEK", 8.8m) },
            { "BRL", ("Brazilian Real", "BRL", 5.0m) },
            { "CNY", ("Chinese Yuan", "CNY", 6.6m) },
            { "INR", ("Indian Rupee", "INR", 0.01m) },
            { "IDR", ("Indonesian Rupiah", "IDR", 0.00006m) },
            { "RUB", ("Russian Ruble", "RUB", 0.012m) },
            { "SGD", ("Singapore Dollar", "SGD", 1.3m) },
            { "ZAR", ("South African Rand", "ZAR", 0.05m) },
            { "KRW", ("South Korean Won", "KRW", 0.00084m) },
            { "TWD", ("Taiwanese Dollar", "TWD", 29.0m) },
            { "THB", ("Thai Baht", "THB", 30.0m) },
            { "TRY", ("Turkish Lira", "TRY", 0.12m) },
            { "UAH", ("Ukrainian Hryvnia", "UAH", 0.03m) },
            { "VND", ("Vietnamese Dong", "VND", 0.00004m) },
            { "NOK", ("Norwegian Krone", "NOK", 0.1m) },
            { "DKK", ("Danish Krone", "DKK", 0.13m) },
            { "PLN", ("Polish Zloty", "PLN", 0.22m) },
            { "CZK", ("Czech Koruna", "CZK", 0.04m) },
        };

        static void Main(string[] args)
        {
            // Load the account information from the JSON file
            accounts = LoadAccounts();

            Console.Clear();

            // Define an array with the options
            string[] options = { "Sign In", "Create Account", "List Accounts and Balances", "Exit" };


            // Use a while loop to show the menu and handle user input
            bool showBankMenu = true;
            while (showBankMenu)
            {
                Console.WriteLine("----------------------------------------\n" +
                    "\n--- Unknown Bank ---\nThank you for choosing Unknown Bank.\n");

                // Print the available options
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == options.Length - 1)
                    {
                        Console.WriteLine("(X) " + options[i]);
                    }
                    else
                    {
                        Console.WriteLine("(" + (i + 1) + ") " + options[i]);
                    }
                }

                Console.Write("\r\nSelect an option ---> ");

                // Get the user's input key
                var userInput = Console.ReadKey(true).Key;
                switch (userInput)
                {
                    case ConsoleKey.D1:
                        Console.Write(options[0]);
                        // Call the Login method and store the returned user object
                        Account loggedInUser = Login();

                        // Check if the user object is not null (i.e. the user was successfully logged in)
                        if (loggedInUser != null)
                        {
                            // User was successfully logged in, call the AccountMainMenu method
                            AccountMainMenu(loggedInUser);
                        }
                        break;
                    case ConsoleKey.D2:
                        Console.Write(options[1]);
                        // Call the CreateAccount method and store the returned user object
                        Account createdUser = CreateAccount();

                        // Check if the user object is not null (i.e. the user was successfully created)
                        if (createdUser != null)
                        {
                            // User was successfully created, call the AccountMainMenu method
                            AccountMainMenu(createdUser);
                        }
                        break;
                    case ConsoleKey.D3:
                        Console.Write(options[2]);
                        // Call the ListAccountsAndBalances method
                        ListAccountsAndBalances();
                        break;
                    case ConsoleKey.X:
                        showBankMenu = false;
                        break;
                    default:
                        // If the user's input key is not valid, show an error message
                        Console.WriteLine("\nSorry, that is not a valid option. Please try again.");
                        break;
                }
                Console.WriteLine();
            }

            // Save the account information to the JSON file
            SaveAccounts();
        }

        private static void ListAccountsAndBalances()
        {
            // Load the account information from the JSON file
            accounts = LoadAccounts();
            Console.Clear();

            // Print the list of accounts and their balances
            Console.WriteLine("List of Accounts and Balances: (For Development Purposes Only)\n");
            foreach (var account in accounts)
            {
                Console.WriteLine($"Name: {account._accountName} \nBalance: {Currencies[account._currency].Symbol}{account._balance} {Currencies[account._currency].Name}\n");
            }

            // Wait for the user to press a key before returning to the main menu
            Console.Write("\r\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        private static Account CreateAccount()
        {
            Console.WriteLine("\n----------------------------------------\n" +
                "\n--- Account Creation Form ---\nHere you will be able to create your account.");

            // Check if the accounts list is null before searching for the user
            if (accounts == null)
            {
                Console.WriteLine("\nSorry, there was an error while accessing the accounts list. Please try again later.");
                return null;
            }

            string userName = InputStringValidator("\nPlease enter your Account Name: ");

            // Check if the account name is already in use
            Account user = accounts.FirstOrDefault(a => a._accountName == userName);
            if (user != null)
            {
                Console.WriteLine("\nSorry, that account name is already in use. Please enter a different name.");
                userName = InputStringValidator("\nPlease enter your Account Name: ");
            }

            string password = PasswordInput("Please enter your Password: ");

            // Prompt the user for their currency and currency type
            decimal balance = InputDecimalValidator("Enter your starting currency amount: ");

            string currency = InputStringValidator("Enter your currency code (eg: USD, GBP, EUR): ").ToUpper();

            // Check if the currency code is valid
            if (!Currencies.ContainsKey(currency))
            {
                Console.WriteLine("\nSorry, that is not a valid currency code. Please enter a valid code.");
                currency = InputStringValidator("Enter your currency code: ");
            }

            // Create a new Account object with the user's information
            Account newAccount = new Account(userName, password, balance, currency);

            // Add the new account to the list of accounts
            accounts.Add(newAccount);

            // Save the account information to the JSON file
            SaveAccounts();

            Console.WriteLine("\nYour account has been created successfully!\n");

            return newAccount;
        }

        private static Account Login()
        {
            Console.WriteLine("\n----------------------------------------\n" +
                "\n--- Sign In ---\nPlease enter your account information to sign in.");

            // Check if the accounts list is null before searching for the user
            if (accounts == null)
            {
                Console.WriteLine("\nSorry, there was an error while accessing the accounts list. Please try again later.");
                return null;
            }

            string userName = InputStringValidator("\nPlease enter your Account Name: ");

            // Search for the user in the list of accounts
            Account user = accounts.FirstOrDefault(a => a._accountName == userName);

            // Check if the user was found in the accounts list
            if (user != null)
            {
                // User was found, prompt for the password
                string password = PasswordInput("Please enter your Password: ");

                // Check if the password is correct
                if (user._password == password)
                {
                    //Console.WriteLine("\nWelcome back, " + user._accountName + "!\n");
                    return user;
                }
                else
                {
                    // Incorrect password, show an error message
                    Console.WriteLine("\nSorry, that is not the correct password. Please try again.");
                    return null;
                }
            }
            else
            {
                // User was not found, show an error message
                Console.WriteLine("\nSorry, no account was found with that name. Please try again.");
                return null;
            }
        }

        private static void AccountMainMenu(Account user)
        {
            // Load the account information from the JSON file
            accounts = LoadAccounts();
            // Define an array with the options
            string[] options = { "Deposit Funds", "Withdraw Funds", "Transfer Funds", "Account Inquiry", "Change Password", "Exit" };


            // Use a while loop to show the menu and handle user input
            bool showUserMenu = true;
            while (showUserMenu)
            {
                Console.WriteLine("----------------------------------------\n" +
                    $"\nWelcome back, {user._accountName}! Your current balance is {user._balance:.###} {user._currency}\n");

                // Print the available options
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == options.Length - 1)
                    {
                        Console.WriteLine("(X) " + options[i]);
                    }
                    else
                    {
                        Console.WriteLine("(" + (i + 1) + ") " + options[i]);
                    }
                }

                Console.Write("\r\nSelect an option ---> ");

                // Get the user's input key
                var userInput = Console.ReadKey(true).Key;
                switch (userInput)
                {
                    case ConsoleKey.D1:
                        Console.Write(options[0]);
                        MakeDeposit(user);
                        break;
                    case ConsoleKey.D2:
                        Console.Write(options[1]);
                        Withdraw(user);
                        break;
                    case ConsoleKey.D3:
                        Console.Write(options[2]);
                        Console.WriteLine("\n----------------------------------------\n" +
                            "\n--- Transfer Funds ---");

                        // Get the recipient account
                        string recipient = InputStringValidator("Which account would you like to transfer to? ");
                        Account recipientAccount = accounts.SingleOrDefault(a => a._accountName == recipient);

                        // Check if the recipient account exists
                        if (recipientAccount == null)
                        {
                            Console.WriteLine("\nRecipient Account not found.");
                            return;
                        }

                        // Get the amount to transfer
                        decimal amount = InputDecimalValidator("\nHow much would you like to transfer? ");
                        Transaction(user, recipientAccount, amount);
                        break;
                    case ConsoleKey.D4:
                        Console.Write(options[3]);
                        ViewAccountBalance(user);
                        break;
                    case ConsoleKey.D5:
                        Console.Write(options[4]);
                        ChangePassword(user);
                        break;
                    case ConsoleKey.X:
                        Console.Write(options[5]);
                        Console.WriteLine("\n\nThank you for choosing Unknown Bank. Have a great day!");
                        // Save the account information to the JSON file
                        SaveAccounts();
                        showUserMenu = false;
                        break;
                    default:
                        // If the user's input key is not valid, show an error message
                        Console.WriteLine("\nSorry, that is not a valid option. Please try again.");
                        break;
                }
                Console.WriteLine();
            }

            // Save the account information to the JSON file
            SaveAccounts();
        }

        private static void ViewAccountBalance(Account user)
        {
            Console.WriteLine("\n----------------------------------------\n" +
                $"\n--- Account Inquiry ---\nYour current balance is {Currencies[user._currency].Symbol}{user._balance} {Currencies[user._currency].Name} " +
                $"\nWith an exchange rate of {Currencies[user._currency].ExchangeRate}.");
        }
        private static void MakeDeposit(Account user)
        {
            Console.WriteLine("\n----------------------------------------\n" +
            "\n--- Deposit Funds ---");
            decimal depositAmount = InputDecimalValidator("Enter the amount you want to deposit: ");
            user._balance += depositAmount;
            Console.WriteLine("\nYour new balance is " + user._balance + " " + user._currency + ".");
            SaveAccounts(accounts);
        }

        private static void Withdraw(Account user)
        {
            Console.WriteLine("\n----------------------------------------\n" +
                "\n--- Withdraw Funds ---");
            decimal withdrawAmount = InputDecimalValidator("Enter the amount you want to withdraw: ");

            // Check if the user has enough funds to withdraw the specified amount
            if (user._balance >= withdrawAmount)
            {
                user._balance -= withdrawAmount;
                Console.WriteLine($"\nYour new balance is {user._balance} {user._currency}.");
                SaveAccounts(accounts);
            }
            else
            {
                Console.WriteLine($"\nInsufficient funds. Your balance is {user._balance} {user._currency}.");
            }
        }

        private static void ChangePassword(Account user)
        {
            // Prompt the user for their current password
            string currentPassword = PasswordInput("Enter your current password: ");

            // Check if the current password is correct
            if (user._password != currentPassword)
            {
                Console.WriteLine("Incorrect password. Please try again.");
                ChangePassword(user);
            }
            else
            {
                // Prompt the user for their new password
                string newPassword = PasswordInput("Enter your new password: ");

                // Update the user's password in the list of accounts
                user._password = newPassword;
                Console.WriteLine("Password successfully changed!");

                SaveAccounts(accounts);
            }
        }


        private static void Transaction(Account senderAccount, Account recipientAccount, decimal amount)
        {
            // Load the accounts from the accounts file
            List<Account> accounts = LoadAccounts();

            // Check if the sender account has sufficient funds to exchange the specified amount
            if (senderAccount._balance >= amount)
            {
                // Convert the amount to the currency of the recipient account
                decimal convertedAmount = CurrencyConverter(senderAccount._currency, recipientAccount._currency, amount);

                // Transfer the converted amount from the sender account to the recipient account
                senderAccount._balance -= amount;
                recipientAccount._balance += convertedAmount;

                // Save the updated accounts to the accounts file
                SaveAccounts(accounts);

                Console.WriteLine("Funds transferred successfully!");
            }
            else
            {
                Console.WriteLine("Insufficient funds to transfer.");
            }
        }

        private static decimal CurrencyConverter(string fromCurrency, string toCurrency, decimal amount)
        {
            // Convert the amount to the base currency (USD)
            decimal baseAmount = amount / Currencies[fromCurrency].ExchangeRate;

            // Convert the base amount to the target currency
            decimal convertedAmount = baseAmount * Currencies[toCurrency].ExchangeRate;

            return convertedAmount;
        }



        private static void SaveAccounts()
        {
            // Save the account information to the JSON file
            SaveAccounts(accounts);
        }

        private static void SaveAccounts(List<Account> accounts)
        {
            // Serialize the list of accounts to a JSON string
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);

            // Write the JSON string to a file
            File.WriteAllText("accounts.json", json);
        }

        private static List<Account> LoadAccounts()
        {
            List<Account> accounts = new List<Account>();

            // Check if the accounts file exists
            if (File.Exists("accounts.json"))
            {
                try
                {
                    // Read the contents of the accounts file
                    string json = File.ReadAllText("./accounts.json");

                    // Deserialize the JSON string and convert it to a list of accounts
                    accounts = JsonConvert.DeserializeObject<List<Account>>(json);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Could not load account information from the accounts.json file.");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                // If the accounts file does not exist, create an array of default accounts
                Account[] defaultAccounts =
                {
                    new Account("user1", "password1", 100, "USD"),
                    new Account("user2", "password2", 200, "EUR"),
                    new Account("user3", "password3", 300, "GBP"),
                    new Account("user4", "password4", 400, "CHF"),
                    new Account("user5", "password5", 500, "JPY")
                };

                // Add the default accounts to the list of accounts
                accounts.AddRange(defaultAccounts);
            }

            return accounts;
        }

        /// <summary>
        ///  These are general functions
        /// </summary>
        private static decimal InputDecimalValidator(string prompt)
        {
            decimal userInput = 0;
            bool validInput = false;
            while (!validInput)
            {
                Console.Write(prompt);
                validInput =
                    decimal.TryParse(Console.ReadLine(), out userInput);
                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
            return userInput;
        }

        private static string InputStringValidator(string prompt)
        {
            string userInput = "";
            while (userInput.Length == 0 || userInput.Contains(" "))
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();
                if (userInput.Length == 0 || userInput.Contains(" "))
                {
                    Console.WriteLine("\nThat is not a valid input. Please try again.");
                }
            }
            return userInput;
        }

        public static string PasswordInput(string prompt)
        {
            string password = "";

            Console.Write(prompt);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // If the key is not a backspace or enter, add it to the password string
                if (
                    key.Key != ConsoleKey.Backspace &&
                    key.Key != ConsoleKey.Enter
                )
                {
                    password += key.KeyChar;
                    Console.Write("•");
                } // If the key is a backspace, remove the last character from the password string
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        public static bool PromptConfirmation(string prompt)
        {
            Console.WriteLine(prompt);
            Console.Write("Enter Y to confirm or N to cancel: ");

            // Keep prompting the user for input until they enter Y or N
            while (true)
            {
                var userInput = Console.ReadKey(false).Key;
                if (userInput == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    return true;
                }
                if (userInput == ConsoleKey.N)
                {
                    Console.WriteLine();
                    return false;
                }
            }
        }
    }

    public class Account
    {
        public string _accountName { get; set; }
        public string _password { get; set; }
        public decimal _balance { get; set; }
        public string _currency { get; set; }

        public Account(string accountName, string password, decimal balance, string currency)
        {
            _accountName = accountName;
            _password = password;
            _balance = balance;
            _currency = currency;
        }
    }
}
