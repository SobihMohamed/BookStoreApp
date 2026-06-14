using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.UI
{
    public static class InputValidator
    {
        public static string GetString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim(); 

                ShowError("Input cannot be empty. Please try again.");
            }
        }
        public static int GetInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= 0)
                    return result;

                ShowError("Invalid input. Please enter a valid positive number.");
            }
        }
        public static decimal GetDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal result) && result > 0)
                    return result;

                ShowError("Invalid input. Please enter a valid price greater than 0.");
            }
        }
        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}");
            Console.ResetColor();
        }

        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCCESS] {message}");
            Console.ResetColor();
        }
    }
}
