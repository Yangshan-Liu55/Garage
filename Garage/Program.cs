using Garage.Helpers;
using Garage.Interfaces;
using Garage.UI;

namespace Garage;

internal class Program
{
    static void Main(string[] args)
    {
        IConsoleUI ui = new ConsoleUI();

        ui.Start();
    }
}