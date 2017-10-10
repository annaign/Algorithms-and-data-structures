using System;
using System.IO;
using System.Text;

namespace Algorithms
{
    class Program
    {
        static Int64[] tempArray;
        static StringBuilder tempAnswer;
        static Int64 counter;
        static void Main(string[] args)
        {
            int n;
            Int64[] array;

            string[] data = File.ReadAllLines("input.txt");

            n = int.Parse(data[0]);
            array = new Int64[n];
            string[] tempAr = data[1].Split(' ');

            for (int i = 0; i < n; i++)
                array[i] = Int64.Parse(tempAr[i]);

            if (File.Exists("output.txt"))
                File.Delete("output.txt");
            var file = File.CreateText("output.txt");

            tempAnswer = new StringBuilder();
            counter = 0;

            MergeSort(array);

            tempAnswer.Append(counter);

            //tempAnswer.Append("\n");
            //for (int i = 0; i < n - 1; i++)
            //    tempAnswer.Append(array[i] + " ");
            //tempAnswer.Append(array[n - 1]);

            file.Write(tempAnswer);
            file.Close();
        }

        static void MergeSort(Int64[] array, int start, int end)
        {
            if (start == end) return;

            int middle = (start + end) / 2;

            //if(!(end - start + 1 > 2))
            //    middle = (end + start) / 2;
            //else if (((end - start + 1) / 2) % 2 == 0)
            //    middle = start + ((end - start + 1) / 2 - 1);
            //else
            //    middle = start + ((end - start + 1) / 2);

            MergeSort(array, start, middle);
            MergeSort(array, middle + 1, end);

            Merge(array, start, middle, end);
        }

        static void MergeSort(Int64[] array)
        {
            tempArray = new Int64[array.Length];
            MergeSort(array, 0, tempArray.Length - 1);
        }

        static void Merge(Int64[] array, int start, int middle, int end)
        {
            int left = start;
            int right = middle + 1;
            int length = end - start + 1;
            Int64 temp = counter;

            for (int i = 0; i < length; i++)
            {
                if (right > end || (left <= middle && array[left] <= array[right]))
                {
                    tempArray[i] = array[left];
                    left++;
                }
                else
                {
                    if (left <= middle && right <= end && array[left] > array[right])
                        counter = counter + (middle + 1) - left;

                    tempArray[i] = array[right];
                    right++;                    
                }
            }

            for (int i = 0; i < length; i++)
                array[i + start] = tempArray[i];

            //tempAnswer.Append((start + 1).ToString() + " " + (end + 1).ToString() + " "
            //    + array[start] + " " + array[end] + ": " + (counter - temp).ToString() + "\n");
        }
    }
}
