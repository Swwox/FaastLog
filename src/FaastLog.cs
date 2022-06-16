namespace FaastLog;

public static class Log
{
    private static List<string> _enabledFlags = new() { "*" };
    private static DateTime _timeNow;


    private struct Message
    {
        public readonly string Content;
        public readonly int Type;
        public readonly List<string> Flags = new() { "*" };
        public Message(string content, int type, List<string> flags)
        {
            this.Content = content;
            this.Type = type;

            foreach (var flag in flags)
            {
                this.Flags.Add(flag);
            }
        }
    }

    public static void SetEnabledFlags(List<string> flags)
    {
        _enabledFlags = flags;
    }

    public static void Success(string msg, List<string> flags)
    {
        flags.Add("Success");
        Message message = new(msg, 0, flags);
        Logger(message);
    }
    public static void Success(string msg)
    {
        Message message = new(msg, 0, new List<string> { "Success" });
        Logger(message);
    }
    public static void Error(string msg, List<string> flags)
    {
        flags.Add("Error");
        Message message = new(msg, 1, flags);
        Logger(message);
    }
    public static void Error(string msg)
    {
        Message message = new(msg, 1, new List<string> { "Error" });
        Logger(message);
    }
    public static void Warn(string msg, List<string> flags)
    {
        flags.Add("Warn");
        Message message = new(msg, 2, flags);
        Logger(message);
    }
    public static void Warn(string msg)
    {
        Message message = new(msg, 2, new List<string> { "Warn" });
        Logger(message);
    }
    public static void Info(string msg, List<string> flags)
    {
        flags.Add("Info");
        Message message = new(msg, 3, flags);
        Logger(message);
    }
    public static void Info(string msg)
    {
        Message message = new(msg, 3, new List<string> { "Info" });
        Logger(message);
    }

    private static void Logger(Message msg)
    {
        if (!HasMsgRightFlag(msg.Flags)) return;
        
        PrintTime(msg.Type);

        switch (msg.Type)
        {

            case 0:
                SuccessCOutSettings();
                Console.Write("[SUCCESS]");
                ResetCOutSettings();
                PrintMsg(msg.Content);
                break;
            case 1:
                ErrorCOutSettings();
                Console.Write("[ERROR]");
                ResetCOutSettings();
                PrintMsg(msg.Content);
                break;
            case 2:
                WarnCOutSettings();
                Console.Write("[WARNING]");
                ResetCOutSettings();
                PrintMsg(msg.Content);
                break;
            case 3:
                InfoCOutSettings();
                Console.Write("[INFO]");
                ResetCOutSettings();
                PrintMsg(msg.Content);
                break;
        }
    }

    private static bool HasMsgRightFlag(List<string> flags)
    {
        return flags.Any(flag => _enabledFlags.Any(enabledFlag => flag == enabledFlag));
    }

    private static void PrintTime(int type)
    {
        UpdateTime();
        Console.ForegroundColor = type switch
        {
            0 => ConsoleColor.Green,
            1 => ConsoleColor.Red,
            2 => ConsoleColor.Yellow,
            3 => ConsoleColor.DarkGray,
            _ => Console.ForegroundColor
        };
        Console.Write("[" + _timeNow.ToLongTimeString() + "] ");
    }
    private static void UpdateTime()
    {
        _timeNow = DateTime.Now;
    }
    private static void PrintMsg(string msg)
    {
        Console.WriteLine(" " + msg);
    }

    private static void ResetCOutSettings()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
    }
    private static void SuccessCOutSettings()
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    private static void InfoCOutSettings()
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    private static void WarnCOutSettings()
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    private static void ErrorCOutSettings()
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
    }
}
