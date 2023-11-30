/* ФИО: Журавель Никита Алексеевич
   Группа: БПИ238
   Подгруппа: 2
   Вариант: 11
   Важно(!): Прочтите содержимое файл Readme.txt(Находится в одном каталоге с файлом решения .sln)
*/

namespace TestHomeWork2
{
     class Program
    {
        static void Main()
        {
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.CursorVisible = false;
                // Даем пользователю возможность выхода на данном этапе.
                Assistance.SlowColorWriteLine("Нажмите Enter для продолжения или Escape для выхода...\n", ConsoleColor.DarkCyan); 
                ConsoleKey exitKey = Console.ReadKey(true).Key;
                // Если пользователь нажимает Escape, то осуществляется немедленный выход из программы.
                if (exitKey == ConsoleKey.Escape)
                {
                    Assistance.SlowColorWriteLine("Exiting...", ConsoleColor.DarkCyan);
                    Thread.Sleep(300);
                    Environment.Exit(0);
                }
                // Если пользователь нажал Enter, то её работа продолжается.
                else if (exitKey == ConsoleKey.Enter)
                {
                    // Флаг для логгера.
                    bool loggingIsEnabled;
                    /* Спрашиваем пользователя, хочет ли он производить логгирование ошибок, 
                      используем метод класса Menu, который реализует основную функциональность программы */
                    Menu.LoggingStart(out loggingIsEnabled);

                    try
                    {
                        /* Даём выбор пользователю: спрашиваем, хочет ли он заполнить файл,
                        содержащий целочисленные значения(размеры двумерных массивов). */
                        Menu.CreateFileWithJaggedArrayDimensions();

                        NumbJagged[] numbJaggeds;
                        int[] data;

                        Menu.ReadDataFromFileAndCreateJaggedArrays(out numbJaggeds, out data);
                        Menu.MainMenu(in numbJaggeds, in data);  
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Assistance.SlowColorWriteLine($"\n{ex.Message}", ConsoleColor.Red);
                        // Производим логгирование ошибок(во всех блоках catch), если пользователь включил его. 
                        
                        if (loggingIsEnabled == true)
                        {
                            CustomLogger log = new();
                            Task.Run(async () => await log.LogAsync(loggingIsEnabled, ex)).Wait();
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Assistance.SlowColorWriteLine($"\n{ex.Message}", ConsoleColor.Red);
                        if (loggingIsEnabled == true)
                        {
                            CustomLogger log = new();
                            Task.Run(async () => await log.LogAsync(loggingIsEnabled, ex)).Wait();
                        }
                    }
                    catch (Exception ex)
                    {
                        Assistance.SlowColorWriteLine($"\n{ex.Message}", ConsoleColor.Red);

                        if (loggingIsEnabled == true)
                        {
                            CustomLogger log = new();
                            Task.Run(async () => await log.LogAsync(loggingIsEnabled, ex)).Wait();
                        }
                    }
                    // Спрашиваем, хочет ли пользователь очистить(удалить) log-файл.
                    Menu.LoggingStop(in loggingIsEnabled);
                    Assistance.SlowColorWriteLine("\n\nPress any key to restart program or \'Escape\' to close...", ConsoleColor.Green);
                }
                // Цикл повторения решения.
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
