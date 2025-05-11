using System;

class Program
{
    // Display a welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // Prompt the user for their name and return it
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // Prompt the user for their favorite number and return it
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }

    // Accept an integer, square it, and return the result
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Accept the user's name and the squared number, then display the result
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
    }

    static void Main()
    {
        // Call the DisplayWelcome function
        DisplayWelcome();

        // Call the PromptUserName and PromptUserNumber functions to get the data
        string userName = PromptUserName();
        int userNumber = PromptUserNumber();

        // Call the SquareNumber function to get the squared number
        int squaredNumber = SquareNumber(userNumber);

        // Call DisplayResult to display the final message
        DisplayResult(userName, squaredNumber);
    }
}
