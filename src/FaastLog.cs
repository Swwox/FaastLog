namespace FaastLog;

static class Log
{
    private static List<string> EnabledFlags = new() { "*" };
    private static DateTime TimeNow;


    private struct Message
    {
        public string Content;
        public int Type;
        public List<string> Flags = new() { "*" };
        public Message(string Content, int Type, List<string> Flags)
        {
            this.Content = Content;
            this.Type = Type;

            foreach (string Flag in Flags)
            {
                this.Flags.Add(Flag);
            }
        }
    }

    public static void SetEnabledFlags(List<string> Flags)
    {
        EnabledFlags = Flags;
    }

    public static void Success(string msg, List<string> Flags)
    {
        Flags.Add("Success");
        Message message = new(msg, 0, Flags);
        Logger(message);
    }
    public static void Success(string msg)
    {
        Message message = new(msg, 0, new List<string> { "Success" });
        Logger(message);
    }
    public static void Error(string msg, List<string> Flags)
    {
        Flags.Add("Error");
        Message message = new(msg, 1, Flags);
        Logger(message);
    }
    public static void Error(string msg)
    {
        Message message = new(msg, 1, new List<string> { "Error" });
        Logger(message);
    }
    public static void Warn(string msg, List<string> Flags)
    {
        Flags.Add("Warn");
        Message message = new(msg, 2, Flags);
        Logger(message);
    }
    public static void Warn(string msg)
    {
        Message message = new(msg, 2, new List<string> { "Warn" });
        Logger(message);
    }
    public static void Info(string msg, List<string> Flags)
    {
        Flags.Add("Info");
        Message message = new(msg, 3, Flags);
        Logger(message);
    }
    public static void Info(string msg)
    {
        Message message = new(msg, 3, new List<string> { "Info" });
        Logger(message);
    }

    private static void Logger(Message msg)
    {
        if (HasMsgRightFlag(msg.Flags))
        {
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
    }

    private static bool HasMsgRightFlag(List<string> Flags)
    {
        foreach (string Flag in Flags)
        {
            foreach (string EnabledFlag in EnabledFlags)
            {
                if (Flag == EnabledFlag)
                    return true;
            }
        }
        return false;
    }

    private static void PrintTime(int type)
    {
        UpdateTime();
        switch (type)
        {
            case 0:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case 1:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
        }
        Console.Write("[" + TimeNow.ToLongTimeString() + "] ");
    }
    private static void UpdateTime()
    {
        TimeNow = DateTime.Now;
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
