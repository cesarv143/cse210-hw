using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // List to store the numbers
        List<int> numbers = new List<int>();

        // User input to populate the list
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        int input;
        
        while (true)
        {
            Console.Write("Enter number: ");
            input = int.Parse(Console.ReadLine());

            // Exit the loop if the user enters 0
            if (input == 0)
                break;

            // Add the number to the list
            numbers.Add(input);
        }

        // Calculate the sum
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }

        // Calculate the average
        double average = (numbers.Count > 0) ? (double)sum / numbers.Count : 0;

        // Find the maximum number
        int max = (numbers.Count > 0) ? numbers[0] : 0;
        foreach (int num in numbers)
        {
            if (num > max)
            {
                max = num;
            }
        }

        // Output the results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch challenge: Find the smallest positive number
        int smallestPositive = int.MaxValue;
        foreach (int num in numbers)
        {
            if (num > 0 && num < smallestPositive)
            {
                smallestPositive = num;
            }
        }

        // If there was no positive number, set smallestPositive to indicate so
        if (smallestPositive == int.MaxValue)
        {
            Console.WriteLine("There is no positive number.");
        }
        else
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }

        // Sort the list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}
