using System.Text;

namespace TestHomeWork2
{
    public class NumbJagged // Класс из задания
    {
        int[][] jagArr;

        public NumbJagged(int N)
        {
            if (N <= 0) throw new ArgumentException("Null size of array.");

            jagArr = new int[N][];
            Random random = new();

            for (int i = 0; i < N; i++)
            {
                jagArr[i] = Assistance.GenerateRandomArray(random);
            }
        }
        // Метод, для представления элементов массива в заданной форме.
        public string AsString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < jagArr.Length; i++)
            {
                sb.Append('[');

                for (int j = 0; j < jagArr[i].Length; j++)
                {
                    sb.Append(jagArr[i][j]);
                    if (j < jagArr[i].Length - 1)
                    {
                        sb.Append(", ");
                    }
                }

                sb.Append(']');
                sb.AppendLine();
            }
            return sb.ToString();
        }
        // Перегрузка метод выше.
        public string AsString(in int[] inputArray)
        {
            if (inputArray.Length == 0 || inputArray is null)
                throw new ArgumentNullException("Input array in null.");

            StringBuilder sb = new();

            sb.Append('[');

            for (int i = 0; i < inputArray.Length; i++)
            {
                sb.Append(inputArray[i]);

                if (i < inputArray.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(']');

            return sb.ToString();
        }
        /* Метод подсчёта возможного количества треугольников,
           которые можно составить из строки зубчатого 
           массива(в качестве параметра передаётся номер строки в массиве).
        */
        public int TriangleNumber(int row) 
        {
            if (row < 0 || row >= jagArr.Length)
                throw new IndexOutOfRangeException("Invalid row index");

            int count = 0;
            int[] rowData = jagArr[row];

            for (int i = 0; i < rowData.Length - 2; i++)
            {
                for (int j = i + 1; j < rowData.Length - 1; j++)
                {
                    for (int k = j + 1; k < rowData.Length; k++)
                    {
                        if (Checkers.TriangleInequalityCheck(rowData[i], rowData[j], rowData[k]))
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public void CompeleString() // Метод, формирующий строку для вывода.
        {
            for (int i = 0; i < jagArr.Length; i++)
            {
                string compeleString = $"From the elements of the array {AsString(in jagArr[i])} " +
                    $"you can make {TriangleNumber(i)} triangles.";
                Console.WriteLine(compeleString);
            }
        }

        public void WriteToFile(string? filePath) // Метод, записывающий данные в файл в определенном формате.
        {
            if (string.IsNullOrEmpty(filePath) || filePath is null)
                throw new ArgumentNullException("File name is null or empty.");

            for (int i = 0; i < jagArr.Length; i++)
            {
                string compeleString = $"From the elements of the array {AsString(in jagArr[i])} " +
                    $"you can make {TriangleNumber(i)} triangles." + Environment.NewLine;
                File.AppendAllText(filePath, compeleString);
            }
        }
    }
}