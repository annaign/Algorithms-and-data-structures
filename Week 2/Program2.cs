using System;
using System.Text;

namespace SortingAlgorithms
{
    class Program
    {
        static int countCompare = 0;

        static void Main(string[] args)
        {
            int n = 9;
            int[] array = new int[n];
            int[] tempArray = new int[n];

            StringBuilder answer = new StringBuilder();
            StringBuilder tempLine = new StringBuilder();
            StringBuilder sorterdLine = new StringBuilder();

            tempLine.Append("1 ");

            for (int arraylength = 1; arraylength <= n; arraylength++)
            {
                int maxCountCompare = 0;
                sorterdLine.Clear();

                for (int ind = 0; ind < arraylength; ind++)
                {
                    tempArray[ind] = ind + 1;
                    sorterdLine.Append((ind + 1).ToString() + " ");
                }

                while (true)
                {
                    int i = NarayanaNextPerm(tempArray, arraylength);
                    if (i == 0) break;

                    for (int a = 0; a < arraylength; a++)
                        array[a] = tempArray[a];

                    countCompare = 0;
                    Quicksort(array, 0, arraylength - 1);

                    if (countCompare > maxCountCompare)
                    {
                        maxCountCompare = countCompare;
                        tempLine.Clear();

                        for (int j = 0; j < arraylength; j++)
                            tempLine.Append(tempArray[j] + " ");
                    }
                }

                answer.Append("\n[" + arraylength + "], ");

                int middleCurretArray = (arraylength - 1) / 2;
                int middle = (n - 1) / 2;
                for (int blank = 0; blank < (middle - middleCurretArray); blank++)
                    answer.Append("  ");
                answer.Append(tempLine);

                middle = (n - 1) / 2 + 1;
                if (arraylength % 2 == 0) middle = (n - 1) / 2 ;

                for (int blank = 0; blank < (middle - middleCurretArray); blank++)
                    answer.Append("  ");

                answer.Append("сравнений: ");
                if (maxCountCompare < 10) answer.Append(" ");
                answer.Append(maxCountCompare + ".     [" + arraylength + "], ");

                middleCurretArray = (arraylength - 1) / 2;
                middle = (n - 1) / 2;
                for (int blank = 0; blank < (middle - middleCurretArray); blank++)
                    answer.Append("  ");
                answer.Append(sorterdLine);
            }

            Console.WriteLine("\nQuicksort\n{0}", answer);

            Console.WriteLine("\nPress any key to continue...");
            Console.Read();
        }

        static int NarayanaNextPerm(int[] array, int arraylength)
        {
            if (arraylength < 2) return 0;

            int k = arraylength - 2;
            while ((k >= 0) && (array[k] >= array[k + 1]))
                k--;
            if (k == -1) return 0;

            int t = arraylength - 1;
            while ((array[k] >= array[t]) && (t >= k + 1))
                t--;
            SwapElements(array, k, t);

            int i;
            for (i = k + 1; i <= (arraylength + k) / 2; i++)
            {
                t = arraylength + k - i;
                SwapElements(array, i, t);
            }
            return i;
        }

        static void Quicksort(int[] array, int left, int right)
        {
            int i = left;
            int j = right;
            int key = array[(left + right) / 2];

            while (!(i > j))
            {
                while (array[i] < key)
                {
                    i++;
                    countCompare++;
                }
                countCompare++;

                while (key < array[j])
                {
                    j--;
                    countCompare++;
                }
                countCompare++;

                if (i <= j)
                {
                    SwapElements(array, i, j);
                    i++;
                    j--;
                }
            }

            if (left < j) Quicksort(array, left, j);
            if (i < right) Quicksort(array, i, right);
        }

        static void SwapElements(int[] array, int indexElement1, int indexElement2)
        {
            if (indexElement1 == indexElement2) return;
            int temp = array[indexElement1];
            array[indexElement1] = array[indexElement2];
            array[indexElement2] = temp;
        }

        static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.Write("{0,3} ", array[i]);
            Console.WriteLine();
        }

    }
}
