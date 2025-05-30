using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; }
    public string Text { get; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }

    public string GetDisplayText()
    {
        return $"{CommenterName}: {Text}";
    }
}

class Video
{
    public string Title { get; }
    public string Author { get; }
    public int LengthInSeconds { get; }

    private List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public List<Comment> GetComments()
    {
        return _comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Intro to C#", "TechAcademy", 300);
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "Looking forward to more."));

        Video video2 = new Video("Abstraction in OOP", "CodeWorld", 420);
        video2.AddComment(new Comment("David", "Now I understand abstraction."));
        video2.AddComment(new Comment("Eva", "Clear and concise!"));
        video2.AddComment(new Comment("Frank", "Best video on the topic."));

        Video video3 = new Video("Understanding Encapsulation", "DevSimplified", 380);
        video3.AddComment(new Comment("Grace", "So easy to follow."));
        video3.AddComment(new Comment("Henry", "That example was perfect."));
        video3.AddComment(new Comment("Ivy", "Can you do one on inheritance?"));

        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        foreach (Video v in videos)
        {
            Console.WriteLine($"Title: {v.Title}");
            Console.WriteLine($"Author: {v.Author}");
            Console.WriteLine($"Length: {v.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {v.GetNumberOfComments()}");

            foreach (Comment c in v.GetComments())
            {
                Console.WriteLine($"  - {c.GetDisplayText()}");
            }

            Console.WriteLine();
        }
    }
}
