# Unknown Bank

This is a simple console application that allows users to create bank accounts, view account balances, and make deposits and withdrawals.

## Features

- Create bank accounts with unique usernames and passwords
- Sign in to existing accounts
- View account balances and transaction history
- Make deposits and withdrawals in multiple currencies
- Change account passwords

## Code Overview

### Program

The Program class contains the Main() method, which is the entry point of the application. It is responsible for showing the main menu and handling user input. The Main() method contains a while loop that displays the menu and prompts the user to select an option. The user's input is then used in a switch statement to call the corresponding method for each option.

The Program class also contains the following methods:

- CreateAccount(): This method prompts the user for an account username and password, and then creates a new Account object with the provided information.
- GetLoginInfo(): This method prompts the user for an account username and password, and then searches the list of accounts for a matching account. If the provided username and password are correct, the AccountMainMenu() method is called for the matching account.
- AccountMainMenu(): This method shows the account menu and handles user input for the account actions, such as viewing the account balance, making deposits and withdrawals, and changing the password.

### Account

The Account class represents a bank account. It contains the following properties:

- AccountName: The unique username of the account.
- Password: The password for the account.
- Balance: The current balance of the account, in the default currency.
- Transactions: A list of Transaction objects that represent the transactions made by the account.

The Account class also contains the following methods:

- MakeDeposit(): This method accepts an amount and a currency code, and adds a new Transaction object to the account's transaction list with the provided information. It also updates the account balance by converting the amount to the default currency and adding it to the balance.
- MakeWithdrawal(): This method accepts an amount and a currency code, and adds a new Transaction object to the account's transaction list with the provided information. It also updates the account balance by converting the amount to the default currency and subtracting it from the balance.
- ChangePassword(): This method accepts a new password and sets the Password property of the account to the provided value.
- ViewAccountBalance(): This method prints the account balance and transaction history to the console.

### Transaction

The Transaction class represents a single transaction, which is either a deposit or withdrawal. It contains the following properties:

- Amount: The amount of the transaction.
- Currency: The currency code of the transaction.
- Type: The type of the transaction (deposit or withdrawal).

### CurrencyConverter

The CurrencyConverter class contains a dictionary of supported currencies and their exchange rates. It can be used to convert an amount of money from one currency to another.

### Limitations

- The application does not support real-time currency conversion, as the exchange rates are fixed and not updated dynamically.
- The application does not support transferring funds between accounts.
- The application does not support account locking or other security measures.
- The application does not support internationalization or localization.

## Usage

To use the application, clone this repository and run the ConsoleBankApp.sln file in Visual Studio. Then, build and run the solution.
