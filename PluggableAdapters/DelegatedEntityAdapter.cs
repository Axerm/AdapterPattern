namespace AdapterPattern.PluggableAdapter.DelegatedEntity;

public delegate void NotifyDelegate(string email);
public static class NotifyDelegatesHolder
{
    public static void MailNotifyDelegate(string email)
        => Console.WriteLine($"default mail notifying {email}");
}

public interface IDelegatedNotifyer
{
    void DelegatedNotify(string email);
}

public class Job
{
    public string NotifyingPersonEmail { get; }
    private NotifyDelegate _notifyDelegate;

    public Job(string notifyingPersonEmail, NotifyDelegate? notifyDelegate = null)
        => (NotifyingPersonEmail, _notifyDelegate) = (notifyingPersonEmail, notifyDelegate ?? NotifyDelegatesHolder.MailNotifyDelegate);

    public void Work()
    {
        Thread.Sleep(1000);
        Notify(NotifyingPersonEmail);
    }

    protected void Notify(string email)
        => _notifyDelegate?.Invoke(email);
}

public class Adapter : IDelegatedNotifyer
{
    private TelegramNotifier _telegramNotifier;

    public Adapter()
        => _telegramNotifier = new();

    public virtual void DelegatedNotify(string email)
    {
        string tel = UserProfile.GetUserTelNumberByEmail(email);
        _telegramNotifier.Send(tel);
    }
}

public static class Runner
{
    public static void Run()
    {
        Adapter adapter = new();
        Job job = new(UserProfile.GetUserEmail(), adapter.DelegatedNotify);
        job.Work();
    }
}