namespace TestHomeWork2
{
    // Тот самый логгер ошибок, речь о котором шла в Program.cs
    public class CustomLogger
    {
        static string? fullLoggingFilePath;
        public static void SetFilePath(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("File path is null or empty.");

            fullLoggingFilePath = filePath;
        }
        public async Task LogAsync(bool loggingIsEnabled, Exception? exception)
        {
            if (exception is null || !loggingIsEnabled)
                return;

            var logMessage = $"{DateTime.Now}: {exception.Message}; {exception.Source}; {exception.StackTrace}";

            using StreamWriter sw = new StreamWriter(fullLoggingFilePath!, true);
            await sw.WriteLineAsync(logMessage + Environment.NewLine);
        }

        public static void LogFileClear() => File.Delete(fullLoggingFilePath!);
    }
}
