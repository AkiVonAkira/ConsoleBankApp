namespace ConsoleBankApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            string[] options = { "Sign In", "Create Account", "Exit" };

            bool showBankMenu = true;
            while (showBankMenu)
            {
                Console.Title = "Krille Bank";
                Console.WriteLine("----------------------------------------\n" +
                    "\n--- Krille Bank ---\nThank you for choosing Krille Bank.\n");
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

                var userInput = Console.ReadKey(false).Key;
                switch (userInput)
                {
                    case ConsoleKey.D1:
                        GetLoginInfo();
                        break;
                    case ConsoleKey.D2:
                        CreateAccount();
                        break;
                    case ConsoleKey.X:
                        showBankMenu = false;
                        break;
                    default:
                        break;
                }
                Console.WriteLine();
            }
        }

        private static Account CreateAccount()
        {
            Console.WriteLine("\n----------------------------------------\n" +
                "\n--- Account Creation Form ---\nHere you will be able to create your account.");
            string userName = InputStringValidator("\nPlease enter your Account Name: ");
            string password = PasswordInput("Please enter your Password: ");
            Console.WriteLine();

            Account signup = new Account(userName, password);
            Console.WriteLine("\nAccount Created!");
            return signup;
        }

        private static void GetLoginInfo()
        {
            int loginAttempts = 3;
            bool validAccount = false;

            Console.WriteLine("\n----------------------------------------\n" +
                "\n--- Krille Login ---\nPlease enter your Account Name and Password below");
            string userName = InputStringValidator("\nAccount Name: ");
            string password = PasswordInput("Password: ");
            loginAttempts -= 1;
            Console.WriteLine();

            Account user = new Account();

            while (loginAttempts > 0 && !validAccount)
            {
                if (user._accountName == userName && user._password == password)
                {
                    validAccount = true;
                    AccountMainMenu(userName);
                    break;
                }
                if (user._accountName != userName || user._password != password)
                {
                    Console.WriteLine("Wrong Login Information, try again.");
                    userName = InputStringValidator("\nAccount Name: ");
                    password = PasswordInput("Password: ");
                    Console.WriteLine();
                    loginAttempts -= 1;
                }
            }
            if (loginAttempts == 0)
            {
                Console.WriteLine("Your Account is now locked.");
            }

        }

        private static void AccountMainMenu(string UserName)
        {
            string[] options = { "Account Inquiry", "Deposit Funds", "Withdraw Funds", "Change Password", "Logout" };

            bool showAccountMainMenu = true;
            while (showAccountMainMenu)
            {
                Console.WriteLine("\n----------------------------------------\n" +
                    "\n--- Krille Account ---\nWelcome " + UserName + "!\n");
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

                var userInput = Console.ReadKey(false).Key;
                Console.WriteLine();
                switch (userInput)
                {
                    case ConsoleKey.D1:
                        AccountInquiry();
                        break;
                    case ConsoleKey.D2:
                        //CreateAccount();
                        break;
                    case ConsoleKey.X:
                        showAccountMainMenu = false;
                        break;
                    default:
                        break;
                }
                Console.WriteLine();
            }
        }


        private static void AccountInquiry()
        {
            int funds = 5000;
            Console.WriteLine("\n----------------------------------------\n" +
                "\nAccount Funds\n$" + funds);
            if (PromptConfirmation("\nReturn To Account Menu")) { } else { Console.WriteLine("You sure?"); }
        }



        /// <summary>
        ///  These are general functions
        /// </summary>
        public static int InputIntValidator(string prompt)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                Console.Write(prompt);
            string? input = Console.ReadLine();
            bool success = int.TryParse(input, out int validInt);
            while (!success)
            {
                Console.WriteLine("Invalid Input. Try again...");
                Console.Write(prompt);
                success = int.TryParse(Console.ReadLine(), out validInt);
            }
            return validInt;
        }

        public static string InputStringValidator(string prompt)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                Console.Write(prompt);
            string? input = Console.ReadLine();
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid Input. Try again...");
                Console.Write(prompt);
                input = Console.ReadLine();
            }
            return input;
        }

        public static string PasswordInput(string prompt)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                Console.Write(prompt);
            string? input = null;
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                if (key.Key == ConsoleKey.Backspace)
                    break;
                input += key.KeyChar;
                Console.Write("•");
            }
            return input;
        }

        public static bool PromptConfirmation(string confirmText = "Would you like to play again?")
        {
            Console.Write(confirmText + " [Y] : ");
            ConsoleKey response = Console.ReadKey(false).Key;
            Console.WriteLine();
            return (response == ConsoleKey.Y);
        }
    }

    class Account
    {
        public int amount;
        public string _accountName;
        public string _password;

        public Account(string AccountName, string Password)
        {
            this._accountName = AccountName;
            this._password = Password;
        }
        public Account()
        {
            this._accountName = "John";
            this._password = "Doe";
        }
    }
}