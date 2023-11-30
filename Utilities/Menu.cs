
namespace TestHomeWork2
{
    public class Menu
    {
        public static void LoggingStart(out bool loggingIsEnabled)
        {
            Thread.Sleep(150);
            Console.Clear();
            Console.WriteLine("[*]Enable error logging?(Y|N)");

            ConsoleKeyInfo enableLoggingKey = Console.ReadKey(true);
            loggingIsEnabled = false;

            if (enableLoggingKey.Key == ConsoleKey.Y)
            {
                CustomLogger.SetFilePath(Path.Combine("../../../../", "Logs.txt"));
                loggingIsEnabled = true;
                Assistance.SlowColorWriteLine("[Logging is enabled]", ConsoleColor.Green);
            }

            else Assistance.SlowColorWriteLine("[Logging is not enabled]", ConsoleColor.Red);

            Thread.Sleep(500);
            Console.Clear();
        }

        public static void LoggingStop(in bool loggingIsEnabled)
        {
            if (loggingIsEnabled)
            {
                Assistance.SlowColorWriteLine("\n\nDo you want to clear the log file?(Y|N)", ConsoleColor.Cyan);

                ConsoleKeyInfo clearKey = Console.ReadKey(true);

                if (clearKey.Key == ConsoleKey.Y)
                {
                    CustomLogger.LogFileClear();
                    Assistance.SlowColorWriteLine("\n[The log file has been successfully deleted]", ConsoleColor.Green);
                }
            }
        }

        public static void ProcessUserInput()
        {
            Assistance.SlowColorWriteLine("Нажмите Enter для продолжения или Escape для выхода...\n", ConsoleColor.DarkCyan);
            ConsoleKey exitKey = Console.ReadKey(true).Key;

            if (exitKey == ConsoleKey.Escape)
            {
                Assistance.SlowColorWriteLine("Exiting...", ConsoleColor.DarkCyan);
                Thread.Sleep(300);
                Environment.Exit(0);
            }
        }

        public static void CreateFileWithJaggedArrayDimensions()
        {
            Assistance.SlowColorWriteLine("Want to create a file containing the dimensions of jagged arrays?(Y|N)", ConsoleColor.Cyan);
            ConsoleKeyInfo choiceKey2 = Console.ReadKey(true);

            if (choiceKey2.Key == ConsoleKey.Y)
            {
                string newFilePath = Checkers.StringIsNullOrEmptyCheck("\nEnter absolute file path(file extension must be .txt): \n-> ", ConsoleColor.DarkCyan);

                if (!Checkers.IsValidAbsolutePath(newFilePath, false) ||
                    !newFilePath.EndsWith(".txt"))
                    throw new ArgumentException("File path is not correct");

                int[] arrayWithFileData = Assistance.FillIntegerArray();
                string[] stringArray = arrayWithFileData.Select(x => x.ToString()).ToArray();

                File.WriteAllLines(newFilePath, stringArray);
                Thread.Sleep(150);
                Assistance.SlowColorWriteLine("\nPath to copy: \n" +
                    $"{newFilePath}\n", ConsoleColor.Green);
                Assistance.SlowColorWriteLine("[Complete]\n\n", ConsoleColor.Green);
            }

            else if (choiceKey2.Key == ConsoleKey.N) Console.Clear();
        }

        public static void ReadDataFromFileAndCreateJaggedArrays(out NumbJagged[] numbJaggeds, out int[] data)
        {
            string filePath = Checkers.StringIsNullOrEmptyCheck("\nEnter the absolute path to the file(.txt):\n-> ", ConsoleColor.Cyan);

            if (!Checkers.FileExtensionCheck(Path.GetFileName(filePath)))
                throw new ArgumentException("File extension is incorrect.\n");

            if (!Checkers.IsValidAbsolutePath(filePath, true))
                throw new ArgumentException("File path is not correct or file does not exists.\n");

            Console.Clear();

            Assistance.SlowColorWriteLine("Reading data from a file......\n", ConsoleColor.Green);
            Thread.Sleep(150);
            data = FileManager.FileOpen(filePath);

            Assistance.SlowColorWriteLine("[Complete]\n", ConsoleColor.Green);
            Thread.Sleep(500);
            Console.Clear();

            List<NumbJagged> jaggedList = new();

            for (int i = 0; i < data.Length; i++)
            {
                NumbJagged jaggedArray = new(data[i]);
                jaggedList.Add(jaggedArray);
            }

            numbJaggeds = jaggedList.ToArray();
        }

