using System.Text.Json.Serialization;

namespace ATM.Models
{
    internal class CardHolder
    {
        [JsonPropertyName("CardNumber")]
        public string CardNumber { get; set; }

        [JsonPropertyName("Pin")]
        public string Pin { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastNumber")]
        public string LastName { get; set; }

        [JsonPropertyName("Balance")]
        public double Balance { get; set; }

        public CardHolder(string CardNumber, string Pin, string FirstName, string LastName, double Balance) 
        {
            this.CardNumber = CardNumber;
            this.Pin = Pin;
            this.FirstName = FirstName; 
            this.LastName = LastName;   
            this.Balance = Balance;
        }
    }
}
