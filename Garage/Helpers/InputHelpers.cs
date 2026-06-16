using Garage.Models.Enums;

namespace Garage.Helpers;

public static class InputHelpers
{
    public static string ReadLine => Console.ReadLine() ?? string.Empty;
    private static Random _rand = new Random();

    public static int ReadInt(string message)
    {
        do
        {
            Console.Write(message);
            string input = ReadLine;

            if (int.TryParse(input, out int result))
            {
                return result;
            }

            Console.WriteLine("Please enter an integer.");
        }
        while (true);
    }

    public static decimal ReadDecimal(string message)
    {
        do
        {
            Console.Write(message);
            string input = ReadLine;

            if (decimal.TryParse(input, out decimal result))
            {
                return result;
            }

            Console.WriteLine("Please enter a decimal number");
        }
        while (true);
    }

    public static string ReadString(string message)
    {
        do
        {
            Console.Write(message);
            string input = ReadLine;

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Input cannot be empty");
        }
        while (true);
    }

    public static string ReadStringToUpper(string message)
    {
        return ReadString(message).ToUpper();
    }

    public static string GenerateRandomUpperAlphanumericString(int size)
    {
        String str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        String randomString = "";
        for (int i = 0; i < size; i++)
        {
            int x = _rand.Next(str.Length);
            randomString += str[x];
        }

        return randomString;
    }

    public static int GetRandomIndex(int length)
    {
        return _rand.Next(length);
    }

    public static string GetRandomColor()
    {
        String[] colors = { "red", "black", "white", "blue" };
        int x = _rand.Next(colors.Length);

        return colors[x];
    }

    public static FuelType GetRandomFuelType()
    {
        FuelType[] types = { FuelType.Gasoline, FuelType.Diesel };
        int x = _rand.Next(2);

        return types[x];
    }
}
