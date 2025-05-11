using System;

class Program
{
    static void Main()
    {
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        int percentage = int.Parse(Console.ReadLine());

        // Variable to store the letter grade
        string letter = "";

        // Determine the letter grade based on the percentage
        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Check if the student passed or failed
        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course!");
        }
        else
        {
            Console.WriteLine("You did not pass the course. Better luck next time!");
        }

        // Variable to store the grade sign (+ or -)
        string sign = "";

        // Only add a "+" or "-" to grades that are not "F"
        if (letter != "F")
        {
            // Get the last digit of the grade percentage
            int lastDigit = percentage % 10;

            // Add the sign based on the last digit
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        // Special case handling: No "A+" or "F+" grade
        if (letter == "A" && sign == "+")
        {
            sign = "-";  // "A+" is not allowed, so we make it "A-"
        }
        else if (letter == "F" && sign != "")
        {
            sign = "";  // No "F+" or "F-" grade, so we clear the sign
        }

        // Print the final grade with or without a sign
        Console.WriteLine($"Your final grade is: {letter}{sign}");
    }
}
