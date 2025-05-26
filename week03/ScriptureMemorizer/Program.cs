using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Create scripture reference
        Reference reference = new Reference("Matthew", 16, 15, 19);

        // Full text of Matthew 16:15–19
        string text = "He saith unto them, But whom say ye that I am? " +
                      "And Simon Peter answered and said, Thou art the Christ, the Son of the living God. " +
                      "And Jesus answered and said unto him, Blessed art thou, Simon Barjona: for flesh and blood " +
                      "hath not revealed it unto thee, but my Father which is in heaven. " +
                      "And I say also unto thee, That thou art Peter, and upon this rock I will build my church; " +
                      "and the gates of hell shall not prevail against it. " +
                      "And I will give unto thee the keys of the kingdom of heaven: and whatsoever thou shalt bind on earth " +
                      "shall be bound in heaven: and whatsoever thou shalt loose on earth shall be loosed in heaven.";

        // Composition: Scripture contains Reference and list of Word
        Scripture scripture = new Scripture(reference, text);

        // Main loop
        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.Write("\nPress Enter to hide more words or type 'quit' to exit: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords();
        }

        // Final display showing scripture fully hidden
        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words are now hidden. Great job memorizing!");
    }
}

// Reference class: holds the book, chapter, and verses
class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int? _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public override string ToString()
    {
        return _endVerse.HasValue
            ? $"{_book} {_chapter}:{_startVerse}-{_endVerse}"
            : $"{_book} {_chapter}:{_startVerse}";
    }
}

// Word class: represents each word in the scripture
class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public bool IsHidden => _isHidden;

    public void Hide()
    {
        _isHidden = true;
    }

    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}

// Scripture class: contains the reference and all words
class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ')
                     .Select(w => new Word(w))
                     .ToList();
    }

    public string GetDisplayText()
    {
        return _reference.ToString() + "\n\n" + string.Join(" ", _words.Select(w => w.GetDisplayText()));
    }

    public void HideRandomWords(int count = 3)
    {
        var visibleWords = _words.Where(w => !w.IsHidden).ToList();
        int toHide = Math.Min(count, visibleWords.Count);

        for (int i = 0; i < toHide; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden()
    {
        return _words.All(w => w.IsHidden);
    }
}
