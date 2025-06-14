using System;
using System.IO;
using System.Collections.Generic;

// Base class for all goals
abstract class Goal
{
    protected string _name;
    protected int _points;

    public Goal(string name, int points)
    {
        _name = name;
        _points = points;
    }

    public abstract bool RecordEvent();
    public abstract string GetStatus();
    public abstract string Serialize();

    public string GetName() => _name;
}

// Simple goal: one-time completion
class SimpleGoal : Goal
{
    private bool _completed;

    public SimpleGoal(string name, int points)
        : base(name, points)
    {
        _completed = false;
    }

    public override bool RecordEvent()
    {
        if (!_completed)
        {
            _completed = true;
            Program.Score += _points;
            Program.CheckLevelUp();
            return true;
        }
        return false;
    }

    public override string GetStatus()
    {
        return $"[{(_completed ? "X" : " ")}] {_name}";
    }

    public override string Serialize()
    {
        return $"Simple|{_name}|{_points}|{_completed}";
    }
}

// Eternal goal: repeatable, never completes
class EternalGoal : Goal
{
    public EternalGoal(string name, int points)
        : base(name, points)
    {
    }

    public override bool RecordEvent()
    {
        Program.Score += _points;
        Program.CheckLevelUp();
        return true;
    }

    public override string GetStatus()
    {
        return $"[∞] {_name} ( +{_points} pts )";
    }

    public override string Serialize()
    {
        return $"Eternal|{_name}|{_points}";
    }
}

// Checklist goal: multiple steps plus bonus
class ChecklistGoal : Goal
{
    private int _required;
    private int _completed;
    private int _bonus;
    private bool _finished;

    public ChecklistGoal(string name, int points, int required, int bonus)
        : base(name, points)
    {
        _required = required;
        _bonus = bonus;
        _completed = 0;
        _finished = false;
    }

    public override bool RecordEvent()
    {
        if (_finished) return false;
        _completed++;
        Program.Score += _points;
        if (_completed >= _required)
        {
            _finished = true;
            Program.Score += _bonus;
        }
        Program.CheckLevelUp();
        return true;
    }

    public override string GetStatus()
    {
        return $"[{(_finished ? 'X' : ' ')}] {_name} -- Completed {_completed}/{_required}";
    }

    public override string Serialize()
    {
        return $"Checklist|{_name}|{_points}|{_required}|{_bonus}|{_completed}|{_finished}";
    }
}

class Program
{
    public static int Score = 0;
    static List<Goal> _goals = new List<Goal>();
    static int _level = 1;

    static void Main()
    {
        LoadGoals();

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Score: {Score} | Level: {_level}");
            Console.WriteLine("Menu:\n1. Add Goal\n2. List Goals\n3. Record Event\n4. Save & Exit");
            switch (Console.ReadLine())
            {
                case "1": AddGoal(); break;
                case "2": ListGoals(); Pause(); break;
                case "3": RecordEvent(); Pause(); break;
                case "4": SaveGoals(); return;
                default: Console.WriteLine("Invalid choice."); Pause(); break;
            }
        }
    }

    static void AddGoal()
    {
        Console.WriteLine("Choose goal type: 1. Simple 2. Eternal 3. Checklist");
        var type = Console.ReadLine();
        Console.Write("Name: "); var name = Console.ReadLine();
        Console.Write("Points: "); var pts = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, pts));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, pts));
                break;
            case "3":
                Console.Write("Required occurrences: ");
                var req = int.Parse(Console.ReadLine());
                Console.Write("Completion bonus: ");
                var bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, pts, req, bonus));
                break;
        }
    }

    static void ListGoals()
    {
        Console.WriteLine("Goals:");
        for (int i = 0; i < _goals.Count; i++)
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()}");
    }

    static void RecordEvent()
    {
        ListGoals();
        Console.Write("Select a goal #: ");
        var idx = int.Parse(Console.ReadLine()) - 1;
        if (idx >= 0 && idx < _goals.Count && _goals[idx].RecordEvent())
        {
            Console.WriteLine($"Event recorded! New score: {Score}");
        }
        else Console.WriteLine("Invalid selection or already completed.");
    }

    static void Pause()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void SaveGoals()
    {
        using var writer = new StreamWriter("goals.txt");
        writer.WriteLine(Score);
        foreach (var g in _goals)
            writer.WriteLine(g.Serialize());
    }

    static void LoadGoals()
    {
        if (!File.Exists("goals.txt")) return;
        var lines = File.ReadAllLines("goals.txt");
        Score = int.Parse(lines[0]);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split('|');
            Goal g = parts[0] switch
            {
                "Simple" => new SimpleGoal(parts[1], int.Parse(parts[2])),
                "Eternal" => new EternalGoal(parts[1], int.Parse(parts[2])),
                "Checklist" => new ChecklistGoal(parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4])),
                _ => null
            };
            if (g is ChecklistGoal cg)
            {
                // restore progress
                typeof(ChecklistGoal)
                    .GetField("_completed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .SetValue(cg, int.Parse(parts[5]));
                typeof(ChecklistGoal)
                    .GetField("_finished", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .SetValue(cg, bool.Parse(parts[6]));
            }
            _goals.Add(g);
        }
    }

    public static void CheckLevelUp()
    {
        int newLevel = Score / 1000 + 1;
        if (newLevel > _level)
        {
            _level = newLevel;
            Console.WriteLine($"🎉 Congratulations, you've reached Level {_level}!");
        }
    }
}
