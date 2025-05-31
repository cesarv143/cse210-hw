using System;
using System.Collections.Generic;

class Comment
{
    private string _commenterName;
    private string _commentText;

    public Comment(string commenterName, string commentText)
    {
        _commenterName = commenterName;
        _commentText = commentText;
    }

    public string GetCommentInfo()
    {
        return $"{_commenterName}: {_commentText}";
    }
}

class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, int lengthInSeconds)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = lengthInSeconds;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_lengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (Comment comment in _comments)
        {
            Console.WriteLine($"  {comment.GetCommentInfo()}");
        }
        Console.WriteLine(new string('-', 40));
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("How to Cook Pasta", "Chef Emma", 320);
        video1.AddComment(new Comment("Alice", "Looks delicious!"));
        video1.AddComment(new Comment("Bob", "I tried it and loved it."));
        video1.AddComment(new Comment("Charlie", "Great tutorial!"));
        videos.Add(video1);

        Video video2 = new Video("Top 5 Programming Tips", "CodeMaster", 450);
        video2.AddComment(new Comment("Dave", "Very useful tips."));
        video2.AddComment(new Comment("Eve", "Thanks for the advice!"));
        video2.AddComment(new Comment("Frank", "I learned something new today."));
        videos.Add(video2);

        Video video3 = new Video("Morning Yoga Routine", "YogaWithMia", 600);
        video3.AddComment(new Comment("Grace", "So relaxing."));
        video3.AddComment(new Comment("Henry", "Perfect start to my day."));
        video3.AddComment(new Comment("Ivy", "Thank you!"));
        videos.Add(video3);

        // Display all video info
        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
