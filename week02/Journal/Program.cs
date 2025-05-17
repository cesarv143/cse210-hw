using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Journal Program - Main menu and logic
/// 
/// ABSTRACTION:
/// - Entry: Represents a journal entry (prompt, response, date).
/// - Journal: Manages a list of entries and file I/O.
/// - PromptGenerator: Provides random prompts for journaling.
///
/// EXCEEDS REQUIREMENTS:
/// - Displays user-friendly messages and validation.
/// - Handles missing file errors gracefully.
/// - Uses clear structure for extensibility (even in one file).
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n--- Journal Menu ---");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option (1-5): ");
            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    string prompt = promptGenerator.GetRandomPrompt();
                    Console.WriteLine($"Prompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    journal.AddEntry(prompt, response);
                    Console.WriteLine("
                    
                    
                    
                    
                    
                    
                    Entry recorded!");
                    break;

                case "2":
                    Console.WriteLine("--- Journal Entries ---");
                    journal.DisplayEntries();
                    break;

                case "3":
                    Console.Write("Enter filename to save to: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    Console.WriteLine("Journal saved successfully.");
                    break;

                case "4":
                    Console.Write("Enter filename to load from: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    Console.WriteLine("Journal loaded successfully.");
                    break;

                case "5":
                    exit = true;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please enter a number from 1 to 5.");
                    break;
            }
        }
    }
}

/// <summary>
/// Represents a single journal entry with date, prompt, and response.
/// </summary>
class Entry
{
    public string Date { get; }
    public string Prompt { get; }
    public string Response { get; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }

    public string ToFileString()
    {
        return $"{Date}|{Prompt}|{Response}";
    }

    public static Entry FromFileString(string line)
    {
        string[] parts = line.Split('|');
        if (parts.Length == 3)
        {
            return new Entry(parts[0], parts[1], parts[2]);
        }
        return null;
    }
}

/// <summary>
/// Manages a list of journal entries and handles saving/loading.
/// </summary>
class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(string prompt, string response)
    {
        string date = DateTime.Now.ToShortDateString();
        _entries.Add(new Entry(date, prompt, response));
    }

    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries found.");
        }
        else
        {
            foreach (Entry entry in _entries)
            {
                Console.WriteLine(entry);
            }
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.ToFileString());
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            _entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                Entry entry = Entry.FromFileString(line);
                if (entry != null)
                {
                    _entries.Add(entry);
                }
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

/// <summary>
/// Provides random prompts for journaling.
/// </summary>
class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private Random _random = new Random();

    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}
