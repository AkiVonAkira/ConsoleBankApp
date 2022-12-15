# Unknown Bank

This is a simple console application that allows users to create bank accounts, view account balances, and make deposits and withdrawals.

## Features

- Create bank users with unique usernames and passwords
- Sign in to existing accounts
- Have multiple bank accounts
- View account balances and transaction history
- Make deposits and withdrawals in multiple currencies
- Change account passwords

## Code Overview

### Program

The `Program` class contains the `Main()` method, which is the entry point of the application. It is responsible for showing the main menu and handling user input. The `Main()` method contains a `while` loop that displays the menu and prompts the user to select an option. The user's input is then used in a `switch` statement to call the corresponding method for each option.

The `Program` class also contains the following methods:

- `SignIn()`: This method prompts the user for an account username and password, and then searches the list of users for a matching account. If the provided username and password are correct, the `UserMainMenu()` method is called for the matching user.
- `CreateAccount()`: This method prompts the user for an account username and password, and then creates a new `User` object with the provided information. It also adds the new `User` object to the list of users and saves the updated list to a JSON file.
- `UserMainMenu()`: This method shows the account menu and handles user input for the user actions, such as viewing the account balances, making deposits and withdrawals, and changing the password.

### User

The `User` class represents a bank user. It contains the following properties:

- `Username`: The unique username of the user.
- `PinCode`: The PIN Code for the user.
- `Accounts`: A list of `Account` objects that represent the accounts owned by the user.

The `User` class also contains the following methods:

- `SignIn()`: This method accepts a password and checks if it matches the user's password.
- `CreateAccount()`: This method guides the user to set up a new account and adds it to the `User` list for the user.
- `ViewAccountBalances()`: This method prints the balances of all accounts owned by the user.
- `ChangePin()`: This method accepts a new PIN Code and sets the `PinCode` property of the user to the provided value.

### Account

The `Account` class represents a bank account. It contains the following properties:

- `AccountName`: The name of the account.
- `Currency`: The currency of the account.
- `Balance`: The current balance of the account, in the default currency.

The `Account` class also contains the following methods:

### Deposit & Withdrawal

- The `MakeDeposit()` method accepts an amount and a currency code. It updates the account balance by adding it to the balance.
- The `MakeWithdrawal()` method accepts an amount and a currency code. It updates the account balance by subtracting it from the balance.

### Transaction

The `Transaction` class represents a single transaction from one bank user to another, or alternativily to yourself. It contains the following properties:

- `Amount`: The amount of the transaction.
- `Currency`: The currency code of the transaction.

### CurrencyConverter

The `CurrencyConverter` class contains a `Dictionary` of supported currencies and their exchange rates. It can be used to convert an amount of money from one currency to another.

### Limitations

- The application does not support real-time currency conversion, as the exchange rates are fixed and not updated dynamically.
- The application does not support account locking or other security measures.
- The application does not support internationalization or localization.
- Cannot change an existing accounts currency.

## Usage

To use the application, clone this repository and run the `ConsoleBankApp.sln` file in Visual Studio. Then, build and run the solution.
