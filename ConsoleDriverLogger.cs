namespace DriverWithLogger
{
    using System;
    using System.Collections;
    using System.Linq;
    using Neo4j.Driver.V1;

    public class ConsoleDriverLogger : IDriverLogger
    {
        public LogLevel Level { get; set; }

        public void Debug(string message, params object[] args)
        {
            Log(LogLevel.Debug, message, args);
        }

        public void Error(Exception cause, string message, params object[] args)
        {
            Log(LogLevel.Error, message, cause, args);
        }

        public void Info(string message, params object[] args)
        {
            Log(LogLevel.Info, message, args);
        }

        public bool IsDebugEnabled()
        {
            return Level >= LogLevel.Debug;
        }

        public bool IsTraceEnabled()
        {
            return Level >= LogLevel.Trace;
        }

        public void Trace(string message, params object[] args)
        {
            Log(LogLevel.Trace, message, args);
        }

        public void Warn(Exception cause, string message, params object[] args)
        {
            Log(LogLevel.Error, message, cause, args);
        }

        private static void Log(LogLevel logLevel, string message, params object[] restOfMessage)
        {
            if (message == null)
                return;

            //This is a quick hack to get this working....
            if (message.Contains("{1}"))
                Console.WriteLine($"{logLevel}>>: {message}");
            else
                Console.WriteLine(message, $"{logLevel}>>: {Format(restOfMessage)}");
        }

        private static string Format(params object[] message)
        {
            if (message == null || !message.Any()) return string.Empty;
            return string.Join(Environment.NewLine, message.Select(m => Format(m)));
        }

        private static string Format(object obj)
        {
            if (obj == null) return string.Empty;
            if (obj is IEnumerable enumerable)
                return string.Join(",", enumerable.Cast<object>().Select(o => o == null ? string.Empty : o.ToString()));

            return obj.ToString();
        }
    }
}