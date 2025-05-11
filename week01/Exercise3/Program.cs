using System;

class Program
{
    static void Main()
    {
        // Game loop to allow multiple rounds
        string playAgain = "yes";
        
        while (playAgain.ToLower() == "yes")
        {
            // Generate a random number between 1 and 100
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 101); // Random number between 1 and 100

            int guess = 0; // The user's guess
            int attempts = 0; // Track the number of attempts

            Console.WriteLine("Welcome to the Guess My Number game!");

            // Game loop where user keeps guessing until correct
            while (guess != magicNumber)
            {
                // Ask for the user's guess
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine()); // Read and convert to integer
                attempts++; // Increment the number of attempts

                // Give feedback based on the guess
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    Console.WriteLine($"It took you {attempts} attempts.");
                }
            }

            // Ask if the user wants to play again
            Console.Write("Do you want to play again? (yes/no): ");
            playAgain = Console.ReadLine();
        }

        Console.WriteLine("Thanks for playing!");
    }
}
