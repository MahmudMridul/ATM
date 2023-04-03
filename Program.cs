using ATM.Models;

namespace ATM
{
    public class Program
    {
        static void Main(string[] args)
        {
            Machine machine = new Machine();
            machine.ProcessTransaction();
        }
    }
}