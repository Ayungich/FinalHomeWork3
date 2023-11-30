namespace TestHomeWork2
{
    public class Assistance
    {
        public static void DrawHeart() // Рисуем сердечко
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  /\\  /\\");
            Console.WriteLine(" /  \\/  \\");
            Console.WriteLine("/        \\");
            Console.WriteLine("\\        /");
            Console.WriteLine(" \\      /");
            Console.WriteLine("  \\    /");
            Console.WriteLine("   \\  /");
            Console.WriteLine("    \\/");
            Console.ResetColor();
        }
        /* Метод для заполнения целочисленного массива данными, 
           которые буду размещены в файле для дальнейшей работы. 
        */
        public static int[] FillIntegerArray() 
        {
            List<int> data = new();
            int value;
            Console.WriteLine("\nTo finish entering, 0");

            do
            {
                value = Checkers.IntegerCheck("Enter an integer value: \n-> ", Console.ForegroundColor);
                data.Add(value);
            } while (value != 0);

            data.RemoveAt(data.Count - 1);
            return data.ToArray();
        }
        // Метод, заполняющий целочисленный массив случайными значениями из интервала [0;5]
        public static int[] GenerateRandomArray(Random random) 
        {
            List<int> data = new();
            int value;

            do
            {
                value = random.Next(6); // Случайные значения от 0 до 5
                data.Add(value);
            } while (value != 0);

            return data.ToArray();
        }
        
        public static void SlowColorWriteLine(string? inputText, ConsoleColor color)
        {
            if (string.IsNullOrEmpty(inputText))
                throw new ArgumentNullException("Input text is null or empty.");

            var defaulColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            foreach (var c in inputText)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }

            Console.ForegroundColor = defaulColor;
        }
    }
}
