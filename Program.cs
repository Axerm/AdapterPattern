namespace AdapterPattern;

#region enviroment

public class TelegramNotifier
{
    public void Send(string tel)
        => Console.WriteLine($"telegram notifying {tel}");
}

public static class UserProfile
{
    public static string GetUserEmail()
        => "person@mail.ru";
    public static string GetUserTelNumberByEmail(string email)
        => "x(xxx)xxx-xx-xx";
}

#endregion

public static class Program
{
    private static void Main(string[] args)
    {
        PluggableAdapter.AbstractOperation.Runner.Run();
        PluggableAdapter.DelegatedEntity.Runner.Run();
    }
}