        public static void MainMenu(in NumbJagged[] numbJaggeds, in int[] data)
        {
            Assistance.SlowColorWriteLine("Choose the number of the menu item to start the action: \n", ConsoleColor.Cyan);
            Console.WriteLine("1. Output the resulting data\r\n" +
                              "2. Write result data to file\r\n" +
                              "3. Write a file containing the sizes of jagged arrays\r\n" +
                              "4. Exit the program \n");

            ConsoleKey choiceKey = Console.ReadKey(true).Key;

            switch (choiceKey)
            {
                case ConsoleKey.D1:
                    Assistance.SlowColorWriteLine("Results:\n\n", ConsoleColor.Cyan);

                    for (int i = 0; i < numbJaggeds.Length; i++)
                    {
                        Console.WriteLine($"[*]Array {i + 1}: ");
                        Console.WriteLine(FileManager.fileDelimiter);
                        numbJaggeds[i].CompeleString();
                        Console.WriteLine(FileManager.fileDelimiter + "\n");
                    }
                    break;

                case ConsoleKey.D2:
                    string fileName = Checkers.StringIsNullOrEmptyCheck("Enter the file name(.txt):\n-> ", ConsoleColor.Cyan);
                    string currentPath = Path.Combine(FileManager.filePath, fileName);

                    if (!Checkers.FileExtensionCheck(fileName))
                        throw new ArgumentException("File extension is incorrect.\n");

                    for (int i = 0; i < numbJaggeds.Length; i++)
                    {
                        string fileHeader = $"Number of rows in the array: {data[i]} \n" +
                            $"{FileManager.fileDelimiter}\n";
                        File.AppendAllText(currentPath, fileHeader);

                        numbJaggeds[i].WriteToFile(currentPath);

                        File.AppendAllText(currentPath, FileManager.fileDelimiter + Environment.NewLine);
                    }

                    Assistance.SlowColorWriteLine("\nData successfully written.....\n", ConsoleColor.Green);
                    Assistance.SlowColorWriteLine($"The file is on the path: " +
                        $"{Path.Combine(currentPath)}\n", ConsoleColor.Green);
                    Thread.Sleep(150);
                    break;

                case ConsoleKey.D3:
                    string newFilePath = Checkers.StringIsNullOrEmptyCheck("Enter absolute file path(file extension must be .txt): \n-> ", ConsoleColor.DarkCyan);

                    if (!Checkers.IsValidAbsolutePath(newFilePath, false) ||
                        !newFilePath.EndsWith(".txt"))
                        throw new ArgumentException("File path is not correct");

                    int[] arrayWithFileData = Assistance.FillIntegerArray();
                    string[] stringArray = arrayWithFileData.Select(x => x.ToString()).ToArray();

                    File.WriteAllLines(newFilePath, stringArray);
                    Thread.Sleep(150);
                    Assistance.SlowColorWriteLine("[Complete]", ConsoleColor.Green);
                    break;

                case ConsoleKey.D4:
                    Assistance.SlowColorWriteLine("Exiting program...........", ConsoleColor.DarkCyan);
                    Thread.Sleep(150);
                    Environment.Exit(0);
                    break;

                case ConsoleKey.P:
                    Assistance.DrawHeart();
                    break;

                default:
                    Assistance.SlowColorWriteLine("You haven't chosen anything.\n", ConsoleColor.Red);
                    break;
            }
        }
    }
}
