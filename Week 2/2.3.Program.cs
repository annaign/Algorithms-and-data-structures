using System;
using System.IO;
using System.Text;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(File.ReadAllText("input.txt"));

            StringBuilder answer = new StringBuilder();
            int[] array = new int[n];

            if (File.Exists("output.txt"))
                File.Delete("output.txt");
            var file = File.CreateText("output.txt");

            int tempElement;

            if (n == 1)
            {
                array[0] = 1;
            }
            else if (n == 2)
            {
                array[0] = 1;
                array[1] = 2;
            }
            else if (n == 3)
            {
                array[0] = 1;
                array[1] = 3;
                array[2] = 2;
            }
            else
            {
                //заполнение первой части массива, включая опорный
                int pivot = (n - 1) / 2;

                array[0] = 1;
                tempElement = 4;
                for (int i = 1; i < pivot; i++)
                {
                    array[i] = tempElement;
                    tempElement += 2;
                }
                array[pivot] = n;

                //заполнение второй части массива
                bool input;
                if (n % 2 == 0) //четное число элеметов в массиве
                {
                    input = true; 
                    tempElement = n - 1;
                }
                else            //нечетное число элеметов в массиве
                {
                    input = false; 
                    tempElement = n - 2;
                }

                for (int k = n - 1; k > pivot; k--)
                {
                    //нашли пустое место в массиве
                    if (array[k] == 0)
                    {
                        //вставляем элементы через одно пустое место
                        if (input)
                        {
                            input = false;
                            array[k] = tempElement;
                            tempElement -= 2;
                            if (tempElement == 1)
                            {
                                tempElement = 2; //последний элемент
                                input = true;
                            }
                        }
                        else
                            input = true;
                    }

                    //прошли массив, проверка осталилсь ли пустые места?
                    if (k == pivot + 1 && tempElement >= 2)
                    {
                        k = n;
                    }
                }
            }

            for (int j = 0; j < n; j++)
                answer.Append(array[j] + " ");
            answer.Remove(answer.Length - 1, 1);

            file.Write(answer);
            file.Close();
        }
    }
}
