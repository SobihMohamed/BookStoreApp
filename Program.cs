using BookStoreConsole.UI;

namespace BookStoreConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var menu = new ConsoleMenu();
            await menu.StartAsync();
        }
    }
}