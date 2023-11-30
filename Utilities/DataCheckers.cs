using System.Globalization;
using System.Text;

namespace TestHomeWork2
{
    // Класс, содержащий различные методы для проверки данных.
    public class Checkers
    {
        public static int IntegerCheck(string outputText, ConsoleColor color)
        {
            Assistance.SlowColorWriteLine(outputText, color);

            bool isCorrect;
            int data;
            CultureInfo culture = CultureInfo.InvariantCulture;

            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), NumberStyles.Any, culture, out data);

                if (!isCorrect)
                {
                    Assistance.SlowColorWriteLine("\nIncorrect data, please try again:\n-> ", ConsoleColor.DarkCyan);
                }
            } while (!isCorrect);

            return data;
        }
        // Метод, для проверки корректности абсолютного пути к файлу.
        public static bool IsValidAbsolutePath(string? filePath, bool needExisting)
        {
            if (string.IsNullOrEmpty(filePath)) return false;
            if (!Path.IsPathRooted(filePath)) return false;
            
            if (needExisting == true)
            {
                if (!File.Exists(filePath)) return false;
            }

            var fileName = Path.GetFileName(filePath);
            var directoryPath = Path.GetDirectoryName(filePath);

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(directoryPath)) return false;

            byte[] pathBytes = Encoding.Default.GetBytes(filePath);
            // Проверка наличия кириллических символов в пути
            foreach (byte b in pathBytes)
            {
                // Диапазон кодов для кириллических символов
                if (b >= 0xC0 && b <= 0xFF) return true;
            }

            foreach (var symbol in Path.GetInvalidFileNameChars())
            {
                if (fileName.Contains(symbol)) return false;
            }

            foreach (var symbol in Path.GetInvalidPathChars())
            {
                if (directoryPath.Contains(symbol)) return false;
            }

            return true;
        }
        // Метод для проверки расширения файла.
        public static bool FileExtensionCheck(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("The file name is null or empty.");

            if (!fileName.EndsWith(".txt")) return false;

            return true;
        }
        // Метод для проверки корректности введённых строковых данных.
        public static string StringIsNullOrEmptyCheck(string outputText, ConsoleColor color) // Проверка для строк
        {
            Assistance.SlowColorWriteLine(outputText, color);
            bool IsNullOrEmpty;
            string output;

            do
            {
                output = Console.ReadLine()!;
                IsNullOrEmpty = string.IsNullOrEmpty(output);
                if (IsNullOrEmpty)
                {
                    Assistance.SlowColorWriteLine("Error: The file path is not set! Please try again...", ConsoleColor.DarkRed);
                }
            } while (IsNullOrEmpty);
            return output;
        }
        // Метод проверки введённых сторон на неравенство треугольника.
        public static bool TriangleInequalityCheck(double firstSide, double secondSide, double thirdSide)
        {
            if (firstSide <= 0 ||
                secondSide <= 0 ||
                thirdSide <= 0)
            {
                // Если хотя бы одна сторона меньше или равна нулю, то треугольник нельзя построить.
                return false;
            }

            if (firstSide + secondSide <= thirdSide ||
                firstSide + thirdSide <= secondSide ||
                secondSide + thirdSide <= firstSide)
            {
                // Если сумма двух сторон меньше или равна третьей стороне, то треугольник неравенство не выполняется.
                return false;
            }

            return true;
        }
    }
}
