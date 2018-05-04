using System;
using TowerFall;

namespace Spire.Logger
{
    public static class Logger
    {
        public static void LogException(Exception e)
        {
            LogException(e, false);
        }

        public static void LogExceptionOnLoad(Exception e)
        {
            LogException(e, true);
        }

        public static void LogMessageOnLoad(string message)
        {
            message = CreateMessageString(message, false);
            TFGame.WriteLineToLoadLog(message);
            Console.WriteLine(message);
        }

        public static void LogMessage(string message)
        {
            message = CreateMessageString(message, false);
            Console.WriteLine(message);
        }

        private static string CreateMessageString(string str, bool isException)
        {
            return isException ? $"[Spire]: [ERROR]: {str}" : $"[Spire]: [INFO]: {str}";
        }

        private static void LogException(Exception exception, bool isOnLoad)
        {
            TFGame.WriteLineToLoadLog(CreateMessageString(exception.Message, true));
            TFGame.Log(exception, isOnLoad);
            Console.WriteLine($"{CreateMessageString(exception.Message, true)}");
        }
    }
}