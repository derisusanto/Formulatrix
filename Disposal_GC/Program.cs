using System;
using WeakReferences;

class Subscriber
{
    private readonly string _name;

    public Subscriber(string name)
    {
        _name = name;
    }

    public void OnEvent()
    {
        Console.WriteLine($"📣 {_name} received event");
    }
}

class Program
{
    static void Main()
    {
        var weakEvent = new WeakDelegate<Action>();

        CreateSubscriber(weakEvent);

        Console.WriteLine("\n🧹 Forcing GC...");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        Console.WriteLine("\n🚀 Invoking event:");
        weakEvent.Target?.Invoke();

        Console.WriteLine("\nDone.");
        Console.ReadKey();
    }

    static void CreateSubscriber(WeakDelegate<Action> weakEvent)
    {
        var sub = new Subscriber("A");
        weakEvent.Combine(sub.OnEvent);

        Console.WriteLine("👤 Subscriber created & subscribed");
    }
}
