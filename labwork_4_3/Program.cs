﻿using System;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace labwork_4_3
{
    //Создать два потока.Первый ищет числа Фибоначчи(каждое последующее число равно сумме двух предыдущих чисел), второй простые числа.
    //Результат работы каждого потока сохраняются в отдельный файл.
    //После остановки потока –программа производит анализ файлов, выводит их на экран, а так же показывает количество найденных чисел Фибоначчии простых чисел.
    class Program
    {
        
        static void Main(string[] args)
        {
            FibonacciNumbers fibonacci = new FibonacciNumbers();
            Thread firstThread = new Thread(fibonacci.ProcessNumbers);
            firstThread.Start();

            SimpleNumbers simple = new SimpleNumbers();
            Thread secondThread = new Thread(simple.ProcessNumbers);
            secondThread.Start();
        }

    }
}
