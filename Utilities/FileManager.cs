namespace TestHomeWork2
{
    // Класс, содержащий методы, необходимые для работы с файлами.
    public class FileManager 
    {
        public static string filePath = AppDomain.CurrentDomain.BaseDirectory;

        public const string fileDelimiter = "------------------------------------------------------------------------------------------------------------";

        public static int[] FileOpen(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath) || filePath is null)
                throw new ArgumentNullException("File path is null or empty");

            if (!File.Exists(filePath))
                throw new ArgumentException("File does not exists");

            string[] dataFromFile = File.ReadAllLines(filePath);

            if (dataFromFile.Length == 0 || dataFromFile is null)
                throw new ArgumentNullException("File is empty.");

            List<int> completeData = new();

            for (int i = 0; i < dataFromFile.Length; i++)
            {
                if (!int.TryParse(dataFromFile[i], out _))
                    throw new ArgumentException("File contains incorrect data.");

                completeData.Add(int.Parse(dataFromFile[i]));
            }

            return completeData.ToArray();
        }
    }
}
