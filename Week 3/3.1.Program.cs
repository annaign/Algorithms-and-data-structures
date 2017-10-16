using System;
using System.IO;
using System.Diagnostics;

namespace Algorithms
{
    class Program
    {
        static int[] tempArray;
        static void Main(string[] args)
        {
            int minK = 0;
            int maxK = 40000;

            //Данные для тестирования
            //int n = 6000;
            //int m = 6000;
            //int[] tempArray1 = new int[n];
            //int[] tempArray2 = new int[m];

            //CreateRandomArray(tempArray1, minK, maxK);
            //CreateRandomArray(tempArray2, minK, maxK);

            string[] data = File.ReadAllLines("input.txt");
            string[] tempLine = data[0].Split(' ');

            //Два исходных массива
            int[] tempArray1 = new int[int.Parse(tempLine[0])];
            int[] tempArray2 = new int[int.Parse(tempLine[1])];

            tempLine = data[1].Split(' ');
            for (int i = 0; i < tempArray1.Length; i++)
                tempArray1[i] = int.Parse(tempLine[i]);

            tempLine = data[2].Split(' ');
            for (int i = 0; i < tempArray2.Length; i++)
                tempArray2[i] = int.Parse(tempLine[i]);

            //tempArray2 должен быть большим по длине для уменьшения времени сортировки нового массива
            if (tempArray1.Length > tempArray2.Length)
            {
                int[] tempArr = tempArray1;
                tempArray1 = tempArray2;
                tempArray2 = tempArr;
            }

            //Сортировка исходных массивов
            int[] array;
            array = CountingSort(tempArray1, minK, maxK);
            tempArray1 = array;

            array = CountingSort(tempArray2, minK, maxK);
            tempArray2 = array;

            //Новый массив, сформированный из двух исходных
            array = new int[tempArray1.Length * tempArray2.Length];
            tempArray = new int[array.Length];

            //Формирование нового массива
            int minInd = 0;
            int maxInd = tempArray2.Length - 1;
            int counter = tempArray2.Length;

            //Stopwatch timer = new Stopwatch();
            //timer.Start();

            for (int j = 0; j < tempArray2.Length; j++)
                array[j] = tempArray1[0] * tempArray2[j];

            for (int i = 1; i < tempArray1.Length; i++)
            {
                for (int j = 0; j < tempArray2.Length; j++) //больший по длинне массив
                {
                    array[counter] = tempArray1[i] * tempArray2[j];
                    counter++;
                }
                maxInd = counter - 1;
            }

            //timer.Stop();
            //Console.WriteLine("Creating C-array: {0} milliseconds", timer.ElapsedMilliseconds);

            //timer.Start();

            //Сортировка нового массива
            int[] arrayInd = new int[tempArray1.Length + 1];
            for (int i = 1; i < arrayInd.Length; i++)
                arrayInd[i] = i * tempArray2.Length;

            minInd = 0;
            maxInd = 0;
            int midInd;

            int k = 0;
            counter = 0;
            int lastMin = -1;     //начало последнего подмассива 

            while (true)
            {
                minInd = arrayInd[k];

                if (k + counter + 1 + counter + 1 < arrayInd.Length)
                {
                    //запомнить начало последнего подмассива  
                    if (k + counter + 1 + counter + 1 == arrayInd.Length - 1) lastMin = k; 

                    midInd = arrayInd[k + counter + 1];
                    maxInd = arrayInd[k + counter + 1 + counter + 1];
                }
                else
                {
                    maxInd = arrayInd[arrayInd.Length - 1];
                    if (lastMin == -1) lastMin = k;
                    midInd = arrayInd[lastMin];
                    lastMin = k;
                }

                if (minInd != midInd) Merge(array, minInd, midInd - 1, maxInd - 1);
                    
                k = k + counter + 1 + counter + 1;

                if (maxInd == array.Length)
                {
                    k = 0;
                    counter = counter + 1 + counter;
                }

                if (counter > arrayInd.Length - 2) break;
            }

            //timer.Stop();
            //Console.WriteLine("Sorting C-array: {0} milliseconds", timer.ElapsedMilliseconds);

            if (File.Exists("output.txt"))
                File.Delete("output.txt");
            var file = File.CreateText("output.txt");

            Int64 sum = 0;
            for (int j = 0; j < array.Length; j += 10)
                sum += array[j];

            file.Write(sum);
            file.Close();
        }

        static int[] CountingSort(int[] array, int minNumber, int maxNumber)
        {
            int[] sortedArray = new int[array.Length];
            int[] numbers = new int[maxNumber - minNumber + 1];
            int i;

            for (i = 0; i < array.Length; i++)
                numbers[array[i] - minNumber] = numbers[array[i] - minNumber] + 1;

            for (i = 1; i <= maxNumber - minNumber; i++)
                numbers[i] = numbers[i] + numbers[i - 1];

            for (i = array.Length - 1; i >= 0; i--)
            {
                sortedArray[numbers[array[i] - minNumber] - 1] = array[i];
                numbers[array[i] - minNumber] = numbers[array[i] - minNumber] - 1;
            }

            return sortedArray;
        }

        static void Merge(int[] array, int start, int middle, int end)
        {
            int left = start;
            int right = middle + 1;
            int length = end - start + 1;

            for (int i = 0; i < length; i++)
            {
                if (right > end || (left <= middle && array[left] <= array[right]))
                {
                    tempArray[i] = array[left];
                    left++;
                }
                else
                {
                    tempArray[i] = array[right];
                    right++;
                }
            }

            for (int i = 0; i < length; i++)
                array[start + i] = tempArray[i];
        }

        static void CreateRandomArray(int[] array, int min, int max)
        {
            var randomNumbers = new Random();

            for (int i = 0; i < array.Length; i++)
                array[i] = randomNumbers.Next(min, max);
        }

    }
}
