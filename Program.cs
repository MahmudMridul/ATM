using ATM.LogUtils;
using ATM.Models;

namespace ATM
{
    public class Program
    {
        static void Main(string[] args)
        {
            Logger.Log("Instantiating machine");
            Machine machine = new Machine();
            Logger.Log("Starting process");
            machine.ProcessTransaction();
        }
    }
}