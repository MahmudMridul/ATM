using ATM.LogUtils;
using System.Text.Json;

namespace ATM.Models
{
    internal class Machine
    {
        public string CurrentCardNumber { get; set; }
        public string CurrentPin { get; set; }
        private string ReadCardNumber()
        {
            
            Console.WriteLine("Enter your card number:");
            string cardNumber = Console.ReadLine();
            return cardNumber;
        }

        private string ReadPin()
        {

            Console.WriteLine("Enter your pin:");
            string pin = Console.ReadLine();
            return pin;
        }

        private Boolean ValidCardInfo(string cardNumber, string pin)
        {
            if (cardNumber == null) return false;
            if(pin == null) return false;
            if (cardNumber.Length == 8 && pin.Length == 4)
            {
                var cardHolders = GetCardHolders();

                foreach (var cardHolder in cardHolders)
                {
                    if (cardNumber.Equals(cardHolder.CardNumber) && pin.Equals(cardHolder.Pin)) return true;
                }
            }
            return false; 
        }

        private CardHolder[] GetCardHolders() 
        {
            string jsonString = File.ReadAllText(@"Data/data.json");
            var objects = JsonSerializer.Deserialize<CardHolder[]>(jsonString);
            return objects.ToArray();
        }

        private void SetCardHolders(CardHolder[] cardHolders)
        {
            string jsonString = JsonSerializer.Serialize(cardHolders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(@"Data/data.json", jsonString);
        }

        private int GetSelectedOption() 
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1 | Withdraw");
            Console.WriteLine("2 | Deposit");
            Console.WriteLine("3 | Check balance");

            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }

        private void ProcessOption(int option)
        {
            if (option == 1)
            {
                Console.WriteLine("Enter the amount you want to withdraw:");
                double amount = Convert.ToDouble(Console.ReadLine());
                Withdraw(amount);
            }
            else if (option == 2)
            {
                Console.WriteLine("Enter the amount you want to deposit:");
                double amount = Convert.ToDouble(Console.ReadLine());
                Deposit(amount);
            }
            else if (option == 3)
            {
                CheckBalance();
            }
            else
            {
                Logger.Log("Invalid option selected");
                Console.WriteLine("Invalid option");
            }
            Console.WriteLine("Press any button to exit");
            string anyInput = Console.ReadLine();
        }

        private void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Logger.Log("Invalid amount given during withdraw");
                Console.WriteLine("Invalid amount");
                return;
            }

            var cardHolders = GetCardHolders();

            foreach (var cardHolder in cardHolders)
            {
                if (CurrentCardNumber.Equals(cardHolder.CardNumber) && CurrentPin.Equals(cardHolder.Pin))
                {
                    if(cardHolder.Balance < amount)
                    {
                        Logger.Log("User tried to withdraw more than balance");
                        Console.WriteLine("Not enough balance. You current balance is " + cardHolder.Balance);
                    }
                    else
                    {
                        cardHolder.Balance -= amount;
                        Console.WriteLine("You successfully withdrawed " +  amount + " from your card");
                        Console.WriteLine("Your current balance is " + cardHolder.Balance);
                        Logger.Log("Successful withdraw");
                        SetCardHolders(cardHolders);
                    }
                    break;
                }
            }
        }

        private void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid amount given during deposit");
                Logger.Log("Invalid amount");
                return;
            }
            var cardHolders = GetCardHolders();

            foreach (var cardHolder in cardHolders)
            {
                if (CurrentCardNumber.Equals(cardHolder.CardNumber) && CurrentPin.Equals(cardHolder.Pin))
                {
                    cardHolder.Balance += amount;
                    Console.WriteLine("You successfully deposited " + amount + " to your card");
                    Console.WriteLine("Your current balance is " + cardHolder.Balance);
                    Logger.Log("Successful deposit  ");
                    SetCardHolders(cardHolders);
                    break;
                }
            }
        }

        private void CheckBalance()
        {
            var cardHolders = GetCardHolders();

            foreach (var cardHolder in cardHolders)
            {
                if (CurrentCardNumber.Equals(cardHolder.CardNumber) && CurrentPin.Equals(cardHolder.Pin))
                {
                    Console.WriteLine("Your current balance is " + cardHolder.Balance);
                    break;
                }
            }
        }

        public void ProcessTransaction()
        {
            string cardNumber = ReadCardNumber();
            string pin = ReadPin();

            if(!ValidCardInfo(cardNumber, pin)) 
            {
                Console.WriteLine("Invalid card info!");
                Logger.Log("Invalid card info");
                return;
            }
            CurrentCardNumber = cardNumber;
            CurrentPin = pin;
            int option = GetSelectedOption();
            ProcessOption(option);
        }
    }
}
