namespace AdapterPattern.PluggableAdapter.AbstractOperation;

public class Job
{
    public string NotifyingPersonEmail { get; }

    public Job(string notifyingPersonEmail)
        => NotifyingPersonEmail = notifyingPersonEmail;

    public void Work()
    {
        Thread.Sleep(1000);
        Notify(NotifyingPersonEmail);
    }

    protected virtual void Notify(string email)
        => Console.WriteLine($"default mail notifying {email}");
}

public class Adapter : Job
{
    private TelegramNotifier _telegramNotifier;

    public Adapter(string notifyingPersonEmail)
        : base (notifyingPersonEmail)
        => _telegramNotifier = new();

    protected override void Notify(string email)
    {
        string tel = UserProfile.GetUserTelNumberByEmail(email);
        _telegramNotifier.Send(tel);
    }
}

public static class Runner
{
    public static void Run()
    {
        Adapter adapter = new(UserProfile.GetUserEmail());
        adapter.Work();
    }
}